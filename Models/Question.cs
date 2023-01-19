using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TontonatorDesktopApp.Data.Repository;
using TontonatorDesktopApp.Models.Enums;

namespace TontonatorDesktopApp.Models
{
	[FirestoreData]
	public class Question : IQuestion, IEntityBase
	{
		[FirestoreProperty]
		public string Id { get; set; }
		[FirestoreProperty]
		public string QuestionName { get; set; }
		[FirestoreProperty]
		public QuestionCategory QuestionCategory { get; set; }
		[FirestoreProperty]
		public Status Status { get; set; }

		// Options disabled. Can be retaken later.
		public string[] QuestionOptions
		{
			get => new string[] {
			"Si",
			"No",
			//"Probablemente",
			//"Probablemente no",
			//"No sé"
		};
		}

		public bool IsCorrect { get; set; }

		[FirestoreProperty]
		public double QuestionRate { get; set; }
		[FirestoreProperty]
		public QuestionOption QuestionOption { get; set; }

		public Question()
		{

		}

		public Question(Question question)
		{
			Id = question.Id;
			QuestionName = question.QuestionName;
			QuestionCategory = question.QuestionCategory;
			Status = question.Status;
			IsCorrect = question.IsCorrect;
			QuestionRate = question.QuestionRate;
			QuestionOption = question.QuestionOption;
		}

		public Question(string questionName, QuestionCategory questionCategory, Status status)
		{
			QuestionName = questionName;
			QuestionCategory = questionCategory;
			IsCorrect = false;
			QuestionRate = 0;
			QuestionOption = QuestionOption.Null;
			Status = status;
		}

		public Question(string questionName, QuestionCategory questionCategory, QuestionOption questionOption, double questionRate, Status status)
		{
			QuestionName = questionName;
			QuestionCategory = questionCategory;
			QuestionOption = questionOption;
			QuestionRate = questionRate;
			Status = status;
		}

		public Question(string questionName, QuestionCategory questionCategory, QuestionOption questionOption, double questionRate)
		{
			QuestionName = questionName;
			QuestionCategory = questionCategory;
			QuestionOption = questionOption;
			QuestionRate = questionRate;
		}

		public void ShowOptions()
		{
			var counter = 1;
			foreach (var option in QuestionOptions)
			{
				Console.WriteLine(counter + ". " + option);
				counter++;
			}
		}

		public Dictionary<string, object> ToDictionary()
		{
			var dictionary = new Dictionary<string, object>();

			dictionary.Add("Id", Id);
			dictionary.Add("QuestionName", QuestionName);
			dictionary.Add("QuestionCategory", QuestionCategory);
			dictionary.Add("Status", Status);

			return dictionary;
		}

		public Dictionary<string, object> ToDictionaryComplete()
		{
			var dictionary = new Dictionary<string, object>();

			dictionary.Add("Id", Id);
			dictionary.Add("QuestionName", QuestionName);
			dictionary.Add("QuestionCategory", QuestionCategory);
			dictionary.Add("QuestionRate", QuestionRate);
			dictionary.Add("QuestionOption", QuestionOption);
			dictionary.Add("Status", Status);

			return dictionary;
		}
	}
}
