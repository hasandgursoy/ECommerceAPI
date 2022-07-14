using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Excepitons;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        // SignInManager kullanıcının giriş yapabilmesi yardım eden service.
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;



        public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager,
            SignInManager<Domain.Entities.Identity.AppUser> signInManager,
            ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            // Eğer yukarıdakinde username null gelirse mail'e bakacağız.
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
            }

            if (user == null)
            {
                throw new NotFoundUserExcepiton();
            }

            // CheckPasswordSignInAsync 'in lockPassInFailure parametresini true yaparsak
            // 3 den fazla yanlış girişti hesap 15 dk kitlensin diyebiliyoruz yada başka senaryolar.
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user,request.Password,false);

            // Artık Giriş Başarılıysa Yetkilendirme(Authorization) işlemine geçelim.
            if (result.Succeeded) // Authentication başarılı :D 
            {
                Token token = _tokenHandler.CreateAccessToken(5);

                return new LoginUserSuccessCommandResponse()
                {
                    Token = token,
                };
               
            }

            throw new AuthenticationErrorExcepiton();

            
           
        }
    }
}
