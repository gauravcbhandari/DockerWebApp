using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerWebApp.Models
{
    public interface IRepository
    {
        Task<List<Employee>> GetAllEmployee();

        Task<int> SaveEmployee(Employee employee);
    }
}
