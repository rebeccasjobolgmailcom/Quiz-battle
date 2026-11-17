using Quiz_Battle.data;
using Quiz_Battle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Quiz_Battle.Models;
using Quiz_Battle.Services;
using System.Xml;

public class QuizGame
{
    // Stores the players in the current game.
    private List<Player> players = new List<Player>();
    
    // Keeps track of which questions each player has received. //J
    private Dictionary<Player, List<Question>> usedQuestions
        = new Dictionary<Player, List<Question>>();

    public void CreatePlayers(List<string> playerNames)
    {    //Removes previous players//J
        players.Clear();
        //resets questions from previous game//J
        usedQuestions.Clear();

        foreach (string name in playerNames)
        {
            Player player = new Player(name);

            players.Add(player);

            usedQuestions[player] = new List<Question>();
        }
    }


    // Selects a random question that the player has not received before. Adding this method starting here //J
    private Question? GetRandomQuestion(Player player, string category, QuestionBank questionBank)
    {
        //Added this because i got an exception error when testing program checking if player already excists in used questions dictionary //J
        if (!usedQuestions.ContainsKey(player))
        {
            usedQuestions[player] = new List<Question>();
        }

       
        

        // Get all questions from the selected category.
        List<Question> categoryQuestions =
            questionBank.GetQuestionsByCategory(category);

        // Remove questions that this player has already received.
        List<Question> availableQuestions = categoryQuestions
            .Where(q => !usedQuestions[player].Contains(q))
            .ToList();

        // Check if there are any questions left for this player.
        if (availableQuestions.Count == 0)
        {
            return null;
        }

        // Select a random question.
        int index = Random.Shared.Next(availableQuestions.Count);

        Question selectedQuestion = availableQuestions[index];

        // Save the question in this player's history.
        usedQuestions[player].Add(selectedQuestion);

        return selectedQuestion;
    }//Ending here//J


    public List<Player> GetPlayers()
    {
        return players;
    }

    public void PlayRounds(int numberOfRounds)
    {
        // Load a fresh set of questions for this game.
        QuestionBank questionBank = new QuestionBank();

        if (questionBank.GetTotalQuestionCount() == 0)
        {
            Console.WriteLine("Inga frågor kunde läsas in. Kontrollera data/questions.json.");
            return;
        }

        for (int round = 1; round <= numberOfRounds; round++)
        {
            Console.WriteLine($"\n=== Runda {round} av {numberOfRounds} ===");

            foreach (Player player in players)
            {
                //Pausing this part due to previously coding players seperated. This scenario should no longer be possible //J
                //if (questionBank.GetTotalQuestionCount() == 0)
                //{
                //    Console.WriteLine("Alla frågor är slut. Spelet avslutas i förtid.");
                //    ShowResults();
                //    return;
                //}

                Question? question = ChooseQuestion(questionBank, player);

                //Adding this method instead of the one above //J
                bool hasQuestionsLeft = questionBank.GetAvailableCategories().Any(category =>questionBank.GetQuestionsByCategory(category).Any(q => !usedQuestions[player].Contains(q)));

                if (!hasQuestionsLeft)
                {
                    Console.WriteLine(
                        $"{player.Name} har inga fler frågor kvar.");

                    continue;
                }
                //J

                if (question == null)
                {
                    Console.WriteLine("Ingen fråga kunde hämtas. Spelet avslutas i förtid.");
                    ShowResults();
                    return;
                }

                int basePoints = question.GetBasePoints();
                Console.WriteLine($"\n{question.Category}, {basePoints} poäng");
                question.Display();

                string answer;

                // Invalid input does not count as an answered question.
                while (true)
                {
                    Console.Write("Ditt svar: ");
                    string? input = Console.ReadLine();

                    if (input == null)
                    {
                        Console.WriteLine("Inmatningen avslutades. Spelet avbryts.");
                        ShowResults();
                        return;
                    }

                    answer = input.Trim();

                    if (question.IsValidAnswer(answer))
                    {
                        break;
                    }

                    Console.WriteLine("Ogiltigt svar. Ange ett av alternativen som visas.");
                }

                bool isCorrect = question.CheckAnswer(answer);
                int awardedPoints = RecordPlayerAnswer(player, isCorrect, basePoints);

                if (isCorrect)
                {
                    int bonus = awardedPoints - basePoints;
                    Console.WriteLine($"Rätt! {player.Name} får {awardedPoints} poäng, varav {bonus} i seriebonus.");
                }
                else
                {
                    Console.WriteLine($"Fel. Rätt svar var: {question.GetCorrectAnswer()}. Du får 0 poäng.");
                }

                Console.WriteLine($"Totalt: {player.Score} poäng.");
            }
        }

        ShowResults();
    }

    // This input and display code can later be moved to ConsoleUI.
    private Question? ChooseQuestion(QuestionBank questionBank, Player player)
    {
        List<string> categories = questionBank.GetAvailableCategories();
        int randomChoice = categories.Count + 1;

        Console.WriteLine($"\n{player.Name}, välj kategori:");

        for (int i = 0; i < categories.Count; i++)
        {
            string category = categories[i];
            //Console.WriteLine($"{i + 1}. {category} ({questionBank.GetQuestionCount(category)} frågor kvar)");
            int remainingQuestions = questionBank.GetQuestionsByCategory(category).Count(q => !usedQuestions[player].Contains(q));

            Console.WriteLine($"{i + 1}. {category} ({remainingQuestions} frågor kvar)");
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

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= randomChoice)
            {
                if (choice == randomChoice)
                {
                    // Select a random category. changing code for random choice//J
                    string randomCategory = categories[Random.Shared.Next(categories.Count)];

                    return GetRandomQuestion(player, randomCategory, questionBank);
                    // return questionBank.PullRandomQuestionFromAnyCategory(); Original code//J
                }

                return GetRandomQuestion( player, categories[choice - 1], questionBank); //Changing it just a little bit//J

                //return questionBank.PullRandomQuestion(categories[choice - 1]);
            }

            Console.WriteLine($"Svara med en siffra mellan 1 och {randomChoice}.");
        }
    }

    public List<Player> GetWinners()
    {
        List<Player> winners = new List<Player>();

        if (players.Count == 0)
        {
            return winners;
        }

        int highestScore = players.Max(player => player.Score);
        int mostCorrectAnswers = 0;

        // Only compare correct answers among the players with the highest score.
        foreach (Player player in players)
        {
            if (player.Score == highestScore && player.CorrectAnswers > mostCorrectAnswers)
            {
                mostCorrectAnswers = player.CorrectAnswers;
            }
        }

        foreach (Player player in players)
        {
            if (player.Score == highestScore && player.CorrectAnswers == mostCorrectAnswers)
            {
                winners.Add(player);
            }
        }

        return winners;
    }

    public int RecordPlayerAnswer(Player player, bool isCorrect, int basePoints)
    {
        return player.RecordAnswer(isCorrect, basePoints);
    }

    // Display code can later be moved to ConsoleUI.
    private void ShowResults()
    {
        Console.WriteLine("\n=== Slutresultat ===");

        List<Player> sortedPlayers = players
            .OrderByDescending(player => player.Score)
            .ThenByDescending(player => player.CorrectAnswers)
            .ToList();

        foreach (Player player in sortedPlayers)
        {
            Console.WriteLine($"{player.Name}: {player.Score} poäng ({player.CorrectAnswers} av {player.AnsweredQuestions} rätt)");
        }

        List<Player> winners = GetWinners();

        if (winners.Count == 1)
        {
            Console.WriteLine($"{winners[0].Name} vinner!");
        }
        else if (winners.Count > 1)
        {
            Console.WriteLine("Delad seger mellan: " + string.Join(", ", winners.Select(player => player.Name)));
        }
    }



    // Creates highscore entries for all players. //J
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
