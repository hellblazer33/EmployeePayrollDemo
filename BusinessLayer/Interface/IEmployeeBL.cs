
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLayer.Models;

namespace BusinessLayer.Interface


{
    public interface IEmployeeBL
    {
        EmployeeModel AddEmployee(EmployeeModel employee);

        bool DeleteEmployee(int employeeId);

        List<EmployeeModel> GetAllEmployees();

        EmployeeModel GetemployeeByemployeeId(int employeeId);

        EmployeeModel UpdateBook(EmployeeModel employee);






    }
}
