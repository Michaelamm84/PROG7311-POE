using System.ComponentModel.DataAnnotations;

namespace PROG_WEB_APP.ViewModels
{
    public class FarmerSignIn
    {


        [Required]
        public string FName { get; set; }

        [Required]
        public string FPassword { get; set; }

    }
}
