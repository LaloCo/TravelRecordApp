using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public MainVM ViewModel { get; set; }

        public LoginCommand(MainVM viewModel)
        {
            ViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var user = (User)parameter;

            if (user == null)
                return false;

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.Login();
        }
    }
}
