using System.ComponentModel.DataAnnotations;

namespace TaskLibraryApp.Models
{
    public class VMLogin
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        public bool KeepLoggedIn { get; set; }
    }
}
