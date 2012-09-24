using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HTTPCaching.API.MessageHandlers {

    public class HttpCachingHandler : DelegatingHandler {

        private static ConcurrentDictionary<string, CachableEntity> _eTagCacheDictionary = 
                new ConcurrentDictionary<string, CachableEntity>();

        public ICollection<Func<string, string[]>> CacheInvalidationStore = 
            new Collection<Func<string, string[]>>();

        private readonly string[] _varyHeaders;

        public HttpCachingHandler(params string[] varyHeaders) {

            _varyHeaders = varyHeaders;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) {

            var resourceKey = GetResourceKey(request.RequestUri, _varyHeaders, request);
            CachableEntity cachableEntity = null;
            var cacheControlHeader = new CacheControlHeaderValue {
                Private = true,
                MustRevalidate = true,
                MaxAge = TimeSpan.FromSeconds(0)
            };

            if (request.Method == HttpMethod.Get) {

                var eTags = request.Headers.IfNoneMatch;
                var modifiedSince = request.Headers.IfModifiedSince;
                var anyEtagsFromTheClientExist = eTags.Any();
                var doWeHaveanyCachableEtityForTheRequest =
                    _eTagCacheDictionary.TryGetValue(resourceKey, out cachableEntity);

                if (anyEtagsFromTheClientExist) {

                    if (doWeHaveanyCachableEtityForTheRequest) {
                        if (eTags.Any(x => x.Tag == cachableEntity.EntityTag.Tag)) {

                            var tempResp = new  HttpResponseMessage(HttpStatusCode.NotModified);
                            tempResp.Headers.CacheControl = cacheControlHeader;
                            return tempResp;
                        }
                    }
                }
                else if (modifiedSince.HasValue) {

                    if (doWeHaveanyCachableEtityForTheRequest) {
                        if (cachableEntity.IsValid(modifiedSince.Value)) {

                            var tempResp = new HttpResponseMessage(HttpStatusCode.NotModified);
                            tempResp.Headers.CacheControl = cacheControlHeader;
                            return tempResp;
                        }
                    }
                }
            }

            HttpResponseMessage response;
            try {

                response = await base.SendAsync(request, cancellationToken);
            }
            catch (Exception ex) {
                
                response = request.CreateErrorResponse(
                    HttpStatusCode.InternalServerError, ex);
            }

            if (response.IsSuccessStatusCode) {

                if (request.Method == HttpMethod.Get &&
                    !_eTagCacheDictionary.TryGetValue(resourceKey, out cachableEntity)) { 

                    cachableEntity = new CachableEntity(resourceKey);
                    cachableEntity.EntityTag = GenerateETag();
                    cachableEntity.LastModfied = DateTimeOffset.Now;

                    _eTagCacheDictionary.AddOrUpdate(
                        resourceKey, cachableEntity, (k, e) => cachableEntity);
                }

                if (request.Method == HttpMethod.Put || 
                    request.Method == HttpMethod.Post || 
                    request.Method == HttpMethod.Delete) {

                    HashSet<string> invalidCaches = new HashSet<string>();
                    invalidCaches.Add(GetRequestUri(request.RequestUri));

                    CacheInvalidationStore.ForEach(
                        func => func(GetRequestUri(request.RequestUri))
                            .ForEach(uri => invalidCaches.Add(uri)));

                    invalidCaches.ForEach(invalidCacheUri => {

                        var cacheEntityKeys = _eTagCacheDictionary.Keys.Where(
                            x => x.StartsWith(
                                string.Format("{0}:", invalidCacheUri), 
                                StringComparison.InvariantCultureIgnoreCase));

                        cacheEntityKeys.ForEach(key => {
                            if (!string.IsNullOrEmpty(key)) {

                                CachableEntity outVal = null;
                                _eTagCacheDictionary.TryRemove(key, out outVal);
                            }
                        });
                    });
                }
                else {

                    response.Headers.CacheControl = cacheControlHeader;
                    response.Headers.ETag = cachableEntity.EntityTag;
                    response.Content.Headers.LastModified = cachableEntity.LastModfied;

                    _varyHeaders.ForEach(
                        varyHeader => response.Headers.Vary.Add(varyHeader));
                }
            }

            return response;
        }

        private string GetResourceKey(
            Uri uri, 
            string[] varyHeaders, 
            HttpRequestMessage request) {

            return GetResourceKey(GetRequestUri(uri), varyHeaders, request);
        }

        private string GetResourceKey(
            string trimedRequestUri, 
            string[] varyHeaders, 
            HttpRequestMessage request) {

            var requestedVaryHeaderValuePairs = request.Headers
                .Where(x => varyHeaders.Contains(x.Key))
                .Select(x => string.Format("{0}:{1}", x.Key, string.Join(";", x.Value)));

            return string.Format(
                "{0}:{1}",
                trimedRequestUri,
                string.Join("_", requestedVaryHeaderValuePairs)).ToLower(
                    CultureInfo.InvariantCulture);
        }

        private EntityTagHeaderValue GenerateETag() {

            var eTag = string.Format("\"{0}\"",
                Guid.NewGuid().ToString().Replace("-", ""));

            return new EntityTagHeaderValue(eTag);
        }

        private string GetRequestUri(Uri requestUri) {

            return string.Format(
                "{0}{1}",
                requestUri.LocalPath.TrimEnd('/'),
                requestUri.Query).ToLower(CultureInfo.InvariantCulture);
        }
    }
}