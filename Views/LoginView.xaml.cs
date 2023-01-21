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
using TontonatorDesktopApp.ViewModel;

namespace TontonatorDesktopApp.Views
{
	/// <summary>
	/// Lógica de interacción para Login.xaml
	/// </summary>
	public partial class Login : Window
	{
		private readonly LoginViewModel _viewModel;
		public Login()
		{
			_viewModel = new LoginViewModel();
			InitializeComponent();
			DataContext = _viewModel;
		}

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
			_viewModel.PasswordText = ((PasswordBox)sender).Password;
		}
    }
}
