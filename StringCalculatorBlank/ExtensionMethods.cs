using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StringCalculatorBlank
{
    public class ExtensionMethods
    {
        [Fact]
        public void Banana()
        {
            int myAge = 11;

            var nums = new List<int>() { 1,2,3,4,5};
            IEnumerable<int> evens = nums.Where(x => x % 2 == 0);

            Console.WriteLine(evens);

            // Extension MEthods
            Assert.True(myAge.IsEven());
        }
    }

    public static class Utils
    {
        public static bool IsEven(this int x)
        {
            return x % 2 == 0;
        }
    }
}
