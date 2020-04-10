using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FinCrmAPICommunication
{
    public class LoginAPI
    {

        class Person
        {
            public string Email { get; set; }
            public string Password { get; set; }

            public override string ToString()
            {
                return $"{Email}: {Password}";
            }
        }

        public async Task<string> LoginProcess(string email, string password)
            {

            HttpClient client = new HttpClient();

            var person = new Person();
            person.Email = email;
            person.Password = password;

            var json = JsonConvert.SerializeObject(person);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://api.fincrm.pl/api/Account/authenticate";

            var response = await client.PostAsync(url, data);
            bool isLoginSuccess = response.IsSuccessStatusCode;
            string result = response.Content.ReadAsStringAsync().Result;
           
            if (isLoginSuccess)
            {
                return result;
            }
            else
            {
                return "NOT";
            }

            
        }
    }
}
