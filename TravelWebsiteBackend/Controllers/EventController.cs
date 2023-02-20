using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;
using TravelWebsiteBackend.Models;

namespace TravelWebsiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public EventController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("events")]
        public JsonResult Events(Events eventobj)
        {
            string query = @"insert into eventdatas (venue,eventname,date,time) values (@venue,@eventname,@date,@time)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@venue", eventobj.venue);
                    mycmd.Parameters.AddWithValue("@eventname", eventobj.eventname);
                    mycmd.Parameters.AddWithValue("@date", eventobj.date);
                    mycmd.Parameters.AddWithValue("@time", eventobj.time);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Added Successfully");

        }
        [HttpGet("eventlist")]
        public String EventList([FromQuery] int id = 0)
        {
            //string que = id > 0 ? "" : "";
            string query = id > 0 ? @"select * from eventdatas where id = " + id : @"select * from eventdatas";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                {

                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();

                }
            }
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
            /*return new JsonResult(JSONString);*/
            /*if (table.Rows.Count > 0)
            {
                return new JsonResult(JSONString);
            }
            else
            {
                return new JsonResult(JSONString);
            }*/

        }
        [HttpPatch("updateevent")]
        public String UpdateEvent(Events eventobj, [FromQuery] int id)
        {
            string query = @"update eventdatas set venue=@venue,eventname=@eventname,date=@date,time=@time where id=" + id;
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                {


                    mycmd.Parameters.AddWithValue("@venue", eventobj.venue);
                    mycmd.Parameters.AddWithValue("@eventname", eventobj.eventname);
                    mycmd.Parameters.AddWithValue("@date", eventobj.date);
                    mycmd.Parameters.AddWithValue("@time", eventobj.time);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();

                }
            }
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
            /*return new JsonResult(JSONString);*/
            /*if (table.Rows.Count > 0)
            {
                return new JsonResult(JSONString);
            }
            else
            {
                return new JsonResult(JSONString);
            }*/

        }

        [HttpDelete("deleteevent")]
        public String DeleteEvent([FromQuery] int id = 0)
        {
            //string que = id > 0 ? "" : "";
            // DELETE FROM tutorials_tbl WHERE tutorial_id = 3;
            string query = @"DELETE  from eventdatas where id = " + id;
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                {

                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();

                }
            }
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;

        }
        [HttpPost("bookingdata")]
        public JsonResult bookingdata(Bookdata eventobj)
        {
            string query = @"insert into bookingdatas (gender,category,seats,age,userId,eventId) values (@gender,@category,@seats,@age,@userId,@eventId)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@gender", eventobj.gender);
                    mycmd.Parameters.AddWithValue("@category", eventobj.category);
                    mycmd.Parameters.AddWithValue("@seats", eventobj.seats);
                    mycmd.Parameters.AddWithValue("@age", eventobj.age);
                    mycmd.Parameters.AddWithValue("@userId", eventobj.userId);
                    mycmd.Parameters.AddWithValue("@eventId", eventobj.eventId);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpGet("bookingdata")]
        public string GetUserBooking([FromQuery] int id=0)
        {
            // string query = id > 0 ? @"select * from eventdatas where id = " + id : @"select * from eventdatas";
            string query = id > 0 ? @"select * from bookingdatas inner join eventdatas on bookingdatas.eventId=eventdatas.id where userId=" + id: @"select * from bookingdatas inner join eventdatas on bookingdatas.eventId=eventdatas.id join userconfiguration on bookingdatas.userId=userconfiguration.id";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                { 
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

        [HttpDelete("deletebook")]
        public String DeleteBookingEvent([FromQuery] int bookId)
        {
            //string que = id > 0 ? "" : "";
            // DELETE FROM tutorials_tbl WHERE tutorial_id = 3;
            string query = @"DELETE  from bookingdatas where bookId="+bookId;
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                {

                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();

                }
            }
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;

        }




    }
}

