using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoBlog.Models.Cars
{
    public class CarDetailsViewModel
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int HorsePower { get; set; }

        public string Engine { get; set; }

        public int Year { get; set; }

        public string Type { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

    }
}
