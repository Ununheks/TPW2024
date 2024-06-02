using Data;
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

        public override IDataBall CreateBall(Vector2 pos, Vector2 velocity, Action<IDataBall> positionUpdatedCallback = null)
        {
            // For testing purposes, return an empty object
            return new IDataBall();
        }
    }
}
