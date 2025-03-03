using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SupportAPI.Domain.Entities
{
    public class User
    {
        public String login { get; set; }
        public String password { get; set; }
        public String name { get; set; }
        public String surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime birthdate { get; set; }
    }
}
