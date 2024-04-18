using Logic;
using System.Numerics;

namespace LogicTest
{
    [TestClass]
    public class LogicAPITest
    {
        [TestMethod]
        public void TestCreateTableAndSpawnBalls()
        {
            MockDataAPI mockDataAPI = new MockDataAPI();
            LogicAPI logicAPI = LogicAPI.CreateLogicService(mockDataAPI);

            logicAPI.Start(20, 2, 100, 200);

            Table table = (Table)logicAPI.GetTableInfo();

            Assert.AreEqual(100, table.Width, "Table width is incorrect.");
            Assert.AreEqual(200, table.Height, "Table height is incorrect.");

            Assert.AreEqual(20, table.Balls.Count, "Incorrect number of balls spawned.");
        }
    }

    class MockDataAPI : Data.DataAPI
    {
        public override object CreateBall(Vector2 pos, Vector2 velocity, Action callback = null)
        {
            // For testing purposes, return an empty object
            return new object();
        }

        public override Vector2 GetBallPosition(object ball)
        {
            throw new NotImplementedException();
        }

        public override Vector2 GetBallVelocity(object ball)
        {
            throw new NotImplementedException();
        }

        public override void SetBallVelocity(object ball, Vector2 newVelocity)
        {
            throw new NotImplementedException();
        }
    }
}
