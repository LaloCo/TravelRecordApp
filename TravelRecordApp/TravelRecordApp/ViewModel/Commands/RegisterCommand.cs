using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        private RegisterVM viewModel;

        public event EventHandler CanExecuteChanged;

        public RegisterCommand(RegisterVM viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            User user = (User)parameter;

            if (user != null)
            {
                if (user.Password == user.ConfirmPassword)
                {
                    if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                        return false;

                    return true;
                }

                return false;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            User user = (User)parameter;
            viewModel.Register(user);
        }
    }
}
