using PROG_WEB_APP.Models;
using System.ComponentModel.DataAnnotations;

namespace PROG_WEB_APP.ViewModels
{
    public class ProductViewModel
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string PName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int FarmerId { get; set; }
        
    }
}

