using System;
using System.Linq;
using System.Text;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();
            Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));
        }
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employeeInformation = context.Employees
                .Select(x => new
                {
                    x.EmployeeId,
                    x.FirstName,
                    x.MiddleName,
                    x.LastName,
                    x.JobTitle,
                    x.Salary
                })
                .OrderBy(x => x.EmployeeId)
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (var employee in employeeInformation)
            {

                output.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }

            return output.ToString().Trim();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employeeSalary = context.Employees
                .Select(x => new
                {
                    x.FirstName,
                    x.Salary
                })
                .Where(x => x.Salary > 50000)
                .OrderBy(x => x.FirstName)
                .ToArray();

            StringBuilder output = new StringBuilder();

            foreach (var employee in employeeSalary)
            {
                output.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }

            return output.ToString().TrimEnd();
        }
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    DepartmentName = x.Department.Name,
                    x.Salary
                })
                .Where(x => x.DepartmentName == "Research and Development")
                .OrderBy(x => x.Salary)
                .ThenByDescending(x => x.FirstName)
                .ToArray(); StringBuilder output = new StringBuilder();

            foreach (var employee in employees)
            {
                output.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - ${employee.Salary:f2}");
            }

            return output.ToString().TrimEnd();
        }
    }
}
