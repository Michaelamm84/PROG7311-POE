using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PROG_WEB_APP.Models
{
    public class Employee : IdentityUser
    {
        
        public string Fullname { get; set; }
    }
}
