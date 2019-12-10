using System.Collections.Generic;

namespace AdventOfCode
{
    class Day4 : Day
    {
        const int min = 147981;
        const int max = 691423;

        private List<int> passwords = new List<int>();

        public override string Part1()
        {
            for (int number = min; number <= max; number++)
            {
                bool good = true;

                // Increasing
                for (int i = 0; i < 5; i++)
                    if (PlaceValue(number, i) > PlaceValue(number, i + 1))
                    {
                        good = false;
                        break;
                    }

                if (!good)
                    continue;

                // Doubles
                good = false;
                for (int i = 0; i < 5; i++)
                {
                    if (PlaceValue(number, i) == PlaceValue(number, i + 1))
                    {
                        good = true;
                        break;
                    }
                }

                if (good)
                    passwords.Add(number);
            }

            return passwords.Count.ToString();
        }

        public override string Part2()
        {
            int count = 0;

            foreach (int number in passwords)
                for (int i = 1; i <= 4; i++)
                {
                    bool leftMatch = PlaceValue(number, i) == PlaceValue(number, i - 1);
                    bool rightMatch = PlaceValue(number, i) == PlaceValue(number, i + 1);

                    if (leftMatch == rightMatch)
                        continue;

                    if (leftMatch && i > 1)
                        if (PlaceValue(number, i) == PlaceValue(number, i - 2))
                            continue;

                    if (rightMatch && i < 4)
                        if (PlaceValue(number, i) == PlaceValue(number, i + 2))
                            continue;

                    ++count;
                    break;
                }

            return count.ToString();
        }

        int PlaceValue(int num, int place)
        {
            return int.Parse(num.ToString()[place].ToString());
        }
    }
}
