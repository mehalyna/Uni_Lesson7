using System;
using System.Linq;
using System.Collections.Generic;

namespace Uni_Lesson7
{
    /// <summary>
    /// Comprehensive LINQ Samples - Practical real-world examples
    /// </summary>
    public class LinqSamples
    {
        public static void RunExamples()
        {
            Console.WriteLine("=== LINQ SAMPLES ===\n");

            // Example 1: Filtering Data
            Example1_Filtering();

            // Example 2: Projection (Select)
            Example2_Projection();

            // Example 3: Sorting
            Example3_Sorting();

            // Example 4: Grouping
            Example4_Grouping();

            // Example 5: Joining
            Example5_Joining();

            // Example 6: Aggregation
            Example6_Aggregation();

            // Example 7: Set Operations
            Example7_SetOperations();

            // Example 8: Complex Query Example
            Example8_ComplexQuery();
        }

        static void Example1_Filtering()
        {
            Console.WriteLine("--- Example 1: Filtering Data (Where) ---");
            
            var employees = GetSampleEmployees();

            // Single condition
            var highEarners = employees.Where(e => e.Salary > 75000);
            Console.WriteLine("Employees earning > $75,000:");
            foreach (var emp in highEarners)
            {
                Console.WriteLine($"  {emp.Name}: ${emp.Salary:N0}");
            }

            // Multiple conditions
            var itManagers = employees.Where(e => e.Department == "IT" && e.Position == "Manager");
            Console.WriteLine("\nIT Managers:");
            foreach (var emp in itManagers)
            {
                Console.WriteLine($"  {emp.Name}");
            }

            // Using OfType for type filtering
            object[] mixed = { 1, "hello", 2, "world", 3, 4.5 };
            var onlyIntegers = mixed.OfType<int>();
            Console.WriteLine($"\nIntegers from mixed array: {string.Join(", ", onlyIntegers)}");
            Console.WriteLine();
        }

        static void Example2_Projection()
        {
            Console.WriteLine("--- Example 2: Projection (Select) ---");
            
            var employees = GetSampleEmployees();

            // Simple projection
            var names = employees.Select(e => e.Name);
            Console.WriteLine($"Employee names: {string.Join(", ", names)}");

            // Project to anonymous type
            var summary = employees.Select(e => new 
            { 
                e.Name, 
                e.Department,
                AnnualBonus = e.Salary * 0.1
            });

            Console.WriteLine("\nEmployee summaries:");
            foreach (var item in summary)
            {
                Console.WriteLine($"  {item.Name} ({item.Department}) - Bonus: ${item.AnnualBonus:N0}");
            }

            // SelectMany (flatten collections)
            var teams = new[]
            {
                new { TeamName = "Alpha", Members = new[] { "Alice", "Bob" } },
                new { TeamName = "Beta", Members = new[] { "Charlie", "Diana", "Eve" } }
            };

            var allMembers = teams.SelectMany(t => t.Members);
            Console.WriteLine($"\nAll team members: {string.Join(", ", allMembers)}");
            Console.WriteLine();
        }

        static void Example3_Sorting()
        {
            Console.WriteLine("--- Example 3: Sorting (OrderBy, ThenBy) ---");
            
            var employees = GetSampleEmployees();

            // Single sort
            var bySalary = employees.OrderByDescending(e => e.Salary);
            Console.WriteLine("Sorted by salary (descending):");
            foreach (var emp in bySalary.Take(3))
            {
                Console.WriteLine($"  {emp.Name}: ${emp.Salary:N0}");
            }

            // Multiple sorts
            var sorted = employees
                .OrderBy(e => e.Department)
                .ThenByDescending(e => e.Salary)
                .ThenBy(e => e.Name);

            Console.WriteLine("\nSorted by Dept (asc), then Salary (desc), then Name (asc):");
            foreach (var emp in sorted)
            {
                Console.WriteLine($"  {emp.Department,-12} {emp.Name,-15} ${emp.Salary:N0}");
            }
            Console.WriteLine();
        }

        static void Example4_Grouping()
        {
            Console.WriteLine("--- Example 4: Grouping (GroupBy) ---");
            
            var employees = GetSampleEmployees();

            // Group by single key
            var byDepartment = employees.GroupBy(e => e.Department);
            
            Console.WriteLine("Employees by Department:");
            foreach (var group in byDepartment)
            {
                Console.WriteLine($"\n  {group.Key}:");
                foreach (var emp in group)
                {
                    Console.WriteLine($"    - {emp.Name} ({emp.Position})");
                }
            }

            // Group with aggregation
            Console.WriteLine("\nDepartment Statistics:");
            var stats = employees.GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    Count = g.Count(),
                    AvgSalary = g.Average(e => e.Salary),
                    TotalSalary = g.Sum(e => e.Salary)
                });

            foreach (var stat in stats)
            {
                Console.WriteLine($"  {stat.Department}:");
                Console.WriteLine($"    Employees: {stat.Count}");
                Console.WriteLine($"    Avg Salary: ${stat.AvgSalary:N0}");
                Console.WriteLine($"    Total: ${stat.TotalSalary:N0}");
            }
            Console.WriteLine();
        }

        static void Example5_Joining()
        {
            Console.WriteLine("--- Example 5: Joining Data ---");
            
            var employees = GetSampleEmployees();
            var departments = new[]
            {
                new { Name = "IT", Budget = 500000, Manager = "John Smith" },
                new { Name = "HR", Budget = 200000, Manager = "Jane Doe" },
                new { Name = "Finance", Budget = 300000, Manager = "Bob Johnson" }
            };

            // Inner join
            var employeeDetails = from emp in employees
                                join dept in departments on emp.Department equals dept.Name
                                select new
                                {
                                    emp.Name,
                                    emp.Position,
                                    Department = dept.Name,
                                    DeptBudget = dept.Budget,
                                    DeptManager = dept.Manager
                                };

            Console.WriteLine("Employee details with department info:");
            foreach (var detail in employeeDetails.Take(5))
            {
                Console.WriteLine($"  {detail.Name} - {detail.Position}");
                Console.WriteLine($"    Dept: {detail.Department} (Manager: {detail.DeptManager})");
            }
            Console.WriteLine();
        }

        static void Example6_Aggregation()
        {
            Console.WriteLine("--- Example 6: Aggregation Functions ---");
            
            var employees = GetSampleEmployees();
            var salaries = employees.Select(e => e.Salary).ToList();

            Console.WriteLine($"Total Employees: {employees.Count()}");
            Console.WriteLine($"Total Salary Budget: ${employees.Sum(e => e.Salary):N0}");
            Console.WriteLine($"Average Salary: ${employees.Average(e => e.Salary):N0}");
            Console.WriteLine($"Highest Salary: ${employees.Max(e => e.Salary):N0}");
            Console.WriteLine($"Lowest Salary: ${employees.Min(e => e.Salary):N0}");

            // Aggregate with custom logic
            var salaryRange = salaries.Aggregate((min: int.MaxValue, max: int.MinValue),
                (acc, salary) => (Math.Min(acc.min, salary), Math.Max(acc.max, salary)));
            
            Console.WriteLine($"\nSalary Range: ${salaryRange.min:N0} - ${salaryRange.max:N0}");

            // Custom aggregation
            var concatenated = employees
                .Take(3)
                .Select(e => e.Name)
                .Aggregate((current, next) => $"{current}, {next}");
            Console.WriteLine($"\nFirst 3 names concatenated: {concatenated}");
            Console.WriteLine();
        }

        static void Example7_SetOperations()
        {
            Console.WriteLine("--- Example 7: Set Operations ---");
            
            int[] numbers1 = { 1, 2, 3, 4, 5 };
            int[] numbers2 = { 4, 5, 6, 7, 8 };

            // Distinct
            int[] duplicates = { 1, 2, 2, 3, 3, 3, 4, 5 };
            var unique = duplicates.Distinct();
            Console.WriteLine($"Distinct: {string.Join(", ", unique)}");

            // Union
            var union = numbers1.Union(numbers2);
            Console.WriteLine($"Union: {string.Join(", ", union)}");

            // Intersect
            var intersect = numbers1.Intersect(numbers2);
            Console.WriteLine($"Intersect: {string.Join(", ", intersect)}");

            // Except
            var except = numbers1.Except(numbers2);
            Console.WriteLine($"Except (1 - 2): {string.Join(", ", except)}");
            Console.WriteLine();
        }

        static void Example8_ComplexQuery()
        {
            Console.WriteLine("--- Example 8: Complex Real-World Query ---");
            Console.WriteLine("Find top 3 departments by average salary, with senior employees\n");
            
            var employees = GetSampleEmployees();

            var result = employees
                .Where(e => e.YearsOfExperience >= 5) // Senior employees only
                .GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    EmployeeCount = g.Count(),
                    AverageSalary = g.Average(e => e.Salary),
                    TopEarner = g.OrderByDescending(e => e.Salary).First().Name,
                    TopEarnerSalary = g.Max(e => e.Salary)
                })
                .OrderByDescending(d => d.AverageSalary)
                .Take(3);

            foreach (var dept in result)
            {
                Console.WriteLine($"{dept.Department} Department:");
                Console.WriteLine($"  Senior Employees: {dept.EmployeeCount}");
                Console.WriteLine($"  Average Salary: ${dept.AverageSalary:N0}");
                Console.WriteLine($"  Top Earner: {dept.TopEarner} (${dept.TopEarnerSalary:N0})");
                Console.WriteLine();
            }
        }

        // Helper method to generate sample data
        static List<Employee> GetSampleEmployees()
        {
            return new List<Employee>
            {
                new Employee { Id = 1, Name = "Alice Johnson", Department = "IT", Position = "Developer", Salary = 75000, YearsOfExperience = 5 },
                new Employee { Id = 2, Name = "Bob Smith", Department = "IT", Position = "Manager", Salary = 95000, YearsOfExperience = 8 },
                new Employee { Id = 3, Name = "Charlie Brown", Department = "HR", Position = "Recruiter", Salary = 55000, YearsOfExperience = 3 },
                new Employee { Id = 4, Name = "Diana Prince", Department = "Finance", Position = "Analyst", Salary = 70000, YearsOfExperience = 6 },
                new Employee { Id = 5, Name = "Eve Davis", Department = "IT", Position = "Developer", Salary = 72000, YearsOfExperience = 4 },
                new Employee { Id = 6, Name = "Frank Miller", Department = "Finance", Position = "Manager", Salary = 90000, YearsOfExperience = 10 },
                new Employee { Id = 7, Name = "Grace Lee", Department = "HR", Position = "Manager", Salary = 80000, YearsOfExperience = 7 },
                new Employee { Id = 8, Name = "Henry Wilson", Department = "IT", Position = "Developer", Salary = 78000, YearsOfExperience = 6 }
            };
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int Salary { get; set; }
        public int YearsOfExperience { get; set; }
    }
}
