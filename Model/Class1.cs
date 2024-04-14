using System.Numerics;
using System.ComponentModel;
using Logic;

namespace Model
{
    // Model class representing individual balls
    public class Ball : INotifyPropertyChanged
    {
        private Vector2 _position;
        private float _radius;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; OnPropertyChanged(); }
        }

        public float Radius
        {
            get { return _radius; }
            set { _radius = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
