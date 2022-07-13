using ECommerceAPI.Application.Excepitons;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = ECommerceAPI.Domain.Entities.Identity;
namespace ECommerceAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        // UserManager servisi EntityFramWork.Identity ile birlikte geldi. Bu sayede repository oluşturmamıza gerek kalmadı.
        // UserManager identity ile ilgili işleri yapmamıza yardımcı olan hazır bir servisdir.
        readonly UserManager<P.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<P.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                // Id'yi bizim burda tanımlamamız lazım çünkü ne vereceğini kendisi bilmiyor.
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email,
                NameSurname = request.NameSurname

            }, request.Password);

            CreateUserCommandResponse response = new() { Succeded = result.Succeeded };

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
