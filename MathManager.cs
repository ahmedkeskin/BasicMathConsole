using BasicMathConsole.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicMathConsole
{
    public class MathManager
    {
        private List<Challenge> _challenges;
        private Random _random;
        ChallengeSettings _cSettings;
        public MathManager()
        {
            _challenges = new List<Challenge>();

            var challenge = new Challenge();
            _random = new Random();
            _cSettings = new ChallengeSettings();
            _cSettings.ChallengeCount = 1;
            _cSettings.MaxNumber = 9;
            _cSettings.MinNumber = 0;
            _cSettings.Operation = new Sum();
            GenerateChallenge(_cSettings);
        }

        public Challenge GetChallenge()
        {
            var challenge= _challenges.Where(w => w.InputNumber == null).FirstOrDefault();
            if(challenge==null)
            {
                GenerateChallenge(_cSettings);
                challenge = _challenges.Where(w => w.InputNumber == null).FirstOrDefault();
            }
            challenge.StartTime = DateTime.Now;
            return challenge;
        }

        public Challenge SaveAnswer(Challenge challenge)
        {
            challenge.AnswerTime = DateTime.Now;
            challenge.Duration = challenge.AnswerTime - challenge.StartTime;
            if (challenge.InputNumber == challenge.Result)
            {
                challenge.IsAnswerCorrect = true;
                challenge.ResultText = "True!";
            }
            else
            {
                challenge.IsAnswerCorrect = false;
                challenge.ResultText = $"False, the answer is {challenge.Result}";
            }
            _challenges.Where(p => p.OrderNumber == challenge.OrderNumber).ToList().ForEach(i => i = challenge);
            return challenge;
        }
        public Summary Finalize()
        {
            var summary = new Summary();
            
            summary.AllChallenges = _challenges.Where(w => w.InputNumber != null).OrderBy(o=>o.Duration).ToList();
            if (summary.AllChallenges.Count == 0)
                throw new Exception("Zero answer");
            summary.ChallengeCount = summary.AllChallenges.Count;
            var firstChallenge = _challenges.Where(w=>w.InputNumber !=null).OrderBy(o => o.StartTime).FirstOrDefault();
            var lastChallenge = _challenges.Where(w => w.InputNumber != null).OrderByDescending(o => o.AnswerTime).FirstOrDefault();
            summary.ChallengeDuration = lastChallenge.AnswerTime-firstChallenge.StartTime;
            summary.TrueCount = _challenges.Where(w => w.InputNumber != null).Count(c => c.IsAnswerCorrect == true);
            summary.FalseCount = _challenges.Where(w => w.InputNumber != null).Count(c => c.IsAnswerCorrect == false);
            summary.LongestChallenge = _challenges.Where(w => w.InputNumber != null).OrderByDescending(o => o.Duration).FirstOrDefault();
            summary.FastestChallenge = _challenges.Where(w => w.InputNumber != null).OrderBy(o => o.Duration).FirstOrDefault();
            return summary;
        }
        private List<Challenge> GenerateChallenge(ChallengeSettings cSettings)
        {
            for (int i = 0; i < cSettings.ChallengeCount; i++)
            {
                var number1 = _random.Next(cSettings.MinNumber, cSettings.MaxNumber);
                var number2 = _random.Next(cSettings.MinNumber, cSettings.MaxNumber);
                var challenge = BuildChallenge(cSettings.Operation, number1, number2);
                challenge.OrderNumber = i;
                _challenges.Add(challenge);
            }
            return _challenges;
        }

        private Challenge BuildChallenge(Operation operation, int number1, int number2)
        {
            var challenge = new Challenge();
            challenge.Question = $"{number1}{operation.Sign}{number2}=?";
            challenge.FirstNumber = number1;
            challenge.LastNumber = number2;
            challenge.Result = operation.Calculate(number1, number2);
            challenge.ResultText = $"{number1}{operation.Sign}{number2}={challenge.Result}";
            return challenge;
        }

    }
}
