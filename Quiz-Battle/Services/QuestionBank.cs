
using Quiz_Battle.Models;
using System.Text.Json;

namespace Quiz_Battle.data
{
    public class QuestionBank
    {
        private readonly string filePath =
            Path.Combine(AppContext.BaseDirectory, "data", "questions.json");

        private List<Question> questions = new List<Question>();

        // Läser in frågorna från JSON-filen
        public void LoadQuestions()
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(
                    "Kunde inte hitta questions.json", filePath);
            }

            string json = File.ReadAllText(filePath);

            var data = JsonSerializer.Deserialize<QuestionDataFile>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (data == null || data.Categories == null)
            {
                throw new InvalidOperationException(
                    "JSON-filen innehåller inga kategorier.");
            }

            questions.Clear();

            foreach (var category in data.Categories)
            {
                foreach (var questionData in category.Questions)
                {
                    Question question = null;

                    // Skapa rätt frågetyp
                    switch (questionData.Type?.ToLower())
                    {
                        case "multiplechoice":
                            question = new MultipleChoiceQuestion
                            {
                                Text = questionData.Text ?? "",
                                Options = questionData.Options ?? new List<string>(),
                                CorrectIndex = questionData.CorrectIndex ?? -1,
                                Difficulty = questionData.Difficulty ?? ""
                            };
                            break;

                        case "truefalse":
                            question = new TrueFalseQuestion
                            {
                                Text = questionData.Text ?? "",
                                Correct = questionData.Correct ?? false,
                                Difficulty = questionData.Difficulty ?? ""
                            };
                            break;
                    }

                    if (question != null)
                    {
                        question.Category = category.Name ?? "";
                        questions.Add(question);
                    }
                }
            }
        }

        // Hämtar alla kategorier
        public List<string> GetCategories()
        {
            return questions
                .Select(q => q.Category)
                .Distinct()
                .ToList();
        }

        // Hämtar alla frågor inom en vald kategori
        public List<Question> GetQuestionsByCategory(string category)
        {
            return questions
                .Where(q => q.Category.Equals(
                    category,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Hämtar en slumpmässig fråga från vald kategori
        public Question GetRandomQuestion(string category)
        {
            List<Question> categoryQuestions =
                GetQuestionsByCategory(category);

            if (categoryQuestions.Count == 0)
            {
                throw new InvalidOperationException(
                    "Inga frågor hittades i kategorin.");
            }

            int index = Random.Shared.Next(categoryQuestions.Count);

            return categoryQuestions[index];
        }
    }

    // Hjälpklasser för att läsa JSON-strukturen
    internal class QuestionDataFile
    {
        public List<CategoryData> Categories { get; set; } =
            new List<CategoryData>();
    }

    internal class CategoryData
    {
        public string? Name { get; set; }

        public List<QuestionData> Questions { get; set; } =
            new List<QuestionData>();
    }

    internal class QuestionData
    {
        public string? Type { get; set; }
        public string? Text { get; set; }
        public List<string>? Options { get; set; }
        public int? CorrectIndex { get; set; }
        public bool? Correct { get; set; }
        public string? Difficulty { get; set; }
    }
}
