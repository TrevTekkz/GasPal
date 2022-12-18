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
    public class LoginController : Controller
    {

        // GET: /<controller>/
        public IActionResult Login(LoginCode login)
        {
           string connstring = "Server=; Database=; UserID=; Password=;";
            MySqlConnection conn = new MySqlConnection(connstring);

            string Email = login.Email;
            string Password = login.Password;

            conn.Open();
            string query = "SELECT * FROM User where email='" + Email + "' AND password='" + Password + "'";
            MySqlCommand logintest = new MySqlCommand(query, conn);
            MySqlDataReader Databaseread;

            using (Databaseread = logintest.ExecuteReader()) {
                if (Databaseread.Read())
                {
                    conn.Close();
                    Databaseread.Close();

                    return View("Index");
                }
                else
                {
                    conn.Close();
                    Databaseread.Close();

                    return View("Login");
                }
                }
            //return View("Login");
        }

        
       

    }
}

