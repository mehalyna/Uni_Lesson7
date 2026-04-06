using System;
using System.Linq;
using System.Collections.Generic;

namespace Uni_Lesson7
{
    /// <summary>
    /// LINQ Operators Overview - Comprehensive guide to all major LINQ operators
    /// </summary>
    public class LinqOperatorsOverview
    {
        public static void RunExamples()
        {
            Console.WriteLine("=== LINQ OPERATORS OVERVIEW ===\n");

            // Filtering Operators
            Example1_FilteringOperators();

            // Projection Operators
            Example2_ProjectionOperators();

            // Sorting Operators
            Example3_SortingOperators();

            // Grouping Operators
            Example4_GroupingOperators();

            // Set Operators
            Example5_SetOperators();

            // Quantifier Operators
            Example6_QuantifierOperators();

            // Element Operators
            Example7_ElementOperators();

            // Partitioning Operators
            Example8_PartitioningOperators();

            // Generation Operators
            Example9_GenerationOperators();

            // Conversion Operators
            Example10_ConversionOperators();
        }

        static void Example1_FilteringOperators()
        {
            Console.WriteLine("--- 1. FILTERING OPERATORS ---");
            
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            string[] words = { "apple", "banana", "cherry", "date", "elderberry" };

            // Where - filter based on predicate
            var evens = numbers.Where(n => n % 2 == 0);
            Console.WriteLine($"Where (evens): {string.Join(", ", evens)}");

            // OfType - filter by type
            object[] mixed = { 1, "hello", 2, "world", 3.14, 4 };
            var integers = mixed.OfType<int>();
            Console.WriteLine($"OfType<int>: {string.Join(", ", integers)}");
            Console.WriteLine();
        }

        static void Example2_ProjectionOperators()
        {
            Console.WriteLine("--- 2. PROJECTION OPERATORS ---");
            
            string[] words = { "apple", "banana", "cherry" };
            var nested = new[] { new[] { 1, 2 }, new[] { 3, 4, 5 }, new[] { 6 } };

            // Select - transform each element
            var upperWords = words.Select(w => w.ToUpper());
            Console.WriteLine($"Select: {string.Join(", ", upperWords)}");

            // Select with index
            var indexed = words.Select((w, i) => $"{i}: {w}");
            Console.WriteLine($"Select (indexed): {string.Join(", ", indexed)}");

            // SelectMany - flatten nested collections
            var flattened = nested.SelectMany(arr => arr);
            Console.WriteLine($"SelectMany: {string.Join(", ", flattened)}");
            Console.WriteLine();
        }

        static void Example3_SortingOperators()
        {
            Console.WriteLine("--- 3. SORTING OPERATORS ---");
            
            int[] numbers = { 5, 2, 8, 1, 9, 3 };
            var people = new[]
            {
                new { Name = "Charlie", Age = 35 },
                new { Name = "Alice", Age = 30 },
                new { Name = "Bob", Age = 30 }
            };

            // OrderBy - ascending
            Console.WriteLine($"OrderBy: {string.Join(", ", numbers.OrderBy(n => n))}");

            // OrderByDescending - descending
            Console.WriteLine($"OrderByDescending: {string.Join(", ", numbers.OrderByDescending(n => n))}");

            // ThenBy - secondary sort
            var sorted = people.OrderBy(p => p.Age).ThenBy(p => p.Name);
            Console.WriteLine("OrderBy Age, ThenBy Name:");
            foreach (var p in sorted)
                Console.WriteLine($"  {p.Name}, {p.Age}");

            // Reverse
            Console.WriteLine($"Reverse: {string.Join(", ", numbers.Reverse())}");
            Console.WriteLine();
        }

        static void Example4_GroupingOperators()
        {
            Console.WriteLine("--- 4. GROUPING OPERATORS ---");
            
            var products = new[]
            {
                new { Name = "Laptop", Category = "Electronics", Price = 1000 },
                new { Name = "Mouse", Category = "Electronics", Price = 25 },
                new { Name = "Desk", Category = "Furniture", Price = 300 },
                new { Name = "Chair", Category = "Furniture", Price = 150 }
            };

            // GroupBy
            var grouped = products.GroupBy(p => p.Category);
            Console.WriteLine("GroupBy Category:");
            foreach (var group in grouped)
            {
                Console.WriteLine($"  {group.Key}: {string.Join(", ", group.Select(p => p.Name))}");
            }

            // ToLookup - similar to GroupBy but executes immediately
            var lookup = products.ToLookup(p => p.Category);
            Console.WriteLine($"\nToLookup - Electronics: {string.Join(", ", lookup["Electronics"].Select(p => p.Name))}");
            Console.WriteLine();
        }

        static void Example5_SetOperators()
        {
            Console.WriteLine("--- 5. SET OPERATORS ---");
            
            int[] set1 = { 1, 2, 3, 4, 5 };
            int[] set2 = { 4, 5, 6, 7, 8 };
            int[] duplicates = { 1, 2, 2, 3, 3, 3 };

            Console.WriteLine($"Set1: {string.Join(", ", set1)}");
            Console.WriteLine($"Set2: {string.Join(", ", set2)}");
            Console.WriteLine($"Distinct: {string.Join(", ", duplicates.Distinct())}");
            Console.WriteLine($"Union: {string.Join(", ", set1.Union(set2))}");
            Console.WriteLine($"Intersect: {string.Join(", ", set1.Intersect(set2))}");
            Console.WriteLine($"Except (Set1 - Set2): {string.Join(", ", set1.Except(set2))}");
            Console.WriteLine();
        }

        static void Example6_QuantifierOperators()
        {
            Console.WriteLine("--- 6. QUANTIFIER OPERATORS ---");
            
            int[] numbers = { 2, 4, 6, 8, 10 };
            int[] mixed = { 1, 2, 3, 4, 5 };
            int[] empty = { };

            Console.WriteLine($"Numbers: {string.Join(", ", numbers)}");
            Console.WriteLine($"All even: {numbers.All(n => n % 2 == 0)}");
            Console.WriteLine($"Any > 5: {numbers.Any(n => n > 5)}");
            Console.WriteLine($"Contains 6: {numbers.Contains(6)}");
            
            Console.WriteLine($"\nMixed: {string.Join(", ", mixed)}");
            Console.WriteLine($"All even: {mixed.All(n => n % 2 == 0)}");
            
            Console.WriteLine($"\nEmpty array has Any: {empty.Any()}");
            Console.WriteLine();
        }

        static void Example7_ElementOperators()
        {
            Console.WriteLine("--- 7. ELEMENT OPERATORS ---");
            
            int[] numbers = { 10, 20, 30, 40, 50 };
            int[] empty = { };

            Console.WriteLine($"Array: {string.Join(", ", numbers)}");
            Console.WriteLine($"First(): {numbers.First()}");
            Console.WriteLine($"First(>25): {numbers.First(n => n > 25)}");
            Console.WriteLine($"FirstOrDefault() on empty: {empty.FirstOrDefault()}");
            
            Console.WriteLine($"Last(): {numbers.Last()}");
            Console.WriteLine($"LastOrDefault(>100): {numbers.LastOrDefault(n => n > 100)}");
            
            Console.WriteLine($"ElementAt(2): {numbers.ElementAt(2)}");
            Console.WriteLine($"ElementAtOrDefault(10): {numbers.ElementAtOrDefault(10)}");
            
            Console.WriteLine($"Single() on [50]: {new[] { 50 }.Single()}");
            // Single() would throw exception on numbers array (multiple elements)
            
            Console.WriteLine($"DefaultIfEmpty on empty: {string.Join(", ", empty.DefaultIfEmpty(-1))}");
            Console.WriteLine();
        }

        static void Example8_PartitioningOperators()
        {
            Console.WriteLine("--- 8. PARTITIONING OPERATORS ---");
            
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Console.WriteLine($"Original: {string.Join(", ", numbers)}");
            Console.WriteLine($"Take(5): {string.Join(", ", numbers.Take(5))}");
            Console.WriteLine($"Skip(5): {string.Join(", ", numbers.Skip(5))}");
            Console.WriteLine($"TakeWhile(<6): {string.Join(", ", numbers.TakeWhile(n => n < 6))}");
            Console.WriteLine($"SkipWhile(<6): {string.Join(", ", numbers.SkipWhile(n => n < 6))}");

            // Pagination example
            int pageSize = 3;
            int pageNumber = 2;
            var page = numbers.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            Console.WriteLine($"Page {pageNumber} (size {pageSize}): {string.Join(", ", page)}");
            Console.WriteLine();
        }

        static void Example9_GenerationOperators()
        {
            Console.WriteLine("--- 9. GENERATION OPERATORS ---");
            
            // Range - generate sequence of integers
            var range = Enumerable.Range(1, 10);
            Console.WriteLine($"Range(1, 10): {string.Join(", ", range)}");

            // Repeat - repeat value n times
            var repeated = Enumerable.Repeat("Hi", 5);
            Console.WriteLine($"Repeat('Hi', 5): {string.Join(", ", repeated)}");

            // Empty - create empty sequence
            var empty = Enumerable.Empty<int>();
            Console.WriteLine($"Empty<int>().Any(): {empty.Any()}");

            // Practical example: Generate multiplication table
            var multTable = Enumerable.Range(1, 5)
                .Select(i => $"{i} x 5 = {i * 5}");
            Console.WriteLine("\nMultiplication table:");
            foreach (var line in multTable)
                Console.WriteLine($"  {line}");
            Console.WriteLine();
        }

        static void Example10_ConversionOperators()
        {
            Console.WriteLine("--- 10. CONVERSION OPERATORS ---");
            
            var numbers = new List<int> { 1, 2, 3, 4, 5 };

            // ToArray
            int[] array = numbers.Where(n => n > 2).ToArray();
            Console.WriteLine($"ToArray: {string.Join(", ", array)}");

            // ToList
            List<int> list = numbers.Where(n => n % 2 == 0).ToList();
            Console.WriteLine($"ToList: {string.Join(", ", list)}");

            // ToDictionary
            var words = new[] { "apple", "banana", "cherry" };
            var dict = words.ToDictionary(w => w[0], w => w.Length);
            Console.WriteLine("ToDictionary:");
            foreach (var kvp in dict)
                Console.WriteLine($"  '{kvp.Key}' -> {kvp.Value}");

            // ToHashSet
            var duplicates = new[] { 1, 2, 2, 3, 3, 3 };
            var hashSet = duplicates.ToHashSet();
            Console.WriteLine($"ToHashSet: {string.Join(", ", hashSet)}");

            // ToLookup
            var products = new[]
            {
                new { Name = "Laptop", Type = "Electronics" },
                new { Name = "Mouse", Type = "Electronics" },
                new { Name = "Desk", Type = "Furniture" }
            };
            var lookup = products.ToLookup(p => p.Type);
            Console.WriteLine($"ToLookup - Electronics: {string.Join(", ", lookup["Electronics"].Select(p => p.Name))}");

            // Cast - converts IEnumerable to IEnumerable<T>
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList { 1, 2, 3 };
            var casted = arrayList.Cast<int>();
            Console.WriteLine($"Cast: {string.Join(", ", casted)}");

            // AsEnumerable - converts to IEnumerable<T>
            var asEnum = numbers.AsEnumerable();
            Console.WriteLine($"AsEnumerable type: {asEnum.GetType().Name}");
            Console.WriteLine();
        }
    }
}
