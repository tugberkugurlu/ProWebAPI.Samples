using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Security;
using System.Net;
using AuthorizeAttributeSample.Models;
using AuthorizeAttributeSample.Services;

namespace AuthorizeAttributeSample.APIs {

    public class AuthController : ApiController {

        private AuthorizationService authService = new AuthorizationService();

        [AllowAnonymous]
        public HttpResponseMessage Post(User user) {

            var response = new HttpResponseMessage();

            if (user != null && authService.Authorize(user.UserName, user.Password)) {

                //user has been authorized
                response.StatusCode = HttpStatusCode.OK;
                response.Headers.SetAuthCookie(user.UserName, true);

                return response;
            }

            //if we come this far, it means that user hasn't been authorized
            response.StatusCode = HttpStatusCode.Unauthorized;
            response.Content = new StringContent("The user hasn't been authorized.");

            return response;
        }
    }

    //HttpResponseHeaders extensions to allow us working with auth cookies easily
    public static class HttpResponseHeadersExtensions {

        public static void RemoveAuthCookie(this HttpResponseHeaders headers) {

            var authCookieName = FormsAuthentication.FormsCookieName;
            var authCookiePath = FormsAuthentication.FormsCookiePath;
            var authCookieExpires = DateTimeOffset.Now.AddYears(-1);

            headers.AddCookies(new List<CookieHeaderValue> { 

                new CookieHeaderValue(authCookieName, string.Empty) { 
                    Path = authCookiePath,
                    Expires = authCookieExpires,
                    HttpOnly = true
                }
            });
        }

        public static void SetAuthCookie(this HttpResponseHeaders headers, string userName, bool createPersistentCookie) {

            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");

            var authCookie = FormsAuthentication.GetAuthCookie(userName, createPersistentCookie);
            headers.AddCookie(authCookie);
        }

        public static void AddCookie(this HttpResponseHeaders headers, HttpCookie httpCookie) {

            headers.AddCookies(new List<CookieHeaderValue> { 

                new CookieHeaderValue(httpCookie.Name, httpCookie.Value) { 
                    Domain = httpCookie.Domain,
                    Expires = httpCookie.Expires,
                    HttpOnly = httpCookie.HttpOnly,
                    Path = httpCookie.Path,
                    Secure =httpCookie.Secure
                }
            });
        }
    }
}