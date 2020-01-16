using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BasicMathConsole
{
    class Program
    {
        private static Random _random;
        private static bool _programStatus = true;
        private static List<bool> _answers;
        private static DateTime _startTime;
        private static DateTime _endTime;
        private static TimeSpan _shortestTime;
        private static TimeSpan _longestTime;
        private static string _shortestQuestion;
        private static string _longestQuestion;
        private static bool _isFirst = true;
        static void Main(string[] args)
        {
            Console.SetWindowSize(40, 10);
            Console.SetBufferSize(40, 10);
            
            _random = new Random();
            _answers = new List<bool>();
            _startTime = DateTime.Now;
            
            while (_programStatus)
            {
                GetSumLesson();
            }
            _endTime = DateTime.Now;
            var correctAnswer = _answers.Count(p => p == true);
            var incorrectAnswer = _answers.Count(p => p == false);
            var total = _answers.Count();
            var duration = _endTime - _startTime;

           
            Console.WriteLine($"Total Duration...: {duration.ToString()}");
            Console.WriteLine($"Total............: {total}");
            Console.WriteLine($"Correct Answer...: {correctAnswer}");
            Console.WriteLine($"Incorrect Answer.: {incorrectAnswer}");
            Console.WriteLine($"Shortest.........: {_shortestQuestion} - {_shortestTime}");
            Console.WriteLine($"Longest.........: {_longestQuestion} - {_longestTime}");
            Console.ReadKey();
            Console.WriteLine("Thanks you!");
           
            Thread.Sleep(500);
        }
        // TODO basamak must be written in English
        private static void GetSumLesson()
        {
            var startTime = DateTime.Now;
            var number1= _random.Next(0, 10);
            var number2 = _random.Next(0, 10);
            string question = $"{number1} + {number2}=";
            var result = false;
            Console.Write(question);
            var input = Console.ReadLine();
            if (input == "")
            {
                _programStatus = false;
            }
            else
            {
                Int32.TryParse(input, out int inputNumber);
                var total = number1 + number2;
                if (inputNumber == total)
                {
                    Console.WriteLine("true!");
                    result = true;
                    _answers.Add(result);
                    var duration = DateTime.Now - startTime;
                    if (_isFirst)
                    {
                        _shortestTime = duration;
                        _longestTime = duration;
                        _shortestQuestion = question;
                        _longestQuestion = question;
                        _isFirst = false;

                    }
                    if (_shortestTime > duration)
                    {
                        _shortestTime = duration;
                        _shortestQuestion = question;
                    }
                    if (_longestTime < duration)
                    {
                        _longestTime = duration;
                        _longestQuestion = question;
                    }
                    
                }
                else
                {
                    Console.WriteLine($"{total}, your answer is uncorrect");
                    _answers.Add(result);
                }
            }
            
        }
    }
}
