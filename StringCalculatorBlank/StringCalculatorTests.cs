using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace StringCalculatorBlank
{
    // https://osherove.com/tdd-kata-1
    public class StringCalculatorTests
    {
        private StringCalculator calculator { get; } = new StringCalculator();

        [Fact]
        public void EmptyStringReturnsZero()
        {
            int actual = calculator.Add("");

            Assert.Equal(0, actual);
        }

        [Theory]
        [InlineData("4", 4)]
        [InlineData("4,2", 6)]
        [InlineData("1,2,3,4,5,5,4,3,2,1", 30)]
        [InlineData("1/n2/r3,4", 10)]
        [InlineData("//;\n1;2", 3)]
        public void StringIntegersGetSummed(string input, int expectedSum)
        {
            int actualSum = calculator.Add(input);

            Assert.Equal(expectedSum, actualSum);
        }

        [Fact]
        public void NegativeNumbersThrowEx()
        {
            var ex = Assert.Throws<Exception>(() => calculator.Add("1,2,-3,-4,5"));
            Assert.Equal("Negatives not Allowed -3 -4", ex.Message);
        }
    }

    public class StringCalculator
    {
        public int Add(string stringOfNumbers)
        {
            var nums = NumsArray(stringOfNumbers);
            var negNums = NegNumsArray(stringOfNumbers);

            if (negNums.Count() > 0) 
                throw new Exception($"Negatives not Allowed {string.Join(" ", negNums)}");

            return nums.Sum();
        }

        private int[] NumsArray(string stringOfNumbers)
        {
            var findNums = new Regex(@"\d+");
            var numMatches =  findNums.Matches(stringOfNumbers);
            var nums = numMatches
                        .Select(m => Convert.ToInt32(m.Value))
                        .ToArray();
            return nums;
        }

        private int[] NegNumsArray(string stringOfNumbers)
        {
            var findNums = new Regex(@"-\d+");
            var negNumMatches = findNums.Matches(stringOfNumbers);
            var negNums = negNumMatches
                        .Select(m => Convert.ToInt32(m.Value))
                        .ToArray();
            return negNums;
        }
    }
}
