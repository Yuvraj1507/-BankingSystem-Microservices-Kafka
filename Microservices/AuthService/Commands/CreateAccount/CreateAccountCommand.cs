using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AuthService.Common.Models;
using AuthService.Exceptions;
using AuthService.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<ResponseModel>
    {
        public UserManager<Entities.Account> userManager { get; private set; }

        public string Login { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Password { get; private set; }

        public CreateAccountCommand(UserManager<Entities.Account> userManager, RegistrationModel model)
        {
            this.userManager = userManager;
            this.Login = model.Login;
            this.FirstName = model.FirstName;
            this.LastName = model.LastName;
            this.Password = model.Password;
        }
    }


    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ResponseModel>
    {
        public async Task<ResponseModel> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            UserManager<Entities.Account> userManager = request.userManager;

            var userExists = await userManager.FindByNameAsync(request.Login);
            if (userExists != null)
            {
                throw new AccountAlreadyExistsException();
            }

            Entities.Account user = new Entities.Account()
            {
                UserName = request.Login,
                FirstName = request.FirstName,
                LastName = request.LastName,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new AccountCreationFailedException();
            }
            return new CreateAccountResponse(HttpStatusCode.OK, user.Id) ;
        }
    }
}
