using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Uni_Lesson7.Tests
{
    [TestFixture]
    public class LinqFeaturesTests
    {
        [TestFixture]
        public class LambdaExpressionTests
        {
            [Test]
            public void LambdaExpression_SingleParameter_ShouldFilter()
            {
                // Arrange
                List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

                // Act
                var evens = numbers.Where(n => n % 2 == 0).ToList();

                // Assert
                Assert.That(evens.Count, Is.EqualTo(5));
                CollectionAssert.AreEqual(new[] { 2, 4, 6, 8, 10 }, evens);
            }

            [Test]
            public void LambdaExpression_MultipleParameters_ShouldIncludeIndex()
            {
                // Arrange
                List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

                // Act
                var indexed = numbers.Select((value, index) => $"[{index}]={value}").ToList();

                // Assert
                Assert.That(indexed[0], Is.EqualTo("[0]=1"));
                Assert.That(indexed[2], Is.EqualTo("[2]=3"));
            }

            [Test]
            public void LambdaExpression_ExpressionBody_ShouldCalculate()
            {
                // Arrange
                System.Func<int, int> square = x => x * x;

                // Act
                int result = square(5);

                // Assert
                Assert.That(result, Is.EqualTo(25));
            }

            [Test]
            public void LambdaExpression_StatementBody_ShouldClassify()
            {
                // Arrange
                System.Func<int, string> classify = x => 
                {
                    if (x < 5) return "Low";
                    if (x < 8) return "Medium";
                    return "High";
                };

                // Act & Assert
                Assert.That(classify(3), Is.EqualTo("Low"));
                Assert.That(classify(7), Is.EqualTo("Medium"));
                Assert.That(classify(9), Is.EqualTo("High"));
            }
        }

        [TestFixture]
        public class ExtensionMethodTests
        {
            [Test]
            public void BuiltInExtensions_ShouldWork()
            {
                // Arrange
                List<string> words = new List<string> { "apple", "banana", "cherry" };

                // Act & Assert
                Assert.That(words.Count(), Is.EqualTo(3));
                Assert.That(words.First(), Is.EqualTo("apple"));
                Assert.That(words.Any(w => w.Contains('e')), Is.True);
                Assert.That(words.All(w => w.Length > 3), Is.True);
            }

            [Test]
            public void CustomExtension_ShouldConcatenate()
            {
                // Arrange
                List<string> words = new List<string> { "apple", "banana", "cherry" };

                // Act
                string result = words.CustomConcat();

                // Assert
                Assert.That(result, Is.EqualTo("apple | banana | cherry"));
            }

            [Test]
            public void ExtensionMethods_ShouldChain()
            {
                // Arrange
                var numbers = new List<int> { 1, 2, 3, 4, 5 };

                // Act
                var result = numbers
                    .Where(n => n > 2)
                    .Select(n => n * 2)
                    .OrderByDescending(n => n)
                    .ToList();

                // Assert
                CollectionAssert.AreEqual(new[] { 10, 8, 6 }, result);
            }
        }

        [TestFixture]
        public class AnonymousTypeTests
        {
            [Test]
            public void AnonymousType_ShouldCreateTypeOnTheFly()
            {
                // Act
                var person = new { Name = "John", Age = 30 };

                // Assert
                Assert.That(person.Name, Is.EqualTo("John"));
                Assert.That(person.Age, Is.EqualTo(30));
            }

            [Test]
            public void AnonymousType_InProjection_ShouldTransform()
            {
                // Arrange
                var products = new[]
                {
                    new { Id = 1, Name = "Laptop", Price = 999.99, Category = "Electronics" },
                    new { Id = 2, Name = "Desk", Price = 299.99, Category = "Furniture" },
                    new { Id = 3, Name = "Mouse", Price = 29.99, Category = "Electronics" }
                };

                // Act
                var summary = products.Select(p => new 
                { 
                    p.Name, 
                    p.Category,
                    PriceWithTax = p.Price * 1.1,
                    IsExpensive = p.Price > 500
                }).ToList();

                // Assert
                Assert.That(summary[0].PriceWithTax, Is.EqualTo(1099.889).Within(0.01));
                Assert.That(summary[0].IsExpensive, Is.True);
                Assert.That(summary[2].IsExpensive, Is.False);
            }

            [Test]
            public void AnonymousType_ShouldSupportEquality()
            {
                // Arrange
                var person1 = new { Name = "John", Age = 30 };
                var person2 = new { Name = "John", Age = 30 };
                var person3 = new { Name = "Jane", Age = 30 };

                // Assert
                Assert.That(person1.Equals(person2), Is.True);
                Assert.That(person1.Equals(person3), Is.False);
            }
        }

        [TestFixture]
        public class TypeInferenceTests
        {
            [Test]
            public void Var_ShouldInferCorrectType()
            {
                // Act
                var number = 42;
                var text = "hello";
                var list = new List<int> { 1, 2, 3 };

                // Assert
                Assert.That(number, Is.TypeOf<int>());
                Assert.That(text, Is.TypeOf<string>());
                Assert.That(list, Is.TypeOf<List<int>>());
            }

            [Test]
            public void Var_WithAnonymousType_IsRequired()
            {
                // Act
                var person = new { Name = "John", Age = 30 };
                var query = from n in new[] { 1, 2, 3 }
                           where n > 1
                           select new { Number = n, Square = n * n };

                // Assert
                Assert.That(person.Name, Is.EqualTo("John"));
                Assert.That(query.Count(), Is.EqualTo(2));
            }

            [Test]
            public void ExplicitTyping_AndInference_ShouldBehaveSame()
            {
                // Arrange & Act
                IEnumerable<int> explicit1 = new List<int> { 1, 2, 3 };
                var inferred = new List<int> { 1, 2, 3 };

                // Assert
                Assert.That(inferred.GetType(), Is.EqualTo(typeof(List<int>)));
                CollectionAssert.AreEqual(explicit1, inferred);
            }
        }

        [TestFixture]
        public class QueryCompositionTests
        {
            [Test]
            public void QueryComposition_ShouldBuildComplexQueries()
            {
                // Arrange
                var employees = new[]
                {
                    new { Name = "Alice", Department = "IT", Salary = 75000, Experience = 5 },
                    new { Name = "Bob", Department = "HR", Salary = 65000, Experience = 3 },
                    new { Name = "Charlie", Department = "IT", Salary = 85000, Experience = 7 },
                    new { Name = "Diana", Department = "Finance", Salary = 90000, Experience = 10 },
                    new { Name = "Eve", Department = "IT", Salary = 70000, Experience = 4 }
                };

                // Act
                var itEmployees = employees.Where(e => e.Department == "IT");
                var seniorItEmployees = itEmployees.Where(e => e.Experience >= 5);
                var result = seniorItEmployees
                    .OrderByDescending(e => e.Salary)
                    .Select(e => new { e.Name, e.Salary, e.Experience })
                    .ToList();

                // Assert
                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result[0].Name, Is.EqualTo("Charlie"));
                Assert.That(result[1].Name, Is.EqualTo("Alice"));
            }

            [Test]
            public void QueryComposition_ShouldSupportConditionalLogic()
            {
                // Arrange
                var numbers = System.Linq.Enumerable.Range(1, 100);
                IEnumerable<int> query = numbers;
                bool applyEvenFilter = true;
                bool applyRangeFilter = true;

                // Act
                if (applyEvenFilter)
                    query = query.Where(n => n % 2 == 0);
                
                if (applyRangeFilter)
                    query = query.Where(n => n >= 20 && n <= 50);
                
                var result = query.ToList();

                // Assert
                Assert.That(result.Count, Is.EqualTo(16));
                Assert.That(result.First(), Is.EqualTo(20));
                Assert.That(result.Last(), Is.EqualTo(50));
            }
        }

        [TestFixture]
        public class LetClauseTests
        {
            [Test]
            public void LetClause_ShouldDefineIntermediateVariable()
            {
                // Arrange
                var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

                // Act
                var result = (from n in numbers
                            let square = n * n
                            where square > 20
                            select new { Number = n, Square = square }).ToList();

                // Assert
                Assert.That(result.Count, Is.EqualTo(5));
                Assert.That(result[0].Number, Is.EqualTo(5));
                Assert.That(result[0].Square, Is.EqualTo(25));
            }

            [Test]
            public void LetClause_ShouldImproveReadability()
            {
                // Arrange
                var words = new[] { "hello", "world", "linq", "csharp" };

                // Act
                var result = (from word in words
                            let length = word.Length
                            let firstChar = word[0]
                            where length > 4
                            orderby length descending
                            select new { Word = word, Length = length, FirstChar = firstChar }).ToList();

                // Assert
                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result[0].Word, Is.EqualTo("csharp"));
                Assert.That(result[0].Length, Is.EqualTo(6));
                Assert.That(result[1].Word, Is.EqualTo("hello"));
            }
        }
    }

    public static class CustomExtensions
    {
        public static string CustomConcat(this IEnumerable<string> source)
        {
            return string.Join(" | ", source);
        }
    }
}
