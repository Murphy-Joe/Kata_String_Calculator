using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculatorBlank;
public class StringCalculator
{
    private readonly ILogger _logger;
    private readonly IWebService _webService;

    public StringCalculator(ILogger logger, IWebService webService)
    {
        _logger = logger;
        _webService = webService;
    }

    public int Add(string stringOfNumbers)
    {
        var nums = GenericNumArray(stringOfNumbers, NumsRegex);
        var negNums = GenericNumArray(stringOfNumbers, NegNumsRegex);

        if (negNums.Count() > 0)
            throw new Exception($"Negatives not Allowed {string.Join(" ", negNums)}");

        try
        {
            _logger.Write(nums.Sum().ToString());
        }
        catch (LoggerException ex)
        {

            _webService.Notify(ex.Message);
        }

        return nums.Sum();
    }

    private int[] NumsArray(string stringOfNumbers)
    {
        var findNums = new Regex(@"\d+");
        var numMatches = findNums.Matches(stringOfNumbers);
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

    private int[] GenericNumArray(string stringOfNumbers, regMe loadedRegex)
    {
        //Regex findNums = new Regex(@"\d+");
        var numMatches = loadedRegex().Matches(stringOfNumbers);
        var nums = numMatches
                    .Select(m => Convert.ToInt32(m.Value))
                    .ToArray();
        return nums;
    }

    private Regex NumsRegex()
    {
        return new Regex(@"\d+");
    }

    private Regex NegNumsRegex()
    {
        return new Regex(@"\d+");
    }


}

public delegate Regex regMe();
