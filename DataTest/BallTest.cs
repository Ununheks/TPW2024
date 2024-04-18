using Data;
using System.Numerics;

namespace DataTest
{
    [TestClass]
    public class BallTest
    {
        [TestMethod]
        public void TestBall()
        {
            // Arrange
            Vector2 initialPosition = new Vector2(10, 10);
            Vector2 initialVelocity = new Vector2(1, -1);
            Vector2 newVelocity = new Vector2(1, 1);

            DataAPI dataAPI = DataAPI.CreateDataService();

            object ball = dataAPI.CreateBall(initialPosition, initialVelocity);

            Assert.AreEqual(initialPosition, dataAPI.GetBallPosition(ball), "Initial position does not match.");
            Assert.AreEqual(initialVelocity, dataAPI.GetBallVelocity(ball), "Initial velocity does not match.");

            dataAPI.SetBallVelocity(ball, newVelocity);

            Assert.AreEqual(newVelocity, dataAPI.GetBallVelocity(ball), "Updated velocity does not match.");
        }
    }
}
