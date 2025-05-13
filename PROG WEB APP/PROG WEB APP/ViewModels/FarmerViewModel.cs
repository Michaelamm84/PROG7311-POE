using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PROG_WEB_APP.ViewModels
{
    public class FarmerViewModel
    {

        [Required]
        public string FName { get; set; }

        public string FAddress { get; set; }


        public string FEmail { get; set; }
        [Required]
        public string FPassword { get; set; }



    }
}
