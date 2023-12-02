namespace AdventOfCode2023.Days;

public class Day2 : Day
{
    private const int GAME_PREFIX_LENGTH = 5;

    public override void PerformCalculations(IEnumerable<string> input)
    {
        List<Game> games = input.Select(line => ReadGame(line)).ToList();

        CalculateAndLogTime(() =>
        {
            int result = CalculateResult1(games);
            Console.WriteLine();
            Console.WriteLine($"Result 1: {result}");
        });

        CalculateAndLogTime(() =>
        {
            int result = CalculateResult2(games);
            Console.WriteLine();
            Console.WriteLine($"Result 2: {result}");
        });

        Console.WriteLine();
    }

    private static int CalculateResult1(List<Game> games)
    {
        return games.Where(game => game.CanBePlayed(12, 13, 14)).Sum(game => game.Id);
    }

    private static Game ReadGame(string line)
    {
        int indexOfColon = line.IndexOf(':');
        int id = int.Parse(line[GAME_PREFIX_LENGTH..indexOfColon]);


        string setsSubsting = line[(indexOfColon + 1)..];

        string[] setsStrings = setsSubsting.Split(';');
        List<Set> sets = setsStrings.Select(s => ReadSet(s)).ToList();

        return new(id, sets);
    }

    private static Set ReadSet(string text)
    {
        Set set = new();
        string[] colors = text.Split(',');

        foreach (string color in colors)
        {
            string[] words = color.Split(' ').Skip(1).ToArray();
            set.Cubes[Enum.Parse<Color>(words[1])] = int.Parse(words[0]);
        }

        return set;
    }

    private static int CalculateResult2(List<Game> games)
    {
        return games.Select(game => game.FewestPossiblePlayableSet()).Sum(set => set.Power);
    }

    private enum Color
    {
        red = 0,
        green = 1,
        blue = 2,
    }

    private class Set
    {
        private Dictionary<Color, int> cubes = new()
        {
            { Color.red, 0 },
            { Color.green, 0 },
            { Color.blue, 0 },
        };

        public Dictionary<Color, int> Cubes => this.cubes;

        public int Power => this.cubes[Color.red] * this.cubes[Color.green] * this.cubes[Color.blue];
    }

    private class Game
    {
        public int Id { get; init; }
        public List<Set> Sets { get; init; }

        public Game(int id, List<Set> sets)
        {
            this.Id = id;
            this.Sets = sets;
        }

        public bool CanBePlayed(int redLimit, int greenLimit, int blueLimit)
        {
            return this.Sets.All(set => set.Cubes[Color.red] <= redLimit
                && set.Cubes[Color.green] <= greenLimit
                && set.Cubes[Color.blue] <= blueLimit);
        }

        public Set FewestPossiblePlayableSet()
        {
            Set fewestSet = new();

            foreach (Color color in fewestSet.Cubes.Keys)
            {
                fewestSet.Cubes[color] = this.Sets.Select(set => set.Cubes[color]).Max();
            }

            return fewestSet;
        }
    }
}
