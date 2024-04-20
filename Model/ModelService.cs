using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Reactive;
using System.Reactive.Linq;
using Logic;

namespace Model
{
    internal class PresentationModel : ModelAbstractApi
    {
        public ObservableCollection<IBall> Balls { get; } = new ObservableCollection<IBall>();
        private LogicAPI _logicAPI;

        public PresentationModel(LogicAPI? logicAPI = null)
        {
            _logicAPI = logicAPI ?? LogicAPI.CreateLogicService();
            _logicAPI.OnBallPositionsUpdated += UpdateBallPosition;
            eventObservable = Observable.FromEventPattern<BallChaneEventArgs>(this, "BallChanged");
        }

        private void UpdateBallPosition(object sender, List<Vector2> positions)
        {
            for (int i = 0; i < Math.Min(Balls.Count, positions.Count); i++)
            {
                IBall ball = Balls[i];
                ball.Top = positions[i].Y;
                ball.Left = positions[i].X; 
            }
        }

        public override void Dispose()
        {
            foreach (ModelBall item in Balls2Dispose)
                item.Dispose();
        }

        public override IDisposable Subscribe(IObserver<IBall> observer)
        {
            return eventObservable.Subscribe(x => observer.OnNext(x.EventArgs.Ball), ex => observer.OnError(ex), () => observer.OnCompleted());
        }

        public override void Start(int ballCount)
        {
            for (int i = 0; i < ballCount; i++)
            {
                ModelBall newBall = new ModelBall() { Diameter = 20 };
                Balls2Dispose.Add(newBall);
                BallChanged?.Invoke(this, new BallChaneEventArgs() { Ball = newBall });
            }
            _logicAPI.Start(ballCount, 10, 400, 420);
        }

        public event EventHandler<BallChaneEventArgs> BallChanged;

        private IObservable<EventPattern<BallChaneEventArgs>> eventObservable = null;
        private List<IDisposable> Balls2Dispose = new List<IDisposable>();
    }
}
