using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_Battle.data
{
    public class HighScoreEntry
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public int CorrectAnswers { get; set; }
        public DateTime Date { get; set; }
    }
}
