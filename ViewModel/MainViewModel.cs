using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase, IDisposable
    {
        private ModelAbstractApi _modelLayer;
        private int _ballCount;
        private string _ballCountText;

        public MainWindowViewModel()
        {
            _modelLayer = ModelAbstractApi.CreateApi();
            StartCommand = new RelayCommand(Start);
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
            _modelLayer.Start(_ballCount);
        }

        public void Dispose()
        {
            _modelLayer.Dispose();
        }
    }
}



