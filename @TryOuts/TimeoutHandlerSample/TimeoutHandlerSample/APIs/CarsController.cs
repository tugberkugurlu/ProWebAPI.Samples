using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace TimeoutHandlerSample.APIs {

    public class CarsController : ApiController {

        public async Task<string[]> Get(CancellationToken token) {

            await Task.Delay(5000, token);

            return new[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }
    }
}