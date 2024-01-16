using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KerlkraftwerkGame.Entities;

namespace KerlkraftwerkGame.Managers
{
    public class MapManager
    {
        private Background background;
        private Character mainCharacter;
        private ObstacleManager obstacleManager;
        private float elapsedTime = 0f;
        private float backgroundChangeInterval = 5f;

        public MapManager(Background background, Character mainCharacter, ObstacleManager obstacleManager)
        {
            this.background = background;
            this.mainCharacter = mainCharacter;
            this.obstacleManager = obstacleManager;
        }

        public void UpdateBackground(GameTime gameTime)
        {
            this.elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (this.elapsedTime >= this.backgroundChangeInterval)
            {
                this.ChangeMap();
                this.elapsedTime = 0f;
            }
        }

        public float GetObstacleSpeedForCurrentBackground() // Geschwindigkeit für neu hinzugefügte Hindernisse basierend auf dem aktuellen Hintergrund anpassen
        {
            switch (this.background.CurrentTextureName)
            {
                case "spaceBackground":
                    return 175f; // Passe die Geschwindigkeit für den Weltraumhintergrund an

                case "gravityMachineBackground":
                    return 425f; // Passe die Geschwindigkeit für den Hintergrund der Gravitationsmaschine an

                case "background":
                    return 300f; // Passe die Geschwindigkeit für den Standardhintergrund an

                default:
                    return 300f; // Standardgeschwindigkeit, wenn der Hintergrund nicht erkannt wird
            }
        }

        public void ChangeMap()
        {
            int randomBackground = new Random().Next(3);
            switch (randomBackground)
            {
                case 0:
                    this.background.ChangeTexture("spaceBackground");
                    this.mainCharacter.SetGravity(650f); // Set-Methode um Gravity zu ändern
                    this.obstacleManager.SetAllObstaclesSpeed(175f); // Geschwindigkeit der Hindernisse anpassen
                    break;

                case 1:
                    this.background.ChangeTexture("gravityMachineBackground");
                    this.mainCharacter.SetGravity(1350f);
                    this.obstacleManager.SetAllObstaclesSpeed(425f); // Geschwindigkeit der Hindernisse anpassen
                    break;

                case 2:
                    this.background.ChangeTexture("background");
                    this.mainCharacter.SetGravity(1000f);
                    this.obstacleManager.SetAllObstaclesSpeed(300f); // Geschwindigkeit der Hindernisse anpassen
                    break;
            }
        }
    }
}
