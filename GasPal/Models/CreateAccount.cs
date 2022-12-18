﻿using System.ComponentModel.DataAnnotations;

namespace GasPal.Models
{
    public class CreateAccount
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Zipcode { get; set; }
    }
}
