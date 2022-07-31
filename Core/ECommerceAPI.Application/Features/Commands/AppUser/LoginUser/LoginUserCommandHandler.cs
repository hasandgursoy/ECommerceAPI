using ECommerceAPI.Application.Abstractions.Services.Authentications;
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
        readonly IInternalAuthentication _authService;

        public LoginUserCommandHandler(IInternalAuthentication authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {

            var token = await _authService.LoginAsync(request.UsernameOrEmail,request.Password,15);
            return new LoginUserSuccessCommandResponse()
            {
                Token = token
            };
           
        }
    }
}
