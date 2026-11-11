namespace Quiz_Battle.Models
{
    //Base class for a generic question in the game
    //string.Empty is used to avoid null reference exceptions
    public abstract class Question
    {
        public string Text { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        
        //Calculate the base points based on difficulty
        public int GetBasePoints()
        {
            return Difficulty.ToLower() switch
            { "easy" => 10, "medium" => 20, "hard" => 30, _ => 0 };
        }

        //Abstract methods to be implemented by child classes

        public abstract void Display();
        public abstract bool IsValidAnswer(string answer);
        public abstract bool CheckAnswer(string answer);
        public abstract string GetCorrectAnswer();
    }
}