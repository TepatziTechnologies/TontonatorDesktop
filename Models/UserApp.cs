using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TontonatorDesktopApp.Data.Repository;

namespace TontonatorDesktopApp.Models
{
	public class UserApp : IEntityBase
	{
		public string? UserFullname { get; set; }
		public string Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }

		public UserApp()
		{

		}

		public UserApp(string username, string password)
		{
			this.Username = username;
			this.Password = password;
		}

		public Dictionary<string, object> ToDictionary()
		{
			var dictionary = new Dictionary<string, object>();

			dictionary.Add("Id", this.Id);
			dictionary.Add("UserFullname", this.UserFullname!);
			dictionary.Add("Username", this.Username);
			dictionary.Add("Password", this.Password);

			return dictionary;
		}
	}
}
