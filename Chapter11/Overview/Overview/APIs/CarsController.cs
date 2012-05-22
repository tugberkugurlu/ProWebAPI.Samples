using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Overview.Models;

namespace Overview.APIs {

    public class CarsController : ApiController {

        private readonly CarsContext carsContext = new CarsContext();

        public IEnumerable<Car> GetCars() {

            return carsContext.All;
        }
    }
}