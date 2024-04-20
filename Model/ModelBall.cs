using Logic;
using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Model
{
    internal class ModelBall : IBall, IDisposable
    {
        public ModelBall()
        {
            Top = 0;
            Left = 0;
        }

        public double Top
        {
            get { return TopBackingField; }
            set
            {
                if (TopBackingField == value)
                    return;
                TopBackingField = value;
                RaisePropertyChanged();
            }
        }

        public double Left
        {
            get { return LeftBackingField; }
            set
            {
                if (LeftBackingField == value)
                    return;
                LeftBackingField = value;
                RaisePropertyChanged();
            }
        }
        public double Diameter { get; internal set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            UpdateTimer.Dispose();
        }

        private double TopBackingField;
        private double LeftBackingField;
        private Timer UpdateTimer;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update(object state)
        {
            Top = 0;
            Left = 0;
        }
    }
}
