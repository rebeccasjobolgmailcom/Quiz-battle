

public class Player
{
    // Stores the player's name.
    public string Name { get; }

    // Stores the total score. Only this class can change it.
    public int Score { get; private set; }

    // Updates the player's statistics and returns the points earned for this answer.
    public int RecordAnswer(bool isCorrect, int basePoints)
    {
        // Count every valid answer, whether correct or incorrect.
        AnsweredQuestions++;

        if (isCorrect)
        {
            // Increase the number of correct answers and the current streak.
            CorrectAnswers++;
            CurrentStreak++;

            // Award 0 bonus points for the first correct answer,
            // then increase the bonus by 5 for each consecutive correct answer.
            int bonus = (CurrentStreak - 1) * 5;

            // Limit the bonus to 20 points.
            if (bonus > 20)
            {
                bonus = 20;
            }

            // Add the question's base points and the streak bonus to the total score.
            int awardedPoints = basePoints + bonus;
            Score += awardedPoints;

            // Return the points earned for this answer.
            return awardedPoints;
        }
        else
        {
            // Reset the streak after an incorrect answer.
            // The player keeps their previously earned points.
            CurrentStreak = 0;
            return 0;
        }
    }

    // Stores the total number of correct answers.
    public int CorrectAnswers { get; private set; }

    // Stores the total number of answered questions.
    public int AnsweredQuestions { get; private set; }

    // Stores the number of consecutive correct answers.
    public int CurrentStreak { get; private set; }

    // Constructor: runs when a new Player object is created.
    public Player(string name)
    {
        // Set the player's name and starting score.
        Name = name;
        Score = 0;
    }
}