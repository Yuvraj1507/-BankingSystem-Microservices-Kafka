using BankAccountService.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccountService.Common.Models
{
    public class MoneyTransferModel
    {        
        public Guid SourceAccountId { get; set; }
        [Required]
        public Guid TargetAccountId { get; set; }
        [Required]
        public float Amount { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
