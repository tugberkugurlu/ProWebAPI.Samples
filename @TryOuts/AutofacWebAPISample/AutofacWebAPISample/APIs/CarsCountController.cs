using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutofacWebAPISample.Application;

namespace AutofacWebAPISample.APIs {

    public class CarsCountController : ApiController {

        private readonly ICarsCountService _carsCountService;

        public CarsCountController(ICarsCountService carsCountService) {

            _carsCountService = carsCountService;
        }

        public int Get() {

            return _carsCountService.GetCount();
        }
    }
}