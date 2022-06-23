using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.StaticServices
{
    public static class NameOperation
    {

        public static string ChracterRegulatory(string name)

            => name.Replace("\"", "")
                .Replace("!", "")
                .Replace("'", "")
                .Replace("^", "")
                .Replace("%", "")
                .Replace("&", "")
                .Replace("/", "")
                .Replace("?", "")
                .Replace("-", "")
                .Replace("+", "")
                .Replace("*", "")
                .Replace("%", "")
                .Replace("#", "")
                .Replace("$", "")
                .Replace("£", "")
                .Replace("~", "")
                .Replace(";", "")
                .Replace(":", "")
                .Replace(".", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "")
                .Replace("ı", "i")
                .Replace("ö", "o")
                .Replace("Ö", "O")
                .Replace("İ", "I")
                .Replace("ğ", "g")
                .Replace("Ğ", "G")
                .Replace("´", "")
                .Replace("Ç", "C")
                .Replace("ç", "c")
                .Replace("Ü", "U")
                .Replace("ü", "u")
                .Replace("Ş", "S")
                .Replace("ş", "s");



    }
}
