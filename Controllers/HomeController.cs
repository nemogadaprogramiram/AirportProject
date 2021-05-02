using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PlaneProject.Models;
using PagedList;

namespace PlaneProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
 
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Flights(int? page, int? pageSize)
        {
            List<Flights> flights = Procedures.Procedures.SelectFlights();
            int pageNumber = (page ?? 1);
            return View(flights.ToPagedList(pageNumber, pageSize??10));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region Reservations
        [HttpPost]
        public ActionResult ReservationInsert(Reservations reservationsModel)
        {
            int id = reservationsModel.Id;
            Flights passangers = Procedures.Procedures.SelectColumnPassangers(id);
           
            if (reservationsModel.TypeOfTicket== "First class"&& passangers.CapacityBusinessClass>0)
            {
                Procedures.Procedures.ChangeCapacityBusinessClass(id);
                Procedures.Procedures.Reservation(reservationsModel);
                SendEmail(reservationsModel,"Successfully made reservation!", $"We wish you wonderful trip! You're flight is {reservationsModel.NameOfFlight} and you're type of ticket is {reservationsModel.TypeOfTicket}.");
            }
            else if (reservationsModel.TypeOfTicket == "Second class" && passangers.CapacityOfPassengers > 0)
            {
                Procedures.Procedures.ChangeCapacityOfPassengers(id);
                Procedures.Procedures.Reservation(reservationsModel);
                SendEmail(reservationsModel, "Successfully made reservation!", $"We wish you wonderful trip! You're flight is {reservationsModel.NameOfFlight} and you're type of ticket is {reservationsModel.TypeOfTicket}.");
            }
            else
            {
                SendEmail(reservationsModel, "Unsuccessfully made reservation!", $"Sorry out of space for the fligth {reservationsModel.NameOfFlight}!");
            }
            return View("Index");
        }

        void SendEmail(Reservations reservationsModel,string sub,string body)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("nqkoisihristo@gmail.com", "Airport");
                    var receiverEmail = new MailAddress(reservationsModel.Email, "Receiver");
                    var password = "zsnmjnzlutvrgwno";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }

                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
        }

        public ActionResult MakeReservation(Flights flightsModel)
        {
            Reservations reservations = new Reservations();
            reservations.NameOfFlight = flightsModel.LocationFrom + "/" + flightsModel.LocationTo;
            reservations.Id = flightsModel.Id;
            return View(reservations);
        }
        #endregion
    }


}