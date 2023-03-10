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
	public class UsersService : EntityBaseRepository<UserApp>
	{
		public UsersService() : base("users")
		{

		}

		public UserApp? GetUserByCredentials(string username, string password){
			var parentCollection = _firestoreDb.Collection(collection);
			var result = parentCollection.WhereIn(nameof(UserApp.Username), new List<string> { username }).GetSnapshotAsync().GetAwaiter().GetResult();

			UserApp? user = null;

			foreach (var document in result)
			{
				if (document.Exists)
				{
					var dictionary = document.ToDictionary();
					var json = JsonConvert.SerializeObject(dictionary);
					var userapp = JsonConvert.DeserializeObject<UserApp>(json);

					if (userapp != null)
					{
						if(userapp.Password == password) user = userapp;
					}
				}
			}

			return user;
		}
	}
}
