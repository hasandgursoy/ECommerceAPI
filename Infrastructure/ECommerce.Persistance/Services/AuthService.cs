using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.DTOs.Facebook;
using ECommerceAPI.Application.Excepitons;
using ECommerceAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistance.Services
{
    public class AuthService : IAuthService
    {
        readonly HttpClient _httpClient;
        readonly IConfiguration _configuration;
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager ;

        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration, UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler)
        {
            httpClientFactory.CreateClient();
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        async Task<Token> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info, int accessTokenLifeTime) 
        {
            bool result = user != null;

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName =email,
                        NameSurname = name
                    };

                    var identityReuslt = await _userManager.CreateAsync(user);
                    result = identityReuslt.Succeeded;
                }
            }

            if (result)
            {
                await _userManager.AddLoginAsync(user, info); // AspNetUserLogins
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);
                return token;
            }
            throw new Exception("Invalid External Authentication.");

        }


        public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
        {
            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}?client_scret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");

            FacebookAccessTokenResponseDto? facebookAccessTokenResponseDto = JsonSerializer.Deserialize<FacebookAccessTokenResponseDto>(accessTokenResponse);

            string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponseDto?.AccessToken}");

            FacebookUserAccessTokenValidationDto? validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidationDto>(userAccessTokenValidation);

            if (validation?.Data.IsValid != null)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

                FacebookUserInfoResponse? facebookUserInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

                var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
                Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                await CreateUserExternalAsync(user,facebookUserInfo.Email,facebookUserInfo.Name,info,accessTokenLifeTime);

            }
            throw new Exception("Invalid External Authentication.");


        }

        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
            Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return  await CreateUserExternalAsync(user,payload.Email,payload.Name,info,accessTokenLifeTime);

        }

        public async Task<Token> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(userNameOrEmail);
            // Eğer yukarıdakinde username null gelirse mail'e bakacağız.
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(userNameOrEmail);
            }

            if (user == null)
            {
                throw new NotFoundUserExcepiton();
            }

            // CheckPasswordSignInAsync 'in lockPassInFailure parametresini true yaparsak
            // 3 den fazla yanlış girişti hesap 15 dk kitlensin diyebiliyoruz yada başka senaryolar.
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password,false );

            // Artık Giriş Başarılıysa Yetkilendirme(Authorization) işlemine geçelim.
            if (result.Succeeded) // Authentication başarılı :D 
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);

                return token;

            }

            throw new AuthenticationErrorExcepiton();
        }
    }
}
