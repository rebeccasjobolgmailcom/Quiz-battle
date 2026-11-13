using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using Quiz_Battle.Models;

namespace Quiz_Battle.Services
{
    //Class to represent a category of questions
    public class CategoryData
    {
        public string Name { get; set; } = string.Empty;
        public List<JsonElement> Questions { get; set; } = new List<JsonElement>();
    }
    //Class to represent the root of the JSON file, with a list of categorys
    public class QuestionRoot
    {
        public List<CategoryData> Categories { get; set; } = new List<CategoryData>();
    }
    //Service class for reading questions from the JSON file and managing remaining questions
    public class QuestionBank
    {
        private readonly Dictionary<string, List<Question>> categories = new Dictionary<string, List<Question>>();
        private readonly Random random = new Random();

        //Constructor that loads the questions from the JSON file when the QuestionBank is instantiated
        public QuestionBank()
        {
            LoadQuestions();
        }

        //Reads the json file and turn into a list 
        private void LoadQuestions()
        {
            //Looks for the file in the base dictionary
            string path = "data/questions.json";
            if (!File.Exists(path)) return; // If the file doesn't exist, exit the method

            try
            {
                string json = File.ReadAllText(path);
                //Ignore big/small letters 
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var root = JsonSerializer.Deserialize<QuestionRoot>(json, options);

                if (root?.Categories != null)
                {
                    //Loops through all categories in the JSON file
                    foreach (var category in root.Categories)
                    {
                        var questionList = new List<Question>();

                        //Loops through all questions in this category 
                        foreach (var questionElement in category.Questions)
                        {
                            //Check the "type" to see if it is a multiple choice or true/false question
                            if (questionElement.TryGetProperty("type", out JsonElement typeElement))
                            {
                                string type = typeElement.GetString() ?? string.Empty;
                                Question? question = null;

                                //Creates the correct question object based on its type
                                if (type == "multipleChoice")
                                {
                                    question = JsonSerializer.Deserialize<MultipleChoiceQuestion>(questionElement.GetRawText(), options);
                                }
                                else if (type == "trueFalse")
                                {
                                    question = JsonSerializer.Deserialize<TrueFalseQuestion>(questionElement.GetRawText(), options);
                                }

                                //Save the category name and add the question to our temporary list
                                if (question != null)
                                {
                                    question.Category = category.Name;
                                    questionList.Add(question);
                                }
                            }
                        }
                        //Only add the category to our main list if it has questions left
                        if (questionList.Count > 0)
                        {
                            categories[category.Name] = questionList;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //print an error message if something goes wrong with the file reading
                Console.WriteLine($"Error loading questions: {ex.Message}");
            }
        }

        //Returns a list of all categories that still have questions left
        public List<string> GetAvailableCategories()
        {
            return categories.Where(kvp => kvp.Value.Count > 0).Select(kvp => kvp.Key).ToList();
        }
        //Returns the number of questions currently remaining in a specific category
        public int GetQuestionCount(string category)
        {
            if (categories.ContainsKey(category))
            {
                return categories[category].Count;
            }
            return 0;
        }

        //returns the number of questions left in the entire game
        public int GetTotalQuestionCount()
        {
            return categories.Values.Sum(list => list.Count);
        }

        //Pick, remove and then returns a random question from a category so it is not repeated
        public Question? PullRandomQuestion(string category)
        {
            if (categories.ContainsKey(category) && categories[category].Count > 0)
            {
                //Pick a random index based on how many questions are left
                int index = random.Next(categories[category].Count);
                Question question = categories[category][index];

                // Remove the question from the list to avoid repetition
                categories[category].RemoveAt(index);
                return question;
            }
            return null; // No questions available in this category
        }

        //Handles the menu option for picking a random category first 
        public Question? PullRandomQuestionFromAnyCategory()
        {
            //Get all categoris that still have questions left
            var availableCategories = GetAvailableCategories();
            //Stop if there are no questions left in any category  
            if (availableCategories.Count == 0) return null;

            //Pick one of the available categories at random
            string randomCategory = availableCategories[random.Next(availableCategories.Count)];
            //Pull a random question from the category thats was just picked
            return PullRandomQuestion(randomCategory);
        }
    }
}




