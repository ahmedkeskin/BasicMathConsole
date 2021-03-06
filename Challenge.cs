﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BasicMathConsole
{
    public class Challenge
    {
        public int OrderNumber { get; set; }
        public string Question { get; set; }
        public bool IsAnswerCorrect { get; set; }
        public int FirstNumber { get; set; }
        public int LastNumber { get; set; }
        public Operation Calculation { get; set; }
        public float? InputNumber { get; set; }
        public float Result { get; set; }
        public string ResultText { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime AnswerTime { get; set; }
    }
}
