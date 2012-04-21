using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AsyncDatabaseCall.Models;

namespace AsyncDatabaseCall.APIs {

    public class CarsSyncController : ApiController {

        readonly GalleryContext galleryContext = new GalleryContext();

        public IEnumerable<Car> Get() {

            return galleryContext.GetCars();
        }
    }
}