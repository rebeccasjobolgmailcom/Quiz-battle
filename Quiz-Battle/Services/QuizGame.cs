using Quiz_Battle.data;
using Quiz_Battle.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class QuizGame
{
    // Stores all players participating in the game.
    private List<Player> players = new List<Player>();

    // Creates players using the provided names.
    public void CreatePlayers(List<string> playerNames)
    {
        foreach (string name in playerNames)
        {
            Player player = new Player(name);
            players.Add(player);
        }
    }

    // Returns all players in the current game.
    public List<Player> GetPlayers()
    {
        return players;
    }

    // Runs the selected number of rounds.
    // Each player gets one turn per round.
    public void PlayRounds(int numberOfRounds,QuestionBank questionBank, ConsoleUI ui) //La till lite i parametern//J
    {
        for (int round = 1; round <= numberOfRounds; round++)
        {
            Console.WriteLine($"=======Runda: {round}========");

            foreach (Player player in players)
            {
                Console.WriteLine($"It's {player.Name}'s turn!");

                List<string> categories = questionBank.GetCategories();

                string category = ui.AskCategory(categories);

                Question question =
                    questionBank.GetRandomQuestion(category);

                ui.ShowQuestion(question);

                string answer = ui.AskAnswer(question);

                bool isCorrect = question.CheckAnswer(answer);

                if (isCorrect)
                {
                    Console.WriteLine("Rätt svar!");

                    int points = question.GetBasePoints();

                    RecordPlayerAnswer(player, true, points);

                    Console.WriteLine($"Du fick {points} poäng!");
                }
                else
                {
                    Console.WriteLine("Fel svar!");

                    Console.WriteLine(
                        $"Rätt svar var: {question.GetCorrectAnswer()}");

                    RecordPlayerAnswer(player, false, 0);
                }

                Console.WriteLine($"Total poäng: {player.Score}");

                Console.WriteLine("Tryck på en tangent för nästa spelare.");
                Console.ReadKey();

                /*
// Draft: connect the question and UI code when ready.
// The objects, category and method names must be checked.

Question question = questionBank.TakeQuestion(category);

ui.ShowQuestion(question);

string answer = ui.ReadAnswer(question);

bool isCorrect = question.CheckAnswer(answer);

int awardedPoints = RecordPlayerAnswer(
    player,
    isCorrect,
    question.Points
);
*/


            }
        }
    }

    /// Returns the winners based on score, then correct answers.
    public List<Player> GetWinners()
    {
        List<Player> winners = new List<Player>();

        if (players.Count == 0)
        {
            return winners;
        }

        int highestScore = players.Max(p => p.Score);

        foreach (Player player in players)
        {
            if (player.Score == highestScore)
            {
                winners.Add(player);
            }
        }

        return winners;
    }

    // Records a player's answer and updates their score.
    public int RecordPlayerAnswer(Player player, bool isCorrect, int basePoints)
    {
        return player.RecordAnswer(isCorrect, basePoints);
    }



    //Lägger till en liten metod här nere för att det ska funka med topplistan //J
    public List<HighScoreEntry> GetHighScoreEntries()
    {
        List<HighScoreEntry> entries = new List<HighScoreEntry>();

        foreach (Player player in players)
        {
            HighScoreEntry entry = new HighScoreEntry
            {
                PlayerName = player.Name,
                Score = player.Score,
                CorrectAnswers = player.CorrectAnswers,
                Date = DateTime.Now
            };

            entries.Add(entry);
        }

        return entries;
    }
}
