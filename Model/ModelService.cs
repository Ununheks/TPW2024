using Logic;
using System.Reactive;
using System.Reactive.Linq;

namespace Model
{
    internal class ModelService : ModelAPI
    {
        private LogicAPI _logicAPI;
        private List<ModelBall> _ballsList = new List<ModelBall>();

        public ModelService(LogicAPI? logicAPI = null)
        {
            _logicAPI = logicAPI ?? LogicAPI.CreateLogicService();
            _logicAPI.OnBallPositionUpdated += UpdateBallPosition;
            eventObservable = Observable.FromEventPattern<BallChangeEventArgs>(this, "BallChanged");
        }

        private void UpdateBallPosition(object sender, ImmutableVector2 position)
        {
            ModelBall ball = _ballsList[0];
            ball.UpdatePosition(position);
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
                BallChanged?.Invoke(this, new BallChangeEventArgs() { Ball = newBall });
            }
            _logicAPI.Start(ballCount, 10, 400, 420);
        }

        public event EventHandler<BallChangeEventArgs> BallChanged;

        private IObservable<EventPattern<BallChangeEventArgs>> eventObservable = null;
        private List<IDisposable> Balls2Dispose = new List<IDisposable>();
    }
}
