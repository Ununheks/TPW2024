using System.Windows.Input;
using GalaSoft.MvvmLight; 
using GalaSoft.MvvmLight.Command; 
using Model;

namespace ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ModelAPI _model;

        private int _numberOfBalls;

        public int NumberOfBalls
        {
            get => _numberOfBalls;
            set
            {
                _numberOfBalls = value;
                RaisePropertyChanged(nameof(NumberOfBalls));
            }
        }

        public ICommand StartSimulationCommand { get; }

        public MainViewModel()
        {
            _model = new ModelAPI();
            StartSimulationCommand = new RelayCommand(StartSimulation);
        }

        private void StartSimulation()
        {
            // Start the simulation using the ModelAPI
            _model.StartSimulation(NumberOfBalls);
        }
    }
}
