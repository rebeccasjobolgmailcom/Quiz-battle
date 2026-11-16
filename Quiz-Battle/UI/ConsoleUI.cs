using Quiz_Battle.UI;


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
            Console.WriteLine("Hur många spelare ska delta?");

            if (int.TryParse(Console.ReadLine(), out numberOfPlayers)
                && numberOfPlayers > 0)
            {
                return numberOfPlayers;
            }

            Console.WriteLine("Ange ett giltigt antal spelare!");
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

}
