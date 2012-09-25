using System;
using GreaterThanAttributeSample.Validation;
using System.ComponentModel.DataAnnotations;

namespace GreaterThanAttributeSample.Models {

    public class Car {

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Make { get; set; }

        [Required]
        [StringLength(20)]
        public string Model { get; set; }

        public int Year { get; set; }

        [Range(0, 500000)]
        public float Price { get; set; }

        public DateTime SalesStartsAt { get; set; }

        [GreaterThan("SalesStartsAt")]
        public DateTime SalesEndsAt { get; set; }
    }
}