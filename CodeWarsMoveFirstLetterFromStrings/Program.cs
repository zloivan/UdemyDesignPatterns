using System;
using System.Linq;

namespace CodeWarsMoveFirstLetterFromStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string testStr = "Some cool string 1234  check this out ,,,+++";

            var result = StringMover.ManipulateViaLinq(testStr);
            testStr = StringMover.MoveNumberOfLettersToEnd(testStr, 1);
            Console.WriteLine(testStr);
            Console.WriteLine($"result: {result}");

            testStr = StringMover.AddForEachWordSomeString(testStr, "ay");

            Console.WriteLine(testStr);
        }
    }

    public static class StringMover
    {
        public static string MoveNumberOfLettersToEnd(string originString, int numberOfLettersToMove)
        {
            if (numberOfLettersToMove < 0)
            {
                return originString;
            }

            var words = originString.GetWords();

            for (int index = 0; index < words.Length; index++)
            {
                if (words[index].Length < numberOfLettersToMove)
                {
                    continue;
                }

                var lettersToMove = words[index].Substring(0, numberOfLettersToMove);
                var lettersThatPersist =
                    words[index].Substring(numberOfLettersToMove); //remove that letter from the word

                words[index] = lettersThatPersist + lettersToMove;
            }

            return string.Join(" ", words);
        }

        public static string AddForEachWordSomeString(string originString, string wordEnding)
        {
            var tempString = originString.GetWords();

            string[] result = new string[tempString.Length];

            for (var index = 0; index < result.Length; index++)
            {
                result[index] = $"{tempString[index]}{wordEnding}";
            }


            return string.Join(" ", result);
        }

        static string[] GetWords(this string unSplitedString)
        {
            var tempString = unSplitedString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return tempString;
        }

        public static string ManipulateViaLinq(string targetString)
        {
            return string.Join(" ",
                targetString.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(word => word.Substring(1) + word[0] + "ay"));
        }
    }
}