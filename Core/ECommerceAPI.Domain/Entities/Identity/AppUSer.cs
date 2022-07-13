using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Identity
{
    // IdentityUser<string> yaptığımız için Guid olarak değer üretecekdir.
    public class AppUser : IdentityUser<string>
    {
        public string NameSurname { get; set; }
        
    }
}
