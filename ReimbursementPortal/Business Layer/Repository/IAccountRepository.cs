using Data_Access_Layer.Data;
using Data_Access_Layer.Entity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_Layer.Repository
{
    public interface IAccountRepository
    {
        bool CreateUser(EmployeeEntity employeeDetails);
        Task<EmployeeEntity> Login(LoginEntity login);
    }
}