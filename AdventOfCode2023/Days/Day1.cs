namespace AdventOfCode2023.Days;

public class Day1 : Day
{
    private static readonly Dictionary<string, int> _digits = new ()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }
    };

    public override void PerformCalculations(IEnumerable<string> input)
    {
        CalculateAndLogTime(() =>
        {
            int result = CalculateResult1(input);
            Console.WriteLine();
            Console.WriteLine($"Result 1: {result}");
        });

        CalculateAndLogTime(() =>
        {
            int result = CalculateResult2(input);
            Console.WriteLine();
            Console.WriteLine($"Result 2: {result}");
        });

        Console.WriteLine();
    }

    private static int CalculateResult1(IEnumerable<string> input)
    {
        return input.Sum(line => CalculateCalibrationValue(line));
    }

    private static int CalculateCalibrationValue(string line)
    {
        int firstDigit = 0;
        int secondDigit = 0;

        for (int i = 0; i < line.Length; ++i)
        {
            if (!char.IsDigit(line[i]))
            {
                continue;
            }

            firstDigit = line[i] - '0';
            break;
        }

        for (int i = line.Length - 1; i >= 0; --i)
        {
            if (!char.IsDigit(line[i]))
            {
                continue;
            }

            secondDigit = line[i] - '0';
            break;
        }

        return 10 * firstDigit + secondDigit;
    }

    private static int CalculateResult2(IEnumerable<string> input)
    {
        return input.Sum(line => CalculateCalibrationValueCorrectly(line));
    }

    private static int CalculateCalibrationValueCorrectly(string line)
    {
        int firstDigit = 0;
        int secondDigit = 0;

        for (int i = 0; i < line.Length; ++i)
        {
            if (char.IsDigit(line[i]))
            {
                firstDigit = line[i] - '0';
                break;
            }

            string substring = line.Substring(i);
            bool canFinish = false;

            foreach(string digit in _digits.Keys)
            {
                if(substring.StartsWith(digit))
                {
                    firstDigit = _digits[digit];
                    canFinish = true;
                    break;
                }
            }

            if (canFinish)
            {
                break;
            }
        }

        for (int i = line.Length - 1; i >= 0; --i)
        {
            if (char.IsDigit(line[i]))
            {
                secondDigit = line[i] - '0';
                break;
            }

            string substring = line.Substring(i);
            bool canFinish = false;

            foreach (string digit in _digits.Keys)
            {
                if (substring.StartsWith(digit))
                {
                    secondDigit = _digits[digit];
                    canFinish = true;
                    break;
                }
            }

            if (canFinish)
            {
                break;
            }
        }

        return 10 * firstDigit + secondDigit;
    }
}
