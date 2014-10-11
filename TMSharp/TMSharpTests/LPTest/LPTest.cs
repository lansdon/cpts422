using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LPTest
{
    /// <summary>
    /// Any class containing test cases must have this attribute.
    /// !!! WARNING: A test class must have a public constructor that takes no args
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class LPTestClass : System.Attribute
    {
    }

    /// <summary>
    /// Any test case in a LPTestClass must have this attribute.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class LPTestMethod : System.Attribute
    { 
    }

    /// <summary>
    /// Assert class is used to test the condition of a test case.
    /// !! Warning - You MUST use this class or the result calculations won't be correct.
    /// </summary>
    static public class Assert
    {
        static public bool IsTrue(bool condition) { return IsTrue(condition, ""); }
        static public bool IsFalse(bool condition) { return IsTrue(!condition, ""); }
        static public bool IsFalse(bool condition, String msg) { return IsTrue(!condition, msg); }
        static public bool Fail() { return IsTrue(false, ""); }
        static public bool Fail(String msg) { return IsTrue(false, msg); }

        static public bool IsTrue(bool condition, String msg)
        {
            if (!condition)
            {
                LPTest._AddFailedTest(msg);
                return false;
            }
            return true;
        }

    }

    /// <summary>
    /// This is a custom testing class. It will search for all classes with LPTestClass attribute.
    /// Inside those classes, it will run all the methods with LPTestMethod attribute.
    /// </summary>
    public class LPTest
    {
        private static int succeeded = 0;
        private static int failed = 0;
        private static bool testInProgress = false; // Assert class sets this to false on failure

        /// <summary>
        /// Call this to run all LPTest Class/Methods
        /// </summary>
        static public void RunTests()
        {
            succeeded = failed = 0;

            List<MethodInfo> tests = FindAllTests();

            // Print Header
            Console.WriteLine("*****************************************************");
            Console.WriteLine("****                   LP Test                    ***");
            Console.WriteLine("*****************************************************");
            Console.WriteLine("Found {0} tests.", tests.Count());

            // Run + Print all tests
            int testNum = 0;
            foreach (MethodInfo method in tests)
            {
                RunTest(method, ref testNum);
            }

            // Print Results
            Console.WriteLine("*****************************************************");
            Console.WriteLine("****                   Results                    ***");
            Console.Write("****         Succeeded: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0}", succeeded);
            Console.ResetColor();
            Console.Write("\tFailed: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("{0}", failed);
            Console.ResetColor();
            Console.Write("         ***\n");
            Console.WriteLine("*****************************************************");
            Console.WriteLine("");
            Console.ReadKey();
        }

        /// <summary>
        /// Find every class with LPTestClass attribute. Inside those classes, find every method with LPTestMethod attribute.
        /// </summary>
        /// <returns></returns>
        static private List<MethodInfo> FindAllTests()
        {
            // Search for Classes with LPTestClass attribute that contain methods with LPTestMethod
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<MethodInfo> tests = new List<MethodInfo>();
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(LPTestClass), true).Length > 0)
                {
                    var methods = type.GetMethods();

                    // Search the test class for all methods with LPTestMethod attribute.
                    foreach (var method in methods)
                    {
                        if (method.GetCustomAttributes(typeof(LPTestMethod), true).Length > 0)
                        {
                            tests.Add(method);
                        }
                    }
                }
            }
            return tests;
        }

        private static void RunTest(MethodInfo method, ref int testNum)
        {
            testInProgress = true;
            Console.Write("{0}: {1} ...... ", ++testNum, method.Name);
            Type type = method.DeclaringType;
            var instance = Activator.CreateInstance(type);
            method.Invoke(instance, new object[] { });

            // The Assert class will set this var to false if a failure occurs
            if (testInProgress)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("OK!\n");
                Console.ResetColor();
                testInProgress = false;
                ++succeeded;
            }
        }

        // DO NOT USE!
        // This is used by the Assert class to update test failures. 
        static public void _AddFailedTest(string msg)
        {
            ++failed;
            testInProgress = false;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("FAIL!");
            Console.ResetColor();
            if (msg != null && msg.Length > 0)
                Console.Write(" ({0})", msg);
            Console.Write("\n");
        }

    }
}
