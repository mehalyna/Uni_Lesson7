using System;
using System.Linq;
using System.Collections.Generic;

namespace Uni_Lesson7
{
    /// <summary>
    /// LINQ Features in C# - Advanced capabilities and patterns
    /// </summary>
    public class LinqFeatures
    {
        public static void RunExamples()
        {
            Console.WriteLine("=== LINQ IN C# FEATURES ===\n");

            // Example 1: Lambda Expressions
            Example1_LambdaExpressions();

            // Example 2: Extension Methods
            Example2_ExtensionMethods();

            // Example 3: Anonymous Types
            Example3_AnonymousTypes();

            // Example 4: Type Inference (var)
            Example4_TypeInference();

            // Example 5: Query Composition
            Example5_QueryComposition();

            // Example 6: Let Clause
            Example6_LetClause();
        }

        static void Example1_LambdaExpressions()
        {
            Console.WriteLine("--- Example 1: Lambda Expressions ---");
            Console.WriteLine("Lambda expressions are inline anonymous functions\n");
            
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Single parameter lambda
            var evens = numbers.Where(n => n % 2 == 0);
            Console.WriteLine($"Even numbers: {string.Join(", ", evens)}");

            // Multiple parameters lambda
            var pairs = numbers.Select((value, index) => $"[{index}]={value}");
            Console.WriteLine($"Indexed: {string.Join(", ", pairs)}");

            // Expression body
            Func<int, int> square = x => x * x;
            Console.WriteLine($"Square of 5: {square(5)}");

            // Statement body
            Func<int, string> classify = x => 
            {
                if (x < 5) return "Low";
                if (x < 8) return "Medium";
                return "High";
            };
            Console.WriteLine($"Classification of 7: {classify(7)}");
            Console.WriteLine();
        }

        static void Example2_ExtensionMethods()
        {
            Console.WriteLine("--- Example 2: Extension Methods ---");
            Console.WriteLine("LINQ is built on extension methods for IEnumerable<T>\n");
            
            List<string> words = new List<string> { "apple", "banana", "cherry" };

            // Built-in extension methods
            Console.WriteLine("Built-in LINQ extensions:");
            Console.WriteLine($"  Count: {words.Count()}");
            Console.WriteLine($"  First: {words.First()}");
            Console.WriteLine($"  Any with 'e': {words.Any(w => w.Contains('e'))}");
            Console.WriteLine($"  All > 3 chars: {words.All(w => w.Length > 3)}");

            // Custom extension method usage
            Console.WriteLine("\nCustom extension method:");
            var result = words.CustomConcat();
            Console.WriteLine($"  CustomConcat: {result}");
            Console.WriteLine();
        }

        static void Example3_AnonymousTypes()
        {
            Console.WriteLine("--- Example 3: Anonymous Types ---");
            Console.WriteLine("Create types on the fly without explicit class definition\n");
            
            var products = new[]
            {
                new { Id = 1, Name = "Laptop", Price = 999.99, Category = "Electronics" },
                new { Id = 2, Name = "Desk", Price = 299.99, Category = "Furniture" },
                new { Id = 3, Name = "Mouse", Price = 29.99, Category = "Electronics" }
            };

            Console.WriteLine("Original products:");
            foreach (var product in products)
            {
                Console.WriteLine($"  {product.Name} - ${product.Price}");
            }

            // Project to new anonymous type
            var productSummary = products.Select(p => new 
            { 
                p.Name, 
                p.Category,
                PriceWithTax = p.Price * 1.1,
                IsExpensive = p.Price > 500
            });

            Console.WriteLine("\nProjected summary:");
            foreach (var item in productSummary)
            {
                Console.WriteLine($"  {item.Name} ({item.Category})");
                Console.WriteLine($"    Price with tax: ${item.PriceWithTax:F2}");
                Console.WriteLine($"    Expensive: {item.IsExpensive}");
            }
            Console.WriteLine();
        }

        static void Example4_TypeInference()
        {
            Console.WriteLine("--- Example 4: Type Inference (var) ---");
            Console.WriteLine("Compiler infers the type from the assigned value\n");
            
            // Explicit typing
            IEnumerable<int> explicitNumbers = new List<int> { 1, 2, 3 };
            Console.WriteLine($"Explicit type: {explicitNumbers.GetType().Name}");

            // Type inference with var
            var inferredNumbers = new List<int> { 1, 2, 3 };
            Console.WriteLine($"Inferred type: {inferredNumbers.GetType().Name}");

            // Essential for anonymous types
            var person = new { Name = "John", Age = 30 };
            Console.WriteLine($"\nAnonymous type (only possible with var):");
            Console.WriteLine($"  Name: {person.Name}, Age: {person.Age}");

            // Complex query result
            var query = from n in inferredNumbers
                       where n > 1
                       select new { Number = n, Square = n * n };
            
            Console.WriteLine($"\nQuery result type: {query.GetType().Name}");
            foreach (var item in query)
            {
                Console.WriteLine($"  {item.Number}² = {item.Square}");
            }
            Console.WriteLine();
        }

        static void Example5_QueryComposition()
        {
            Console.WriteLine("--- Example 5: Query Composition ---");
            Console.WriteLine("Building complex queries by composing simpler ones\n");
            
            var employees = new[]
            {
                new { Name = "Alice", Department = "IT", Salary = 75000, Experience = 5 },
                new { Name = "Bob", Department = "HR", Salary = 65000, Experience = 3 },
                new { Name = "Charlie", Department = "IT", Salary = 85000, Experience = 7 },
                new { Name = "Diana", Department = "Finance", Salary = 90000, Experience = 10 },
                new { Name = "Eve", Department = "IT", Salary = 70000, Experience = 4 }
            };

            // Step 1: Filter by department
            var itEmployees = employees.Where(e => e.Department == "IT");
            Console.WriteLine($"IT Employees: {itEmployees.Count()}");

            // Step 2: Add more filtering
            var seniorItEmployees = itEmployees.Where(e => e.Experience >= 5);
            Console.WriteLine($"Senior IT Employees: {seniorItEmployees.Count()}");

            // Step 3: Project and sort
            var result = seniorItEmployees
                .OrderByDescending(e => e.Salary)
                .Select(e => new { e.Name, e.Salary, e.Experience });

            Console.WriteLine("\nSenior IT Employees (sorted by salary):");
            foreach (var emp in result)
            {
                Console.WriteLine($"  {emp.Name}: ${emp.Salary:N0} ({emp.Experience} years)");
            }
            Console.WriteLine();
        }

        static void Example6_LetClause()
        {
            Console.WriteLine("--- Example 6: Let Clause ---");
            Console.WriteLine("Define intermediate variables in query syntax\n");
            
            var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Without let clause
            Console.WriteLine("Without let clause:");
            var withoutLet = from n in numbers
                           where n * n > 20
                           select new { Number = n, Square = n * n };

            foreach (var item in withoutLet)
            {
                Console.WriteLine($"  {item.Number}² = {item.Square}");
            }

            // With let clause (more efficient, calculates once)
            Console.WriteLine("\nWith let clause (better):");
            var withLet = from n in numbers
                         let square = n * n
                         where square > 20
                         select new { Number = n, Square = square };

            foreach (var item in withLet)
            {
                Console.WriteLine($"  {item.Number}² = {item.Square}");
            }

            // Complex let usage
            Console.WriteLine("\nComplex let clause example:");
            var textAnalysis = from word in new[] { "hello", "world", "linq", "csharp" }
                             let length = word.Length
                             let firstChar = word[0]
                             where length > 4
                             orderby length descending
                             select new { Word = word, Length = length, FirstChar = firstChar };

            foreach (var item in textAnalysis)
            {
                Console.WriteLine($"  '{item.Word}' - {item.Length} chars, starts with '{item.FirstChar}'");
            }
            Console.WriteLine();
        }
    }

    // Extension method example
    public static class CustomExtensions
    {
        public static string CustomConcat(this IEnumerable<string> source)
        {
            return string.Join(" | ", source);
        }
    }
}
