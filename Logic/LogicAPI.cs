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

        public abstract object CreateTable(float width, float height);
        public abstract void SpawnBalls(int amount, float radius);
        public abstract void CheckCollisions();
    }
}
