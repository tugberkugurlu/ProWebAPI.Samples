using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HTTPCaching.API.MessageHandlers {

    public class CachableEntity {

        public CachableEntity(string requestUri) {
            
            RequestUri = requestUri;
        }

        public string RequestUri { get; private set; }
        public EntityTagHeaderValue EntityTag { get; set; }
        public DateTimeOffset LastModfied { get; set; }

        public bool IsExpired(DateTimeOffset modifiedSince) {

            return (LastModfied > modifiedSince);
        }
    }

    public class ETagHandler : DelegatingHandler {

        private static ConcurrentDictionary<string, CachableEntity> _eTagCacheDictionary =
            new ConcurrentDictionary<string, CachableEntity>();

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, 
            CancellationToken cancellationToken) {

            var requestUri = request.RequestUri.ToString();
            CachableEntity cachableEntity = null;
            
            if (request.Method == HttpMethod.Get) {

                //TODO: If the IfModifiedSince is supplied + LastModfied > IfModifiedSince,
                //      the client will not get a 304. Also, if we already have a valid ETag for this,
                //      we don't wanna generate a new ETag for that. Just serve the 

                //TODO: What happens when the client both supllies the IfNoneMatch and IfModifiedSince?
                //TODO: Do we need to take IfMatch and IfUnmodifiedSince?

                var eTags = request.Headers.IfNoneMatch;
                var modifiedSince = request.Headers.IfModifiedSince;
                var doesAnyEtagExist = eTags.Any();

                if (doesAnyEtagExist || modifiedSince != null) {

                    if (doesAnyEtagExist && 
                        _eTagCacheDictionary.TryGetValue(
                            requestUri, out cachableEntity)) {

                        if ((modifiedSince.HasValue &&
                            !cachableEntity.IsExpired(modifiedSince.Value)) ||
                            eTags.Any(x => x.Tag == cachableEntity.EntityTag.Tag)) {

                            return new HttpResponseMessage(HttpStatusCode.NotModified);
                        }
                    }
                }
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (!_eTagCacheDictionary.TryGetValue(requestUri, out cachableEntity) || request.Method == HttpMethod.Put || request.Method == HttpMethod.Post) {

                cachableEntity = new CachableEntity(requestUri);

                cachableEntity.EntityTag = new EntityTagHeaderValue(
                    string.Format("\"{0}\"", 
                        Guid.NewGuid().ToString().Replace("-", "")));

                cachableEntity.LastModfied = DateTimeOffset.Now;

                _eTagCacheDictionary.AddOrUpdate(
                    requestUri, cachableEntity, (k, e) => cachableEntity);
            }

            response.Headers.ETag = cachableEntity.EntityTag;
            response.Content.Headers.LastModified = cachableEntity.LastModfied;

            return response;
        }
    }
}