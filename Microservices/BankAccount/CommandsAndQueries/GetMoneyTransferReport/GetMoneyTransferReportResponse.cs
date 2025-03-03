using System.Net;
using BankAccountService.Common.Models;

namespace BankAccountService.CommandsAndQueries.GetMoneyTransferReport
{
    public class GetMoneyTransferReportResponse : ResponseModel
    {

        public GetMoneyTransferReportResponse(HttpStatusCode status, byte[] file) : base(status)
        {
            Data = new GetAccountBalanceResponseData(file);
        }
    }

    public class GetAccountBalanceResponseData
    {
        public byte[] File { get; private set; }

        public GetAccountBalanceResponseData(byte[] file)
        {
            File = file;
        }
    }
}
