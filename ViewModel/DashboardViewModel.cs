using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TontonatorDesktopApp.Commands;
using TontonatorDesktopApp.Commands.Actions;
using TontonatorDesktopApp.Helpers;
using TontonatorDesktopApp.Models;
using TontonatorDesktopApp.Services;
using TontonatorDesktopApp.Views.Dialogs;

namespace TontonatorDesktopApp.ViewModel
{
	public class DashboardViewModel : INotifyPropertyChanged
	{
		#region DatabaseServices
		private readonly QuestionsService _questionsService = new QuestionsService();
		private readonly CharactersService _charactersService = new CharactersService();
		#endregion

		#region PrivateVars
		private static Color _primaryColor = Color.FromRgb(45, 45, 45);
		private static Color _whiteColor = Color.FromRgb(255, 255, 255);
		private SolidColorBrush _primarySolidColorBrush = new SolidColorBrush(_primaryColor);
		private SolidColorBrush _whiteSolidColorBrush = new SolidColorBrush(_whiteColor);
		private List<Question> _questions = new List<Question>();
		private List<Character> _characetrs = new List<Character>();
		private FontFamily _fontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Assets/Fonts/#Montserrat");
		private DelegateLoadedAction _loadAction = null;
		#endregion

		#region Commands
		public ICommand HomeMenuButton { get; set; }
		public ICommand QuestionMenuButton { get; set; }
		public ICommand QuestionListButtonCommand { get; set; }
		public ICommand QuestionAddButtonCommand { get; set; }
		public ICommand QuestionInfoButtonCommand { get; set; }
		public ICommand QuestionEditButtonCommand { get; set; }
		public ICommand QuestionDeleteButtonCommand { get; set; }

		public ICommand CharacterMenuButtonCommand { get; set; }
		public ICommand CharacterListButtonCommand { get; set; }
		public ICommand CharacterInfoButtonCommand {get; set;}
		public ICommand CharacterEditButtonCommand { get; set; }
		public ICommand CharacterDeleteButtonCommand { get; set; }

		#endregion

		#region QuestionVars
		public Visibility QuestionGridVisibility { get; set; }
		public Visibility QuestionListVisibility { get; set; }
		public Visibility QuestionAddVisibility { get; set; }
		public SolidColorBrush QuestionListButtonColor { get; set; }
		public SolidColorBrush QuestionListButtonFontColor { get; set; }
		public SolidColorBrush QuestionAddButtonColor { get; set; }
		public SolidColorBrush QuestionAddButtonFontColor { get; set; }
		public bool QuestionListIsCurrent { get; set; }
		public bool QuestionAddIsCurrent { get; set; }
		#endregion

		#region CharacterVars
		public Visibility CharactersGridVisibility { get; set; }
		public Visibility CharactersListVisibility { get; set; }
		public Visibility CharactersAddVisibility { get; set; }
		public SolidColorBrush CharactersListButtonColor { get; set; }
		public SolidColorBrush CharactersListButtonFontColor { get; set; }

		public DashboardViewModel()
		{
			QuestionGridVisibility = Visibility.Visible;
			QuestionListVisibility = Visibility.Visible;
			QuestionAddVisibility = Visibility.Hidden;
			QuestionListButtonColor = _primarySolidColorBrush;
			QuestionAddButtonColor = _whiteSolidColorBrush;
			QuestionListButtonColor = _primarySolidColorBrush;
			QuestionAddButtonColor = _whiteSolidColorBrush;
			QuestionListButtonFontColor = _whiteSolidColorBrush;
			QuestionAddButtonFontColor = _primarySolidColorBrush;
			QuestionMenuButton = new RelayCommand(new Action<object>(QuestionMenuButtonAction));
			QuestionListButtonCommand = new RelayCommand(new Action<object>(QuestionListButtonAction));
			QuestionAddButtonCommand = new RelayCommand(new Action<object>(QuestionAddButtonAction));
			QuestionInfoButtonCommand = new RelayCommand(new Action<object>(QuestionInfoButtonAction));
			QuestionEditButtonCommand = new RelayCommand(new Action<object>(QuestionEditButtonAction));
			QuestionDeleteButtonCommand = new RelayCommand(new Action<object>(QuestionDeleteButtonAction));
			CharacterMenuButtonCommand = new RelayCommand(new Action<object>(CharacterMenuButtonAction));
			QuestionListIsCurrent = true;
			QuestionAddIsCurrent = false;

			CharactersGridVisibility = Visibility.Hidden;
			CharactersAddVisibility = Visibility.Hidden;
			CharactersListVisibility = Visibility.Hidden;
		}

		private void QuestionListButtonAction(object obj)
		{
			if (!QuestionListIsCurrent)
			{
				QuestionListIsCurrent = true;
				QuestionAddIsCurrent = false;
				QuestionListVisibility = Visibility.Visible;
				QuestionAddVisibility = Visibility.Hidden;
			}
		}

		private void QuestionAddButtonAction(object obj)
		{
			if (!QuestionAddIsCurrent)
			{
				QuestionListIsCurrent = false;
				QuestionAddIsCurrent = true;
				QuestionListVisibility = Visibility.Hidden;
				QuestionAddVisibility = Visibility.Visible;
				OnPropertyChanged("QuestionListVisibility");
				OnPropertyChanged("QuestionAddVisibility");
			}
		}

		private void QuestionMenuButtonAction(object obj)
		{
			QuestionGridVisibility = Visibility.Visible;
			CharactersGridVisibility = Visibility.Hidden;
			OnPropertyChanged("QuestionGridVisibility");
			OnPropertyChanged("CharactersGridVisibility");
		}

		private void CharacterMenuButtonAction(object obj)
		{
			QuestionGridVisibility = Visibility.Hidden;
			CharactersGridVisibility = Visibility.Visible;
			OnPropertyChanged("QuestionGridVisibility");
			OnPropertyChanged("CharactersGridVisibility");
		}
		
		public DelegateLoadedAction LoadAction
		{
			get
			{
				return _loadAction ?? (_loadAction = new DelegateLoadedAction(async () =>
				{
					SetQuestionItems();
					SetCharactersItems();
				}));
			}
		}

		private void SetQuestionItems()
		{
			var stackPanelList = FindChildHelper.Get<StackPanel>(Application.Current.MainWindow, "questionsTable");

			if (stackPanelList != null)
			{
				stackPanelList.Children.Clear();
				_questions = _questionsService.ReadAll();

				if (_questions.Count > 0)
				{
					foreach (var item in _questions)
					{
						stackPanelList.Children.Add(AddListQuestionItem(item));
					}
					stackPanelList.UpdateLayout();
				}

			}
		}

		private void SetCharactersItems()
		{
			var stackPanelList = FindChildHelper.Get<StackPanel>(Application.Current.MainWindow, "charactersTable");

			if (stackPanelList != null)
			{
				stackPanelList.Children.Clear();
				_characetrs = _charactersService.ReadAll();

				if (_characetrs.Count > 0)
				{
					foreach (var item in _characetrs)
					{
						stackPanelList.Children.Add(AddListQuestionItem(item));
					}
					stackPanelList.UpdateLayout();
				}

			}
		}



		private Grid AddListCharacterItem(Character character)
		{
			var grid = new Grid();
			var separator = new Separator();

			separator.HorizontalAlignment = HorizontalAlignment.Left;
			separator.VerticalAlignment = VerticalAlignment.Top;
			separator.Height = 27;
			separator.Width = 620;
			separator.Margin = new Thickness(0, 20, 0, 0);
			separator.Background = new SolidColorBrush(Colors.LightGray);

			grid.Height = 36;
			grid.Width = 640;
			grid.Children.Add(separator);
			grid.Children.Add(GenerateTableSeparator(new Thickness(321, 10, 0, 0)));
			grid.Children.Add(GenerateTableSeparator(new Thickness(141, 10, 0, 0)));
			grid.Children.Add(GenerateTableSeparator(new Thickness(430, 10, 0, 0)));
			grid.Children.Add(GenerateTableSeparator(new Thickness(520, 10, 0, 0)));
			grid.Children.Add(GenerateLabel(character.Id, 141, new Thickness(10, 5, 0, 0)));
			grid.Children.Add(GenerateLabel(character.CharacterName, 170, new Thickness(156, 5, 0, 0)));
			grid.Children.Add(GenerateLabel("Enabled", 81, new Thickness(445, 5, 0, 0)));
			grid.Children.Add(GenerateLabel(character.CharacterCategory.ToString(), 98, new Thickness(336, 5, 0, 0)));
			grid.Children.Add(GenerateButtonAction("Assets/png/info.png", new Thickness(544, 7, 0, 0), QuestionInfoButtonCommand, character.Id));
			grid.Children.Add(GenerateButtonAction("Assets/png/edit.png", new Thickness(569, 7, 0, 0), QuestionEditButtonCommand, character.Id));
			grid.Children.Add(GenerateButtonAction("Assets/png/trash.png", new Thickness(594, 7, 0, 0), QuestionDeleteButtonCommand, character.Id));

			return grid;
		}

		private Grid AddListQuestionItem(Question question)
		{
			var grid = new Grid();
			var separator = new Separator();

			separator.HorizontalAlignment = HorizontalAlignment.Left;
			separator.VerticalAlignment = VerticalAlignment.Top;
			separator.Height = 27;
			separator.Width = 620;
			separator.Margin = new Thickness(0, 20, 0, 0);
			separator.Background = new SolidColorBrush(Colors.LightGray);

			grid.Height = 36;
			grid.Width = 640;
			grid.Children.Add(separator);
			grid.Children.Add(GenerateTableSeparator(new Thickness(321, 10, 0, 0)));
			grid.Children.Add(GenerateTableSeparator(new Thickness(141, 10, 0, 0)));
			grid.Children.Add(GenerateTableSeparator(new Thickness(430, 10, 0, 0)));
			grid.Children.Add(GenerateTableSeparator(new Thickness(520, 10, 0, 0)));
			grid.Children.Add(GenerateLabel(question.Id, 141, new Thickness(10, 5, 0, 0)));
			grid.Children.Add(GenerateLabel(question.QuestionName, 170, new Thickness(156, 5, 0, 0)));
			grid.Children.Add(GenerateLabel(question.Status.ToString(), 81, new Thickness(445, 5, 0, 0)));
			grid.Children.Add(GenerateLabel(question.QuestionCategory.ToString(), 98, new Thickness(336, 5, 0, 0)));
			grid.Children.Add(GenerateButtonAction("Assets/png/info.png", new Thickness(544, 7, 0, 0), QuestionInfoButtonCommand, question.Id));
			grid.Children.Add(GenerateButtonAction("Assets/png/edit.png", new Thickness(569, 7, 0, 0), QuestionEditButtonCommand, question.Id));
			grid.Children.Add(GenerateButtonAction("Assets/png/trash.png", new Thickness(594, 7, 0, 0), QuestionDeleteButtonCommand, question.Id));

			return grid;
		}

		private Label GenerateLabel(string content, double width, Thickness margin)
		{
			var label = new Label();

			label.Content = content;
			label.HorizontalAlignment = HorizontalAlignment.Left;
			label.VerticalAlignment = VerticalAlignment.Top;
			label.FontSize = 10;
			label.FontFamily = _fontFamily;
			label.HorizontalContentAlignment = HorizontalAlignment.Center;
			label.Padding = new Thickness(0, 5, 0, 5);
			label.Margin = margin;
			label.Width = width;

			return label;
		}

		private Separator GenerateTableSeparator(Thickness margin)
		{
			var separator = new Separator();

			separator.HorizontalAlignment = HorizontalAlignment.Left;
			separator.VerticalAlignment = VerticalAlignment.Top;
			separator.Height = 15;
			separator.Width = 20;
			separator.RenderTransform = new RotateTransform(90);
			separator.RenderTransformOrigin = new Point(0.5, 0.5);
			separator.Background = new SolidColorBrush(Colors.LightGray);
			separator.Margin = margin;

			return separator;
		}

		private Button GenerateButtonAction(string imagePath, Thickness margin, ICommand command, string buttonParameter)
		{
			var button = new Button();
			var imageBrush = new ImageBrush();
			imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/" + imagePath, UriKind.Absolute));

			button.Content = "";
			button.Style = Application.Current.FindResource("RoundedCorners2") as Style;
			button.HorizontalAlignment = HorizontalAlignment.Left;
			button.VerticalAlignment = VerticalAlignment.Top;
			button.BorderBrush = null;
			button.Margin = margin;
			button.Padding = new Thickness(0, 0, 0, 0);
			button.BorderThickness = new Thickness(0, 0, 0, 0);
			button.Width = 20;
			button.Height = 20;
			button.Background = imageBrush;
			button.CommandParameter = buttonParameter;
			button.Command = command;

			return button;
		}

		public void QuestionInfoButtonAction(object obj)
		{
			var id = "";
			if (obj != null) id = (string) obj;

			var question = _questionsService.Read(nameof(Question.Id), id);

			if(question != null) new InfoQuestionDialog(question).ShowDialog();
			
		}

		public void QuestionEditButtonAction(object obj)
		{
			var id = "";
			if (obj != null) id = (string)obj;

			var question = _questionsService.Read(nameof(Question.Id), id);

			if (question != null) new EditQuestionDialog(question).ShowDialog();

			SetQuestionItems();
		}

		public void QuestionDeleteButtonAction(object obj)
		{
			var id = "";
			if (obj != null) id = (string)obj;

			var result = MessageBox.Show("¿Seguro que desea borrar el elemento?", "Atención", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes) _questionsService.Delete(id);

			SetQuestionItems();
		}

		public void CharacterInfoButtonAction(object obj)
		{
			var id = "";
			if (obj != null) id = (string) obj;

			var question = _questionsService.Read(nameof(Question.Id), id);

			if(question != null) new InfoQuestionDialog(question).ShowDialog();
			
		}

		public void CharacterEditButtonAction(object obj)
		{
			var id = "";
			if (obj != null) id = (string)obj;

			var question = _questionsService.Read(nameof(Question.Id), id);

			if (question != null) new EditQuestionDialog(question).ShowDialog();

			SetQuestionItems();
		}

		public void CharacterDeleteButtonAction(object obj)
		{
			var id = "";
			if (obj != null) id = (string)obj;

			var result = MessageBox.Show("¿Seguro que desea borrar el elemento?", "Atención", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes) _questionsService.Delete(id);

			SetQuestionItems();
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion
	}
}
