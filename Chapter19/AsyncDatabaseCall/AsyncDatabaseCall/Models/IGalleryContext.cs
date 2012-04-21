using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDatabaseCall.Models {

    public interface IGalleryContext {

        IEnumerable<Car> GetCars();
        Task<IEnumerable<Car>> GetCarsAsync();
    }
}
