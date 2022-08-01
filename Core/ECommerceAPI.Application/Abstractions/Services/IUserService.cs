using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        // UserService Asıl kodlayacağımız yer veri tabanıyla ilgili kalıcılık işlemi gerçekleştirdiğimiz Persistence olacak.
        Task<CreateUserResponseDto> CreateAsync(CreateUserDto model);
        Task UpdateRefreshToken(string refreshToken, AppUser user,DateTime accessTokenDate, int addOnAccessTokenLifeTime);
    }
}
