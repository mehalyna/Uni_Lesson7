using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Uni_Lesson7.Tests
{
    [TestFixture]
    public class IEnumerableVsIQueryableTests
    {
        [TestFixture]
        public class BasicDifferencesTests
        {
            [Test]
            public void IEnumerable_ShouldWorkWithInMemoryCollections()
            {
                // Arrange
                List<int> numbers = System.Linq.Enumerable.Range(1, 100).ToList();

                // Act
                IEnumerable<int> query = numbers.Where(n => n > 50);
                var result = query.ToList();

                // Assert
                Assert.That(result.Count, Is.EqualTo(50));
                Assert.That(query, Is.InstanceOf<IEnumerable<int>>());
            }

            [Test]
            public void IQueryable_ShouldWorkWithAsQueryable()
            {
                // Arrange
                List<int> numbers = System.Linq.Enumerable.Range(1, 100).ToList();

                // Act
                IQueryable<int> query = numbers.AsQueryable().Where(n => n > 50);
                var result = query.ToList();

                // Assert
                Assert.That(result.Count, Is.EqualTo(50));
                Assert.That(query, Is.InstanceOf<IQueryable<int>>());
            }

            [Test]
            public void BothApproaches_ShouldProduceSameResults()
            {
                // Arrange
                List<int> numbers = System.Linq.Enumerable.Range(1, 100).ToList();

                // Act
                var enumerableResult = numbers.Where(n => n > 50).ToList();
                var queryableResult = numbers.AsQueryable().Where(n => n > 50).ToList();

                // Assert
                CollectionAssert.AreEqual(enumerableResult, queryableResult);
            }
        }

        [TestFixture]
        public class DeferredExecutionTests
        {
            [Test]
            public void IEnumerable_ShouldSupportDeferredExecution()
            {
                // Arrange
                List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

                // Act
                IEnumerable<int> query = numbers.Where(n => n > 2);
                numbers.Add(6);
                var result = query.ToList();

                // Assert
                Assert.That(result.Count, Is.EqualTo(4));
                CollectionAssert.Contains(result, 6);
            }

            [Test]
            public void IQueryable_ShouldSupportDeferredExecution()
            {
                // Arrange
                List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

                // Act
                IQueryable<int> query = numbers.AsQueryable().Where(n => n > 2);
                numbers.Add(6);
                var result = query.ToList();

                // Assert
                Assert.That(result.Count, Is.EqualTo(4));
                CollectionAssert.Contains(result, 6);
            }
        }

        [TestFixture]
        public class ExpressionTreeTests
        {
            [Test]
            public void IQueryable_ShouldUseExpressionTrees()
            {
                // Arrange
                var numbers = System.Linq.Enumerable.Range(1, 10).ToList();
                System.Linq.Expressions.Expression<System.Func<int, bool>> expr = n => n > 5;

                // Act
                var result = numbers.AsQueryable().Where(expr).ToList();

                // Assert
                Assert.That(result.Count, Is.EqualTo(5));
                Assert.That(expr.Body, Is.Not.Null);
            }

            [Test]
            public void ExpressionTree_ShouldBeAnalyzable()
            {
                // Arrange
                System.Linq.Expressions.Expression<System.Func<int, bool>> expr = n => n > 5;

                // Act & Assert
                Assert.That(expr.ToString(), Does.Contain("n => (n > 5)"));
                Assert.That(expr.Body, Is.Not.Null);
            }
        }

        [TestFixture]
        public class QueryCompositionTests
        {
            [Test]
            public void IEnumerable_ShouldSupportQueryComposition()
            {
                // Arrange
                var numbers = System.Linq.Enumerable.Range(1, 100).ToList();

                // Act
                IEnumerable<int> query = numbers
                    .Where(n => n > 50)
                    .Where(n => n < 75)
                    .Where(n => n % 2 == 0);
                
                var result = query.ToList();

                // Assert
                Assert.That(result.Count, Is.EqualTo(12));
                Assert.That(result.All(n => n > 50 && n < 75 && n % 2 == 0), Is.True);
            }

            [Test]
            public void IQueryable_ShouldSupportQueryComposition()
            {
                // Arrange
                var numbers = System.Linq.Enumerable.Range(1, 100).ToList();

                // Act
                IQueryable<int> query = numbers.AsQueryable()
                    .Where(n => n > 50)
                    .Where(n => n < 75)
                    .Where(n => n % 2 == 0);
                
                var result = query.ToList();

                // Assert
                Assert.That(result.Count, Is.EqualTo(12));
            }
        }

        [TestFixture]
        public class DynamicQueriesTests
        {
            [Test]
            public void IQueryable_ShouldSupportDynamicFilters()
            {
                // Arrange
                var employees = new List<EmployeeData>
                {
                    new EmployeeData { Id = 1, Name = "Alice", Department = "IT", Salary = 75000 },
                    new EmployeeData { Id = 2, Name = "Bob", Department = "IT", Salary = 82000 },
                    new EmployeeData { Id = 3, Name = "Charlie", Department = "HR", Salary = 65000 }
                };

                IQueryable<EmployeeData> query = employees.AsQueryable();
                string? filterDept = "IT";
                int? minSalary = 70000;

                // Act
                if (!string.IsNullOrEmpty(filterDept))
                    query = query.Where(e => e.Department == filterDept);
                
                if (minSalary.HasValue)
                    query = query.Where(e => e.Salary > minSalary.Value);
                
                var result = query.ToList();

                // Assert
                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result.All(e => e.Department == "IT" && e.Salary > 70000), Is.True);
            }
        }

        [TestFixture]
        public class PerformanceTests
        {
            [Test]
            public void DeferredExecution_ShouldReExecuteQuery()
            {
                // Arrange
                var numbers = new List<int> { 1, 2, 3, 4, 5 };
                var query = numbers.Where(n => n > 2);

                // Act
                var firstCount = query.Count();
                numbers.Add(6);
                var secondCount = query.Count();

                // Assert
                Assert.That(firstCount, Is.EqualTo(3));
                Assert.That(secondCount, Is.EqualTo(4));
            }

            [Test]
            public void ToList_ShouldCacheResults()
            {
                // Arrange
                var numbers = new List<int> { 1, 2, 3, 4, 5 };
                var list = numbers.Where(n => n > 2).ToList();

                // Act
                numbers.Add(6);

                // Assert
                Assert.That(list.Count, Is.EqualTo(3));
            }
        }

        [TestFixture]
        public class ConversionTests
        {
            [Test]
            public void AsQueryable_ShouldConvertToIQueryable()
            {
                // Arrange
                List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

                // Act
                IQueryable<int> queryable = numbers.AsQueryable();

                // Assert
                Assert.That(queryable, Is.InstanceOf<IQueryable<int>>());
                Assert.That(queryable.Count(), Is.EqualTo(5));
            }

            [Test]
            public void AsEnumerable_ShouldConvertToIEnumerable()
            {
                // Arrange
                var queryable = System.Linq.Enumerable.Range(1, 10).AsQueryable();

                // Act
                IEnumerable<int> enumerable = queryable.AsEnumerable();

                // Assert
                Assert.That(enumerable, Is.InstanceOf<IEnumerable<int>>());
                Assert.That(enumerable.Count(), Is.EqualTo(10));
            }
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
