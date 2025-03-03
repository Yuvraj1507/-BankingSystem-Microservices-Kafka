using Aspose.Cells;
using BankAccountService.Common.Models;
using BankAccountService.Data;
using MediatR;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccountService.CommandsAndQueries.GetMoneyTransferReport
{
    public class GetMoneyTransferReportQuery : IRequest<ResponseModel>
    {
        public Guid AccountId { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }

        public GetMoneyTransferReportQuery(Guid accountId, DateTime from, DateTime to)
        {
            AccountId = accountId;
            From = from;
            To = to;
        }

        public class GetMoneyTransferReportQueryHandler : IRequestHandler<GetMoneyTransferReportQuery, ResponseModel>
        {
            private readonly DataContext _dataContext;

            public GetMoneyTransferReportQueryHandler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<ResponseModel> Handle(GetMoneyTransferReportQuery request, CancellationToken cancellationToken)
            {
                Workbook wb = new Workbook();
                Worksheet sheet = wb.Worksheets[0];
                Cell cell;

                var moneyTransfers = from transfer in _dataContext.MoneyTransfers
                                     where (transfer.AccountFrom == request.AccountId || transfer.AccountTo == request.AccountId)
                                     select transfer;
                if (request.From != new DateTime())
                {
                    moneyTransfers = from transfer in moneyTransfers
                                     where (transfer.Date > request.From)
                                     select transfer;
                }
                if (request.To != new DateTime())
                {
                    moneyTransfers = from transfer in moneyTransfers
                                     where (transfer.Date < request.To)
                                     select transfer;
                }
                var cells = new (string, string)[] { ("A1", "Title"), ("B1", "In/Out"), ("C1", "From/To"), ("D1", "Amount"), ("E1", "Date") };
                foreach (var c in cells)
                {
                    cell = sheet.Cells[c.Item1];
                    cell.PutValue(c.Item2);
                }

                int i = 2;
                foreach (var transfer in moneyTransfers)
                {
                    cell = sheet.Cells["A" + i];
                    cell.PutValue(transfer.Title);
                    if(request.AccountId == transfer.AccountTo)
                    {
                        cell = sheet.Cells["B" + i];
                        cell.PutValue("in");
                        cell = sheet.Cells["C" + i];
                        cell.PutValue(transfer.AccountTo);
                    } else
                    {
                        cell = sheet.Cells["B" + i];
                        cell.PutValue("out");
                        cell = sheet.Cells["C" + i];
                        cell.PutValue(transfer.AccountFrom);
                    }
                    cell = sheet.Cells["D" + i];
                    cell.PutValue(transfer.Amount);
                    cell = sheet.Cells["E" + i];
                    cell.PutValue(transfer.Date.ToString(CultureInfo.CreateSpecificCulture("pl")));
                    i++;
                }
                var file = wb.SaveToStream().ToArray();
                return new GetMoneyTransferReportResponse(System.Net.HttpStatusCode.OK, file);
            }
        }
    }
}
