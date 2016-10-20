using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.Test
{
    [TestFixture]
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
    }
}
