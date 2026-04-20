using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Uni_Lesson7.Tests
{
    [TestFixture]
    public class RegularExpressionTests
    {
        [TestFixture]
        public class EmailValidationTests
        {
            private string _emailPattern;

            [SetUp]
            public void Setup()
            {
                _emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            }

            [Test]
            [TestCase("user@example.com", true)]
            [TestCase("john.doe@company.co.uk", true)]
            [TestCase("valid_email123@test-site.org", true)]
            [TestCase("invalid.email@", false)]
            [TestCase("missing@domain", false)]
            [TestCase("@nodomain.com", false)]
            [TestCase("noemail.com", false)]
            [TestCase("email@.com", false)]
            public void EmailPattern_ShouldValidateCorrectly(string email, bool expectedValid)
            {
                // Act
                bool isValid = Regex.IsMatch(email, _emailPattern);

                // Assert
                Assert.That(isValid, Is.EqualTo(expectedValid),
                    $"Email '{email}' validation failed. Expected: {expectedValid}, Got: {isValid}");
            }

            [Test]
            public void ValidEmail_ShouldMatchPattern()
            {
                // Arrange
                string validEmail = "test@example.com";

                // Act
                Match match = Regex.Match(validEmail, _emailPattern);

                // Assert
                Assert.That(match.Success, Is.True);
                Assert.That(match.Value, Is.EqualTo(validEmail));
            }
        }

        [TestFixture]
        public class PhoneNumberTests
        {
            private string _phonePattern;

            [SetUp]
            public void Setup()
            {
                _phonePattern = @"(\+?\d{1,3}[-.\s]?)?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}";
            }

            [Test]
            [TestCase("(123) 456-7890")]
            [TestCase("987-654-3210")]
            [TestCase("+1-555-123-4567")]
            [TestCase("555.987.6543")]
            public void PhonePattern_ShouldMatchValidPhoneNumbers(string phoneNumber)
            {
                // Act
                bool isMatch = Regex.IsMatch(phoneNumber, _phonePattern);

                // Assert
                Assert.That(isMatch, Is.True, $"Phone number '{phoneNumber}' should match the pattern");
            }

            [Test]
            public void PhonePattern_ShouldExtractMultiplePhoneNumbers()
            {
                // Arrange
                string text = @"Contact us at (123) 456-7890 or 987-654-3210. 
                               Office: +1-555-123-4567, Mobile: 555.987.6543";

                // Act
                MatchCollection matches = Regex.Matches(text, _phonePattern);

                // Assert
                Assert.That(matches.Count, Is.EqualTo(4), "Should find 4 phone numbers");
            }
        }

        [TestFixture]
        public class PasswordValidationTests
        {
            private string _passwordPattern;

            [SetUp]
            public void Setup()
            {
                // At least 8 chars, 1 uppercase, 1 lowercase, 1 digit, 1 special char
                _passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            }

            [Test]
            [TestCase("Valid@Pass123", true)]
            [TestCase("Pass123!", true)]
            [TestCase("weakpass", false)]
            [TestCase("NOLOWERCASE123!", false)]
            [TestCase("NoSpecialChar123", false)]
            [TestCase("Short1!", false)]
            public void PasswordPattern_ShouldValidateStrength(string password, bool expectedValid)
            {
                // Act
                bool isValid = Regex.IsMatch(password, _passwordPattern);

                // Assert
                Assert.That(isValid, Is.EqualTo(expectedValid));
            }
        }

        [TestFixture]
        public class PatternMatchingTests
        {
            [Test]
            public void WordPattern_ShouldMatchFiveLetterWords()
            {
                // Arrange
                string text = "The quick brown fox jumps over the lazy dog";
                string pattern = @"\b\w{5}\b";

                // Act
                MatchCollection matches = Regex.Matches(text, pattern);

                // Assert
                Assert.That(matches.Count, Is.EqualTo(3));
                Assert.That(matches[0].Value, Is.EqualTo("quick"));
                Assert.That(matches[1].Value, Is.EqualTo("brown"));
                Assert.That(matches[2].Value, Is.EqualTo("jumps"));
            }

            [Test]
            public void Regex_Replace_ShouldConvertDateFormat()
            {
                // Arrange
                string text = "The date is 2024-01-15";
                string pattern = @"(\d{4})-(\d{2})-(\d{2})";
                string replacement = "$2/$3/$1";

                // Act
                string result = Regex.Replace(text, pattern, replacement);

                // Assert
                Assert.That(result, Is.EqualTo("The date is 01/15/2024"));
            }

            [Test]
            public void Regex_Replace_ShouldRemoveExtraWhitespace()
            {
                // Arrange
                string text = "This   has    too     many      spaces";
                string pattern = @"\s+";

                // Act
                string result = Regex.Replace(text, pattern, " ");

                // Assert
                Assert.That(result, Is.EqualTo("This has too many spaces"));
            }
        }

        [TestFixture]
        public class GroupCapturingTests
        {
            [Test]
            public void UrlPattern_ShouldCaptureComponents()
            {
                // Arrange
                string url = "https://www.example.com:8080/page";
                string pattern = @"^(https?|ftp)://([^/:]+)(:\d+)?(/.*)?$";

                // Act
                Match match = Regex.Match(url, pattern);

                // Assert
                Assert.That(match.Success, Is.True);
                Assert.That(match.Groups[1].Value, Is.EqualTo("https"));
                Assert.That(match.Groups[2].Value, Is.EqualTo("www.example.com"));
                Assert.That(match.Groups[3].Value, Is.EqualTo(":8080"));
                Assert.That(match.Groups[4].Value, Is.EqualTo("/page"));
            }

            [Test]
            public void UrlPattern_ShouldHandleDefaultPort()
            {
                // Arrange
                string url = "http://subdomain.site.org/path";
                string pattern = @"^(https?|ftp)://([^/:]+)(:\d+)?(/.*)?$";

                // Act
                Match match = Regex.Match(url, pattern);

                // Assert
                Assert.That(match.Success, Is.True);
                Assert.That(match.Groups[3].Success, Is.False);
            }
        }

        [TestFixture]
        public class RegexOptionsTests
        {
            [Test]
            public void Regex_WithIgnoreCase_ShouldMatchCaseInsensitive()
            {
                // Arrange
                string text = "Hello WORLD";
                string pattern = "hello";

                // Act
                bool matchesWithoutFlag = Regex.IsMatch(text, pattern);
                bool matchesWithFlag = Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase);

                // Assert
                Assert.That(matchesWithoutFlag, Is.False);
                Assert.That(matchesWithFlag, Is.True);
            }

            [Test]
            public void Regex_Compiled_ShouldWork()
            {
                // Arrange
                string pattern = @"\d+";
                Regex compiledRegex = new Regex(pattern, RegexOptions.Compiled);
                string text = "123 456 789";

                // Act
                MatchCollection matches = compiledRegex.Matches(text);

                // Assert
                Assert.That(matches.Count, Is.EqualTo(3));
            }
        }
    }
}
