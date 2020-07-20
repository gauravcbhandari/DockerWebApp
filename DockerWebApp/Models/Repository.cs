using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DockerWebApp.Models
{
    public class Repository : IRepository
    {

        private HttpClient _ihttpclient;

        private readonly string myIP;
        private readonly string port;

        public Repository(HttpClient httpClient)
        {
            _ihttpclient = httpClient;

            myIP = Environment.GetEnvironmentVariable("SERVICE-ADRESS");
            port = Environment.GetEnvironmentVariable("SERVICE-PORT");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> GetAllEmployee()
        {
            List<Employee> model = null;
            //await _ihttpclient.GetAsync("https://host.docker.internal:32768/api/Employee")
            //.ContinueWith((taskwithresponse) =>
            //{
            //    var response = taskwithresponse.Result;
            //    var jsonString = response.Content.ReadAsStringAsync();
            //    jsonString.Wait();
            //    model = JsonConvert.DeserializeObject<List<Employee>>(jsonString.Result);
            //});

            using (var client = new HttpClient())
            {
                var request = new System.Net.Http.HttpRequestMessage();
                request.RequestUri = new Uri("https://localhost:44366/api/Employee"); // ASP.NET 2.x
                var response = await client.SendAsync(request);
                var jsonString = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<List<Employee>>(jsonString);
            }

            return model;
        }

        public async Task<int> SaveEmployee(Employee employee)
        {
            var jsonInString = JsonConvert.SerializeObject(employee);

            using (var client = new HttpClient())
            {
                var request = new System.Net.Http.HttpRequestMessage();
                request.RequestUri = new Uri("https://localhost:44366/api/Employee"); // ASP.NET 2.x
                var response = await client.PostAsync(request.RequestUri, new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                if (response != null)
                {
                    return 1;
                }
                return 0;
            }

            //var result = await _ihttpclient.PostAsync($"http://{myIP}:{port}/api/Employee", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

        }
    }
}
