using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutofacWebAPISample.Application {

    public class CarsCountService : AutofacWebAPISample.Application.ICarsCountService {

        private readonly ICarsService _carsService;

        public CarsCountService (ICarsService carsService) {

            _carsService = carsService;
	    }

        public int GetCount() {

            return _carsService.GetCars().Count();
        }
    }
}