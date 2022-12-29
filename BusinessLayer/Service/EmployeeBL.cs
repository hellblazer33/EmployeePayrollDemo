namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using RepoLayer.Interface;


    public class EmployeeBL : IEmployeeBL
    {

        private readonly IEmployeeRL employeeRL;

        public EmployeeBL(IEmployeeRL employeeRL)
        {
            this.employeeRL = employeeRL;
        }


        public EmployeeModel AddEmployee(EmployeeModel employee)
        {
            try
            {
                return this.employeeRL.AddEmployee(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                return this.employeeRL.DeleteEmployee(employeeId);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<EmployeeModel> GetAllEmployees()
        {
            try
            {
                return this.employeeRL.GetAllEmployees();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public EmployeeModel GetemployeeByemployeeId(int employeeId)
        {
            try
            {
                return this.employeeRL.GetemployeeByemployeeId(employeeId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EmployeeModel UpdateBook(EmployeeModel employee)
        {
            throw new NotImplementedException();
        }

        public EmployeeModel UpdateEmployee(EmployeeModel employee)
        {
            try
            {
                return this.employeeRL.Updateemployee(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}