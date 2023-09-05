using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace HaikuDetectorCS
{
    public class HaikuDetector
    {
        public string DefaultDirectoryPath { get; }

        public const int NumberOfLinesInHaiku = 3;
        public static readonly int[] HaikuSyllablesPerLine = new int[] { 5, 7, 5 };

        public static class HaikuRules
        {
            public const string InvalidLength = "poem is not exactly three lines long.";
            public const string IncorrectNumberOfSyllables = "poem has the incorrect number of syllables in line";
        }

        public HaikuDetector()
        {
            DefaultDirectoryPath = $"{Environment.CurrentDirectory}/Data";
        }

        private int GetNumberOfSyllablesInLine(string line)
        {
            int numberOfSyllables = 0;
            Regex regEx = new Regex(@"[aeiouy]+", RegexOptions.IgnoreCase);

            string[] words = line.Split(' ');

            foreach (string word in words)
            {
                var numberOfSyllablesInWord = regEx.Matches(word).Count;

                if (word.EndsWith('e') && numberOfSyllablesInWord > 1)
                    numberOfSyllablesInWord--;

                numberOfSyllables += numberOfSyllablesInWord;
            }

            return numberOfSyllables;
        }

        public string ReadFromFile(string file)
        {
            string[] lines = File.ReadAllLines(file);

            if (lines.Length != NumberOfLinesInHaiku)
            {
                return $"Not a haiku poem because {HaikuRules.InvalidLength}";
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (GetNumberOfSyllablesInLine(lines[i]) != HaikuSyllablesPerLine[i])
                    return $"Not a haiku poem because {HaikuRules.IncorrectNumberOfSyllables} {i + 1}.";
            }

            return "Valid haiku";
        }

        public string[] ReadAllFromDirectory(string path)
        {
            List<string> messages = new List<string>();

            foreach (string file in Directory.EnumerateFiles(path ?? DefaultDirectoryPath, "*.txt"))
            {
                string message = ReadFromFile(file);
                messages.Add(message);
            }

            return messages.ToArray();
        }
    }
}
