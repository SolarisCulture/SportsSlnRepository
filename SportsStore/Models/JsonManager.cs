using Newtonsoft.Json;

namespace SportsStore.Models
{
    public class JsonManager
    {
        public List<QuestionModel> ReadQuestionsFromFile(string filePath)
        {
            List<QuestionModel> questions = new List<QuestionModel>();

            try
            {
                string json = File.ReadAllText(filePath);
                questions = JsonConvert.DeserializeObject<List<QuestionModel>>(json);
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., file not found, invalid JSON format)
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
            }

            return questions;
        }
    }
}
