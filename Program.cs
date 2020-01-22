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
            Console.SetWindowSize(40, 10);
            Console.SetBufferSize(40, 10);
            _mathManager = new MathManager();

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
            Console.Clear();
            Console.SetWindowSize(120, 30);
            Console.SetBufferSize(120, 30);
            string lines="<=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=->";
            Console.WriteLine($"{lines} <o> S U M M A R Y <o> {lines}");
            
            Console.SetCursorPosition(0, 1);
            Console.Write("Total");

            Console.SetCursorPosition(0, 2);
            Console.Write(summary.ChallengeCount);

            Console.SetCursorPosition(16, 1);
            Console.Write("Duration");

            Console.SetCursorPosition(16, 2);
            Console.Write(summary.ChallengeDuration.ToString(@"hh\:mm\:ss"));

            Console.SetCursorPosition(25, 1);
            Console.Write($"True");

            Console.SetCursorPosition(25, 2);
            Console.Write(summary.TrueCount);

            Console.SetCursorPosition(37, 1);
            Console.Write("False");

            Console.SetCursorPosition(37, 2);
            Console.Write(summary.FalseCount);

            Console.SetCursorPosition(50, 1);
            Console.Write("Fastest");

            Console.SetCursorPosition(50, 2);
            Console.Write(summary.FastestChallenge.Duration.ToString(@"hh\:mm\:ss"));

            Console.SetCursorPosition(67, 1);
            Console.Write("Fastest");

            Console.SetCursorPosition(67, 2);
            Console.Write(summary.FastestChallenge.Question);

            Console.SetCursorPosition(85, 1);
            Console.Write("Longest");

            Console.SetCursorPosition(85, 2);
            Console.Write(summary.LongestChallenge.Duration.ToString(@"hh\:mm\:ss"));

            Console.SetCursorPosition(102, 1);
            Console.Write("Longest");

            Console.SetCursorPosition(102, 2);
            Console.Write(summary.LongestChallenge.Question);

            WriteList(summary.AllChallenges);
            Console.ReadLine();
        }
        static void WriteList(List<Challenge> challenges)
        {
            //challenges[0].OrderNumber;
            //challenges[0].Duration;
            //challenges[0].Question;
            //challenges[0].InputNumber;
            //challenges[0].IsAnswerCorrect;

            string[] titles =  {"Number","Duration","Question","Answer","Status" };
            var titlesWidth = titles.Sum(p=>p.Length) + titles.Length;
            var width = Console.WindowWidth / titles.Length;
            width++;
            
            
            
            for (int row = 0; row <= challenges.Count; row++)
            {
                for (int i = 0; i < titles.Length; i++)
                {
                    Console.SetCursorPosition(i * width, row+4);
                    Console.Write(titles[i]);
                }
                if (row == challenges.Count)
                    break;
                titles[0] = challenges[row].OrderNumber.ToString();
                titles[1] = challenges[row].Duration.ToString();
                titles[2] = challenges[row].Question.ToString();
                titles[3] = challenges[row].InputNumber.ToString();
                titles[4] = challenges[row].IsAnswerCorrect.ToString();

            }

        }
    }
}
