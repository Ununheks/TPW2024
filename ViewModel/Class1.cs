using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using Model;

namespace ViewModel
{
    // ViewModel class for individual balls
    public class BallViewModel : INotifyPropertyChanged
    {
        private Ball _ball;

        public Vector2 Position => _ball.Position;
        public float Radius => _ball.Radius;

        public BallViewModel(Ball ball)
        {
            _ball = ball;
            _ball.PropertyChanged += (_, args) => OnPropertyChanged(args.PropertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // ViewModel class for the ball simulation
    public class BallSimulationViewModel
    {
        private LogicService _logicService;
        private List<BallViewModel> _ballViewModels;

        public List<BallViewModel> BallViewModels => _ballViewModels;

        public BallSimulationViewModel()
        {
            _logicService = new LogicService(new DataService());
            _ballViewModels = new List<BallViewModel>();
        }

        // Methods to initialize the table, spawn balls, and start the simulation...
    }
}
