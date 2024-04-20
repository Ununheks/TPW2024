using System;
using System.ComponentModel;
using Logic;

namespace Model
{
    public interface IBall : INotifyPropertyChanged
    {
        double Top { get; }
        double Left { get; }
        double Diameter { get; }
    }

    public class BallChaneEventArgs : EventArgs
    {
        public IBall Ball { get; internal set; }
    }

    public abstract class ModelAbstractApi : IObservable<IBall>, IDisposable
    {
        public static ModelAbstractApi CreateApi()
        {
            PresentationModel model = new PresentationModel();
            return model;
        }

        public abstract void Start();

        #region IObservable

        public abstract IDisposable Subscribe(IObserver<IBall> observer);

        #endregion IObservable

        #region IDisposable

        public abstract void Dispose();

        #endregion IDisposable
    }
}
