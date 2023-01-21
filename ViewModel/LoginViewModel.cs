using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TontonatorDesktopApp.Commands;
using TontonatorDesktopApp.Models;
using TontonatorDesktopApp.Services;
using TontonatorDesktopApp.Views;

namespace TontonatorDesktopApp.ViewModel
{
	public class LoginViewModel : INotifyPropertyChanged
	{
		private readonly UsersService _usersService;

		private string _usernameText { get; set; }
		private string _passwordText { get; set; }
		public ICommand QuitWindowCommand { get; set; }
		public ICommand LoginCommand { get; set; }
		public string ErrorText { get; set; }

		public string UsernameText
		{
			get { return _usernameText; }
			set 
			{ 
				_usernameText = value;
				OnPropertyChanged("UsernameText"); 
			}
		}
		
		public string PasswordText
		{
			get { return _passwordText; }
			set 
			{ 
				_passwordText = value;
				OnPropertyChanged("PasswordText"); 
			}
		}

		public LoginViewModel()
		{
			_usersService = new UsersService();
			ErrorText = "";
			UsernameText = "Usuario";
			QuitWindowCommand = new RelayCommand(new Action<object>(QuitWindow));
			LoginCommand = new RelayCommand(new Action<object>(LoginAction));
		}

		private void QuitWindow(object obj) => Application.Current.Shutdown();

		private void LoginAction(object obj){
			UserApp? user = _usersService.GetUserByCredentials(_usernameText, _passwordText);

			if(user != null){
				var dashboard = new DashboardView(user);
				Application.Current.MainWindow.Close();
				Application.Current.MainWindow = dashboard;
				dashboard.Show();
			}
			else {
				ErrorText = "Usuario no encontrado.";
				OnPropertyChanged("ErrorText");
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
