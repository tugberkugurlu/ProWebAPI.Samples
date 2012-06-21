using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutofacWebAPISample.Application;

namespace AutofacWebAPISample.APIs {

    public class CarsController : ApiController {

        private readonly ICarsService _carsService;

        public CarsController(ICarsService carsService) {

            _carsService = carsService;
        }

        public string[] Get() {

            return _carsService.GetCars();
        }
    }
}