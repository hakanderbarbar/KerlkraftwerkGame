using KerlkraftwerkGame.Entities;
using System.Numerics;

namespace KerlkraftwerkGame.TestEntities
{
    [TestClass]
    public class ObstacleTest
    {
        Obstacle obstacle = new Obstacle(800);

        [TestMethod]
        public void Postition()
        {
            Vector2 testPosition = new Vector2(800, 300);
            Assert.AreEqual(testPosition, obstacle.Position);

            testPosition = new Vector2(500, 500);
            obstacle.Position = testPosition;
            Assert.AreEqual(testPosition, obstacle.Position);
        }
    }
}
