using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Excepitons
{
    public class AuthenticationErrorExcepiton : Exception
    {
        public AuthenticationErrorExcepiton():base("Kimlik doğrulama hatası")
        {
        }

        public AuthenticationErrorExcepiton(string? message) : base(message)
        {
        }

        public AuthenticationErrorExcepiton(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
