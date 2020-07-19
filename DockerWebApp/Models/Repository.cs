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
            // Get the IP  
            // string myIP = GetIPAddress();

            List<Employee> model = null;
            await _ihttpclient.GetAsync("https://localhost:32768/api/Employee")
          .ContinueWith((taskwithresponse) =>
          {
              var response = taskwithresponse.Result;
              var jsonString = response.Content.ReadAsStringAsync();
              jsonString.Wait();
              model = JsonConvert.DeserializeObject<List<Employee>>(jsonString.Result);
          });

            return model;
        }

        public async Task<int> SaveEmployee(Employee employee)
        {
            var jsonInString = JsonConvert.SerializeObject(employee);

            var result = await _ihttpclient.PostAsync("https://localhost:32768/api/Employee", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            if (result != null)
            {
                return 1;
            }
            return 0;
        }
    }
}
