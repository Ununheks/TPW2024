using System;
using System.Numerics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAPI
    {
        public abstract void CreateTable(float width, float height);
        public abstract void CreateBall(Vector2 pos, Vector2 velocity, float radius);
        public abstract Table GetTableInfo();
    }

    public class DataService : DataAPI
    {
        private Table? _table;

        public override void CreateTable(float width, float height)
        {
            _table = new Table(width, height);
        }

        public override void CreateBall(Vector2 pos, Vector2 velocity, float radius)
        {
            Ball newBall = new Ball(pos, velocity, radius);

            if (_table != null)
            {
                _table.AddBall(newBall);
            }
            else
            {
                Console.WriteLine("Table is not created yet. Cannot add ball.");
            }
        }

        public override Table GetTableInfo()
        {
            return _table;
        }
    }

    public class Ball
    {
        private Vector2 _pos;
        private Vector2 _velocity;
        private float _radius;
        private bool _isUpdated = false;

        public Vector2 Position { get => _pos; set => _pos = value; }
        public Vector2 Velocity { get => _velocity; set => _velocity = value; }
        public float Radius => _radius;

        public delegate void PositionUpdatedEventHandler();
        public event PositionUpdatedEventHandler PositionUpdated;

        public Ball(Vector2 pos, Vector2 velocity, float radius)
        {
            _pos = pos;
            _velocity = velocity;
            _radius = radius;

            Task.Run(async () =>
            {
                while (true)
                {
                    if(!_isUpdated)
                    {
                        Update();
                        await Task.Delay(1000);
                    }
                }
            });
        }

        public void Update()
        {
            Console.WriteLine($"Ball position: {_pos}");
            _pos += _velocity;
            _isUpdated = true;
            OnPositionUpdated();
        }

        protected virtual void OnPositionUpdated()
        {
            PositionUpdated?.Invoke();
        }
    }


    public class Table
    {
        private float _width;
        private float _height;
        private List<Ball> _balls;

        public float Width => _width;
        public float Height => _height;
        public List<Ball> Balls => _balls;

        public Table(float width, float height)
        {
            _width = width;
            _height = height;
            _balls = new List<Ball>();
        }

        public void AddBall(Ball ball)
        {
            _balls.Add(ball);
        }
    }
}
