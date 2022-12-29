namespace Employee.Controllers
{

    //using CommonLayer.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;
    using RepoLayer.Interface;
    //using RepositoryLayer.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CommonLayer.Models;
    using BusinessLayer;
    using BusinessLayer.Interface;

    /// <summary>
    ///  Book Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly EmployeeBL employeeBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public EmployeeController(IEmployeeBL employeeBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.employeeBL = (EmployeeBL)employeeBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }



        [HttpPost("post")]
        public IActionResult AddBook(EmployeeModel employee)
        {
            try
            {
                var bookDetail = this.employeeBL.AddEmployee(employee);
                if (bookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Employee Added Sucessfully", Response = bookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Some Error Occured" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }


        [HttpPut("Update")]
        public IActionResult UpdateEmployee(EmployeeModel employee)
        {
            try
            {
                var updatedEmployeeDetail = this.employeeBL.UpdateEmployee(employee);
                if (updatedEmployeeDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Employee Updated Sucessfully", Response = updatedEmployeeDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Some Error Occured" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }



        [HttpDelete("Delete")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            try
            {
                if (this.employeeBL.DeleteEmployee(employeeId))
                {
                    return this.Ok(new { Success = true, message = "Employee Deleted Sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Employee Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }


        //[Authorize(Roles = Role.User)]

        [HttpGet("{employeeId}/Get")]
        public IActionResult GetEmployeeByEmployeeId(int employeeId)
        {
            try
            {
                var updatedEmployeeDetail = this.employeeBL.GetemployeeByemployeeId(employeeId);
                if (updatedEmployeeDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Employee Detail Fetched Sucessfully", Response = updatedEmployeeDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Correct Employee Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }


        ////[Authorize(Roles = Role.User)]
        ////[Authorize]
        [HttpGet("redisGetAllEmployees")]
        public async Task<IActionResult> GetAllBooksUsingRedisCache()
        {
            var cacheKey = "EmployeeList";
            string serializedEmployeeList;
            var EmployeeList = new List<EmployeeModel>();
            var redisEmployeeList = await distributedCache.GetAsync(cacheKey);
            if (redisEmployeeList != null)
            {
                serializedEmployeeList = Encoding.UTF8.GetString(redisEmployeeList);
                EmployeeList = JsonConvert.DeserializeObject<List<EmployeeModel>>(serializedEmployeeList);
            }
            else
            {
                EmployeeList = (List<EmployeeModel>)employeeBL.GetAllEmployees();
                serializedEmployeeList = JsonConvert.SerializeObject(EmployeeList);
                redisEmployeeList = Encoding.UTF8.GetBytes(serializedEmployeeList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisEmployeeList, options);
            }
            return Ok(EmployeeList);
        }


    }

}