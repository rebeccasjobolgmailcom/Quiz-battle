using System;
using System.Collections.Generic;

namespace Quiz_Battle.Models
{
    //Derived class for multiple choice questions
    public class MultipleChoiceQuestion : Question
    {
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectIndex { get; set; }

        //Prints the question and loops through all available options
        public override void Display()
        {
            Console.WriteLine(Text);
            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine($"{i+ 1}. {Options[i]}");
            }
        }
        //Check if the provided answer is valid (i.e., one of the options)
        public override bool IsValidAnswer(string answer)
        {
            if (int.TryParse(answer, out int choice))
            {
                return choice >= 1 && choice <= Options.Count;
            }
            return false;
        }
        //Check if the provided answer is correct
        //Reduce the players choice by 1 to match a zero-based index
        public override bool CheckAnswer(string answer)
        {
            if(int.TryParse(answer, out int choice))
            {
                return (choice - 1) == CorrectIndex;
            }
            return false;
        }
        //Return the correct answer
        public override string GetCorrectAnswer()
        {   
            if(CorrectIndex >= 0 && CorrectIndex < Options.Count)
            {
                return Options[CorrectIndex];
            }
            return string.Empty;
        }
    }
}