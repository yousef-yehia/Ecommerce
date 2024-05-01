using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        [NotMapped]
        public Address Address { get; set; }
    }
}
