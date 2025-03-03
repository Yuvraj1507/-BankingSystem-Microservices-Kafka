using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupportAPI.Domain.Entities;

namespace SupportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public User Get()
        {
            return new User
            {
                login = "chris",
                password = "user",
                name = "Krzysztof",
                surname = "Swiecicki",
                birthdate = DateTime.Now
            };
        }

        [HttpPost]
        public User Post([FromBody] User user)
        {
            return user;
        }
    }
}
