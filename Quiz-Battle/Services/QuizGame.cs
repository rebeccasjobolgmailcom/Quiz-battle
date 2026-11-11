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
    public void PlayRounds(int numberOfRounds)
    {
        for (int round = 1; round <= numberOfRounds; round++)
        {
            Console.WriteLine($"Round {round}");

            foreach (Player player in players)
            {
                Console.WriteLine($"It's {player.Name}'s turn!");

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
}
