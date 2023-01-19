using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TontonatorDesktopApp.Data.Repository;
using TontonatorDesktopApp.Models;

namespace TontonatorDesktopApp.Services
{
	public class CharactersService : EntityBaseRepository<Character>
	{
		private QuestionsService _questionsService;

		public CharactersService() : base("characters")
		{
			_questionsService = new QuestionsService();
		}

		public virtual Character AddCharacter(Character entity)
		{
			DocumentReference document = _firestoreDb.Collection(collection).Document();
			entity.Id = document.Id;

			var questionsCollection = document.Collection("questions");

			foreach (var question in entity.Questions)
			{
				if (string.IsNullOrEmpty(question.Id))
				{
					var questionFromDb = _questionsService.Read(nameof(Question.QuestionName), question.QuestionName);
					if (string.IsNullOrEmpty(questionFromDb.Id))
					{
						var newQuestionCreated = _questionsService.Add(question);
						question.Id = newQuestionCreated.Id;
						questionsCollection.Document(question.Id).SetAsync(question.ToDictionaryComplete()).GetAwaiter().GetResult();
					}
					else
					{
						question.Id = questionFromDb.Id;
						questionsCollection.Document(question.Id).SetAsync(question.ToDictionaryComplete()).GetAwaiter().GetResult();
					}
				}
				else
				{
					questionsCollection.Document(question.Id).CreateAsync(question.ToDictionaryComplete()).GetAwaiter().GetResult();
				}
			}

			var result = document.SetAsync(entity.ToDictionary()).GetAwaiter().GetResult();

			return new Character();
		}

		public List<Character> ReadByQuestions(List<Question> questions)
		{
			List<Character> characters = new List<Character>();
			List<string> values = new List<string>();

			var parentCollection = _firestoreDb.Collection(collection);

			foreach (var question in questions)
			{
				values.Add(question.QuestionName);
			}

			return characters;
		}

		public List<Character> ReadByQuestion(Question question)
		{
			List<Character> characters = new List<Character>();

			var parentCollection = _firestoreDb.Collection(collection);
			var result = parentCollection.WhereArrayContains(nameof(Character.IdQuestions), question.Id).GetSnapshotAsync().GetAwaiter().GetResult();

			foreach (var document in result)
			{
				if (document.Exists)
				{
					var dictionary = document.ToDictionary();
					var json = JsonConvert.SerializeObject(dictionary);
					var character = JsonConvert.DeserializeObject<Character>(json);

					if (character != null)
					{
						character.Questions = GetCollectionQuestions(character.Id);
						characters.Add(character);
					}
				}
			}

			return characters;
		}

		private List<Question> GetCollectionQuestions(string id)
		{
			List<Question> questions = new List<Question>();

			var parentCollection = _firestoreDb.Collection(collection);
			var documentReference = parentCollection.Document(id);
			var questionsCollection = documentReference.Collection("questions");
			var result = questionsCollection.GetSnapshotAsync().GetAwaiter().GetResult();

			foreach (var document in result)
			{
				if (document.Exists)
				{
					var dictionary = document.ToDictionary();
					var json = JsonConvert.SerializeObject(dictionary);
					var character = JsonConvert.DeserializeObject<Question>(json);

					if (character != null) questions.Add(character);
				}
			}

			return questions;
		}
	}
}
