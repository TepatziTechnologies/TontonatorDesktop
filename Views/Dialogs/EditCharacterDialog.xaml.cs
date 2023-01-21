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
	public partial class EditCharacterDialog : Window
	{
		private Character character;
		private readonly CharactersService _charactersService;

		public EditCharacterDialog(Character character)
		{
			InitializeComponent();
			_charactersService = new CharactersService();
			this.character = character;
			idTB.Text = character.Id;
			questionTB.Text = character.CharacterName;
			categoryCB.Text = character.CharacterCategory.ToString();
		}

		private void acceptBtn_Click(object sender, RoutedEventArgs e)
		{
			character.CharacterCategory = Enum.Parse<CharacterCategory>(categoryCB.Text, true);
			_charactersService.Update(character);
			this.Close();
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
