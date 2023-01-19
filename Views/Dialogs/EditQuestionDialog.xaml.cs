using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TontonatorDesktopApp.Models;
using TontonatorDesktopApp.Models.Enums;
using TontonatorDesktopApp.Services;

namespace TontonatorDesktopApp.Views.Dialogs
{
	/// <summary>
	/// Lógica de interacción para InfoQuestionDialog.xaml
	/// </summary>
	public partial class EditQuestionDialog : Window
	{
		private Question question;
		private readonly QuestionsService _questionsService;
		public EditQuestionDialog(Question question)
		{
			InitializeComponent();
			_questionsService = new QuestionsService();
			this.question = question;
			idTB.Text = question.Id;
			questionTB.Text = question.QuestionName;
			categoryCB.Text = question.QuestionCategory.ToString();
			statusCB.Text = question.Status.ToString();
		}

		private void acceptBtn_Click(object sender, RoutedEventArgs e)
		{
			question.QuestionCategory = Enum.Parse<QuestionCategory>(categoryCB.Text, true);
			question.Status = Enum.Parse<Status>(statusCB.Text, true);
			_questionsService.Update(question);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}
	}
}
