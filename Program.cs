using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace BasicMathConsole
{
    class Program
    {
        private static bool _programStatus = true;
        private static MathManager _mathManager;
        static void Main(string[] args)
        {
            ArchiveManager archiveManager = new ArchiveManager();
            var sum =archiveManager.GeneralSummary(archiveManager.ImportFiles(Environment.CurrentDirectory + "\\database"));
            var orderedSum = sum.OrderByDescending(o => o.AskCount);
            foreach (var item in orderedSum)
            {
                Console.WriteLine($"{item.AskCount} {item.Question} {item.BestTime} {item.AvgDuration} {item.WorstTime}");
            }
            Console.ReadLine();
            _mathManager = new MathManager();
            for(int i=3; i > 0; i--)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write($"{i} {i} {i} {i} {i} {i} {i} {i} {i} {i} {i}");
                Thread.Sleep(1000);
                Console.Clear();
            }

            while (_programStatus)
            {
                var challenge = _mathManager.GetChallenge();
                Console.Write(challenge.Question);
                var input = Console.ReadLine();
                if (input == string.Empty)
                {
                    break;
                }
                int.TryParse(input, out int inputNumber);
                challenge.InputNumber = inputNumber;
                var result = _mathManager.SaveAnswer(challenge);
                Console.WriteLine(result.ResultText);
                Console.ReadLine();
            }
            try
            {
                var summary = _mathManager.Finalize();
                WriteSummary(summary);
            }
            catch (Exception exception)
            {

                Console.WriteLine(exception.Message);
            }
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();

        }
        static void WriteSummary(Summary summary)
        {
            start:
            WriteHeader(summary);
            WriteList(summary.AllChallenges);
            WriteToFile(summary);
            var input = Console.ReadLine();
            if(input ==" ")
            {
                goto start;
            }
        }

        private static void WriteToFile(Summary summary)
        {
            var json = JsonConvert.SerializeObject(summary, Formatting.Indented);
            var path = Directory.CreateDirectory(Environment.CurrentDirectory + "\\database");
            File.WriteAllText(path + "\\" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".json", json);
        }

        private static void WriteHeader(Summary summary)
        {
            Console.Clear();

            string[] titles = { "Total", "Duration", "Avg Dur","Fast","Slow", "True",
                "False", "Fastest","Fastest","Slowest","Slowest" };
            var titlesWidth = titles.Sum(p => p.Length) + titles.Length;
            var width = Console.WindowWidth / titles.Length;
            width++;


            for (int row = 0; row <= 2; row++)
            {
                SetColor(row);

                for (int i = 0; i < titles.Length; i++)
                {

                    Console.SetCursorPosition(i * width, row);
                    Console.Write(titles[i]);
                }
                if (row == 1)
                {
                    SetDefaultColor();
                    break;
                }
                titles[0] = summary.ChallengeCount.ToString();
                titles[1] = summary.ChallengeDuration.ToString(@"hh\:mm\:ss");
                titles[2] = summary.AvgDuration.ToString(@"hh\:mm\:ss");
                titles[3] = summary.AnswerCountByLowersFromAvg.ToString();
                titles[4] = summary.AnswerCountByUppersFromAvg.ToString();
                titles[5] = summary.TrueCount.ToString();
                titles[6] = summary.FalseCount.ToString();
                titles[7] = summary.FastestChallenge.Question;
                titles[8] = summary.FastestChallenge.Duration.ToString(@"hh\:mm\:ss");
                titles[9] = summary.SlowestChallenge.Question;
                titles[10] = summary.SlowestChallenge.Duration.ToString(@"hh\:mm\:ss");
            }
        }

        static void WriteList(List<Challenge> challenges)
        {
            var startRow = 2;
            string[] titles = { "Number", "Duration", "Question", "Given Answer", "Status" };
            var titlesWidth = titles.Sum(p => p.Length) + titles.Length;
            var width = Console.WindowWidth / titles.Length;
            width++;

            for (int row = 0; row <= challenges.Count; row++)
            {
                SetColor(row,startRow);
                for (int i = 0; i < titles.Length; i++)
                {

                    Console.SetCursorPosition(i * width, row + startRow);
                    Console.Write(titles[i]);
                }
                if (row == challenges.Count)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                titles[0] = challenges[row].OrderNumber.ToString();
                titles[1] = challenges[row].Duration.ToString(@"hh\:mm\:ss\.ff");
                titles[2] = challenges[row].Question.ToString();
                titles[3] = challenges[row].InputNumber.ToString();
                titles[4] = challenges[row].IsAnswerCorrect.ToString();

            }

        }

        private static void SetDefaultColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void SetColor(int row,int startRow=0)
        {
            if (row == 0)
            {
                FillHeaderColor(row +startRow);
            }
            if (row % 2 == 0 && row != 0)
            {
                FillOddRowColor(row + startRow);

            }
            else if (row != 0)
            {
                FillSameRowColor(row + startRow);

            }
        }

        private static void FillSameRowColor(int row)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            string space = " ";
            for (int z = 0; z < Console.WindowWidth - 1; z++)
            {
                space += " ";
            }
            Console.SetCursorPosition(0, row);
            Console.Write(space);
        }

        private static void FillOddRowColor(int row)
        {
            string space = " ";
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            for (int z = 0; z < Console.WindowWidth - 1; z++)
            {
                space += " ";
            }
            Console.SetCursorPosition(0, row);
            Console.Write(space);
        }

        private static void FillHeaderColor(int row)
        {
            string space = " ";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int z = 0; z < Console.WindowWidth - 1; z++)
            {
                space += " ";
            }
            Console.SetCursorPosition(0, row);
            Console.Write(space);
        }
    }
}
