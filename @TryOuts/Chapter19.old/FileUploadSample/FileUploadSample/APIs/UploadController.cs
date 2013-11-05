using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace FileUploadSample.APIs {

    public class UploadController : ApiController {

        public async Task Post() {

            //Check whether it is an HTML form file upload request
            if(!Request.Content.IsMimeMultipartContent()) {

                //return UnsupportedMediaType response back if not
                throw new HttpResponseException(
                    new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType)
                );
            }

            //Determine the upload path
            var uploadPath = HttpContext.Current.Server.MapPath("~/Files");

            MultipartFormDataStreamProvider multipartFormDataStreamProvider = 
                new MultipartFormDataStreamProvider(uploadPath);

            //Read the MIME multipart content using the stream provider we just created.
            IEnumerable<HttpContent> bodyparts =
                await Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);
        }
    }
}