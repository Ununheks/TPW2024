using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight; 
using GalaSoft.MvvmLight.Command; 
using Model;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase, IDisposable

    {
        #region public API

        public MainWindowViewModel()
        {
            ModelLayer = ModelAbstractApi.CreateApi();
            IDisposable observer = ModelLayer.Subscribe<IBall>(x => Balls.Add(x));
            ModelLayer.Start();
        }

        public ObservableCollection<IBall> Balls { get; } = new ObservableCollection<IBall>();

        #endregion public API

        #region IDisposable

        public void Dispose()
        {
            ModelLayer.Dispose();
        }

        #endregion IDisposable

        #region private

        private ModelAbstractApi ModelLayer;

        #endregion private
    }
}
