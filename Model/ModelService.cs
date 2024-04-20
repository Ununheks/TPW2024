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
        private LogicAPI _logicAPI;
        private List<ModelBall> _ballsList = new List<ModelBall>();

        public PresentationModel(LogicAPI? logicAPI = null)
        {
            _logicAPI = logicAPI ?? LogicAPI.CreateLogicService();
            _logicAPI.OnBallsPositionsUpdated += UpdateBallsPosition;
            eventObservable = Observable.FromEventPattern<BallChaneEventArgs>(this, "BallChanged");
        }

        private void UpdateBallsPosition(object sender, List<Vector2> positions)
        {
            for (int i = 0; i < Math.Min(_ballsList.Count, positions.Count); i++)
            {
                ModelBall ball = _ballsList[i];
                ball.UpdatePosition(positions[i]);
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
                _ballsList.Add(newBall); // Add the ball to the list
                BallChanged?.Invoke(this, new BallChaneEventArgs() { Ball = newBall });
            }
            _logicAPI.Start(ballCount, 10, 400, 420);
        }

        public event EventHandler<BallChaneEventArgs> BallChanged;

        private IObservable<EventPattern<BallChaneEventArgs>> eventObservable = null;
        private List<IDisposable> Balls2Dispose = new List<IDisposable>();
    }
}
