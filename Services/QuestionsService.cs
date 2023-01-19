using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TontonatorDesktopApp.Data.Repository;
using TontonatorDesktopApp.Models;

namespace TontonatorDesktopApp.Services
{
	public class QuestionsService : EntityBaseRepository<Question>
	{
		public QuestionsService() : base("questions")
		{

		}
	}
}
