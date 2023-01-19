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

namespace TontonatorDesktopApp.Views.Dialogs
{
	/// <summary>
	/// Lógica de interacción para InfoQuestionDialog.xaml
	/// </summary>
	public partial class InfoQuestionDialog : Window
	{
		public InfoQuestionDialog(Question question)
		{
			InitializeComponent();
			idTB.Text = question.Id;
			questionTB.Text = question.QuestionName;
			categoryTB.Text = question.QuestionCategory.ToString();
			statusTB.Text = question.Status.ToString();
		}

		private void acceptBtn_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
			this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
			this.DragMove();
        }
    }
}
