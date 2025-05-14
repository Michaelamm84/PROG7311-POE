using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;


namespace PROG_WEB_APP.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int FarmerId { get; set; }
        public Farmer Farmer { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
