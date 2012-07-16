﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ActionBasedRoutingSample.APIs {

    public class CarsController : ApiController {

        [HttpGet]
        public string[] List() {

            return new string[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }
    }
}