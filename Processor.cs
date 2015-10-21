using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Laba1
{
    public enum Languages
    {
        Ru,
        Eng
    }

    public enum Keys
    {
        Straight,
        Progressive,
        Auto
    }

    public class Pair
    {
        public int Index { get; set; }
        public int Value { get; set; }
    }

    public struct Frequency
    {
        public char Letter;
        public int Count;
    }

    public class Doubles
    {
        public char Letter;
        public double Frequency;
    }

    /// <summary>
    /// Вычислительное ядро
    /// </summary>
    public class Processor
    {
        public static List<Button> Buttons = new List<Button>();
        public static List<TextBox> TextBoxs = new List<TextBox>();
        public static List<CheckBox> CheckBoxs = new List<CheckBox>();

        public static TextBox Goal, KasiskeKey, KasiskeDigrams;

        public static List<Doubles> RusDoubles = new List<Doubles>(), EngDoubles = new List<Doubles>();

        public static ProgressBar Bar;

        public const int MinDigramLength = 5; //+1 

        public static int DigramLength, KeyLen;

        public static Languages Lang;

        public static Keys Task;

        public static string Text, Key, Cipertext, AnalizeText, AnalizeResult, AnalizeLog;

        public const string
            BigRus = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ",
            SmlRus = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя",
            BigEng = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            SmlEng = "abcdefghijklmnopqrstuvwxyz";

        public static readonly SortedSet<char> RussianText = new SortedSet<char>
        {
            'А','Б','В','Г','Д','Е','Ё','Ж','З','И','Й','К','Л','М','Н','О','П','Р','С','Т','У','Ф','Х','Ц','Ч','Ш','Щ','Ъ','Ы','Ь','Э','Ю','Я',
            'а','б','в','г','д','е','ё','ж','з','и','й','к','л','м','н','о','п','р','с','т','у','ф','х','ц','ч','ш','щ','ъ','ы','ь','э','ю','я'
        };

        public static readonly SortedSet<char> EnglishText = new SortedSet<char>
        {
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'
        };

        public static int Nod(int a, int b)
        {
            while (true)
            {
                if (b == 0) return a;
                var a1 = a;
                a = b;
                b = a1%b;
            }
        }

        public static string KeyPreparation(string key, Keys keyType, string text, Languages lang)
        {
            switch (keyType)
            {
                case Keys.Straight:
                    while (key.Length < text.Length)
                    {
                        key += key;
                    }
                    if (key.Length > text.Length)
                    {
                        key = key.Substring(0, text.Length);
                    }
                    break;
                case Keys.Progressive:
                    var s = key;
                    while (key.Length < text.Length)
                    {
                        if (lang == Languages.Eng)
                        {
                            s = s.Where(c => c != ' ')
                                .Aggregate("", (current, c) => current + SmlEng[(c - 'a' + 1)%26]);
                        }
                        else
                        {
                            s = s.Where(c => c != ' ')
                                .Aggregate("",
                                    (current, c) => current + SmlRus[(Regex.Match(SmlRus, c.ToString()).Index + 1)%33]);
                        }
                        key += s;
                    }
                    if (key.Length > text.Length)
                    {
                        key = key.Substring(0, text.Length);
                    }
                    break;
                case Keys.Auto:
                    key += text.Substring(0, text.Length - key.Length);
                    break;
            }
            return key;
        }

        public static MatchCollection Find(string str, string substr)
        {
            try
            {
                return new Regex(substr, RegexOptions.IgnoreCase).Matches(str);
            }
            catch (Exception)
            {
                return new Regex("a").Matches("b");
            }
        }

        public static List<int> FindProgressive(string String, string subString)
        {
            var result = new List<int>();
            bool haveResult;
            var lastDigram = 0;
            do
            {
                haveResult = false;
                var subResult = new List<int>();
                var i = 0;
                while (i < (Lang == Languages.Ru ? SmlRus.Length : SmlEng.Length))
                {
                    var regex = new Regex(subString, RegexOptions.IgnoreCase);
                    var match = regex.Match(String, lastDigram);
                    if (match.Success)
                        subResult.Add(match.Index);
                    subString = IncString(subString);
                    i++;
                }
                subResult.Sort();
                if (subResult.Count == 0) continue;
                result.Add(subResult[0]);
                lastDigram = subResult[0] + DigramLength;
                haveResult = true;
            } while (haveResult);
            return result;
        }

        public static string IncString(string digram)
        {
            if (Lang == Languages.Ru)
                return digram.Where(char.IsLetter)
                    .Aggregate("",
                        (current, c) =>
                            current + SmlRus[(Regex.Match(SmlRus, c.ToString(), RegexOptions.IgnoreCase).Index + 1)%33]);
            return digram.Where(char.IsLetter)
                .Aggregate("",
                    (current, c) =>
                        current + SmlEng[(Regex.Match(SmlEng, c.ToString(), RegexOptions.IgnoreCase).Index + 1)%26]);
        }

        public static void DeactivateElements()
        {
            foreach (var Object in Buttons)
            {
                Object.IsEnabled = !Object.IsEnabled;
            }
            foreach (var Object in TextBoxs)
            {
                Object.IsEnabled = !Object.IsEnabled;
            }
            foreach (var Object in CheckBoxs)
            {
                Object.IsEnabled = !Object.IsEnabled;
            }
        }

        public static string GenerateKey(string text, int keyLength)
        {
            var key = "";
            var strings = new string[keyLength];
            for (var i = 0; i < text.Length; i++)
            {
                strings[i%keyLength] += Task == Keys.Straight
                    ? text[i]
                    : Lang == Languages.Ru
                        ? SmlRus[(SmlRus.IndexOf(char.ToLower(text[i])) + SmlRus.Length - (i/keyLength)%SmlRus.Length)%SmlRus.Length]
                        : SmlEng[(SmlEng.IndexOf(char.ToLower(text[i])) + SmlEng.Length - (i/keyLength)%SmlEng.Length)%SmlEng.Length];
            }
            for (var i = 0; i < keyLength; i++)
            {
                strings[i] = strings[i].ToLower();
            }
            for (var i = 0; i < keyLength; i++)
            {
                var frequency = new Frequency[SmlRus.Length];
                if (Lang == Languages.Ru)
                {
                    for (var j = 0; j < strings[i].Length; j++)
                    {
                        frequency[SmlRus.IndexOf(strings[i][j])].Count++;
                        frequency[SmlRus.IndexOf(strings[i][j])].Letter = strings[i][j];
                    }
                    frequency = frequency.OrderByDescending(p => p.Count).ToArray();
                    var number =
                        Math.Abs(SmlRus.IndexOf(frequency[0].Letter) + 33 - SmlRus.IndexOf(RusDoubles[0].Letter))%33;
                    key += SmlRus[number];
                }
                else
                {
                    for (var j = 0; j < strings[i].Length; j++)
                    {
                        frequency[SmlEng.IndexOf(strings[i][j])].Count++;
                        frequency[SmlEng.IndexOf(strings[i][j])].Letter = strings[i][j];
                    }
                    frequency = frequency.OrderByDescending(p => p.Count).ToArray();
                    var number =
                        Math.Abs(SmlEng.IndexOf(frequency[0].Letter) + 26 - SmlEng.IndexOf(EngDoubles[0].Letter)) % 26;
                    key += SmlEng[number];
                }
            }
            return key;
        }
    }
}