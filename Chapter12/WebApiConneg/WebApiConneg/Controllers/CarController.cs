using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiConneg.Entities;

namespace WebApiConneg.Controllers {
    public class CarController : ApiController {
        public Car Get(int id) {
            return new Car() {
                Id = id,
                Make = "Porsche",
                Model = "911",
                Price = 100000f,
                Year = 2012
            };
        }

        public Car Put([FromUri]int id, Car car) {
            return car;
        }
    }
}