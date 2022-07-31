using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        // Uretilen jwt tokenın adı CreateAccesToken 
        DTOs.Token CreateAccessToken(int second);

    }
}
