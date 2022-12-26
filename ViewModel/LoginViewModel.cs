using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TontonatorDesktopApp.Commands;

namespace TontonatorDesktopApp.ViewModel
{
	public class LoginViewModel : INotifyPropertyChanged
	{
		private ICommand quitWindowCommand;

		private string _usernameText { get; set; }
		public ICommand QuitWindowCommand { get; set; }

		public string UsernameText
		{
			get { return _usernameText; }
			set 
			{ 
				_usernameText = value;
				OnPropertyChanged("UsernameText"); 
			}
		}

		public LoginViewModel()
		{
			UsernameText = "Usuario";
			QuitWindowCommand = new RelayCommand(new Action<object>(QuitWindow));
		}

		private void QuitWindow(object obj) => Application.Current.Shutdown();

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
