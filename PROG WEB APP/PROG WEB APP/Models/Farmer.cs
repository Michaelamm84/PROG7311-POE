using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PROG_WEB_APP.Models
{
   
        public class Farmer 
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public string FName { get; set; }

            [Required]
            public string FPassword { get; set; }

            public string FAddress { get; set; }

            public string FEmail { get; set; }

            public List<Product> Products { get; set; } = new List<Product>();
        }
   }


