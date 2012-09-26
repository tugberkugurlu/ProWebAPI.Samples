using System.ComponentModel.DataAnnotations;

namespace CompareAttributeSample.Models {

    public class Car {

        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Compare("Model")]
        public string ModelAgain { get; set; }

        public int Year { get; set; }

        public float Price { get; set; }
    }
}