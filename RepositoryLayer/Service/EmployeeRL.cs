namespace RepoLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    //using CloudinaryDotNet;
    //using CloudinaryDotNet.Actions;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using MySql.Data.MySqlClient;
    using RepoLayer.Interface;

    /// <summary>
    ///  Service class for Interface 
    /// </summary>
    /// <seealso cref="RepoLayer.Interface.IEmployeeRL" />
    public class EmployeeRL : IEmployeeRL
    {

        private MySqlConnection sqlConnection;


        public EmployeeRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        private IConfiguration configuration { get; }


        public EmployeeModel AddEmployee(EmployeeModel employee)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.configuration["ConnectionStrings:EmployeePayroll"]);
                MySqlCommand cmd = new MySqlCommand("AddEmployee", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_employeeId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("_employeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("_salary", employee.Salary);
                cmd.Parameters.AddWithValue("_da", employee.DA);
                cmd.Parameters.AddWithValue("_hra", employee.HRA);
                cmd.Parameters.AddWithValue("_bonus", employee.Bonus);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return employee;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }


        public EmployeeModel Updateemployee(EmployeeModel employee)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.configuration["ConnectionStrings:EmployeePayroll"]);
                MySqlCommand cmd = new MySqlCommand("UpdateEmployee", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_employeeId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("_employeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("_salary", employee.Salary);
                cmd.Parameters.AddWithValue("_da", employee.DA);
                cmd.Parameters.AddWithValue("_hra", employee.HRA);
                cmd.Parameters.AddWithValue("_bonus", employee.Bonus);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return employee;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }


        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.configuration["ConnectionStrings:EmployeePayroll"]);
                MySqlCommand cmd = new MySqlCommand("DeleteEmployee", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_employeeId", employeeId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }


        public EmployeeModel GetemployeeByemployeeId(int employeeId)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.configuration["ConnectionStrings:EmployeePayroll"]);
                MySqlCommand cmd = new MySqlCommand("GetemployeeByemployeeId", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_employeeId", employeeId);
                this.sqlConnection.Open();
                EmployeeModel employeeModel = new EmployeeModel();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employeeModel.EmployeeId = Convert.ToInt32(reader["employeeId"]);
                        employeeModel.EmployeeName = reader["employeeName"].ToString();
                        employeeModel.Salary = Convert.ToInt32(reader["salary"]);
                        employeeModel.HRA = Convert.ToInt32(reader["hra"]);
                        employeeModel.DA = Convert.ToInt32(reader["da"]);
                        employeeModel.Bonus = Convert.ToInt32(reader["bonus"]);
                    }

                    this.sqlConnection.Close();
                    return employeeModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }


        public List<EmployeeModel> GetAllEmployees()
        {
            try
            {
                List<EmployeeModel> employee = new List<EmployeeModel>();
                this.sqlConnection = new MySqlConnection(this.configuration["ConnectionStrings:EmployeePayroll"]);
                MySqlCommand cmd = new MySqlCommand("GetAllEmployees", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                this.sqlConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employee.Add(new EmployeeModel
                        {
                            EmployeeId = Convert.ToInt32(reader["employeeId"]),
                            EmployeeName = reader["employeeName"].ToString(),
                            Salary = Convert.ToInt32(reader["salary"]),
                            HRA = Convert.ToInt32(reader["hra"]),
                            DA = Convert.ToInt32(reader["da"]),
                            Bonus = Convert.ToInt32(reader["bonus"]),
                            
                        });
                    }

                    this.sqlConnection.Close();
                    return employee;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

       
    }
}