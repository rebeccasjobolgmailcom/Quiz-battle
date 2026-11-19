


using Quiz_Battle.data;
using Quiz_Battle.Models;
using Quiz_Battle.Services;

ConsoleUI ui = new ConsoleUI();

while (true)
{
    // Visa menyn och ta emot användarens val
    string choice = ui.ShowMenu();

    switch (choice)
    {
        case "1":

            // Starta spelet
            Console.Write("Startar spel");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);

            // Fråga efter antal spelare och deras namn
            int numberOfPlayers = ui.AskNumberOfPlayers();

            List<string> players =
                ui.AskPlayerNames(numberOfPlayers);

            // Fråga efter antal rundor
            int rounds = ui.AskRounds();

            // Skapa QuizGame
            QuizGame quizGame = new QuizGame();

            // Skapa Player-objekt från namnen
            quizGame.CreatePlayers(players);

            // Skapa QuestionBank
            // Frågorna laddas automatiskt i konstruktorn
            QuestionBank questionBank = new QuestionBank();

            // Starta spelet
            quizGame.PlayRounds(rounds, questionBank);

            // Skapa repository
            IHighScore repository = new JsonHighScore();

            // Hämta tidigare resultat
            List<HighScoreEntry> scores =
                repository.LoadScores();

            // Lägg till alla spelares nya resultat
            scores.AddRange(quizGame.GetHighScoreEntries());

            // Spara alla resultat
            repository.SaveScores(scores);

            Console.WriteLine("Spelet är slut!");
            Console.ReadKey();

            break;

        case "2":

            Console.Write("Visar highscores");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);

            ui.ShowHighScores();

            break;

        case "3":

            Console.WriteLine("Hejdå!");
            Console.ReadKey();
            return;
    }
}
