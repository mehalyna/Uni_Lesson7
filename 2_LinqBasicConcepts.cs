using System;
using System.Linq;
using System.Collections.Generic;

namespace Uni_Lesson7
{
    /// <summary>
    /// LINQ Basic Concepts - Language Integrated Query
    /// LINQ provides a consistent model for working with data across various data sources
    /// </summary>
    public class LinqBasicConcepts
    {
        public static void RunExamples()
        {
            Console.WriteLine("=== WHAT IS LINQ? BASIC CONCEPTS ===\n");

            // Example 1: What is LINQ?
            Example1_WhatIsLinq();

            // Example 2: Query Syntax vs Method Syntax
            Example2_QueryVsMethodSyntax();

            // Example 3: Deferred Execution
            Example3_DeferredExecution();

            // Example 4: Immediate Execution
            Example4_ImmediateExecution();

            // Example 5: LINQ to Objects
            Example5_LinqToObjects();
        }

        static void Example1_WhatIsLinq()
        {
            Console.WriteLine("--- Example 1: What is LINQ? ---");
            Console.WriteLine("LINQ (Language Integrated Query) is a set of features that extends");
            Console.WriteLine("powerful query capabilities to C# language syntax.\n");
            
            Console.WriteLine("Key Features:");
            Console.WriteLine("  • Unified syntax for querying different data sources");
            Console.WriteLine("  • Strongly typed queries with IntelliSense support");
            Console.WriteLine("  • Compile-time checking");
            Console.WriteLine("  • Improved code readability\n");

            // Simple demonstration
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            Console.WriteLine("Without LINQ:");
            List<int> evenNumbers = new List<int>();
            foreach (int num in numbers)
            {
                if (num % 2 == 0)
                    evenNumbers.Add(num);
            }
            Console.WriteLine($"Even numbers: {string.Join(", ", evenNumbers)}");
            
            Console.WriteLine("\nWith LINQ:");
            var evenNumbersLinq = numbers.Where(n => n % 2 == 0);
            Console.WriteLine($"Even numbers: {string.Join(", ", evenNumbersLinq)}");
            Console.WriteLine();
        }

        static void Example2_QueryVsMethodSyntax()
        {
            Console.WriteLine("--- Example 2: Query Syntax vs Method Syntax ---");
            
            List<string> fruits = new List<string> 
            { 
                "Apple", "Banana", "Cherry", "Date", "Elderberry", "Fig", "Grape" 
            };

            // Query Syntax (SQL-like)
            Console.WriteLine("Query Syntax:");
            var queryResult = from fruit in fruits
                            where fruit.Length > 5
                            orderby fruit
                            select fruit.ToUpper();
            
            Console.WriteLine($"  {string.Join(", ", queryResult)}");

            // Method Syntax (Extension methods with lambda)
            Console.WriteLine("\nMethod Syntax:");
            var methodResult = fruits
                .Where(fruit => fruit.Length > 5)
                .OrderBy(fruit => fruit)
                .Select(fruit => fruit.ToUpper());
            
            Console.WriteLine($"  {string.Join(", ", methodResult)}");
            Console.WriteLine("\nBoth produce the same result!\n");
        }

        static void Example3_DeferredExecution()
        {
            Console.WriteLine("--- Example 3: Deferred Execution ---");
            Console.WriteLine("Query is not executed until you iterate over results\n");
            
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            
            Console.WriteLine("Creating query...");
            var query = numbers.Where(n => n > 2); // Query defined but NOT executed
            
            Console.WriteLine("Modifying source data...");
            numbers.Add(6);
            numbers.Add(7);
            
            Console.WriteLine("Executing query (enumerating)...");
            Console.WriteLine($"Results: {string.Join(", ", query)}"); // Query executed NOW
            
            Console.WriteLine("\nNotice: Query includes 6 and 7 that were added AFTER query definition");
            Console.WriteLine("This demonstrates deferred execution.\n");
        }

        static void Example4_ImmediateExecution()
        {
            Console.WriteLine("--- Example 4: Immediate Execution ---");
            Console.WriteLine("Using ToList(), ToArray(), Count(), etc. forces immediate execution\n");
            
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            
            Console.WriteLine("Creating query with ToList() - immediate execution:");
            var list = numbers.Where(n => n > 2).ToList(); // Executed immediately
            
            Console.WriteLine($"Initial results: {string.Join(", ", list)}");
            
            Console.WriteLine("Modifying source data...");
            numbers.Add(6);
            numbers.Add(7);
            
            Console.WriteLine("Checking results again:");
            Console.WriteLine($"Results: {string.Join(", ", list)}"); // Still the same
            
            Console.WriteLine("\nNotice: Results don't include 6 and 7 - query was executed earlier.\n");
        }

        static void Example5_LinqToObjects()
        {
            Console.WriteLine("--- Example 5: LINQ to Objects ---");
            Console.WriteLine("Querying in-memory collections like arrays, lists, etc.\n");
            
            // Sample data
            var students = new[]
            {
                new { Name = "Alice", Grade = 85, Subject = "Math" },
                new { Name = "Bob", Grade = 92, Subject = "Science" },
                new { Name = "Charlie", Grade = 78, Subject = "Math" },
                new { Name = "Diana", Grade = 95, Subject = "Science" },
                new { Name = "Eve", Grade = 88, Subject = "Math" }
            };

            // Complex query
            var topMathStudents = from student in students
                                where student.Subject == "Math" && student.Grade >= 85
                                orderby student.Grade descending
                                select new { student.Name, student.Grade };

            Console.WriteLine("Top Math Students (Grade >= 85):");
            foreach (var student in topMathStudents)
            {
                Console.WriteLine($"  {student.Name}: {student.Grade}");
            }

            // Aggregation
            var averageGrade = students.Average(s => s.Grade);
            Console.WriteLine($"\nAverage Grade: {averageGrade:F2}");

            // Grouping
            var bySubject = students.GroupBy(s => s.Subject);
            Console.WriteLine("\nStudents by Subject:");
            foreach (var group in bySubject)
            {
                Console.WriteLine($"  {group.Key}: {string.Join(", ", group.Select(s => s.Name))}");
            }
            Console.WriteLine();
        }
    }
}
