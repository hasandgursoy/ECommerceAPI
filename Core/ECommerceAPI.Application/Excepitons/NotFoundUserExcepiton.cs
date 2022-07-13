using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Excepitons
{
    public class NotFoundUserExcepiton : Exception
    {
        public NotFoundUserExcepiton() : base("Kullanıcı Adı veya Şifre Hatalı")
        {
        }

        public NotFoundUserExcepiton(string? message) : base(message)
        {
        }

        public NotFoundUserExcepiton(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }


}
