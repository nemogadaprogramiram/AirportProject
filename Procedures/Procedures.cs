using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PlaneProject.Models;
using Dapper;
using System.Text;

namespace PlaneProject.Procedures
{
    public class Procedures
    {
        public static string cnnString = @"Server=.\SQLEXPRESS;Database=AirportDatabase;Trusted_Connection=True;";
        static string Decode(string password)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(password));
        }
        public static void InsertUser(UserModel registerModel)
        {
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spRegister";
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = registerModel.Username;
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = registerModel.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = registerModel.LastName;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = registerModel.Password;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = registerModel.Email;
            cmd.Parameters.Add("@Phonenumber", SqlDbType.VarChar).Value = registerModel.Phonenumber;
            cmd.Parameters.Add("@EGN", SqlDbType.VarChar).Value = registerModel.EGN;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = registerModel.Address;
            cmd.Parameters.Add("@Role", SqlDbType.VarChar).Value = registerModel.Role;
            cnn.Open();
            _ = cmd.ExecuteScalar();
            cnn.Close();

        }


        public static void UpdateUsers(UserModel userModel, int id)
        {
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spUpdateUsers";
            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = userModel.Username;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = userModel.Password;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = userModel.Email;
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = userModel.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = userModel.LastName;
            cmd.Parameters.Add("@EGN", SqlDbType.VarChar).Value = userModel.EGN;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = userModel.Address;
            cmd.Parameters.Add("@Phonenumber", SqlDbType.VarChar).Value = userModel.Phonenumber;
            cmd.Parameters.Add("@Role", SqlDbType.VarChar).Value = userModel.Role;
            cnn.Open();
            _ = cmd.ExecuteScalar();
            cnn.Close();

        }
        public static void DeleteUsers(int id)
        {
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spDeleteUsers";
            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
            cnn.Open();
            _ = cmd.ExecuteScalar();
            cnn.Close();

        }

        public static void Reservation(Reservations reservationModel)
        {
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spReservation";
            cmd.Parameters.Add("@NameOfFlight", SqlDbType.VarChar).Value = reservationModel.NameOfFlight;
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = reservationModel.FirstName;
            cmd.Parameters.Add("@SecondName", SqlDbType.VarChar).Value = reservationModel.SecondName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = reservationModel.LastName;
            cmd.Parameters.Add("@EGN", SqlDbType.VarChar).Value = reservationModel.EGN;
            cmd.Parameters.Add("@Phonenumber", SqlDbType.VarChar).Value = reservationModel.PhoneNumber;
            cmd.Parameters.Add("@Nationoality", SqlDbType.VarChar).Value = reservationModel.Nationality;
            cmd.Parameters.Add("@Type_of_ticket", SqlDbType.VarChar).Value = reservationModel.TypeOfTicket;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = reservationModel.Email;
            cnn.Open();
            _ = cmd.ExecuteScalar();
            cnn.Close();

        }

        public static void ChangeCapacityOfPassengers(int id)
        {
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spChangeCapacityOfPassengers";
            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
            cnn.Open();
            _ = cmd.ExecuteScalar();
            cnn.Close();

        }
        public static void ChangeCapacityBusinessClass(int id)
        {
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spChangeCapacityBusinessClass";
            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
            cnn.Open();
            _ = cmd.ExecuteScalar();
            cnn.Close();

        }


        public static Flights SelectColumnPassangers(int id)
        {
            using (IDbConnection con = new SqlConnection(cnnString))
            {
                var parameter = new { Id = id };
                var output = con.QuerySingle<Flights>("spSelectColumnPassangers @Id",parameter);
                return output;
            }
        }


        public static void UpdateFlights(Flights flightModel,int id)
        {
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spUpdateFlights";
            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@Duration", SqlDbType.Int).Value = flightModel.Duration;
            cmd.Parameters.Add("@LocationFrom", SqlDbType.VarChar).Value = flightModel.LocationFrom;
            cmd.Parameters.Add("@LocationTo", SqlDbType.VarChar).Value = flightModel.LocationTo;
            cmd.Parameters.Add("@TimeOfFlight", SqlDbType.DateTime).Value = flightModel.TimeOfFlight;
            cmd.Parameters.Add("@TimeOfArrival", SqlDbType.DateTime).Value = flightModel.TimeOfArrival;
            cmd.Parameters.Add("@TypeOfPlane", SqlDbType.VarChar).Value = flightModel.TypeOfPlane;
            cmd.Parameters.Add("@UniqueNumberOfPlane", SqlDbType.VarChar).Value = flightModel.UniqueNumberOfPlane;
            cmd.Parameters.Add("@NamePilot", SqlDbType.VarChar).Value = flightModel.NamePilot;
            cmd.Parameters.Add("@CapacityOfPassengers", SqlDbType.Int).Value = flightModel.CapacityOfPassengers;
            cmd.Parameters.Add("@CapacityBusinessClass", SqlDbType.Int).Value = flightModel.CapacityBusinessClass;
            cnn.Open();
            _ = cmd.ExecuteScalar();
            cnn.Close();

        }
        public static void DeleteFlights(int id)
        {
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spDeleteFlights";
            cmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = id;
            cnn.Open();
            _ = cmd.ExecuteScalar();
            cnn.Close();

        }
        
        public static void InsertFlight(Flights flightModel)
        {
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spInsertFlights";
            cmd.Parameters.Add("@Duration", SqlDbType.Int).Value = flightModel.Duration;
            cmd.Parameters.Add("@LocationFrom", SqlDbType.VarChar).Value = flightModel.LocationFrom;
            cmd.Parameters.Add("@LocationTo", SqlDbType.VarChar).Value = flightModel.LocationTo;
            cmd.Parameters.Add("@TimeOfFlight", SqlDbType.SmallDateTime).Value = flightModel.TimeOfFlight;
            cmd.Parameters.Add("@TimeOfArrival", SqlDbType.SmallDateTime).Value = flightModel.TimeOfArrival;
            cmd.Parameters.Add("@TypeOfPlane", SqlDbType.VarChar).Value = flightModel.TypeOfPlane;
            cmd.Parameters.Add("@UniqueNumberOfPlane", SqlDbType.VarChar).Value = flightModel.UniqueNumberOfPlane;
            cmd.Parameters.Add("@NamePilot", SqlDbType.VarChar).Value = flightModel.NamePilot;
            cmd.Parameters.Add("@CapacityOfPassengers", SqlDbType.Int).Value = flightModel.CapacityOfPassengers;
            cmd.Parameters.Add("@CapacityBusinessClass", SqlDbType.Int).Value = flightModel.CapacityBusinessClass;
            cnn.Open();
            _ = cmd.ExecuteScalar();
            cnn.Close();

        }
        public static List<UserModel> SelectUsers()
        {
            using (IDbConnection con = new SqlConnection(cnnString))
            {
                var output = con.Query<UserModel>("spSelectUsers").ToList();
                foreach (var item in output)
                {
                    item.Password = Decode(item.Password);
                }
                return output;
            }
        }
        public static List<UserModel> ByOrderUsers(string orderBy)
        {
            using (IDbConnection con = new SqlConnection(cnnString))
            {
                var parameter = new { OrderBy = orderBy};
                var output = con.Query<UserModel>("spSelectByOrderUsers @OrderBy", parameter).ToList();
                return output;
            }
        }
        public static List<Flights> SelectFlights()
        {
            using (IDbConnection con = new SqlConnection(cnnString))
            {
                var output = con.Query<Flights>("spSelectFlights").ToList();
                return output;
            }
        }
        public static List<Reservations> SelectReservations()
        {
            using (IDbConnection con = new SqlConnection(cnnString))
            {
                var output = con.Query<Reservations>("spSelectReservations").ToList();
                return output;
            }
        }
       
        public static List<UserModel> UserInfo()
        {
            using (IDbConnection con = new SqlConnection(cnnString))
            {
                var output = con.Query<UserModel>("spUserInfo").ToList();
                return output;
            }
        }

    }
}