using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TontonatorDesktopApp.Data.Repository;
using TontonatorDesktopApp.Models.Enums;

namespace TontonatorDesktopApp.Models
{
	public class Character : IEntityBase
	{
		public string Id { get; set; }
		public string CharacterName { get; set; }
		public CharacterCategory CharacterCategory { get; set; }
		public List<Question> Questions { get; set; }
		public string[] IdQuestions { get; set; }

		public Character()
		{

		}

		public Character(string characterName, CharacterCategory characterCategory, List<Question> questions)
		{
			CharacterName = characterName;
			CharacterCategory = characterCategory;
			Questions = questions;
		}

		public Dictionary<string, object> ToDictionary()
		{
			var dictionary = new Dictionary<string, object>();

			dictionary.Add("Id", Id);
			dictionary.Add("CharacterName", CharacterName);
			dictionary.Add("CharacterCategory", CharacterCategory);

			return dictionary;
		}
	}
}
