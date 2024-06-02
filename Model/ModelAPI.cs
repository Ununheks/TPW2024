using System.ComponentModel;

namespace Model
{
    public interface IBall : INotifyPropertyChanged
    {
        double Top { get; }
        double Left { get; }
        double Diameter { get; }
    }

    public class BallChangeEventArgs : EventArgs
    {
        public IBall Ball { get; internal set; }
    }

    public abstract class ModelAPI : IObservable<IBall>, IDisposable
    {
        public static ModelAPI CreateService()
        {
            ModelService model = new ModelService();
            return model;
        }

        public abstract void Start(int ballCount);

        public abstract IDisposable Subscribe(IObserver<IBall> observer);

        public abstract void Dispose();
    }
}
