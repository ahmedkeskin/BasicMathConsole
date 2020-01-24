using System;
using System.Collections.Generic;
using System.Text;

namespace BasicMathConsole
{
    public interface IArchiveManager
    {
        List<Challenge> ImportFiles(string directoryPath);
        string Analyze(List<Challenge> challenges, Challenge challenge);
        List<ChallengeResult> GeneralSummary(List<Challenge> challenges);

    }
}
