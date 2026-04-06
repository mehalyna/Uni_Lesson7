using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Uni_Lesson7
{
    /// <summary>
    /// IEnumerable vs IQueryable - Understanding the differences
    /// </summary>
    public class IEnumerableVsIQueryable
    {
        public static void RunExamples()
        {
            Console.WriteLine("=== IEnumerable Vs IQueryable In LINQ ===\n");

            // Example 1: Basic Differences
            Example1_BasicDifferences();

            // Example 2: Execution Location
            Example2_ExecutionLocation();

            // Example 3: Expression Trees
            Example3_ExpressionTrees();

            // Example 4: Performance Comparison
            Example4_PerformanceComparison();

            // Example 5: When to Use What
            Example5_WhenToUseWhat();

            // Example 6: Real-World Scenarios
            Example6_RealWorldScenarios();
        }

        static void Example1_BasicDifferences()
        {
            Console.WriteLine("--- Example 1: Basic Differences ---\n");

            Console.WriteLine("IEnumerable<T>:");
            Console.WriteLine("  • Namespace: System.Collections.Generic");
            Console.WriteLine("  • Best for: In-memory collections (List, Array)");
            Console.WriteLine("  • Execution: Client-side (in application memory)");
            Console.WriteLine("  • Query: Not translated, uses delegates");
            Console.WriteLine("  • Deferred Execution: Yes");
            Console.WriteLine("  • Multiple Enumeration: Re-executes each time\n");

            Console.WriteLine("IQueryable<T>:");
            Console.WriteLine("  • Namespace: System.Linq");
            Console.WriteLine("  • Best for: Remote data sources (Database, OData)");
            Console.WriteLine("  • Execution: Server-side (on data source)");
            Console.WriteLine("  • Query: Translated to SQL/Other query language");
            Console.WriteLine("  • Deferred Execution: Yes");
            Console.WriteLine("  • Expression Trees: Uses expression trees");
            Console.WriteLine();

            // Code demonstration
            List<int> numbers = Enumerable.Range(1, 100).ToList();

            // IEnumerable - uses Func<T, bool>
            IEnumerable<int> enumerableQuery = numbers.Where(n => n > 50);
            Console.WriteLine($"IEnumerable query type: {enumerableQuery.GetType().Name}");

            // IQueryable - uses Expression<Func<T, bool>>
            IQueryable<int> queryableQuery = numbers.AsQueryable().Where(n => n > 50);
            Console.WriteLine($"IQueryable query type: {queryableQuery.GetType().Name}");
            Console.WriteLine();
        }

        static void Example2_ExecutionLocation()
        {
            Console.WriteLine("--- Example 2: Execution Location ---\n");

            var employees = GetEmployees();

            Console.WriteLine("IEnumerable - All data loaded, then filtered in memory:");
            IEnumerable<EmployeeData> enumerable = employees;
            var enumerableResult = enumerable.Where(e => e.Salary > 70000); // Filter in memory
            Console.WriteLine("  Query: WHERE Salary > 70000");
            Console.WriteLine("  Execution: ALL records loaded, THEN filtered in C#");
            Console.WriteLine($"  Results: {enumerableResult.Count()} employees\n");

            Console.WriteLine("IQueryable - Query translated and executed on data source:");
            IQueryable<EmployeeData> queryable = employees.AsQueryable();
            var queryableResult = queryable.Where(e => e.Salary > 70000); // Would be SQL WHERE
            Console.WriteLine("  Query: WHERE Salary > 70000");
            Console.WriteLine("  Execution: Translated to SQL, filtered on database");
            Console.WriteLine("  (If connected to DB: SELECT * FROM Employees WHERE Salary > 70000)");
            Console.WriteLine($"  Results: {queryableResult.Count()} employees\n");
        }

        static void Example3_ExpressionTrees()
        {
            Console.WriteLine("--- Example 3: Expression Trees ---\n");

            var numbers = Enumerable.Range(1, 10).ToList();

            // IEnumerable uses delegates (Func<T, bool>)
            Console.WriteLine("IEnumerable - Uses Delegates:");
            Func<int, bool> delegateFilter = n => n > 5;
            var enumResult = numbers.Where(delegateFilter);
            Console.WriteLine($"  Delegate type: {delegateFilter.GetType().Name}");
            Console.WriteLine($"  Cannot inspect: delegate body is compiled code");
            Console.WriteLine($"  Result: {string.Join(", ", enumResult)}\n");

            // IQueryable uses expression trees (Expression<Func<T, bool>>)
            Console.WriteLine("IQueryable - Uses Expression Trees:");
            System.Linq.Expressions.Expression<Func<int, bool>> expressionFilter = n => n > 5;
            var queryResult = numbers.AsQueryable().Where(expressionFilter);
            Console.WriteLine($"  Expression type: {expressionFilter.GetType().Name}");
            Console.WriteLine($"  Can inspect: Expression tree can be analyzed");
            Console.WriteLine($"  Expression: {expressionFilter}");
            Console.WriteLine($"  Body: {expressionFilter.Body}");
            Console.WriteLine($"  Result: {string.Join(", ", queryResult)}\n");
        }

        static void Example4_PerformanceComparison()
        {
            Console.WriteLine("--- Example 4: Performance Comparison ---\n");

            // Large dataset
            var largeList = Enumerable.Range(1, 1000000).ToList();

            // IEnumerable - all operations in memory
            Stopwatch sw = Stopwatch.StartNew();
            IEnumerable<int> enumerable = largeList;
            var enumResult = enumerable
                .Where(n => n > 100000)
                .Where(n => n < 200000)
                .Where(n => n % 2 == 0)
                .Take(100)
                .ToList();
            sw.Stop();
            Console.WriteLine($"IEnumerable approach: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"  All 1M records processed in memory");
            Console.WriteLine($"  Multiple Where clauses = multiple passes");
            Console.WriteLine($"  Results: {enumResult.Count} items\n");

            // IQueryable - optimized query
            sw = Stopwatch.StartNew();
            IQueryable<int> queryable = largeList.AsQueryable();
            var queryResult = queryable
                .Where(n => n > 100000)
                .Where(n => n < 200000)
                .Where(n => n % 2 == 0)
                .Take(100)
                .ToList();
            sw.Stop();
            Console.WriteLine($"IQueryable approach: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"  Query optimized before execution");
            Console.WriteLine($"  In real DB scenario: combined WHERE clause in SQL");
            Console.WriteLine($"  Results: {queryResult.Count} items\n");

            Console.WriteLine("Note: In database scenarios, IQueryable dramatically outperforms");
            Console.WriteLine("IEnumerable because filtering happens on the database server.\n");
        }

        static void Example5_WhenToUseWhat()
        {
            Console.WriteLine("--- Example 5: When to Use What ---\n");

            Console.WriteLine("Use IEnumerable<T> when:");
            Console.WriteLine("  ? Working with in-memory collections (List, Array)");
            Console.WriteLine("  ? Data is already loaded in memory");
            Console.WriteLine("  ? Using LINQ to Objects");
            Console.WriteLine("  ? Simple operations on small datasets");
            Console.WriteLine("  ? Need to use methods not translatable to SQL");
            Console.WriteLine("  Example:");
            
            List<string> names = new List<string> { "Alice", "Bob", "Charlie", "David" };
            var shortNames = names.Where(n => n.Length <= 5);
            Console.WriteLine($"    In-memory filter: {string.Join(", ", shortNames)}\n");

            Console.WriteLine("Use IQueryable<T> when:");
            Console.WriteLine("  ? Working with remote data sources (Databases)");
            Console.WriteLine("  ? Using Entity Framework, LINQ to SQL");
            Console.WriteLine("  ? Need server-side filtering and query optimization");
            Console.WriteLine("  ? Large datasets that shouldn't be loaded entirely");
            Console.WriteLine("  ? Building dynamic queries");
            Console.WriteLine("  Example (pseudo-code):");
            Console.WriteLine("    // dbContext.Employees.Where(e => e.Age > 30)");
            Console.WriteLine("    // Translates to: SELECT * FROM Employees WHERE Age > 30\n");
        }

        static void Example6_RealWorldScenarios()
        {
            Console.WriteLine("--- Example 6: Real-World Scenarios ---\n");

            var employees = GetEmployees();

            Console.WriteLine("Scenario 1: Report Generation (use IEnumerable)");
            Console.WriteLine("  Data already in memory, need complex processing:");
            IEnumerable<EmployeeData> memoryEmployees = employees;
            var report = memoryEmployees
                .Where(e => e.Department == "IT")
                .Select(e => new
                {
                    e.Name,
                    e.Salary,
                    Bonus = CalculateComplexBonus(e), // Complex calculation
                    FormattedSalary = FormatCurrency(e.Salary)
                })
                .ToList();
            
            Console.WriteLine($"  Generated report for {report.Count} IT employees");
            foreach (var item in report.Take(2))
            {
                Console.WriteLine($"    {item.Name}: {item.FormattedSalary} + ${item.Bonus} bonus");
            }
            Console.WriteLine();

            Console.WriteLine("Scenario 2: Database Query (use IQueryable)");
            Console.WriteLine("  Large table, need server-side filtering:");
            IQueryable<EmployeeData> dbEmployees = employees.AsQueryable();
            var dbQuery = dbEmployees
                .Where(e => e.Department == "IT")
                .Where(e => e.Salary > 70000)
                .OrderByDescending(e => e.Salary)
                .Take(5);
            
            Console.WriteLine("  SQL: SELECT TOP 5 * FROM Employees");
            Console.WriteLine("       WHERE Department = 'IT' AND Salary > 70000");
            Console.WriteLine("       ORDER BY Salary DESC");
            Console.WriteLine($"  Retrieved {dbQuery.Count()} employees (filtered on server)\n");

            Console.WriteLine("Scenario 3: Dynamic Filters (use IQueryable)");
            IQueryable<EmployeeData> dynamicQuery = employees.AsQueryable();
            
            string? filterDept = "IT";
            int? minSalary = 70000;
            
            if (!string.IsNullOrEmpty(filterDept))
                dynamicQuery = dynamicQuery.Where(e => e.Department == filterDept);
            
            if (minSalary.HasValue)
                dynamicQuery = dynamicQuery.Where(e => e.Salary > minSalary.Value);
            
            var dynamicResults = dynamicQuery.ToList();
            Console.WriteLine($"  Applied dynamic filters: Department={filterDept}, MinSalary={minSalary}");
            Console.WriteLine($"  Results: {dynamicResults.Count} employees\n");
        }

        // Helper methods
        static List<EmployeeData> GetEmployees()
        {
            return new List<EmployeeData>
            {
                new EmployeeData { Id = 1, Name = "Alice", Department = "IT", Salary = 75000 },
                new EmployeeData { Id = 2, Name = "Bob", Department = "IT", Salary = 82000 },
                new EmployeeData { Id = 3, Name = "Charlie", Department = "HR", Salary = 65000 },
                new EmployeeData { Id = 4, Name = "Diana", Department = "Finance", Salary = 78000 },
                new EmployeeData { Id = 5, Name = "Eve", Department = "IT", Salary = 71000 }
            };
        }

        static decimal CalculateComplexBonus(EmployeeData e)
        {
            // Simulated complex calculation that can't be translated to SQL
            return e.Salary * 0.1m + (e.Name.Length * 100);
        }

        static string FormatCurrency(int amount)
        {
            return $"${amount:N0}";
        }
    }

    public class EmployeeData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public int Salary { get; set; }
    }
}
