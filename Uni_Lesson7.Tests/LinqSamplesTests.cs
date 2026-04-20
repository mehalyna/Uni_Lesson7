using System.Linq;
using NUnit.Framework;

namespace Uni_Lesson7.Tests
{
    [TestFixture]
    public class LinqSamplesTests
    {
        [Test]
        public void FilteringDemo_ShouldWork()
        {
            // Arrange
            var employees = new[]
            {
                new { Name = "Alice", Salary = 75000, Department = "IT", Position = "Developer" },
                new { Name = "Bob", Salary = 85000, Department = "IT", Position = "Manager" },
                new { Name = "Charlie", Salary = 65000, Department = "HR", Position = "Recruiter" }
            };

            // Act
            var highEarners = employees.Where(e => e.Salary > 70000).ToList();
            var itManagers = employees.Where(e => e.Department == "IT" && e.Position == "Manager").ToList();

            // Assert
            Assert.That(highEarners.Count, Is.EqualTo(2));
            Assert.That(itManagers.Count, Is.EqualTo(1));
            Assert.That(itManagers[0].Name, Is.EqualTo("Bob"));
        }

        [Test]
        public void ProjectionDemo_ShouldWork()
        {
            // Arrange
            var employees = new[]
            {
                new { Name = "Alice", Salary = 75000 },
                new { Name = "Bob", Salary = 85000 }
            };

            // Act
            var names = employees.Select(e => e.Name).ToList();
            var summary = employees.Select(e => new { e.Name, Bonus = e.Salary * 0.1 }).ToList();

            // Assert
            Assert.That(names.Count, Is.EqualTo(2));
            Assert.That(summary[0].Bonus, Is.EqualTo(7500));
        }

        [Test]
        public void SortingDemo_ShouldWork()
        {
            // Arrange
            var employees = new[]
            {
                new { Name = "Charlie", Department = "HR", Salary = 65000 },
                new { Name = "Alice", Department = "IT", Salary = 75000 },
                new { Name = "Bob", Department = "IT", Salary = 85000 }
            };

            // Act
            var sorted = employees
                .OrderBy(e => e.Department)
                .ThenByDescending(e => e.Salary)
                .ToList();

            // Assert
            Assert.That(sorted[0].Department, Is.EqualTo("HR"));
            Assert.That(sorted[1].Name, Is.EqualTo("Bob"));
            Assert.That(sorted[2].Name, Is.EqualTo("Alice"));
        }

        [Test]
        public void GroupingDemo_ShouldWork()
        {
            // Arrange
            var employees = new[]
            {
                new { Name = "Alice", Department = "IT", Salary = 75000 },
                new { Name = "Bob", Department = "IT", Salary = 85000 },
                new { Name = "Charlie", Department = "HR", Salary = 65000 }
            };

            // Act
            var byDepartment = employees.GroupBy(e => e.Department).ToList();
            var stats = employees.GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    Count = g.Count(),
                    AvgSalary = g.Average(e => e.Salary)
                }).ToList();

            // Assert
            Assert.That(byDepartment.Count, Is.EqualTo(2));
            var itGroup = byDepartment.First(g => g.Key == "IT");
            Assert.That(itGroup.Count(), Is.EqualTo(2));
            Assert.That(stats.First(s => s.Department == "IT").AvgSalary, Is.EqualTo(80000));
        }

        [Test]
        public void AggregationDemo_ShouldWork()
        {
            // Arrange
            var employees = new[]
            {
                new { Name = "Alice", Salary = 75000 },
                new { Name = "Bob", Salary = 85000 },
                new { Name = "Charlie", Salary = 65000 }
            };

            // Act
            var count = employees.Count();
            var total = employees.Sum(e => e.Salary);
            var avg = employees.Average(e => e.Salary);
            var max = employees.Max(e => e.Salary);
            var min = employees.Min(e => e.Salary);

            // Assert
            Assert.That(count, Is.EqualTo(3));
            Assert.That(total, Is.EqualTo(225000));
            Assert.That(avg, Is.EqualTo(75000));
            Assert.That(max, Is.EqualTo(85000));
            Assert.That(min, Is.EqualTo(65000));
        }

        [Test]
        public void SetOperationsDemo_ShouldWork()
        {
            // Arrange
            int[] set1 = { 1, 2, 3, 4, 5 };
            int[] set2 = { 4, 5, 6, 7, 8 };
            int[] duplicates = { 1, 2, 2, 3, 3, 3 };

            // Act
            var distinct = duplicates.Distinct().ToList();
            var union = set1.Union(set2).ToList();
            var intersect = set1.Intersect(set2).ToList();
            var except = set1.Except(set2).ToList();

            // Assert
            Assert.That(distinct.Count, Is.EqualTo(3));
            Assert.That(union.Count, Is.EqualTo(8));
            Assert.That(intersect.Count, Is.EqualTo(2));
            Assert.That(except.Count, Is.EqualTo(3));
        }

        [Test]
        public void ComplexQueryDemo_ShouldWork()
        {
            // Arrange
            var employees = new[]
            {
                new { Name = "Alice", Department = "IT", Salary = 75000, Experience = 5 },
                new { Name = "Bob", Department = "IT", Salary = 85000, Experience = 8 },
                new { Name = "Charlie", Department = "HR", Salary = 65000, Experience = 3 },
                new { Name = "Diana", Department = "IT", Salary = 95000, Experience = 10 }
            };

            // Act
            var result = employees
                .Where(e => e.Experience >= 5)
                .GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    Count = g.Count(),
                    AvgSalary = g.Average(e => e.Salary),
                    TopEarner = g.OrderByDescending(e => e.Salary).First().Name
                })
                .OrderByDescending(d => d.AvgSalary)
                .ToList();

            // Assert
            Assert.That(result.Count, Is.GreaterThan(0));
            var itDept = result.First(r => r.Department == "IT");
            Assert.That(itDept.Count, Is.EqualTo(3));
            Assert.That(itDept.TopEarner, Is.EqualTo("Diana"));
        }

        [Test]
        public void JoiningDemo_ShouldWork()
        {
            // Arrange
            var employees = new[]
            {
                new { Id = 1, Name = "Alice", DeptId = 1 },
                new { Id = 2, Name = "Bob", DeptId = 1 },
                new { Id = 3, Name = "Charlie", DeptId = 2 }
            };

            var departments = new[]
            {
                new { Id = 1, Name = "IT", Budget = 500000 },
                new { Id = 2, Name = "HR", Budget = 200000 }
            };

            // Act
            var joined = from emp in employees
                        join dept in departments on emp.DeptId equals dept.Id
                        select new { emp.Name, Department = dept.Name, dept.Budget };

            var result = joined.ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.All(r => r.Budget > 0), Is.True);
        }
    }
}
