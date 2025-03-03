using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BankAccountService.Common.Exceptions;
using BankAccountService.Common.Models;
using BankAccountService.Data;
using BankAccountService.Data.Entities;
using MediatR;

namespace BankAccountService.CommandsAndQueries.CreateBankAccount
{
    public class CreateBankAccountCommand : IRequest<ResponseModel>
    {
        public Guid UserId { get; private set; }
       

        public CreateBankAccountCommand(Guid userId)
        {
            UserId = userId;
        }
    }

    public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, ResponseModel>
    {
        private readonly DataContext dataContext;

        public CreateBankAccountCommandHandler(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<ResponseModel> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
        {
            var existingAccount = dataContext.BankAccounts.Find(request.UserId);
            if (existingAccount != null)
            {
                throw new UserAlreadyHaveBankAccountException();
            }
            var newAccount = new BankAccount(request.UserId);
            dataContext.BankAccounts.Add(newAccount);
            dataContext.SaveChanges();

            return new CreateBankAccountResponse(HttpStatusCode.OK, newAccount.Id);
        }
    }
}