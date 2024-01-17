using KerlkraftwerkGame.Entities;
using KerlkraftwerkGame.Managers;

namespace KerlkraftwerkGame.TestManagers
{
    [TestClass]
    public class ObstacleManagerTest
    {
        ObstacleManager obstacleManager = new ObstacleManager();

        [TestMethod]
        public void Add()
        {
            Obstacle obstacle = new Obstacle(800);

            Assert.IsTrue(obstacleManager.Obstacles.Count == 0);

            obstacleManager.AddObstacle(obstacle);

            Assert.IsTrue(obstacleManager.Obstacles.Count == 1);
            Assert.AreEqual(obstacle, obstacleManager.Obstacles[0]);
        }

        [TestMethod]
        public void Reset()
        {
            Obstacle obstacle = new Obstacle(800);

            Assert.IsTrue(obstacleManager.Obstacles.Count == 0);

            obstacleManager.AddObstacle(obstacle);

            Assert.IsTrue(obstacleManager.Obstacles.Count == 1);
            
            obstacleManager.Reset();

            Assert.IsTrue(obstacleManager.Obstacles.Count == 0);
        }

        [TestMethod]
        public void SetAllObstaclesSpeed()
        {
            Obstacle obstacle1 = new Obstacle(800);
            Obstacle obstacle2 = new Obstacle(800);

            obstacleManager.AddObstacle(obstacle1);
            obstacleManager.AddObstacle(obstacle2);

            obstacleManager.SetAllObstaclesSpeed(5.0f);

            Assert.AreEqual(5.0f, obstacle1.ObstacleSpeed);
            Assert.AreEqual(5.0f, obstacle2.ObstacleSpeed);
        }
    }
}
