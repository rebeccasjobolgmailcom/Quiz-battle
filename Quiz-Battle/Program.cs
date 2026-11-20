


using Quiz_Battle.data;
using Quiz_Battle.Models;
using Quiz_Battle.Services;

ConsoleUI ui = new ConsoleUI();

while (true)
{
    string choice = ui.ShowMenu();

    switch (choice)
    {
        case "1":

            Console.Write("Startar spel");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);

            int numberOfPlayers = ui.AskNumberOfPlayers();

            List<string> players = ui.AskPlayerNames(numberOfPlayers);

            int rounds = ui.AskRounds();

            QuizGame quizGame = new QuizGame();

            quizGame.CreatePlayers(players);

            QuestionBank questionBank = new QuestionBank();

            quizGame.PlayRounds(rounds, questionBank);

            IHighScore repository = new JsonHighScore();

            List<HighScoreEntry> scores = repository.LoadScores();

            scores.AddRange(quizGame.GetHighScoreEntries());

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
