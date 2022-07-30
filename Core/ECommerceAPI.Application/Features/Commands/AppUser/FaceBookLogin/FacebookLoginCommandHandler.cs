using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.DTOs.Facebook;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Commands.AppUser.FaceBookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly HttpClient _httpClient;

        public FacebookLoginCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler, IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {

            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id=546631843676576?client_scret=d323213123539934939asad92139139ac&grant_type=client_credentials");

            FacebookAccessTokenResponseDto facebookAccessTokenResponseDto = JsonSerializer.Deserialize<FacebookAccessTokenResponseDto>(accessTokenResponse);

            string userAccessTokenValidation =  await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={request.AuthToken}&access_token={facebookAccessTokenResponseDto.AccessToken}");

            FacebookUserAccessTokenValidationDto validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidationDto>(userAccessTokenValidation);

            if (validation.Data.IsValid)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={request.AuthToken}");
           
                FacebookUserInfoResponse facebookUserInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

                var info = new UserLoginInfo("FACEBOOK",validation.Data.UserId,"FACEBOOK");
                Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                bool result = user != null;

                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(facebookUserInfo.Email);
                    if (user == null)
                    {
                        user = new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Email = facebookUserInfo.Email,
                            UserName = facebookUserInfo.Email,
                            NameSurname = facebookUserInfo.Name
                        };

                        var identityReuslt = await _userManager.CreateAsync(user);
                        result = identityReuslt.Succeeded;
                    }
                }

                if (result)
                {
                    await _userManager.AddLoginAsync(user, info); // AspNetUserLogins
                    Token token = _tokenHandler.CreateAccessToken(5);
                    return new()
                    {
                        Token = token,
                    };
                }
                

                
            }
            throw new Exception("Invalid External Authentication.");



        }
    }
}
