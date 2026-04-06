using System;

namespace Uni_Lesson7
{
    /// <summary>
    /// Main demo runner - Execute all LINQ and Regular Expression examples
    /// </summary>
    public class DemoRunner
    {
        public static void RunAllDemos()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("??????????????????????????????????????????????????????????????");
                Console.WriteLine("?          LINQ & REGULAR EXPRESSIONS DEMO                   ?");
                Console.WriteLine("?                   Lesson 7 Examples                        ?");
                Console.WriteLine("??????????????????????????????????????????????????????????????");
                Console.WriteLine();
                Console.WriteLine("Select a demo to run:");
                Console.WriteLine();
                Console.WriteLine("  1. Regular Expression Examples");
                Console.WriteLine("  2. LINQ Basic Concepts");
                Console.WriteLine("  3. LINQ Features in C#");
                Console.WriteLine("  4. LINQ Samples");
                Console.WriteLine("  5. LINQ Operators Overview");
                Console.WriteLine("  6. IEnumerable vs IQueryable");
                Console.WriteLine("  7. Run All Demos");
                Console.WriteLine("  0. Exit");
                Console.WriteLine();
                Console.Write("Enter your choice: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        RunDemo("Regular Expression Examples", RegularExpressionDemo.RunExamples);
                        break;
                    case "2":
                        RunDemo("LINQ Basic Concepts", LinqBasicConcepts.RunExamples);
                        break;
                    case "3":
                        RunDemo("LINQ Features in C#", LinqFeatures.RunExamples);
                        break;
                    case "4":
                        RunDemo("LINQ Samples", LinqSamples.RunExamples);
                        break;
                    case "5":
                        RunDemo("LINQ Operators Overview", LinqOperatorsOverview.RunExamples);
                        break;
                    case "6":
                        RunDemo("IEnumerable vs IQueryable", IEnumerableVsIQueryable.RunExamples);
                        break;
                    case "7":
                        RunAllSequentially();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Thank you for using the demo! Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        WaitForUser();
                        break;
                }
            }
        }

        static void RunDemo(string title, Action demoAction)
        {
            Console.Clear();
            Console.WriteLine("????????????????????????????????????????????????????????????");
            Console.WriteLine($"  {title}");
            Console.WriteLine("????????????????????????????????????????????????????????????");
            Console.WriteLine();

            try
            {
                demoAction();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError occurred: {ex.Message}");
                Console.ResetColor();
            }

            WaitForUser();
        }

        static void RunAllSequentially()
        {
            Console.Clear();
            Console.WriteLine("????????????????????????????????????????????????????????????");
            Console.WriteLine("  Running All Demos Sequentially");
            Console.WriteLine("????????????????????????????????????????????????????????????");
            Console.WriteLine();

            var demos = new (string Title, Action Action)[]
            {
                ("1. Regular Expression Examples", RegularExpressionDemo.RunExamples),
                ("2. LINQ Basic Concepts", LinqBasicConcepts.RunExamples),
                ("3. LINQ Features in C#", LinqFeatures.RunExamples),
                ("4. LINQ Samples", LinqSamples.RunExamples),
                ("5. LINQ Operators Overview", LinqOperatorsOverview.RunExamples),
                ("6. IEnumerable vs IQueryable", IEnumerableVsIQueryable.RunExamples)
            };

            foreach (var demo in demos)
            {
                Console.WriteLine($"\n{'?',60:?>60}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"  {demo.Title}");
                Console.ResetColor();
                Console.WriteLine($"{'?',60:?>60}\n");

                try
                {
                    demo.Action();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nError in {demo.Title}: {ex.Message}");
                    Console.ResetColor();
                }

                Console.WriteLine("\n");
            }

            Console.WriteLine("????????????????????????????????????????????????????????????");
            Console.WriteLine("  All Demos Completed!");
            Console.WriteLine("????????????????????????????????????????????????????????????");
            WaitForUser();
        }

        static void WaitForUser()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press any key to continue...");
            Console.ResetColor();
            Console.ReadKey(true);
        }
    }
}
