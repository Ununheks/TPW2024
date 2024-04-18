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

        public abstract void Start(int ballAmount, float ballRadius, float tableWidth, float tableHeight);
        public abstract object GetTableInfo();
        public abstract void CheckCollisions();
    }
}
