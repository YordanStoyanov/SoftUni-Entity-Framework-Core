using Microsoft.Data.SqlClient;
using System;

namespace AdoNetDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection sqlConnection = new SqlConnection("Server=.;Database=SoftUni;Integrated Security=true;"))
            {
                sqlConnection.Open();
                var command = "SELECT FirstName, LastName, Salary FROM [Employees]";
                var sqlCommnad = new SqlCommand(command, sqlConnection);
                using (SqlDataReader result = sqlCommnad.ExecuteReader())
                {
                    while (result.Read())
                    {
                        string firstName = (string)result["FirstName"];//(string)result[0]
                        string lastName = (string)result["LastName"];//(string)result[1]
                        decimal salary = (decimal)result["Salary"];
                        Console.WriteLine($"{firstName} {lastName} => {salary:F2}");
                    }
                }

                SqlCommand updateSalaryCommand = new SqlCommand(
                    "UPDATE Employees SET Salary = Salary * 1.10",
                    sqlConnection);
                updateSalaryCommand.ExecuteNonQuery();
                Console.WriteLine();
                command = "SELECT FirstName, LastName, Salary FROM [Employees]";
                sqlCommnad = new SqlCommand(command, sqlConnection);
                using (SqlDataReader result = sqlCommnad.ExecuteReader())
                {
                    while (result.Read())
                    {
                        string firstName = (string)result["FirstName"];//(string)result[0]
                        string lastName = (string)result["LastName"];//(string)result[1]
                        decimal salary = (decimal)result["Salary"];
                        Console.WriteLine($"{firstName} {lastName} => {salary:F2}");
                    }
                }
            }
        }
    }
}
