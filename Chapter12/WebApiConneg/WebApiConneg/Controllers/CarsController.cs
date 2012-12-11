using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using WebApiConneg.Entities;

namespace WebApiConneg.Controllers
{
    public class CarsController : ApiController
    {
        public List<Car> Get()
        {
            return new List<Car>(){
				 new Car() { Id = 17, Make = "VW", Model = "Golf", Year = 1999, Price = 1500f },
				 new Car() { Id = 24, Make = "Porsche", Model = "911", Year = 2011, Price = 100000f },
				 new Car() { Id = 30, Make = "Mercedes", Model = "A-Class", Year = 2007, Price = 10000f }
			 };
        }

        //public Car Post(Car car) {
        //    if (null != car) {
        //        car.Id = 1;
        //        return car;
        //    }
        //    throw new HttpResponseException(new HttpResponseMessage() {
        //        StatusCode = HttpStatusCode.BadRequest,
        //        ReasonPhrase = "Car data must contain at least one value."
        //    });
        //}

        public Car Post(FormDataCollection carFormData) {
            var carFormKeyValues = carFormData.ReadAsNameValueCollection();
            var car = new Car() {
                Make = carFormKeyValues["Make"],
                Model = carFormKeyValues["Model"],
                Price = Convert.ToSingle(carFormKeyValues["Price"]),
                Year = Convert.ToInt32(carFormKeyValues["Year"])
            };
            return car;
        }

        //public Car Post(string make)
        //{
        //    return new Car()
        //    {
        //        Id = 1,
        //        Make = "Porsche",
        //        Model = "911",
        //        Price = 100000f,
        //        Year = 2012
        //    };
        //}
    }
}