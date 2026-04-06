# LINQ and Regular Expression Demo Examples

## Overview
This project contains comprehensive examples for Lesson 7, covering Regular Expressions and LINQ (Language Integrated Query) in C#.

## Project Structure

### Demo Files

1. **1_RegularExpression.cs**
   - Basic pattern matching
   - Email validation
   - Phone number extraction
   - Find and replace operations
   - Group capturing
   - Password validation

2. **2_LinqBasicConcepts.cs**
   - What is LINQ?
   - Query syntax vs Method syntax
   - Deferred execution
   - Immediate execution
   - LINQ to Objects

3. **3_LinqFeatures.cs**
   - Lambda expressions
   - Extension methods
   - Anonymous types
   - Type inference (var)
   - Query composition
   - Let clause

4. **4_LinqSamples.cs**
   - Filtering data (Where)
   - Projection (Select, SelectMany)
   - Sorting (OrderBy, ThenBy)
   - Grouping (GroupBy)
   - Joining data
   - Aggregation (Sum, Average, Count, etc.)
   - Set operations (Union, Intersect, Except)
   - Complex real-world queries

5. **5_LinqOperatorsOverview.cs**
   - Filtering operators
   - Projection operators
   - Sorting operators
   - Grouping operators
   - Set operators
   - Quantifier operators
   - Element operators
   - Partitioning operators
   - Generation operators
   - Conversion operators

6. **6_IEnumerableVsIQueryable.cs**
   - Basic differences between IEnumerable and IQueryable
   - Execution location (client-side vs server-side)
   - Expression trees
   - Performance comparison
   - When to use what
   - Real-world scenarios

### Supporting Files

- **DemoRunner.cs** - Interactive menu system to run individual demos or all demos sequentially
- **Program.cs** - Entry point that launches the demo runner

## How to Run

1. Build the project (F6 or Build ? Build Solution)
2. Run the project (F5 or Debug ? Start Debugging)
3. Use the interactive menu to select which demo to run:
   - Press 1-6 to run individual demos
   - Press 7 to run all demos sequentially
   - Press 0 to exit

## Key Features

### Regular Expressions
- Pattern matching with various real-world examples
- Validation scenarios (email, password, phone numbers)
- Text extraction and transformation
- Group capturing for complex parsing

### LINQ Concepts
- **Deferred Execution**: Queries are not executed until enumerated
- **Query Syntax**: SQL-like syntax for queries
- **Method Syntax**: Fluent API using extension methods
- **Expression Trees**: Enable query translation for IQueryable

### LINQ Operators Categories
1. **Filtering**: Where, OfType
2. **Projection**: Select, SelectMany
3. **Sorting**: OrderBy, ThenBy, Reverse
4. **Grouping**: GroupBy, ToLookup
5. **Set Operations**: Distinct, Union, Intersect, Except
6. **Quantifiers**: All, Any, Contains
7. **Element**: First, Last, Single, ElementAt
8. **Partitioning**: Take, Skip, TakeWhile, SkipWhile
9. **Generation**: Range, Repeat, Empty
10. **Conversion**: ToArray, ToList, ToDictionary

### IEnumerable vs IQueryable
- **IEnumerable<T>**: Best for in-memory collections
  - Executes on client-side
  - Uses Func<T, bool> delegates
  - All data loaded before filtering

- **IQueryable<T>**: Best for remote data sources
  - Executes on server-side (e.g., database)
  - Uses Expression<Func<T, bool>> expression trees
  - Query translated to SQL or other query language
  - Only required data is retrieved

## Learning Path

Recommended order for studying:
1. Start with Regular Expressions (file 1) to understand pattern matching
2. Learn LINQ Basic Concepts (file 2) for foundational knowledge
3. Study LINQ Features (file 3) to understand C# features that enable LINQ
4. Practice with LINQ Samples (file 4) for real-world scenarios
5. Review LINQ Operators Overview (file 5) for comprehensive operator knowledge
6. Understand IEnumerable vs IQueryable (file 6) for performance optimization

## Additional Notes

- All examples include console output with clear explanations
- Each file is self-contained and can be studied independently
- Examples progress from simple to complex
- Real-world scenarios are included to demonstrate practical applications
- Code follows C# 12.0 and .NET 8 conventions

## Tips for Learning

1. Run each demo individually to focus on specific concepts
2. Modify the examples to experiment with different scenarios
3. Pay attention to the console output - it explains what's happening
4. Try writing your own queries based on the patterns shown
5. Use the debugger to step through queries and understand execution flow

## Common Use Cases Covered

- Data filtering and searching
- Data transformation and projection
- Sorting and ordering collections
- Grouping and aggregating data
- Joining multiple data sources
- Validating user input with regex
- Parsing and extracting structured data from text
- Building dynamic queries
- Optimizing database queries

Happy Learning!
