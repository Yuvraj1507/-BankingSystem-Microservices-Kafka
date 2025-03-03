using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AuthService.Commands.CreateAccount;
using AuthService.Common.Models;
using AuthService.Exceptions;
using AuthService.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Commands.Login
{
    public class LoginCommand : IRequest<ResponseModel>
    {
        public UserManager<Entities.Account> userManager { get; private set; }

        public string Login { get; private set; }

        public string Password { get; private set; }

        public LoginCommand(UserManager<Entities.Account> userManager, LoginModel model)
        {
            this.userManager = userManager;
            this.Login = model.Login;
            this.Password = model.Password;
        }
    }


    public class CreateAccountCommandHandler : IRequestHandler<LoginCommand, ResponseModel>
    {
        public async Task<ResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            UserManager<Entities.Account> userManager = request.userManager;
            var user = await userManager.FindByNameAsync(request.Login);

            if (user != null && await userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("UserId", user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Name, userRole));
                }

                // TODO: security Key to configuration file
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"));

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddMinutes(10),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );


                return new LoginResponse(HttpStatusCode.OK, new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);

            }
            else
            {
                throw new BadCredentialsException();
            }

        }

    }
}
