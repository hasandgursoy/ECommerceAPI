using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Excepitons
{
    public class UserCreateFaildException : Exception
    {

        public UserCreateFaildException():base("Kullanıcı oluştururken beklenmedik bir hata ile karşılaşıldı.")
        {
        }

        public UserCreateFaildException(string? message) : base(message)
        {
        }

        public UserCreateFaildException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }

}
