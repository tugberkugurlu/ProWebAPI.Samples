using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FirstActionSample.Models;

namespace FirstActionSample.APIs {

    public class CarsController : ApiController {

        private readonly CarsContext _carsCtx = new CarsContext();

        public IEnumerable<Car> Get() {

            var cars = _carsCtx.All;
            return cars;
        }

        public HttpResponseMessage PostCar(Car car) {

            _carsCtx.Add(car);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        public Car PutCar(int id, Car car) {

            if (!_carsCtx.TryUpdate(id, car)) {

                var response = new HttpResponseMessage(HttpStatusCode.NotFound) {
                    Content = new StringContent("Car not found")
                };

                throw new HttpResponseException(response);
            }

            return car;
        }

        public HttpResponseMessage DeleteCar(int id) {

            var car = _carsCtx.GetSingle(x => x.Id == id);

            if (car == null) {

                var response = new HttpResponseMessage(HttpStatusCode.NotFound) {
                    Content = new StringContent("Car not found")
                };

                throw new HttpResponseException(response);
            }

            _carsCtx.Delete(car);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        //Second GET method
        public Car GetCar(int id) {

            var car = _carsCtx.GetSingle(x => x.Id == id);

            if (car == null) {

                var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            return car;
        }
    }
}