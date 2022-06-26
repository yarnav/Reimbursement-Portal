using Business_Layer.Repository;
using Data_Access_Layer.Data;
using Data_Access_Layer.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Presentation_Layer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository iAccountRepository;

        /// <summary>
        /// Constructor method to initialize IAccountRepository Interface's instance
        /// </summary>
        /// <param name="accountRepository"></param>
        public AccountController(IAccountRepository accountRepository)
        {
            iAccountRepository = accountRepository;
        }

        /// <summary>
        /// Calls CreateUser method for new User Registration
        /// </summary>
        /// <param name="employee">Contains details of the employee who is trying to register</param>
        /// <returns>Returns True if User is Registered successfully, else returns False</returns>
        [HttpPost]
        [Route("Signup")]
        public bool Signup([FromBody] EmployeeEntity employee)
        {
            if(iAccountRepository.CreateUser(employee))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Calls Login method for Signing in the employee
        /// </summary>
        /// <param name="employee">Contains details of the employee who is trying to SignIn</param>
        /// <returns>Returns employee details who logged in successfully, else returns null object</returns>
        [HttpPost]
        [Route("Login")]
        public async Task<EmployeeEntity> Login([FromBody] LoginEntity login)
        {
            var EmployeeData = new EmployeeEntity();
            EmployeeData = await iAccountRepository.Login(login);
            try
            {
                if (EmployeeData is null)
                {
                    EmployeeData = null;
                    return EmployeeData;
                }

                else
                    return EmployeeData;
            }
            catch(Exception exception)
            {
                EmployeeData = null;
                return EmployeeData;
            }
                
        }
    }
}
