using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TravelRecordApp.ViewModel.Commands
{
    public class NavigationCommand : ICommand
    {
        public HomeVM HomeViewModel { get; set; }

        public NavigationCommand(HomeVM homeVM)
        {
            HomeViewModel = homeVM;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            HomeViewModel.Navigate();
        }
    }
}
