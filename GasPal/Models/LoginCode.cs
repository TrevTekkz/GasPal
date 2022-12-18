using System.ComponentModel.DataAnnotations;


namespace GasPal.Models
{
    public class LoginCode
    {
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}
