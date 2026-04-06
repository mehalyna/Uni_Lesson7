using System;
using System.Text.RegularExpressions;

namespace Uni_Lesson7
{
    /// <summary>
    /// Regular Expression Examples - Pattern Matching and Text Processing
    /// </summary>
    public class RegularExpressionDemo
    {
        public static void RunExamples()
        {
            Console.WriteLine("=== REGULAR EXPRESSION EXAMPLES ===\n");

            // Example 1: Basic Pattern Matching
            Example1_BasicMatching();

            // Example 2: Email Validation
            Example2_EmailValidation();

            // Example 3: Phone Number Extraction
            Example3_PhoneNumberExtraction();

            // Example 4: Find and Replace
            Example4_FindAndReplace();

            // Example 5: Group Capturing
            Example5_GroupCapturing();

            // Example 6: Password Validation
            Example6_PasswordValidation();
        }

        static void Example1_BasicMatching()
        {
            Console.WriteLine("--- Example 1: Basic Pattern Matching ---");
            
            string text = "The quick brown fox jumps over the lazy dog";
            string pattern = @"\b\w{5}\b"; // Words with exactly 5 letters

            MatchCollection matches = Regex.Matches(text, pattern);
            
            Console.WriteLine($"Text: {text}");
            Console.WriteLine($"Pattern: {pattern} (5-letter words)");
            Console.WriteLine($"Matches found: {matches.Count}");
            
            foreach (Match match in matches)
            {
                Console.WriteLine($"  - '{match.Value}' at position {match.Index}");
            }
            Console.WriteLine();
        }

        static void Example2_EmailValidation()
        {
            Console.WriteLine("--- Example 2: Email Validation ---");
            
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            
            string[] emails = 
            {
                "user@example.com",
                "john.doe@company.co.uk",
                "invalid.email@",
                "missing@domain",
                "valid_email123@test-site.org"
            };

            foreach (string email in emails)
            {
                bool isValid = Regex.IsMatch(email, emailPattern);
                Console.WriteLine($"{email,-35} - {(isValid ? "? Valid" : "? Invalid")}");
            }
            Console.WriteLine();
        }

        static void Example3_PhoneNumberExtraction()
        {
            Console.WriteLine("--- Example 3: Phone Number Extraction ---");
            
            string text = @"Contact us at (123) 456-7890 or 987-654-3210. 
                           Office: +1-555-123-4567, Mobile: 555.987.6543";
            
            // Pattern for various phone formats
            string phonePattern = @"(\+?\d{1,3}[-.\s]?)?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}";
            
            MatchCollection matches = Regex.Matches(text, phonePattern);
            
            Console.WriteLine("Text with phone numbers:");
            Console.WriteLine(text);
            Console.WriteLine($"\nFound {matches.Count} phone numbers:");
            
            foreach (Match match in matches)
            {
                Console.WriteLine($"  - {match.Value.Trim()}");
            }
            Console.WriteLine();
        }

        static void Example4_FindAndReplace()
        {
            Console.WriteLine("--- Example 4: Find and Replace ---");
            
            string text = "The date is 2024-01-15 and the event was on 2023-12-25.";
            Console.WriteLine($"Original: {text}");
            
            // Replace date format from YYYY-MM-DD to MM/DD/YYYY
            string pattern = @"(\d{4})-(\d{2})-(\d{2})";
            string replacement = "$2/$3/$1";
            
            string result = Regex.Replace(text, pattern, replacement);
            Console.WriteLine($"Modified: {result}");
            
            // Remove extra whitespace
            string textWithSpaces = "This   has    too     many      spaces";
            string cleaned = Regex.Replace(textWithSpaces, @"\s+", " ");
            Console.WriteLine($"\nOriginal: '{textWithSpaces}'");
            Console.WriteLine($"Cleaned:  '{cleaned}'");
            Console.WriteLine();
        }

        static void Example5_GroupCapturing()
        {
            Console.WriteLine("--- Example 5: Group Capturing ---");
            
            string[] urls = 
            {
                "https://www.example.com/page",
                "http://subdomain.site.org:8080/path",
                "ftp://files.server.net/download"
            };
            
            string urlPattern = @"^(https?|ftp)://([^/:]+)(:\d+)?(/.*)?$";
            
            foreach (string url in urls)
            {
                Match match = Regex.Match(url, urlPattern);
                if (match.Success)
                {
                    Console.WriteLine($"URL: {url}");
                    Console.WriteLine($"  Protocol: {match.Groups[1].Value}");
                    Console.WriteLine($"  Domain:   {match.Groups[2].Value}");
                    Console.WriteLine($"  Port:     {(match.Groups[3].Success ? match.Groups[3].Value : "default")}");
                    Console.WriteLine($"  Path:     {(match.Groups[4].Success ? match.Groups[4].Value : "/")}");
                    Console.WriteLine();
                }
            }
        }

        static void Example6_PasswordValidation()
        {
            Console.WriteLine("--- Example 6: Password Validation ---");
            Console.WriteLine("Requirements: 8+ chars, uppercase, lowercase, digit, special char\n");
            
            string[] passwords = 
            {
                "Pass123!",
                "weakpass",
                "NOLOWERCASE123!",
                "NoSpecialChar123",
                "Valid@Pass123"
            };

            // At least 8 chars, 1 uppercase, 1 lowercase, 1 digit, 1 special char
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            
            foreach (string password in passwords)
            {
                bool isValid = Regex.IsMatch(password, passwordPattern);
                Console.WriteLine($"{password,-20} - {(isValid ? "? Strong" : "? Weak")}");
            }
            Console.WriteLine();
        }
    }
}
