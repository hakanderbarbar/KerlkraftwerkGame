using KerlkraftwerkGame.Entities;
using KerlkraftwerkGame.Managers;

namespace KerlkraftwerkGame.TestManagers
{
    [TestClass]
    public class MapManagerTest
    {
        [TestMethod]
        public void GetObstacleSpeedTest() 
        {
            Character character = new Character();
            ObstacleManager obstacleManager = new ObstacleManager();
            Background background = new Background();

            MapManager mapManager = new MapManager(background, character, obstacleManager);

            background.SetCurrentTextureName("spaceBackground");

            Assert.AreEqual(175f, mapManager.GetObstacleSpeedForCurrentBackground());

            background.SetCurrentTextureName("gravityMachineBackground");

            Assert.AreEqual(425f, mapManager.GetObstacleSpeedForCurrentBackground());

            background.SetCurrentTextureName("background");

            Assert.AreEqual(300f, mapManager.GetObstacleSpeedForCurrentBackground());

            background.SetCurrentTextureName("default");

            Assert.AreEqual(300f, mapManager.GetObstacleSpeedForCurrentBackground());
        }

    }
}
