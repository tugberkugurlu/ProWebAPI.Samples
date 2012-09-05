using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutofacWebAPISample.Application {

    public class CarsService : ICarsService {

        public string[] GetCars() {

            return new string[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }
    }
}