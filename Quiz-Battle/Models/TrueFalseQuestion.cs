using System;

namespace Quiz_Battle.Models
{
    //Derived class for true/false questions
    public class TrueFalseQuestion : Question
    {
        public bool Correct { get; set; }
        //Prints the question and the available options (Sant/falskt)
        public override void Display()
        {
            Console.WriteLine($"Påstående: {Text}");
            Console.WriteLine("Sant (s) eller falskt (f)?");
        }
        //Check if the provided answer is valid ("s" or "f")
        public override bool IsValidAnswer(string answer)
        {
            string normalized = answer.Trim().ToLower();
            return normalized == "s" || normalized == "f";
        }
        //Check if the provided answer is correct
        public override bool CheckAnswer(string answer)
        {
            string normalized = answer.Trim().ToLower();
            if (normalized == "s") return Correct == true;
            if (normalized == "f") return Correct == false;
            return false; // Should not reach here if IsValidAnswer is used before
        }
        //Return the correct answer as a string ("sant" or "falskt")
        public override string GetCorrectAnswer()
        {
            return Correct ? "s (Sant)" : "f (Falskt)";
        }
    }
}