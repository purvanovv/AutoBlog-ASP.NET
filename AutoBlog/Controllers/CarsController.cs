
using AutoBlog.Data;
using AutoBlog.Models;
using AutoBlog.Models.Cars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AutoBlog.Controllers
{
    public class CarsController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;
        public CarsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;

        }
        // GET/Cars/Add
        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        //POST/Cars/Add
        [Authorize]
        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
        {
            if(ModelState.IsValid)
            {
                var userId = userManager.GetUserId(this.User);
                Car addCar = new Car
                {
                    Brand = car.Brand,
                    Category = car.Category,
                    Engine = car.Engine,
                    HorsePower = car.HorsePower,
                    ImageUrl = car.ImageUrl,
                    Model = car.Model,
                    Type = car.Type,
                    Year = car.Year,
                    UserId = userId
                };
                this.db.Cars.Add(addCar);
                this.db.SaveChanges();
                return RedirectToAction("Details",new {addCar.Id });
            }
            return View(car);
        }

        //Get/Cars/All
        [HttpGet]
        public IActionResult All()
        {
            var cars = this.db
                .Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarListingViewModel
                {
                    Id=c.Id,
                    Brand=c.Brand,
                    Model=c.Model,
                    ImageUrl=c.ImageUrl

                })
                .ToList();
            return View(cars);
                
        }

        //GET/Cars/Details{id}
        [HttpGet]
        public IActionResult Details(int id)
        {
            var car = db
                .Cars
                .Where(c => c.Id == id)
                .Select(c => new CarDetailsViewModel
                {
                    Id=c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    Engine=c.Engine,
                    HorsePower=c.HorsePower,
                    Type=c.Type,
                    UserId=c.UserId,
                    ImageUrl = c.ImageUrl,
                    Username = c.User.UserName
                })
                .FirstOrDefault();
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        //GET/CARS/Delete{id}
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var userId = userManager.GetUserId(this.User);
            var car = this.db
                .Cars
                .Where(c => c.Id == id && c.UserId == userId)
                .Select(c => new CarDeleteViewModel
                {
                    Id = c.Id,
                    Brand=c.Brand,
                    Model=c.Model
                })
                .FirstOrDefault();
            if (car==null)
            {
                return NotFound();
            }
            return View(car);
                
        }

        //POST/Cars/ConfirmDelete{id}
        [Authorize]
        public IActionResult ConfirmDelete(int id)
        {
            var userId = userManager.GetUserId(this.User);
            var car = this.db
                .Cars
                .Where(c => c.Id == id && c.UserId == userId)
                .FirstOrDefault();
            if (car == null)
            {
                return NotFound();
            }
            this.db.Cars.Remove(car);
            this.db.SaveChanges();

            return RedirectToAction("All");
        }

        //GET/Cars/Edit{id}
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = userManager.GetUserId(this.User);
            var car = this.db
                .Cars
                .Where(c => c.Id == id && c.UserId == userId)
                .Select(c => new CarEditViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Category=c.Category,
                    Model = c.Model,
                    Year = c.Year,
                    Engine = c.Engine,
                    HorsePower = c.HorsePower,
                    Type = c.Type,
                    ImageUrl = c.ImageUrl,
                    Username = c.User.UserName
                })
                .FirstOrDefault();
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id,CarEditViewModel car)
        {
            var userId = userManager.GetUserId(this.User);
           
            if (ModelState.IsValid)
            {
                Car carToDb = new Car
                {
                    Id = car.Id,
                    Brand = car.Brand,
                    Category = car.Category,
                    Model = car.Model,
                    Year = car.Year,
                    Engine = car.Engine,
                    HorsePower = car.HorsePower,
                    Type = car.Type,
                    ImageUrl = car.ImageUrl,
                    UserId = userId
                };
                db.Update(carToDb);
                db.SaveChanges();
                return RedirectToAction("All");
            }
            return View(car);
            
        }
    }
}
