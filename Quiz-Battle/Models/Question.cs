namespace Quiz_Battle.Models
{
    public abstract class Question
    {
        public string Text { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        
        public int GetBasePoints()
        {
            return Difficulty.ToLower() switch
            { "easy" => 10, "medium" => 20, "hard" => 30, _ => 0 };
        }


        public abstract void Display();
        public abstract bool IsValidAnswer(string answer);
        public abstract bool CheckAnswer(string answer);
        public abstract string GetCorrectAnswer();
    }
}