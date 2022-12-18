using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GasPal.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GasPal.Controllers
{
    public class CreateAccountController : Controller
    {
        // GET: /<controller>/
        public IActionResult CreateAccount(CreateAccount signup)
        {
            string connstring = "Server=; Database=; UserID=; Password=;";
            MySqlConnection conn = new MySqlConnection(connstring);

            string Email = signup.Email;
            string Password = signup.Password;
            string FirstName = signup.FirstName;
            string LastName = signup.LastName;
            string Zipcode = signup.Zipcode;

            conn.Open();
            string query = $"insert into User (email, password, fname, lname, zipcode)" + $"values(@Email, @Password, @fname, @lname, @zipcode)";
            MySqlCommand connect = new MySqlCommand(query, conn);
            connect.CommandType = CommandType.Text;
            connect.Parameters.AddWithValue("@Email", Email);
            connect.Parameters.AddWithValue("@Password", Password);
            connect.Parameters.AddWithValue("@fname", FirstName);
            connect.Parameters.AddWithValue("@lname", LastName);
            connect.Parameters.AddWithValue("@zipcode", Zipcode);
            connect.Prepare();
            connect.ExecuteReader();
            conn.Close();

            return View("CreateAccount");
        }
    }
}

