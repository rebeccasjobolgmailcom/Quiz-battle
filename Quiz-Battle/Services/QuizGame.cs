using Quiz_Battle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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
            }
        }
    }

    // Returns the player or players with the highest score.
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
}