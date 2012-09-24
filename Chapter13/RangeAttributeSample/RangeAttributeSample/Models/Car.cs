using System;
using System.ComponentModel.DataAnnotations;

namespace RangeAttributeSample.Models {

    public class Car {

        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 20)]
        public string Make { get; set; }

        [Required]
        [StringLength(maximumLength: 20)]
        public string Model { get; set; }

        public int Year { get; set; }

        [Range(minimum: 0F, maximum: 500000F)]
        public float Price { get; set; }

        [Range(typeof(DateTime), "2010-01-01", "9999-12-31")]
        public DateTime PurchasedOn { get; set; }
    }
}