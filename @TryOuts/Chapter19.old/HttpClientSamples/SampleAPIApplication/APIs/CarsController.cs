using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using SampleAPIApplication.Models;

namespace SampleAPIApplication.APIs {

    public class CarsController : ApiController {

        CarsContext _carsContext = new CarsContext();

        public IEnumerable<Car> Get() {

            return _carsContext.All;
        }

        public Car Get(int id) {

            var car = _carsContext.GetSingle(x => x.Id == id);

            if (car == null) { 

                var response = new HttpResponseMessage(HttpStatusCode.NotFound) { 
                    Content = new StringContent("Contact not found")
                };
                
                throw new HttpResponseException(response);
            }

            return car;
        }

        public HttpResponseMessage Post(Car car) {

            _carsContext.Add(car);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        public Car Put(int id, Car car) {

            if (!_carsContext.TryUpdate(id, car)) {

                var response = new HttpResponseMessage(HttpStatusCode.NotFound) {
                    Content = new StringContent("Car not found")
                };

                throw new HttpResponseException(response);
            }

            return car;
        }

        public HttpResponseMessage Delete(int id) {

            var car = _carsContext.GetSingle(x => x.Id == id);

            if (car == null) {

                var response = new HttpResponseMessage(HttpStatusCode.NotFound) {
                    Content = new StringContent("Contact not found")
                };

                throw new HttpResponseException(response);
            }

            _carsContext.Delete(car);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}