using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using HttpRequests.Models;

namespace HttpRequests.APIs {

    public class AsyncCarsController : ApiController {

        //HTTP service base address
        const string _countryAPIBaseAddress = "http://localhost:11338/api/cars";

        public async Task<IEnumerable<Car>> Get() {

            using (HttpClient httpClient = new HttpClient()) {

                var response = await httpClient.GetAsync(_countryAPIBaseAddress);
                var content = await response.Content.ReadAsAsync<List<Car>>();

                return content.Where(x => x.Price > 30000.00F);
            }
        }
    }
}