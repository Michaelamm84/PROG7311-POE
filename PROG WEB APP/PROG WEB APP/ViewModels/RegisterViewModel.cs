using System.ComponentModel.DataAnnotations;

namespace PROG_WEB_APP.ViewModels
{
    

   
    
        public class RegisterViewModel
        {
            [Required(ErrorMessage = "Name is required.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Email is required.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            [StringLength(16, MinimumLength = 8, ErrorMessage = "The {0} must be at {2} and at max {1} characters")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

               //[Required(ErrorMessage = "Confirm Password is required.")]
               //[DataType(DataType.Password)]
              //public string ConfirmPassword { get; set; }
        }
    }

