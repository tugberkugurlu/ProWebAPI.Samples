using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace ActionValueBinderSample.APIs {

    public class Person {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class CarsController : ApiController {

        public string[] Get([ModelBinder]Person myval) {

            return new[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }
    }
}