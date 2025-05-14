using PROG_WEB_APP.Models;
using System.ComponentModel.DataAnnotations;

namespace PROG_WEB_APP.ViewModels
{
    public class FarmerSearchViewModel
    {
       
        public string FName { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
        public List<Farmer> farmers { get; set; } = new List<Farmer>();

        
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    }
}
