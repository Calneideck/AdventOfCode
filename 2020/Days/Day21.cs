using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day21 : Day
    {
        string[] lines = File.ReadAllLines("Input/21.txt").ToArray();
        Dictionary<string, string> allergens = new Dictionary<string, string>();

        public override object Part1()
        {
            Dictionary<string, List<List<string>>> allergenPossibleIngredients = new Dictionary<string, List<List<string>>>();
            List<string> allIngredients = new List<string>();
            Regex reg = new Regex(@"[a-z]+");

            foreach (var line in lines)
            {
                var words = reg.Matches(line);
                bool gettingIngredients = true;

                List<string> ingredients = new List<string>();

                foreach (var word in words)
                    if (word.ToString() == "contains")
                        gettingIngredients = false;
                    else if (gettingIngredients)
                    {
                        ingredients.Add(word.ToString());
                        allIngredients.Add(word.ToString());
                    }
                    else
                    {
                        allergenPossibleIngredients.TryAdd(word.ToString(), new List<List<string>>());
                        allergenPossibleIngredients[word.ToString()].Add(ingredients);
                    }
            }

            while (allergens.Keys.Count != allergenPossibleIngredients.Keys.Count)
                foreach (var allergen in allergenPossibleIngredients)
                {
                    var allergenIngredients = allergen.Value.Aggregate((list, other) => list.Intersect(other).ToList()).Except(allergens.Values);
                    if (allergenIngredients.Count() == 1)
                        allergens.Add(allergen.Key, allergenIngredients.First());
                }

            return allIngredients.Count(i => !allergens.Values.Contains(i));
        }

        public override object Part2()
        {
            var _allergens = allergens.ToList();
            _allergens.Sort((a, b) => a.Key.CompareTo(b.Key));
            return string.Join(',', _allergens.Select(a => a.Value));
        }
    }
}
