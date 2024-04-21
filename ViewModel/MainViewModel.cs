using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase, IDisposable
    {
        private ModelAPI _modelLayer;
        private int _ballCount;
        private string _ballCountText;
        private bool _isStarted = false;

        public ObservableCollection<IBall> Balls { get; } = new ObservableCollection<IBall>();

        public MainWindowViewModel()
        {
            _modelLayer = ModelAPI.CreateService();
            IDisposable observer = _modelLayer.Subscribe<IBall>(x => Balls.Add(x));
            StartCommand = new RelayCommand(Start, () => !_isStarted);
        }

        public string BallCountText
        {
            get { return _ballCountText; }
            set { Set(ref _ballCountText, value); }
        }

        public int BallCount
        {
            get { return _ballCount; }
            set { Set(ref _ballCount, value); }
        }

        public ICommand StartCommand { get; }

        private void Start()
        {
            if (int.TryParse(BallCountText, out _ballCount) && _ballCount > 0)
            {
                _modelLayer.Start(_ballCount);
                _isStarted = true; 
                RaisePropertyChanged(() => StartCommand); 
            }
            else
            {
                // Handle invalid input
            }
        }

        public void Dispose()
        {
            _modelLayer.Dispose();
        }
    }
}
