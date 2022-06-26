using Data_Access_Layer.Data;
using Data_Access_Layer.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repository
{
    public class AccountRepository : IAccountRepository

    /// <summary>
    /// All Methods related to User and Account
    /// </summary>
    /// <params name=""></params>
    {
        public ReimbursementContext _dbContext;

        /// <summary>
        /// Constructor method to initialize the Context.
        /// </summary>
        /// <param name="context">This is the context we created</param>
        public AccountRepository(ReimbursementContext context)
        {
            _dbContext = context;

        }

        /// <summary>
        /// This method will be called everytime a user try to register.
        /// </summary>
        /// <param name="employeeDetails"></param>
        /// <returns>Returns True if Account is created Successfully, else it will return false</returns>
        public bool CreateUser(EmployeeEntity employeeDetails)
        {
            try
            {
                var Check = _dbContext.Employee.Where(x => x.Email == employeeDetails.Email);
                if(Check!=null)
                {
                    return false;
                }
                else
                {
                    var employee = new EmployeeEntity()
                    {
                        Email = employeeDetails.Email,
                        Password = employeeDetails.Password,
                        Name = employeeDetails.Name,
                        PAN = employeeDetails.PAN,
                        Bank = employeeDetails.Bank,
                        AccountNumber = employeeDetails.AccountNumber

                    };
                    if (String.Compare(employeeDetails.Email, "Admin@gmail.com") == 0)
                    {
                        employee.isApprover = true;
                    }
                    _dbContext.Employee.Add(employee);
                    _dbContext.SaveChanges();
                    return true;
                }
                
            }
            catch(Exception exception)
            {
                return false;
            }
            }            

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Returns Employee Details who logged in successfully, Else Returns null object</returns>
        public async Task<EmployeeEntity> Login(LoginEntity login)
        {
            var EmployeeData = new EmployeeEntity();
            try
            {
                var Record = _dbContext.Employee.Where(x => x.Email == login.Email && x.Password == login.Password).First();                
                if (Record is null)
                {
                    EmployeeData = null;
                    return EmployeeData;
                }
                else
                {
                    EmployeeData = _dbContext.Employee.Where(x => x.Email == login.Email).First();
                    return EmployeeData;
                }
            }
            catch(Exception exception)
            {
                EmployeeData = null;
                return EmployeeData;
            } 
        }
    }
}
