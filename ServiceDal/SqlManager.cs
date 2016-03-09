using ServiceModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ServiceDal
{
    public class SqlManager
    {
        private static string connString = String.Empty;


        static SqlManager()
        {
           // SqlManager.connString = @"Data Source=SVR-DEV-BD1.TESFRI.INTRA\WTESFRI_SQL2008;Initial Catalog=TEST-SofricaCIE9-2;Persist Security Info=False;User ID=Tesfri_client;Password=tesfri;Connect Timeout=5; MAX POOL SIZE=500;POOLING=TRUE";
            SqlManager.connString = @"Data Source=192.168.7.72\STONER;Initial Catalog=AdventureWorks2012;Persist Security Info=False;User ID=team_project;Password=K@r@m@z0v;Connect Timeout=5; MAX POOL SIZE=500;POOLING=TRUE"; 
        }


        public static Employee GetEmployeeDetails(int employeeId)
        {
            Employee employee = null;

            string sqlCommand = "SELECT BusinessEntityID, Title, FirstName, MiddleName, LastName FROM Person.Person WHERE BusinessEntityID=@employeeId";

            using (SqlConnection connection = new SqlConnection(SqlManager.connString))
            {
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.Add("@employeeId", SqlDbType.Int);
                    command.Parameters["@employeeId"] = new SqlParameter("@employeeId", employeeId);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        employee = new Employee
                        {
                            LastName = reader["LastName"] as String,
                            MiddleName = reader["MiddleName"] as String,
                            FirstName = reader["FirstName"] as String,
                            Title = reader["Title"] as String,
                            BusinessEntityID = Convert.ToInt32(reader["BusinessEntityID"])
                        };
                    }
                    reader.Close();
                }
            }
            return employee;
        }

        public static void UpdateEmployeeName(int employeeId, string employeeName)
        {
            string sqlCommand = "update Person.Person set LastName='@LastName' WHERE BusinessEntityID=@BusinessEntityID";

            using (SqlConnection connection = new SqlConnection(SqlManager.connString))
            {
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.Add("@BusinessEntityID", SqlDbType.VarChar);
                    command.Parameters["@BusinessEntityID"] = new SqlParameter("@BusinessEntityID", employeeId);
                    command.Parameters.Add("@LastName", SqlDbType.VarChar);
                    command.Parameters["@LastName"] = new SqlParameter("@LastName", employeeName);
                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<Employee> GetAllEmployeesDetails()
        {

            List<Employee> lstAllEmployees = new List<Employee>();
            Employee employee = null;

            string sqlCommand = "SELECT BusinessEntityID, Title, FirstName, MiddleName, LastName FROM Person.Person";

            using (SqlConnection connection = new SqlConnection(SqlManager.connString))
            {
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {                  
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        employee = new Employee
                        {
                            LastName = reader["LastName"] as String,
                            MiddleName = reader["MiddleName"] as String,
                            FirstName = reader["FirstName"] as String,
                            Title = reader["Title"] as String,
                            BusinessEntityID = Convert.ToInt32(reader["BusinessEntityID"])
                        };

                        lstAllEmployees.Add(employee);
                    }
                    reader.Close();
                }
            }
            return lstAllEmployees;
        }
    }
}
