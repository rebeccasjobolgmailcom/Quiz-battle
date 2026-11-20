using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Battle.data
{
    public interface IHighScore
    {
        List<HighScoreEntry> LoadScores();

        void SaveScores(List<HighScoreEntry> Scores);
    }
}
