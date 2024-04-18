using Data;
using System.Numerics;

namespace Logic
{
    public abstract class LogicAPI
    {
        public static LogicAPI CreateLogicService(DataAPI dataAPI = null)
        {
            return new LogicService(dataAPI);
        }

        public abstract void CreateTable(float width, float height);
        public abstract Vector2 GetTableDimensions();
        public abstract void SpawnBalls(int amount, float radius);
        public abstract List<object> GetBallData();
        public abstract void CheckCollisions();
    }
}
