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
            await _ihttpclient.GetAsync($"http://{myIP}:{port}/api/Employee")
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

            var result = await _ihttpclient.PostAsync($"http://{myIP}:{port}/api/Employee", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            if (result != null)
            {
                return 1;
            }
            return 0;
        }
    }
}
