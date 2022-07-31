using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.DTOs.User;
using ECommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistance.Services
{
    public class UserService : IUserService
    {
        // UserManager servisi EntityFramWork.Identity ile birlikte geldi. Bu sayede repository oluşturmamıza gerek kalmadı.
        // UserManager identity ile ilgili işleri yapmamıza yardımcı olan hazır bir servisdir.

        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto model)
        {
            

            IdentityResult result = await _userManager.CreateAsync(new()
            {
                // Id'yi bizim burda tanımlamamız lazım çünkü ne vereceğini kendisi bilmiyor.
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                NameSurname = model.NameSurname

            }, model.Password);

            CreateUserResponseDto response = new() { Succeded = result.Succeeded };

            // Eğer başarılıysa 
            if (result.Succeeded)
            {
                response.Message = "Kullanıcı Başarıyla Oluşturuldu.";
            }
            else
            {
                // Birden fazla hatamız varas result.Errors

                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description}";
                }
            }

            return response;
        }
    }
}
