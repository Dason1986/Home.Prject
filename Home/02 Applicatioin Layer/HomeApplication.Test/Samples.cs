using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.Test
{
    [TestFixture(Category = "樣例")]
    public class Samples
    {
        //[Platform(Exclude="Win98,WinME")]
        //https://github.com/nunit/docs/wiki/Platform-Attribute

        [Test, Sequential]
        public void TestSequential([Values(1, 2, 3)] int x, [Values("A", "B")] string s)
        {
            Console.WriteLine("{0} {1}  ", x, s);
        }

        [Test, Pairwise]
        public void TestPairwise([Values("a", "b", "c")] string a, [Values("+", "-")] string b, [Values("x", "y")] string c)
        {
            Console.WriteLine("{0} {1} {2}", a, b, c);
        }

        [Test, Combinatorial]
        public void TestCombinatorial([Values(1, 2, 3)] int x, [Values("A", "B")] string s)
        {
            Console.WriteLine("{0} {1}  ", x, s);
        }

        [Test]
        public void TestValues([Values(1, 2, 3)] int x, [Values("A", "B")] string s)
        {
        }

        [Test]
        public void TestRange([Values(1, 2, 3)] int x, [Range(0.2, 0.6, 0.2)] double d)
        {
            Console.WriteLine("{0} {1}  ", x, d);
        }

        [Test, TestCaseSource(typeof(MyDataClass), "TestCases")]
        public int DivideTest(int n, int d)
        {
            return n / d;
        }

        [Test, TestCaseSource(typeof(MyDataClass), "TestCases2")]
        public int DivideTest2(int n, int d, int c, int a)
        {
            return n / d;
        }

        [Test, TestCaseSource(typeof(MyDataClass), "TestCases3")]
        public void TestClassObj(TestClassObj a)
        {
            Console.WriteLine(a.Name);
        }
    }

    public class MyDataClass
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(12, 3).Returns(4);
                yield return new TestCaseData(12, 2).Returns(6);
                yield return new TestCaseData(12, 4).Returns(3);
            }
        }

        public static IEnumerable TestCases2
        {
            get
            {
                yield return new TestCaseData(12, 3, 4, 5).Returns(4);
                yield return new TestCaseData(12, 2, 5, 9).Returns(6);
                yield return new TestCaseData(12, 4, 10, 95).Returns(3);
            }
        }

        public static IEnumerable TestCases3
        {
            get
            {
                yield return new TestCaseData(new TestClassObj { Name = "A1", Action = "+" });
                yield return new TestCaseData(new TestClassObj { Name = "A2", Action = "-" });
                yield return new TestCaseData(new TestClassObj { Name = "A3", Action = "*" });
            }
        }
    }

    public class TestClassObj
    {
        public string Name { get; set; }
        public string Action { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}