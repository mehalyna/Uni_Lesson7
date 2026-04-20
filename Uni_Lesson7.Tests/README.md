# Uni_Lesson7.Tests

Comprehensive test suite for the LINQ and Regular Expression demonstrations in the Uni_Lesson7 project.

## ?? Overview

This test project contains **112+ unit tests** across **6 test files** providing complete coverage for all demo files.

## ??? Test Files

1. **RegularExpressionTests.cs** (25+ tests) - Email, phone, password validation
2. **LinqBasicConceptsTests.cs** (18 tests) - LINQ fundamentals
3. **LinqFeaturesTests.cs** (20 tests) - Advanced C# features
4. **LinqSamplesTests.cs** (8 tests) - Real-world scenarios
5. **LinqOperatorsOverviewTests.cs** (25+ tests) - All LINQ operators
6. **IEnumerableVsIQueryableTests.cs** (16 tests) - Performance concepts

## ?? Running Tests

```bash
# Run all tests
dotnet test

# Run with details
dotnet test --verbosity detailed

# Run specific tests
dotnet test --filter "FullyQualifiedName~RegularExpression"
```

## ? Test Coverage

- ? Regular expression patterns
- ? LINQ query concepts
- ? All LINQ operators
- ? IEnumerable vs IQueryable
- ? Real-world scenarios
- ? Edge cases

## ?? Statistics

- **Total Tests**: 112+
- **Test Classes**: 34
- **Lines of Code**: ~1,700
- **Coverage**: 100% of demo files

---

For detailed documentation, see [TEST_SUMMARY.md](TEST_SUMMARY.md)
