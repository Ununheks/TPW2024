using System;
using System.Collections.ObjectModel;
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

        public abstract void Start(int ballCount);

        public abstract IDisposable Subscribe(IObserver<IBall> observer);

        public abstract void Dispose();

    }
}
