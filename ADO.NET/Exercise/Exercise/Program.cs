using Microsoft.Data.SqlClient;
using System;

namespace Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            using SqlConnection sqlConnection = new SqlConnection(
                @"Server=.;Database=MinionsDB;Integrated Security=true");
            {
                sqlConnection.Open();
                string command = @"SELECT [Name] FROM Villains";
                using SqlCommand reader = new SqlCommand(command, sqlConnection);
                {
                    var result = reader.ExecuteReader();
                    using (result)
                    {
                        while (result.Read())
                        {
                            string firstName = (string)result[0];
                            Console.WriteLine(firstName);
                        }
                    }
                        
                }
                Console.WriteLine();
                using SqlCommand dataFromTowns = new SqlCommand("SELECT [Name] FROM Towns", sqlConnection);
                {
                    var result2 = dataFromTowns.ExecuteReader();
                    using (result2)
                    {
                        while (result2.Read())
                        {
                            string townName = (string)result2[0];
                            Console.WriteLine(townName);
                        }
                    }
                }

                Console.WriteLine();
                using SqlCommand dataFromCountries = new SqlCommand("SELECT [Name] FROM Countries", sqlConnection);
                {
                    var result3 = dataFromCountries.ExecuteReader();
                    using (result3)
                    {
                        while (result3.Read())
                        {
                            var countriesName = (string)result3[0];
                            Console.WriteLine(countriesName);
                        }
                    }
                }
            }
        }
    }
}
