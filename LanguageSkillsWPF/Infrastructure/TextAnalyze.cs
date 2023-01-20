using LanguageSkillsWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace LanguageSkillsWPF.Infrastructure
{
    public static class TextAnalyze
    {
        private static TextExtractor extractor;

        static TextAnalyze() { extractor = new TextExtractor(); }

        private static bool IsRussianSymbols(string text) => !Regex.IsMatch(text, @"\P{IsCyrillic}");


        private static bool IsValidWord(string text) => 
            (text.IndexOfAny(".,/?!@;:#$%^&*()_-+=[]{}<>|'\\1234567890".ToCharArray()) == -1) &&
            (text != string.Empty) &&
            (text.Length > 1);

        public static IEnumerable<SearchEntryModel> GetTopNWords(string file, int n)
        {
            var text = extractor.Extract(file).Trim();
            var orderedWords = text.ToLower()
                .Split(' ', '.', ',', '-', '?', '!', '<', '>', '&', '[', ']', '(', ')', '\n', '\t')
                .GroupBy(x => x)
                .Select(x => new SearchEntryModel { Word = x.Key.Trim(), Count = x.Count() })
                .Where(x => TextAnalyze.IsValidWord(x.Word) && TextAnalyze.IsRussianSymbols(x.Word))
                .OrderByDescending(x => x.Count);
               // .Take(n);
            return orderedWords;
        }
    }
}
