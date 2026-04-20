using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Uni_Lesson7.Tests
{
    [TestFixture]
    public class LinqOperatorsOverviewTests
    {
        [TestFixture]
        public class FilteringOperatorsTests
        {
            [Test]
            public void Where_ShouldFilterElements()
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
            public void OfType_ShouldFilterByType()
            {
                // Arrange
                object[] mixed = { 1, "hello", 2, "world", 3.14 };

                // Act
                var integers = mixed.OfType<int>().ToList();
                var strings = mixed.OfType<string>().ToList();

                // Assert
                Assert.That(integers.Count, Is.EqualTo(2));
                Assert.That(strings.Count, Is.EqualTo(2));
            }
        }

        [TestFixture]
        public class ProjectionOperatorsTests
        {
            [Test]
            public void Select_ShouldTransform()
            {
                // Arrange
                var numbers = new[] { 1, 2, 3, 4, 5 };

                // Act
                var squares = numbers.Select(n => n * n).ToList();

                // Assert
                CollectionAssert.AreEqual(new[] { 1, 4, 9, 16, 25 }, squares);
            }

            [Test]
            public void SelectMany_ShouldFlatten()
            {
                // Arrange
                var nested = new[] { new[] { 1, 2 }, new[] { 3, 4, 5 } };

                // Act
                var flattened = nested.SelectMany(arr => arr).ToList();

                // Assert
                CollectionAssert.AreEqual(new[] { 1, 2, 3, 4, 5 }, flattened);
            }
        }

        [TestFixture]
        public class SortingOperatorsTests
        {
            [Test]
            public void OrderBy_ShouldSortAscending()
            {
                // Arrange
                int[] numbers = { 5, 2, 8, 1, 9 };

                // Act
                var sorted = numbers.OrderBy(n => n).ToList();

                // Assert
                CollectionAssert.AreEqual(new[] { 1, 2, 5, 8, 9 }, sorted);
            }

            [Test]
            public void OrderByDescending_ShouldSortDescending()
            {
                // Arrange
                int[] numbers = { 5, 2, 8, 1, 9 };

                // Act
                var sorted = numbers.OrderByDescending(n => n).ToList();

                // Assert
                CollectionAssert.AreEqual(new[] { 9, 8, 5, 2, 1 }, sorted);
            }

            [Test]
            public void ThenBy_ShouldProvideSecondarySort()
            {
                // Arrange
                var people = new[]
                {
                    new { Name = "Charlie", Age = 35 },
                    new { Name = "Alice", Age = 30 },
                    new { Name = "Bob", Age = 30 }
                };

                // Act
                var sorted = people.OrderBy(p => p.Age).ThenBy(p => p.Name).ToList();

                // Assert
                Assert.That(sorted[0].Name, Is.EqualTo("Alice"));
                Assert.That(sorted[1].Name, Is.EqualTo("Bob"));
            }
        }

        [TestFixture]
        public class SetOperatorsTests
        {
            [Test]
            public void Distinct_ShouldRemoveDuplicates()
            {
                // Arrange
                int[] duplicates = { 1, 2, 2, 3, 3, 3 };

                // Act
                var unique = duplicates.Distinct().ToList();

                // Assert
                CollectionAssert.AreEqual(new[] { 1, 2, 3 }, unique);
            }

            [Test]
            public void Union_ShouldCombine()
            {
                // Arrange
                int[] set1 = { 1, 2, 3 };
                int[] set2 = { 3, 4, 5 };

                // Act
                var union = set1.Union(set2).ToList();

                // Assert
                CollectionAssert.AreEqual(new[] { 1, 2, 3, 4, 5 }, union);
            }

            [Test]
            public void Intersect_ShouldFindCommon()
            {
                // Arrange
                int[] set1 = { 1, 2, 3, 4 };
                int[] set2 = { 3, 4, 5, 6 };

                // Act
                var intersect = set1.Intersect(set2).ToList();

                // Assert
                CollectionAssert.AreEqual(new[] { 3, 4 }, intersect);
            }

            [Test]
            public void Except_ShouldFindDifference()
            {
                // Arrange
                int[] set1 = { 1, 2, 3, 4 };
                int[] set2 = { 3, 4 };

                // Act
                var except = set1.Except(set2).ToList();

                // Assert
                CollectionAssert.AreEqual(new[] { 1, 2 }, except);
            }
        }

        [TestFixture]
        public class QuantifierOperatorsTests
        {
            [Test]
            public void All_ShouldCheckAllElements()
            {
                // Arrange
                int[] evens = { 2, 4, 6, 8 };
                int[] mixed = { 1, 2, 3 };

                // Act & Assert
                Assert.That(evens.All(n => n % 2 == 0), Is.True);
                Assert.That(mixed.All(n => n % 2 == 0), Is.False);
            }

            [Test]
            public void Any_ShouldCheckExistence()
            {
                // Arrange
                int[] numbers = { 1, 3, 5, 7 };

                // Act & Assert
                Assert.That(numbers.Any(n => n > 5), Is.True);
                Assert.That(numbers.Any(n => n > 10), Is.False);
            }

            [Test]
            public void Contains_ShouldFindElement()
            {
                // Arrange
                int[] numbers = { 1, 2, 3, 4, 5 };

                // Act & Assert
                Assert.That(numbers.Contains(3), Is.True);
                Assert.That(numbers.Contains(10), Is.False);
            }
        }

        [TestFixture]
        public class ElementOperatorsTests
        {
            [Test]
            public void First_ShouldReturnFirst()
            {
                // Arrange
                int[] numbers = { 10, 20, 30 };

                // Act & Assert
                Assert.That(numbers.First(), Is.EqualTo(10));
                Assert.That(numbers.First(n => n > 15), Is.EqualTo(20));
            }

            [Test]
            public void Last_ShouldReturnLast()
            {
                // Arrange
                int[] numbers = { 10, 20, 30 };

                // Act & Assert
                Assert.That(numbers.Last(), Is.EqualTo(30));
            }

            [Test]
            public void ElementAt_ShouldReturnAtIndex()
            {
                // Arrange
                int[] numbers = { 10, 20, 30 };

                // Act & Assert
                Assert.That(numbers.ElementAt(1), Is.EqualTo(20));
            }
        }

        [TestFixture]
        public class PartitioningOperatorsTests
        {
            [Test]
            public void Take_ShouldTakeFirst()
            {
                // Arrange
                int[] numbers = { 1, 2, 3, 4, 5 };

                // Act
                var result = numbers.Take(3).ToList();

                // Assert
                CollectionAssert.AreEqual(new[] { 1, 2, 3 }, result);
            }

            [Test]
            public void Skip_ShouldSkipFirst()
            {
                // Arrange
                int[] numbers = { 1, 2, 3, 4, 5 };

                // Act
                var result = numbers.Skip(2).ToList();

                // Assert
                CollectionAssert.AreEqual(new[] { 3, 4, 5 }, result);
            }

            [Test]
            public void TakeWhile_ShouldTakeUntilConditionFails()
            {
                // Arrange
                int[] numbers = { 1, 2, 3, 4, 5 };

                // Act
                var result = numbers.TakeWhile(n => n < 4).ToList();

                // Assert
                CollectionAssert.AreEqual(new[] { 1, 2, 3 }, result);
            }
        }

        [TestFixture]
        public class GenerationOperatorsTests
        {
            [Test]
            public void Range_ShouldGenerateSequence()
            {
                // Act
                var range = System.Linq.Enumerable.Range(1, 5).ToList();

                // Assert
                CollectionAssert.AreEqual(new[] { 1, 2, 3, 4, 5 }, range);
            }

            [Test]
            public void Repeat_ShouldRepeatValue()
            {
                // Act
                var repeated = System.Linq.Enumerable.Repeat("Hi", 3).ToList();

                // Assert
                Assert.That(repeated.Count, Is.EqualTo(3));
                Assert.That(repeated.All(s => s == "Hi"), Is.True);
            }
        }

        [TestFixture]
        public class AggregateOperatorsTests
        {
            [Test]
            public void Sum_ShouldCalculateSum()
            {
                // Arrange
                int[] numbers = { 1, 2, 3, 4, 5 };

                // Act
                var sum = numbers.Sum();

                // Assert
                Assert.That(sum, Is.EqualTo(15));
            }

            [Test]
            public void Average_ShouldCalculateAverage()
            {
                // Arrange
                int[] numbers = { 2, 4, 6 };

                // Act
                var avg = numbers.Average();

                // Assert
                Assert.That(avg, Is.EqualTo(4));
            }

            [Test]
            public void MinMax_ShouldFindExtremes()
            {
                // Arrange
                int[] numbers = { 5, 2, 8, 1, 9 };

                // Act & Assert
                Assert.That(numbers.Min(), Is.EqualTo(1));
                Assert.That(numbers.Max(), Is.EqualTo(9));
            }
        }
    }
}
