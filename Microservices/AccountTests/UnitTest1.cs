using BankAccountService.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AccountTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateBankAccountTest()
        {
            var mediator = new Mock<IMediator>();
            var account_controller = new BankAccountController(mediator.Object);
            var result = account_controller.createBankAccount(new System.Guid("99999999-9999-9999-9999-999999999999"));
            var obj_result = result as ObjectResult;
            Assert.IsNotNull(obj_result);
            Assert.AreEqual(200, obj_result.StatusCode);

        }

        [TestMethod]
        public void AccountBalanceTest()
        {
            var mediator = new Mock<IMediator>();
            var account_controller = new BankAccountController(mediator.Object);
            account_controller.createBankAccount(new System.Guid("99999999-9999-9999-9999-999999999998"));
            var result = account_controller.getAccountBalanceAsync(new System.Guid("99999999-9999-9999-9999-999999999998"));
            var obj_result = result.Result as ObjectResult;
            Assert.IsNotNull(obj_result);
            Assert.AreEqual(200, obj_result.StatusCode);

        }

        [TestMethod]
        public void MoneyTransferTest()
        {
            var mediator = new Mock<IMediator>();
            var account_controller = new BankAccountController(mediator.Object);
            account_controller.createBankAccount(new System.Guid("99999999-9999-9999-9999-999999999999"));
            account_controller.createBankAccount(new System.Guid("99999999-9999-9999-9999-999999999998"));
            var money_controller = new MoneyTransferController(mediator.Object);
            var money_transfer = new BankAccountService.Common.Models.MoneyTransferModel();
            money_transfer.Amount = 100.0f;
            money_transfer.TargetAccountId = new System.Guid("99999999-9999-9999-9999-999999999999");
            var result = money_controller.makeTransfer(new System.Guid("99999999-9999-9999-9999-999999999998"), money_transfer);
            var obj_result = result.Result as ObjectResult;
            Assert.IsNotNull(obj_result);
            Assert.AreEqual(200, obj_result.StatusCode);

        }

        [TestMethod]
        public void PerfTest1()
        {
            var mediator = new Mock<IMediator>();
            var account_controller = new BankAccountController(mediator.Object);
            account_controller.createBankAccount(new System.Guid("99999999-9999-9999-9999-999999999999"));
            List<Thread> list = new List<Thread>();

            for (int i = 0; i < 10000; i++)
            {
                account_controller.createBankAccount(new System.Guid("99999999-" + i.ToString().PadLeft(4, '0') + "-9999-9999-999999999999"));
                list.Append(new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    var money_controller = new MoneyTransferController(mediator.Object);
                    var money_transfer = new BankAccountService.Common.Models.MoneyTransferModel();
                    money_transfer.Amount = 100.0f;
                    money_transfer.TargetAccountId = new System.Guid("99999999-9999-9999-9999-999999999999");
                    var result = money_controller.makeTransfer(new System.Guid("99999999-" + i.ToString().PadLeft(4, '0') + "-9999-9999-999999999999"), money_transfer);
                    var obj_result = result.Result as ObjectResult;
                    Assert.IsNotNull(obj_result);
                    Assert.AreEqual(200, obj_result.StatusCode);
                }));
            }

            foreach (Thread t in list)
                t.Start();


        }
        [TestMethod]
        public void PerfTest2()
        {
            var mediator = new Mock<IMediator>();
            var account_controller = new BankAccountController(mediator.Object);
            account_controller.createBankAccount(new System.Guid("99999999-9999-9999-9999-999999999999"));
            List<Thread> list = new List<Thread>();

            for (int i = 0; i < 100000; i++)
            {
                account_controller.createBankAccount(new System.Guid(i.ToString().PadLeft(8, '0') + "-9999-9999-9999-999999999999"));
                list.Append(new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    var money_controller = new MoneyTransferController(mediator.Object);
                    var money_transfer = new BankAccountService.Common.Models.MoneyTransferModel();
                    money_transfer.Amount = 100.0f;
                    money_transfer.TargetAccountId = new System.Guid("99999999-9999-9999-9999-999999999999");
                    var result = money_controller.makeTransfer(new System.Guid(i.ToString().PadLeft(8, '0') + "-9999-9999-9999-999999999999"), money_transfer);
                    var obj_result = result.Result as ObjectResult;
                    Assert.IsNotNull(obj_result);
                    Assert.AreEqual(200, obj_result.StatusCode);
                }));
            }

            foreach (Thread t in list)
                t.Start();

        }
        [TestMethod]
        public void PerfTest3()
        {
            var mediator = new Mock<IMediator>();
            var account_controller = new BankAccountController(mediator.Object);
            account_controller.createBankAccount(new System.Guid("99999999-9999-9999-9999-999999999999"));
            List<Thread> list = new List<Thread>();

            for (int i = 0; i < 1000000; i++)
            {
                account_controller.createBankAccount(new System.Guid(i.ToString().PadLeft(8, '0') + "-9999-9999-9999-999999999999"));
                list.Append(new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    var money_controller = new MoneyTransferController(mediator.Object);
                    var money_transfer = new BankAccountService.Common.Models.MoneyTransferModel();
                    money_transfer.Amount = 100.0f;
                    money_transfer.TargetAccountId = new System.Guid("99999999-9999-9999-9999-999999999999");
                    var result = money_controller.makeTransfer(new System.Guid(i.ToString().PadLeft(8, '0') + "-9999-9999-9999-999999999999"), money_transfer);
                    var obj_result = result.Result as ObjectResult;
                    Assert.IsNotNull(obj_result);
                    Assert.AreEqual(200, obj_result.StatusCode);
                }));
            }

            foreach (Thread t in list)
                t.Start();

        }
    }
}
