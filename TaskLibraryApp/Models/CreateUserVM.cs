using System.ComponentModel.DataAnnotations;

namespace TaskLibraryApp.Models
{
    public class CreateUserVM
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [DataType(DataType.Password)]
        public string Name { get; set; }
    }
}
