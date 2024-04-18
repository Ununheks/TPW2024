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

            logicAPI.CreateTable(100, 200);
            logicAPI.SpawnBalls(20, 2);

            Assert.AreEqual(100, logicAPI.GetTableDimensions().X, "Table width is incorrect.");
            Assert.AreEqual(200, logicAPI.GetTableDimensions().Y, "Table height is incorrect.");

            Assert.AreEqual(20, logicAPI.GetBallData().Count, "Incorrect number of balls spawned.");
        }
    }

    // Mock implementation of DataAPI for testing purposes
    class MockDataAPI : Data.DataAPI
    {
        public override object CreateBall(Vector2 pos, Vector2 velocity, Action callback = null)
        {
            // For testing purposes, return an empty object
            return new object();
        }

        public override System.Numerics.Vector2 GetBallPosition(object ball)
        {
            throw new NotImplementedException();
        }

        public override System.Numerics.Vector2 GetBallVelocity(object ball)
        {
            throw new NotImplementedException();
        }

        public override void SetBallVelocity(object ball, System.Numerics.Vector2 newVelocity)
        {
            throw new NotImplementedException();
        }
    }
}
