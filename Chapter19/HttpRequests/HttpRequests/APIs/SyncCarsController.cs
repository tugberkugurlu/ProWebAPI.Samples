using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using HttpRequests.Models;

namespace HttpRequests.APIs {

    public class SyncCarsController : ApiController {

        const string _countryAPIBaseAddress = "http://localhost:11338/api/cars";

        public IEnumerable<Car> Get() {

            using (HttpClient httpClient = new HttpClient()) {

                var response = httpClient.GetAsync(_countryAPIBaseAddress).Result;
                var content = response.Content.ReadAsAsync<List<Car>>().Result;

                return content;
            }
        }
    }
}