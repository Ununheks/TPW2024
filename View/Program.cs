using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace View
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var viewModel = new BallSimulationViewModel();
            var view = new View.MainView();
            view.DataContext = viewModel; // Set the ViewModel as the DataContext
            view.ShowDialog(); // Show the view as a dialog
        }
    }
}
