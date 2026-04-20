using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Uni_Lesson7.Tests
{
    [TestFixture]
    public class LinqBasicConceptsTests
    {
        [TestFixture]
        public class QuerySyntaxVsMethodSyntaxTests
        {
            private List<string> _fruits;

            [SetUp]
            public void Setup()
            {
                _fruits = new List<string> 
                { 
                    "Apple", "Banana", "Cherry", "Date", "Elderberry", "Fig", "Grape" 
                };
            }

            [Test]
            public void QuerySyntax_AndMethodSyntax_ShouldProduceSameResults()
            {
                // Arrange & Act
                var queryResult = (from fruit in _fruits
                                 where fruit.Length > 5
                                 orderby fruit
                                 select fruit.ToUpper()).ToList();

                var methodResult = _fruits
                    .Where(fruit => fruit.Length > 5)
                    .OrderBy(fruit => fruit)
                    .Select(fruit => fruit.ToUpper())
                    .ToList();

                // Assert
                Assert.That(methodResult, Is.EqualTo(queryResult));
                NUnit.Framework.Legacy.CollectionAssert.AreEqual(queryResult, methodResult);
            }

            [Test]
            public void QuerySyntax_ShouldFilterAndTransform()
            {
                // Act
                var result = (from fruit in _fruits
                            where fruit.Length > 5
                            orderby fruit
                            select fruit.ToUpper()).ToList();

                // Assert
                Assert.That(result.Count, Is.EqualTo(3));
                Assert.That(result[0], Is.EqualTo("BANANA"));
                Assert.That(result[1], Is.EqualTo("CHERRY"));
                Assert.That(result[2], Is.EqualTo("ELDERBERRY"));
            }

            [Test]
            public void MethodSyntax_ShouldFilterAndTransform()
            {
                // Act
                var result = _fruits
                    .Where(f => f.Length > 5)
                    .OrderBy(f => f)
                    .Select(f => f.ToUpper())
                    .ToList();

                // Assert
                Assert.That(result.Count, Is.EqualTo(3));
                CollectionAssert.AreEqual(new[] { "BANANA", "CHERRY", "ELDERBERRY" }, result);
            }
        }

        [TestFixture]
        public class DeferredExecutionTests
        {
            [Test]
            public void DeferredExecution_ShouldIncludeModificationsBeforeEnumeration()
            {
                // Arrange
                List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
                
                // Act
                var query = numbers.Where(n => n > 2);
                
                numbers.Add(6);
                numbers.Add(7);
                
                var results = query.ToList();

                // Assert
                Assert.That(results.Count, Is.EqualTo(5));
                CollectionAssert.Contains(results, 6);
                CollectionAssert.Contains(results, 7);
            }

            [Test]
            public void DeferredExecution_ShouldReExecuteOnEachEnumeration()
            {
                // Arrange
                List<int> numbers = new List<int> { 1, 2, 3 };
                var query = numbers.Where(n => n > 1);

                // Act
                var firstEnumeration = query.ToList();
                
                numbers.Add(4);
                numbers.Add(5);
                
                var secondEnumeration = query.ToList();

                // Assert
                Assert.That(firstEnumeration.Count, Is.EqualTo(2));
                Assert.That(secondEnumeration.Count, Is.EqualTo(4));
            }
        }

        [TestFixture]
        public class ImmediateExecutionTests
        {
            [Test]
            public void ToList_ShouldExecuteImmediately()
            {
                // Arrange
                List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
                
                // Act
                var list = numbers.Where(n => n > 2).ToList();
                
                numbers.Add(6);
                numbers.Add(7);

                // Assert
                Assert.That(list.Count, Is.EqualTo(3));
                CollectionAssert.DoesNotContain(list, 6);
                CollectionAssert.DoesNotContain(list, 7);
            }

            [Test]
            public void ToArray_ShouldExecuteImmediately()
            {
                // Arrange
                List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
                
                // Act
                var array = numbers.Where(n => n > 2).ToArray();
                numbers.Add(6);

                // Assert
                Assert.That(array.Length, Is.EqualTo(3));
                CollectionAssert.DoesNotContain(array, 6);
            }

            [Test]
            public void Count_ShouldExecuteQuery()
            {
                // Arrange
                List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
                var query = numbers.Where(n => n > 2);
                
                // Act
                int countBefore = query.Count();
                numbers.Add(6);
                int countAfter = query.Count();

                // Assert
                Assert.That(countBefore, Is.EqualTo(3));
                Assert.That(countAfter, Is.EqualTo(4));
            }
        }

        [TestFixture]
        public class LinqToObjectsTests
        {
            [Test]
            public void ComplexQuery_ShouldFilterAndSort()
            {
                // Arrange
                var students = new[]
                {
                    new { Name = "Alice", Grade = 85, Subject = "Math" },
                    new { Name = "Bob", Grade = 92, Subject = "Science" },
                    new { Name = "Charlie", Grade = 78, Subject = "Math" },
                    new { Name = "Diana", Grade = 95, Subject = "Science" },
                    new { Name = "Eve", Grade = 88, Subject = "Math" }
                };

                // Act
                var topMathStudents = (from student in students
                                     where student.Subject == "Math" && student.Grade >= 85
                                     orderby student.Grade descending
                                     select new { student.Name, student.Grade }).ToList();

                // Assert
                Assert.That(topMathStudents.Count, Is.EqualTo(2));
                Assert.That(topMathStudents[0].Name, Is.EqualTo("Eve"));
                Assert.That(topMathStudents[0].Grade, Is.EqualTo(88));
                Assert.That(topMathStudents[1].Name, Is.EqualTo("Alice"));
            }

            [Test]
            public void Average_ShouldCalculateCorrectly()
            {
                // Arrange
                var students = new[]
                {
                    new { Name = "Alice", Grade = 85 },
                    new { Name = "Bob", Grade = 92 },
                    new { Name = "Charlie", Grade = 78 },
                    new { Name = "Diana", Grade = 95 },
                    new { Name = "Eve", Grade = 88 }
                };

                // Act
                var averageGrade = students.Average(s => s.Grade);

                // Assert
                Assert.That(averageGrade, Is.EqualTo(87.6).Within(0.01));
            }

            [Test]
            public void GroupBy_ShouldGroupBySubject()
            {
                // Arrange
                var students = new[]
                {
                    new { Name = "Alice", Subject = "Math" },
                    new { Name = "Bob", Subject = "Science" },
                    new { Name = "Charlie", Subject = "Math" },
                    new { Name = "Diana", Subject = "Science" },
                    new { Name = "Eve", Subject = "Math" }
                };

                // Act
                var bySubject = students.GroupBy(s => s.Subject).ToList();

                // Assert
                Assert.That(bySubject.Count, Is.EqualTo(2));
                
                var mathGroup = bySubject.First(g => g.Key == "Math");
                var scienceGroup = bySubject.First(g => g.Key == "Science");
                
                Assert.That(mathGroup.Count(), Is.EqualTo(3));
                Assert.That(scienceGroup.Count(), Is.EqualTo(2));
            }
        }

        [TestFixture]
        public class BasicLinqOperationsTests
        {
            [Test]
            public void Where_ShouldFilterEvenNumbers()
            {
                // Arrange
                int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

                // Act
                var evens = numbers.Where(n => n % 2 == 0).ToList();

                // Assert
                Assert.That(evens.Count, Is.EqualTo(5));
                CollectionAssert.AreEqual(new[] { 2, 4, 6, 8, 10 }, evens);
            }

            [Test]
            public void Select_ShouldTransformElements()
            {
                // Arrange
                var numbers = new[] { 1, 2, 3, 4, 5 };

                // Act
                var squares = numbers.Select(n => n * n).ToList();

                // Assert
                CollectionAssert.AreEqual(new[] { 1, 4, 9, 16, 25 }, squares);
            }

            [Test]
            public void OrderBy_ShouldSortAscending()
            {
                // Arrange
                var numbers = new[] { 5, 2, 8, 1, 9 };

                // Act
                var sorted = numbers.OrderBy(n => n).ToList();

                // Assert
                CollectionAssert.AreEqual(new[] { 1, 2, 5, 8, 9 }, sorted);
            }

            [Test]
            public void FirstOrDefault_ShouldReturnDefaultWhenEmpty()
            {
                // Arrange
                var emptyList = new List<int>();

                // Act
                var result = emptyList.FirstOrDefault();

                // Assert
                Assert.That(result, Is.EqualTo(0));
            }

            [Test]
            public void Any_ShouldReturnTrueWhenConditionMet()
            {
                // Arrange
                var numbers = new[] { 1, 2, 3, 4, 5 };

                // Act
                bool hasEven = numbers.Any(n => n % 2 == 0);

                // Assert
                Assert.That(hasEven, Is.True);
            }

            [Test]
            public void All_ShouldReturnTrueWhenAllMatch()
            {
                // Arrange
                var evenNumbers = new[] { 2, 4, 6, 8 };

                // Act
                bool allEven = evenNumbers.All(n => n % 2 == 0);

                // Assert
                Assert.That(allEven, Is.True);
            }
        }
    }
}
