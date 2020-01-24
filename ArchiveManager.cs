using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BasicMathConsole
{
    public class ArchiveManager : IArchiveManager
    {
        public string Analyze(List<Challenge> challenges, Challenge challenge)
        {
            var c = challenges.Where(w => w.InputNumber != null && w.Question == challenge.Question);
            var generalAvg = TimeSpan.FromMilliseconds(challenges.Where(w => w.InputNumber != null).Average(a => a.Duration.TotalMilliseconds));
            var avgTime = TimeSpan.FromMilliseconds(c.Average(a => a.Duration.TotalMilliseconds));
            var badTime = TimeSpan.FromMilliseconds(c.Max(a => a.Duration.TotalMilliseconds));
            var bestTime = TimeSpan.FromMilliseconds(c.Min(a => a.Duration.TotalMilliseconds));
            var dangerTime = (badTime - avgTime) / 2;
            if (c.Count() < 50)
            {
                return ".";
            }

            if (challenge.Duration < bestTime)
            {
                return "New best record!";
            }
            else if (challenge.Duration <= avgTime)
            {
                return "Good";
            }
            else if (challenge.Duration > avgTime && challenge.Duration < dangerTime)
            {
                return "Bad";
            }
            else if (challenge.Duration >= dangerTime && challenge.Duration > generalAvg)
            {
                return "Distractibility";
            }
            else
            {
                return "Worse :)";
            }

        }

        public List<ChallengeResult> GeneralSummary(List<Challenge> challenges)
        {
            var summary = new List<ChallengeResult>();
            var questions = challenges.Select(s => s.Question).Distinct();
            foreach (var question in questions)
            {
                var root = challenges.Where(w => w.InputNumber != null && w.Question == question);
                var avg = root.Average(a => a.Duration.TotalMilliseconds);
                var best = root.Min(m => m.Duration.TotalMilliseconds);
                var worst = root.Max(m => m.Duration.TotalMilliseconds);
                summary.Add(new ChallengeResult
                {
                    Question = question,
                    AvgDuration = TimeSpan.FromMilliseconds(avg),
                    AskCount = root.Count(),
                    BestTime = TimeSpan.FromMilliseconds(best),
                    WorstTime = TimeSpan.FromMilliseconds(worst)
                }) ;
            }
            return summary;
        }

        public List<Challenge> ImportFiles(string directoryPath)
        {
            var files = Directory.GetFiles(directoryPath, "*.json");

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new SummaryConverter());

            var summaries = new List<Summary>();
            var challenges = new List<Challenge>();
            if (files.Length > 0)
            {
                foreach (var file in files)
                {
                    var text = File.ReadAllText(file);
                    summaries.Add(JsonConvert.DeserializeObject<Summary>(text, settings));
                }
            }
            var allChallenges = summaries.Select(s => s.AllChallenges);
            foreach (var challengeList in allChallenges)
            {
                foreach (var challenge in challengeList)
                {
                    challenges.Add(challenge);
                }
            }
            return challenges;
        }
    }
}
