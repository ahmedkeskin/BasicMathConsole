﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BasicMathConsole
{
    public class Summary
    {
        public int ChallengeCount { get; set; }
        public TimeSpan ChallengeDuration { get; set; }
        public int TrueCount { get; set; }
        public int FalseCount { get; set; }
        public Challenge FastestChallenge { get; set; }
        public Challenge LongestChallenge { get; set; }

        public List<Challenge> AllChallenges { get; set; }
    }
}