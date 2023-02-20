using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;
using Microsoft.Extensions.Configuration;
using TravelWebsiteBackend.Models;
using System.Configuration;

namespace TravelWebsiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public  UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("Register")]
        public JsonResult Register(Userlogin userobj)
        {
            string query = @"insert into userconfiguration (Name,PhoneNo,Mail,Role,Password) values (@Name,@PhoneNo,@Mail,@Role,@Password)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@Name", userobj.Name);
                    mycmd.Parameters.AddWithValue("@PhoneNo", userobj.PhoneNo);
                    mycmd.Parameters.AddWithValue("@Mail", userobj.Mail);
                    mycmd.Parameters.AddWithValue("@Role", userobj.Role);
                    mycmd.Parameters.AddWithValue("@Password", userobj.Password);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Added Successfully");

        }
        [HttpPost("Login")]
        public JsonResult Login(Login userobj)
        {
            string query = @"select * from userconfiguration where Mail = @Mail and Password = @Password";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@Mail", userobj.Mail);
                    mycmd.Parameters.AddWithValue("@Password", userobj.Password);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();

                }
            }
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            //return JSONString;
            return new JsonResult(JSONString);
            /*if (table.Rows.Count > 0)
            {
                return new JsonResult(JSONString);
            }
            else
            {
                return new JsonResult(JSONString);
            }*/

        }
    }
}
