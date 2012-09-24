using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DataAnnotationValidationAttributesSample.Models {

    public class Car {

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Make { get; set; }

        [Required]
        [StringLength(20)]
        public string Model { get; set; }

        public int Year { get; set; }

        public float Price { get; set; }
    }
}