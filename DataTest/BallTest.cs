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

            Ball ballFromDataService = (Ball)dataAPI.CreateBall(initialPosition, initialVelocity);

            Assert.AreEqual(initialPosition, ballFromDataService.Position, "Initial position does not match.");
            Assert.AreEqual(initialVelocity, ballFromDataService.Velocity, "Initial velocity does not match.");

            dataAPI.SetBallVelocity(ballFromDataService, newVelocity);

            Assert.AreEqual(newVelocity, ballFromDataService.Velocity, "Updated velocity does not match.");
        }
    }
}
