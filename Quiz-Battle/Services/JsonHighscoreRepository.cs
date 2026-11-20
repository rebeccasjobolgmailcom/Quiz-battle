using Quiz_Battle.data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Quiz_Battle.data
{
    public class JsonHighScore : IHighScore
    {
        private readonly string filePath = "data/highScores.json";

        public List<HighScoreEntry> LoadScores()
        {
            if (!File.Exists(filePath))
            {
                return new List<HighScoreEntry>();
            }

            string json = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<HighScoreEntry>();
            }

            List<HighScoreEntry> Scores = JsonSerializer.Deserialize<List<HighScoreEntry>>(json);

            return Scores ?? new List<HighScoreEntry>();
        }

        
        public void SaveScores(List<HighScoreEntry> Scores)
        {
            string directory = Path.GetDirectoryName(filePath);

            Directory.CreateDirectory(directory);

            string json = JsonSerializer.Serialize( Scores, new JsonSerializerOptions { WriteIndented = true } );

            File.WriteAllText(filePath, json);
        }
    }




}