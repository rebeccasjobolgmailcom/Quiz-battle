
using Quiz_Battle.data;
using Quiz_Battle.Models;
using System.Linq;

public class ConsoleUI
{
    public string ShowMenu()
    {
        Console.Clear();

        Console.WriteLine("========= Quiz Battle =========");
        Console.WriteLine("1. Spela");
        Console.WriteLine("2. Topplista");
        Console.WriteLine("3. Avsluta");
        Console.WriteLine("=================================");
        Console.WriteLine();
        Console.WriteLine("Ta ett val (1-3)");

        return Console.ReadLine();
    }

    public int AskRounds()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Hur många rundor vill ni spela?");



            if (int.TryParse(Console.ReadLine(), out int rounds) && rounds > 0)
            {
                return rounds;
                
            }
            else
            {
                Console.WriteLine("Välj ett giltligt nummer antal rundor");
                Thread.Sleep(2000);
            }

        }


    }


    public int AskNumberOfPlayers()
    {
        int numberOfPlayers;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Hur många spelare ska delta?");

            if (int.TryParse(Console.ReadLine(), out numberOfPlayers)
                && numberOfPlayers > 0 && numberOfPlayers < 4)
            {
                return numberOfPlayers;
            }
            else
            {
                Console.WriteLine("Ange ett giltigt antal spelare!");
                Thread.Sleep(2500);
            }
        }
    }


    public List<string> AskPlayerNames(int numberOfPlayers)
    {
        List<string> playerNames = new List<string>();

        for (int i = 0; i < numberOfPlayers; i++)
        {
            Console.WriteLine($"Ange namn för spelare {i + 1}:");

            string name = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(name) ||
                playerNames.Contains(name, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine("Namnet är tomt eller redan upptaget!");
                i--;
                continue;
            }

            playerNames.Add(name);
        }

        return playerNames;
    }


    public void ShowHighScores()
    {
        Console.Clear();

        Console.WriteLine("========= TOPPLISTA =========");
        Console.WriteLine();

        // Skapa repository
        IHighScore repository = new JsonHighScore();

        // Hämta sparade resultat
        List<HighScoreEntry> Scores = repository.LoadScores();

        // Kontrollera om det finns några resultat
        if (Scores.Count == 0)
        {
            Console.WriteLine("Det finns inga sparade resultat ännu.");
            Console.ReadKey();
            return;
        }

        // Sortera resultaten och hämta de tio bästa
        List<HighScoreEntry> topTen = Scores
            .OrderByDescending(s => s.Score)
            .ThenByDescending(s => s.CorrectAnswers)
            .Take(10)
            .ToList();

        // Visa resultaten
        Console.WriteLine(
            $"{"Placering",-12} {"Namn",-15} {"Poäng",-10} {"Antal rätt",-12} {"Datum"}");

        Console.WriteLine("---------------------------------------------------------------");

        for (int i = 0; i < topTen.Count; i++)
        {
            HighScoreEntry entry = topTen[i];

            Console.WriteLine(
                $"{i + 1,-12} {entry.PlayerName,-15} " +
                $"{entry.Score,-10} {entry.CorrectAnswers,-12} " +
                $"{entry.Date:yyyy-MM-dd}");
        }

        Console.WriteLine();
        Console.WriteLine("Tryck på en tangent för att återgå till menyn.");
        Console.ReadKey();
    }



    public string AskCategory(List<string> categories)
    {
        Console.WriteLine("Välj en kategori:");

        for (int i = 0; i < categories.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {categories[i]}");
        }

        while (true)
        {
            Console.Write("Ditt val: ");

            if (int.TryParse(Console.ReadLine(), out int choice)
                && choice >= 1 && choice <= categories.Count)
            {
                return categories[choice - 1];
            }

            Console.WriteLine("Ogiltigt val, försök igen.");
        }
    }

    public string AskAnswer(Question question)
    {
        string answer;

        do
        {
            Console.Write("Ditt svar: ");
            answer = Console.ReadLine() ?? "";

            if (!question.IsValidAnswer(answer))
            {
                Console.WriteLine("Ogiltigt svar, försök igen.");
            }

        } while (!question.IsValidAnswer(answer));

        return answer;
    }


    public void ShowQuestion(Question question)
    {
        Console.WriteLine();
        Console.WriteLine($"Kategori: {question.Category}");
        Console.WriteLine($"Svårighetsgrad: {question.Difficulty}");
        Console.WriteLine();

        question.Display();
    }


}
