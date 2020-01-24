using System;
using System.Collections.Generic;
using System.Text;

namespace BasicMathConsole
{
    public class ChallengeResult
    {
        public string Question { get; set; }
        public TimeSpan AvgDuration { get; set; }
        public TimeSpan BestTime { get; set; }
        public TimeSpan WorstTime { get; set; }
        public int AskCount { get; set; }

    }
}
