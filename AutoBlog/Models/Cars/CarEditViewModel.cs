using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoBlog.Models.Cars
{
    public class CarEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required(ErrorMessage = "The value Year is invalid.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "The value HorsePower is invalid.")]
        public int HorsePower { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Engine { get; set; }

        [Display(Name = "Image URL")]
        [Required]
        public string ImageUrl { get; set; }

        public string Username { get; set; }

    }
}
