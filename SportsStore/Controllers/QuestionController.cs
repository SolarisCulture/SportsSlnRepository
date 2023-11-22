using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
	public class QuestionController : Controller
	{
		private List<QuestionModel> questions_does_not_use = //LoadQuestionsFromJson();
			new List<QuestionModel>
			{
				new QuestionModel { QuestionId = 1, QuestionText = "What is your name?"},
				new QuestionModel { QuestionId = 2, QuestionText = "Where are you from"},
				new QuestionModel { QuestionId = 3, QuestionText = "Your Age"},
			};

        private readonly JsonManager jsonManager = new JsonManager();
        private List<QuestionModel> questions;

        public QuestionController()
        {
			// Assuming the JSON file is in the project root directory
			string jsonFilePath = @"C:\Users\Polina\source\repos\AdamFreeman\SportsSln\SportsStore\questions.json";
            questions = jsonManager.ReadQuestionsFromFile(jsonFilePath);
        }


        public IActionResult Index()
		{
			return View(questions);
		}


		[HttpPost]
		public ActionResult Index(List<string> answers, List<int> questionIds)
		{
            // Combine questionId and answer into a dictionary for easy processing
            var userResponses = questionIds.Zip(answers, (id, answer) => new { QuestionId = id, Answer = answer })
                                            .ToDictionary(x => x.QuestionId, x => x.Answer);

            // Process and save the user responses, you can save them to a file or database
            // For simplicity, this example assumes answers are just strings and saves them to a file
            foreach (var response in userResponses)
            {
                System.IO.File.AppendAllText("answers.txt", $"QuestionId: {response.Key}, Answer: {response.Value}\n");
            }

            return RedirectToAction("ThankYou"); // Redirect to a thank-you page or another action
		}


		public ActionResult ThankYou()
		{
			return View();
		}

	}
}
