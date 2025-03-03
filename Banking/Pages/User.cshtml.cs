using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Banking.Pages
{
    public class UserModel : PageModel
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

        public void OnGet()
        {
            ViewData["Message"] = "Add user";
        }

        public async Task OnPost([FromForm] User user)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri("http://supportapi/api/User");
                var serialized = JsonSerializer.Serialize(user);
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);
                ViewData["Message"] = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
