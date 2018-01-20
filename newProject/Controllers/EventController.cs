using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using newProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace newProject.Controllers
{
    public class EventController : Controller
    {
         private TravelContext _context;
 
        public EventController(TravelContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("dashboard")]
        //display the dashboard showing all trips and the option to join
        public IActionResult Dashboard()
        {
            //check whether user is logged in, and if so get session variables (user id and name)
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) {
                return RedirectToAction("Index", "User");
            }
            ViewBag.userid = userId;
            ViewBag.username = HttpContext.Session.GetString("Username");
            
            // grab all trips to show on DB
            // ViewBag.trips = _context.Trips.Include(w => w.User).ToList();

            //create a list of all the trips IDs the current user is traveling ..
            List<Plan> plans = _context.Plans.Include(p => p.User).Include(p => p.Trip).ToList().Where(p => p.UserId == userId).ToList();
            ViewBag.trips = _context.Trips.Include(t => t.User).ToList().Where(t => 

            {
                if(t.UserId == userId)return false;
                foreach(var plan in plans){
                    if(plan.TripId == t.TripId) return false;
                }
                return true;
            });


            ViewBag.AllUserPlans = plans;
           
            return View();
        }
      

        [HttpGet]
        [Route("add")]
        //show the page to add a trip
        public IActionResult ShowAdd() {
            //check whether user is logged in == get session variables (user id and name)
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) {
                return RedirectToAction("Index", "User");
            }
            ViewBag.userid = userId;
            ViewBag.username = HttpContext.Session.GetString("Username");
            return View("Add");
        }

        [HttpPost]
        [Route("create")]
        //validate the submission and add the trip to the db
        public IActionResult CreateTrip(TripViewModel voyage) {
            //check whether user is logged in, and if so get session variables (user id and name)
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) {
                return RedirectToAction("Index", "User");
            }            
            if (ModelState.IsValid) 
            {   //set current user as wedding creator and add to db

                Trip newTrip = new Trip 
                {
                    Destination = voyage.Destination,
                    Description = voyage.Description,
                    TravelStartDate = voyage.TravelStartDate,
                    TravelEndDate = voyage.TravelEndDate,
                    UserId = (int)HttpContext.Session.GetInt32("UserId"),
                };
                // voyage.UserId = (int)userId;
                _context.Add(newTrip);
                // _context.SaveChanges();
                int lastTripId = _context.Trips.LastOrDefault().TripId;
                _context.Plans.Add(new Plan{
                    TripId = lastTripId,
                    UserId = (int) userId
                });
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            //show add page with errors if u have validations
            return View("Add");
        }

        [HttpGet]
        [Route("show/{id}")]
        //show trip details
        public IActionResult Show(int id) 
        { 
            //check whether user is logged in == get session variables (user id and name)     
            // int? userId = HttpContext.Session.GetInt32("UserId");
            // if (userId == null) {
            //     return RedirectToAction("Index", "User");
            // }
            // ViewBag.userid = userId;
            // ViewBag.username = HttpContext.Session.GetString("Username");
            //collect trip details
            // ViewBag.trip = _context.Trips.Where(w => w.TripId == id).Include(w => w.Plans).ThenInclude(g => g.User).SingleOrDefault();
            ViewBag.trip = _context.Trips.Include(t => t.User).ToList().Where(t => t.TripId == id).FirstOrDefault();

            ViewBag.attending = _context.Plans.Include(p => p.User).ToList().Where(p => p.TripId == id);
            
            return View();
        }

         [HttpGet]
        [Route("join/{id}")]
        //show trip details
        public IActionResult Join(int id) 
        {
            _context.Plans.Add(new Plan{
                TripId = id,
                UserId = (int) HttpContext.Session.GetInt32("UserId")
            });
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        } 
        
   }
}
    