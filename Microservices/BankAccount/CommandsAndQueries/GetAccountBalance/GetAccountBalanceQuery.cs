using System;
using System.Threading;
using System.Threading.Tasks;
using BankAccountService.CommandsAndQueries.CreateBankAccount;
using BankAccountService.Common.Exceptions;
using BankAccountService.Common.Models;
using BankAccountService.Data;
using MediatR;

namespace BankAccountService.CommandsAndQueries.GetAccountBalance
{
    public class GetAccountBalanceQuery: IRequest<ResponseModel>
    {
        public Guid AccountId { get; private set; }

        public GetAccountBalanceQuery(Guid accountId)
        {
            AccountId = accountId;
        }
    }

    public class GetAccountBalanceQueryHandler : IRequestHandler<GetAccountBalanceQuery, ResponseModel>
    {
        private readonly DataContext _dataContext;

        public GetAccountBalanceQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ResponseModel> Handle(GetAccountBalanceQuery request, CancellationToken cancellationToken)
        {
            var bankAccount = _dataContext.BankAccounts.Find(request.AccountId);
            if (bankAccount == null)
            {
                throw new BankAccountNotFoundException();
            }
            float balance = bankAccount.Balance;
            return new GetAccountBalanceResponse(System.Net.HttpStatusCode.OK, balance);
        }
    }

}