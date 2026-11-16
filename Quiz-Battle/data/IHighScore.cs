using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Battle.data
{
    public interface IHighScore
    {
        // Hämtar alla sparade resultat
        List<HighScoreEntry> LoadScores();

        // Sparar resultat till JSON-filen
        void SaveScores(List<HighScoreEntry> Scores);
    }
}
