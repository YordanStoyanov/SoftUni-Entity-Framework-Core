using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main()
        {
            Models.SoftUniContext context = new Models.SoftUniContext();
            string result = GetEmployeesWithSalaryOver50000(context);
            Console.WriteLine(result);

            result = GetEmployeesFromResearchAndDevelopment(context);
            Console.WriteLine(result);

            result = AddNewAddressToEmployee(context);
            Console.WriteLine(result);

            result = GetEmployeesInPeriod(context);
            Console.WriteLine(result);

            result = GetEmployee147(context);
            Console.WriteLine(result);

            result = GetDepartmentsWithMoreThan5Employees(context);
            Console.WriteLine(result);
        }
        public static string GetEmployeesWithSalaryOver50000(Models.SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Employees.Where(e => e.Salary > 50000).OrderBy(e => e.FirstName).ToList();
            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(Models.SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Employees.
                Where(e => e.Department.Name == "Research and Development ").
                OrderBy(e => e.Salary).
                ThenByDescending(e => e.FirstName).
                ToList();
            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from Research and Development - ${employee.Salary:F2}");
            }
            return sb.ToString().TrimEnd();
        }

        public static string AddNewAddressToEmployee(Models.SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            Address newAddress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4,
            };

            Employee employeeNakov = context.Employees.First(e => e.LastName == "Nakov");
            context.Addresses.Add(newAddress);
            employeeNakov.Address = newAddress;
            context.SaveChanges();
            List<string> addresses = context.Employees.
                OrderByDescending(e => e.AddressId).
                Select(e => e.Address.AddressText).
                Take(10).
                ToList();

            foreach (var address in addresses)
            {
                sb.AppendLine(address);
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesInPeriod(Models.SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Employees.
                Where(e => e.EmployeesProjects.
                Any(ep => ep.Project.StartDate.Year > 2000 && ep.Project.StartDate.Year <= 2003)).
                Take(10).
                Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Project = e.EmployeesProjects.Select(ep => new
                    {
                        ProjectName = ep.Project.Name,
                        StartDate = ep.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                        EndDate = ep.Project.EndDate.HasValue ?
                        ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) : "not Finished"
                    }).ToList()
                }).ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} – Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");
                foreach (var project in employee.Project)
                {
                    sb.AppendLine($"--{project.ProjectName} - {project.StartDate} - {project.EndDate}");
                }
            }
            return sb.ToString().TrimEnd();
        }

        public static string GetEmployee147(Models.SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employee = context.Employees.Where(e => e.EmployeeId == 147).Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.JobTitle,
                Projects = e.EmployeesProjects.Select(ep => ep.Project.Name)
            });

            foreach (var e in employee)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetDepartmentsWithMoreThan5Employees(Models.SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var departments = context.Departments.Where(d => d.Employees.Count > 5).Select(d => new {
                d.Name,
                ManagerFirstName = d.Manager.FirstName,
                ManagerLastName = d.Manager.LastName,
                Employees = d.Employees.Select(e => new
                {
                    EmployeeFirstName = e.FirstName,
                    EmployeeLastName = e.LastName,
                    EmployeeJobTitle = e.JobTitle
                })
            });

            foreach (var department in departments)
            {
                sb.AppendLine($"{department.Name} – {department.ManagerFirstName} {department.ManagerLastName}");
                foreach (var employee in department.Employees)
                {
                    sb.AppendLine($"{employee.EmployeeFirstName} {employee.EmployeeLastName} - {employee.EmployeeJobTitle}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
