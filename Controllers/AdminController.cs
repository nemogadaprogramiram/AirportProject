using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using PlaneProject.Models;


namespace PlaneProject.Controllers
{
    public class AdminController : Controller
    {
        static bool dontCheck;
        static bool Check;
        // GET: Admin
        #region Register
        [Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public ActionResult RegisterInsert(UserModel userModel)
        {
            userModel.Password = Encode(userModel.Password);
            Procedures.Procedures.InsertUser(userModel);
            return Redirect("/Admin/Users");
        }

        #endregion

        #region Reservations
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Reservations(int? page,int? pageSize)
        {
            List<Reservations> reservationsModel = Procedures.Procedures.SelectReservations();
            int pageNumber = (page ?? 1);
            ViewBag.Message = pageNumber.ToString();
            return View(reservationsModel.ToPagedList(pageNumber, pageSize??10));
        }
        #endregion

        #region Decode&&Encode
        string Encode(string password)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }

        static string Decode(string password)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(password));
        }

        #endregion

        #region Users

        public ActionResult OrderByUsers(string page, string orderBy)
        {
            List<UserModel> reservationsModel = Procedures.Procedures.ByOrderUsers(orderBy);
            int pageNumber = Convert.ToInt32(page);
            ViewBag.Message = pageNumber.ToString();
            foreach (var item in reservationsModel)
            {
                item.Password = Decode(item.Password);
            }
            return View("Users", reservationsModel.ToPagedList(pageNumber, 10));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Users(int? page,int? pageSize)
        {
            List<UserModel> userModels = Procedures.Procedures.SelectUsers();
            int pageNumber = (page ?? 1);

            ViewBag.Message = pageNumber.ToString();
            return View(userModels.ToPagedList(pageNumber, pageSize??10));
        }

        public ActionResult EditUsers(UserModel userModel)
        {
            return View(userModel);
        }
        [HttpPost]

        public ActionResult EditUser(UserModel userModel)
        {
            userModel.Password = Encode(userModel.Password);
            Procedures.Procedures.UpdateUsers(userModel,userModel.Id);
            return Redirect("/Admin/Users");
        }

        public ActionResult DeleteUser(UserModel userModel)
        {
            Procedures.Procedures.DeleteUsers(userModel.Id);
            return Redirect("/Admin/Users");
        }

        #endregion

        #region Flights
        [Authorize(Roles = "Admin")]
        public ActionResult CreateFlight()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult FlightInsert(Flights flightModel)
        {
            
            int hour_of_arrival = Convert.ToInt32(flightModel.TimeOfArrival.ToString("HH"));
            int hour_of_flight = Convert.ToInt32(flightModel.TimeOfFlight.ToString("HH"));

            if (hour_of_arrival == 0)
            {
                dontCheck = true;
                Check = true;
                hour_of_arrival = 24;
            }
            if (hour_of_flight == 0)
            {
                dontCheck = true;
                Check = true;
                hour_of_flight = 24;
            }
            if (hour_of_flight>hour_of_arrival)
            {
                dontCheck =false;
            }
            if (flightModel.TimeOfFlight.ToUniversalTime() > flightModel.TimeOfArrival.ToUniversalTime()&&dontCheck==false)
            {
                ViewBag.Message = "Invalid time of flight and time of arrival!";
                return View("CreateFlight",flightModel);
            }
            if (flightModel.TimeOfFlight.ToUniversalTime() > flightModel.TimeOfArrival.ToUniversalTime())
            {
                Check = false;
            }

            int days = Math.Abs(Convert.ToInt32(flightModel.TimeOfArrival.ToString("dd")) - Convert.ToInt32(flightModel.TimeOfFlight.ToString("dd")));
            if (Check)
            {
                flightModel.Duration = days * 24 + Math.Abs(Convert.ToInt32(flightModel.TimeOfArrival.ToString("HH")) + Convert.ToInt32(flightModel.TimeOfFlight.ToString("HH")));
            }
            else
            {
                flightModel.Duration = days * 24 + Math.Abs(Convert.ToInt32(hour_of_arrival - hour_of_flight));
            }

            Procedures.Procedures.InsertFlight(flightModel);
            return Redirect("/Home/Flights");
        }
        public ActionResult EditFlight(Flights flightModel)
        {
            return View(flightModel);
        }

        [HttpPost]
        public ActionResult EditFlights(Flights flightModel)
        {
            int hour_of_arrival = Convert.ToInt32(flightModel.TimeOfArrival.ToString("HH"));
            int hour_of_flight = Convert.ToInt32(flightModel.TimeOfFlight.ToString("HH"));
            if (hour_of_arrival == 0)
            {
                Check = true;
                dontCheck = true;
                hour_of_arrival = 24;
            }
            if (hour_of_flight == 0)
            {
                Check = true;
                dontCheck = true;
                hour_of_flight = 24;
            }
            if (hour_of_flight > hour_of_arrival)
            {
                dontCheck = false;
            }
            if (flightModel.TimeOfFlight.ToUniversalTime() > flightModel.TimeOfArrival.ToUniversalTime() && dontCheck == false)
            {
                ViewBag.Message = "Invalid time of flight and time of arrival!";
                return View("CreateFlight", flightModel);
            }
            if (flightModel.TimeOfFlight.ToUniversalTime() > flightModel.TimeOfArrival.ToUniversalTime())
            {
                Check = false;
            }

            int days = Math.Abs(Convert.ToInt32(flightModel.TimeOfArrival.ToString("dd")) - Convert.ToInt32(flightModel.TimeOfFlight.ToString("dd")));
            if (Check)
            {
                flightModel.Duration = days * 24 + Math.Abs(Convert.ToInt32(flightModel.TimeOfArrival.ToString("HH")) + Convert.ToInt32(flightModel.TimeOfFlight.ToString("HH")));
            }
            else
            {
                flightModel.Duration = days * 24 + Math.Abs(Convert.ToInt32(hour_of_arrival - hour_of_flight));
            }
           
            Procedures.Procedures.UpdateFlights(flightModel,flightModel.Id);
            return Redirect("/Home/Flights");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteFlights(Flights flightModel)
        {
            Procedures.Procedures.DeleteFlights(flightModel.Id);
            return Redirect("/Home/Flights");
        }
        #endregion
    }
}