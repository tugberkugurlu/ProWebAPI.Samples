using System.Collections.Generic;
using System.Web.Http;
using ActionFilters.Models;
using ActionFilters.Filters;
using System.Net.Http;
using System.Net;

namespace ActionFilters.APIs {

    public class CarsController : ApiController {

        private readonly CarsContext _carsContext = new CarsContext();

        public IEnumerable<Car> GetCars() {

            return _carsContext.All;
        }

        public Car GetCar(int id) {

            return _carsContext.GetSingle(car => car.Id == id);
        }

        [ValidateModelState]
        public HttpResponseMessage PostCar(Car car) {

            _carsContext.Add(car);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}