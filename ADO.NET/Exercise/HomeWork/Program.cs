using Microsoft.Data.SqlClient;
using System;

namespace HomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            using SqlConnection sqlConnection = new SqlConnection(
                @"Server=.;Database=MinionsDB;Integrated Security=true");
            {
                sqlConnection.Open();
                string minionsAge = @"SELECT [Name], [Age] FROM Minions ORDER BY [Name] ASC";
                using SqlCommand reader = new SqlCommand(minionsAge, sqlConnection);
                {
                    var result = reader.ExecuteReader();
                    using (result)
                    {
                        while (result.Read())
                        {
                            var name = (string)result[0];
                            var age = (int)result[1];
                            Console.WriteLine(name + " " + age);
                        }
                    }
                }
            }
        }
    }
}
