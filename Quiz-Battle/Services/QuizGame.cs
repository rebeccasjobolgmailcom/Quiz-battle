using System;
using System.Collections.Generic;
using System.Linq;
using Quiz_Battle.Models;
using Quiz_Battle.Services;
using Quiz_Battle.data;

public class QuizGame
{
    // Stores all players in the current game.
    private List<Player> players = new List<Player>();

    // Creates a separate Player object for each name.
    public void CreatePlayers(List<string> playerNames)
    {
        players.Clear();

        foreach (string name in playerNames)
        {
            Player player = new Player(name);
            players.Add(player);
        }
    }

    // Returns the players in the current game.
    public List<Player> GetPlayers()
    {
        return players;
    }

    // Gives each player one turn per round.
    public void PlayRounds(int numberOfRounds)
    {
        // All players share this question bank.
        // A new game starts with a fresh set of questions.
        QuestionBank questionBank = new QuestionBank();

        if (questionBank.GetTotalQuestionCount() == 0)
        {
            Console.WriteLine(
                "Inga frågor kunde läsas in. Kontrollera data/questions.json.");

            return;
        }

        for (int round = 1; round <= numberOfRounds; round++)
        {
            Console.WriteLine(
                $"\n=== Runda {round} av {numberOfRounds} ===");

            foreach (Player player in players)
            {
                // Check before asking the player to choose a category.
                if (questionBank.GetTotalQuestionCount() == 0)
                {
                    Console.WriteLine(
                        "Alla frågor är slut. Spelet avslutas i förtid.");

                    ShowResults();
                    return;
                }

                Question? question = ChooseQuestion(questionBank, player);

                if (question == null)
                {
                    Console.WriteLine(
                        "Ingen fråga hämtades. Spelet avbryts.");

                    ShowResults();
                    return;
                }

                int basePoints = question.GetBasePoints();

                Console.WriteLine(
                    $"\n{question.Category}, {basePoints} poäng");

                question.Display();

                string answer;

                // Keep asking until the player enters a valid answer.
                while (true)
                {
                    Console.Write("Ditt svar: ");
                    string? input = Console.ReadLine();

                    if (input == null)
                    {
                        Console.WriteLine(
                            "Inmatningen avslutades. Spelet avbryts.");

                        ShowResults();
                        return;
                    }

                    answer = input.Trim();

                    if (question.IsValidAnswer(answer))
                    {
                        break;
                    }

                    Console.WriteLine(
                        "Ogiltigt svar. Ange ett av alternativen som visas.");
                }

                // The question checks the answer.
                // Player calculates and stores the points.
                bool isCorrect = question.CheckAnswer(answer);

                int awardedPoints = RecordPlayerAnswer(
                    player,
                    isCorrect,
                    basePoints);

                if (isCorrect)
                {
                    int bonus = awardedPoints - basePoints;

                    Console.WriteLine(
                        $"Rätt! {player.Name} får {awardedPoints} poäng, " +
                        $"varav {bonus} i seriebonus.");
                }
                else
                {
                    Console.WriteLine(
                        $"Fel. Rätt svar var: {question.GetCorrectAnswer()}. " +
                        "Du får 0 poäng.");
                }

                Console.WriteLine(
                    $"Totalt: {player.Score} poäng.");
            }
        }

        ShowResults();
    }

    // Shows available categories and returns a question.
    private Question? ChooseQuestion(
        QuestionBank questionBank,
        Player player)
    {
        List<string> categories = questionBank.GetAvailableCategories();

        int randomChoice = categories.Count + 1;

        Console.WriteLine($"\n{player.Name}, välj kategori:");

        for (int i = 0; i < categories.Count; i++)
        {
            string category = categories[i];

            int remainingQuestions =
                questionBank.GetQuestionCount(category);

            Console.WriteLine(
                $"{i + 1}. {category} ({remainingQuestions} frågor kvar)");
        }

        Console.WriteLine($"{randomChoice}. Slumpa");

        while (true)
        {
            Console.Write("Val: ");
            string? input = Console.ReadLine();

            if (input == null)
            {
                return null;
            }

            if (int.TryParse(input, out int choice)
                && choice >= 1
                && choice <= randomChoice)
            {
                // Both methods remove the selected question
                // from the shared question bank.
                if (choice == randomChoice)
                {
                    return questionBank.PullRandomQuestionFromAnyCategory();
                }

                string selectedCategory = categories[choice - 1];

                return questionBank.PullRandomQuestion(selectedCategory);
            }

            Console.WriteLine(
                $"Svara med en siffra mellan 1 och {randomChoice}.");
        }
    }

    // Finds the winners by score, then by correct answers.
    public List<Player> GetWinners()
    {
        List<Player> winners = new List<Player>();

        if (players.Count == 0)
        {
            return winners;
        }

        // Find the highest score.
        int highestScore = players[0].Score;

        foreach (Player player in players)
        {
            if (player.Score > highestScore)
            {
                highestScore = player.Score;
            }
        }

        // Compare correct answers only among the top-scoring players.
        int mostCorrectAnswers = 0;

        foreach (Player player in players)
        {
            if (player.Score == highestScore
                && player.CorrectAnswers > mostCorrectAnswers)
            {
                mostCorrectAnswers = player.CorrectAnswers;
            }
        }

        // More than one player can share the victory.
        foreach (Player player in players)
        {
            if (player.Score == highestScore
                && player.CorrectAnswers == mostCorrectAnswers)
            {
                winners.Add(player);
            }
        }

        return winners;
    }

    // Sends the result to Player and returns the points earned.
    public int RecordPlayerAnswer(
        Player player,
        bool isCorrect,
        int basePoints)
    {
        return player.RecordAnswer(isCorrect, basePoints);
    }

    // Shows players sorted by score, then by correct answers.
    private void ShowResults()
    {
        Console.WriteLine("\n=== Slutresultat ===");

        List<Player> sortedPlayers = players
            .OrderByDescending(player => player.Score)
            .ThenByDescending(player => player.CorrectAnswers)
            .ToList();

        foreach (Player player in sortedPlayers)
        {
            Console.WriteLine(
                $"{player.Name}: {player.Score} poäng " +
                $"({player.CorrectAnswers} av " +
                $"{player.AnsweredQuestions} rätt)");
        }

        List<Player> winners = GetWinners();

        if (winners.Count == 1)
        {
            Console.WriteLine($"{winners[0].Name} vinner!");
        }
        else if (winners.Count > 1)
        {
            Console.WriteLine("Delad seger mellan:");

            foreach (Player winner in winners)
            {
                Console.WriteLine(winner.Name);
            }
        }
    }

    // Creates results for the highscore code to save.
    // This method does not write to the file itself.
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