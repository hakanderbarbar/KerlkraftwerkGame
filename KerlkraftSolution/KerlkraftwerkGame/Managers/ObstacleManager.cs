using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KerlkraftwerkGame.Entities;
using KerlkraftwerkGame.Global;

namespace KerlkraftwerkGame.Managers
{
    public class ObstacleManager
    {
        private List<Obstacle> obstacles;
        private CollisionManager collisionManager;
        private Random random;

        public ObstacleManager(CollisionManager collisionManager)
        {
            this.obstacles = new List<Obstacle>();
            this.collisionManager = collisionManager;
        }

        public ObstacleManager()
        {
            this.collisionManager = new CollisionManager();
            this.obstacles = new List<Obstacle>();
            this.random = new Random();
        }

        public event Action CollisionDetected;

        // Fügt neues Obstacle in die Liste hinzu
        public void AddObstacle(Obstacle obstacle)
        {
            this.obstacles.Add(obstacle);
        }

        public void Reset()
        {
            this.obstacles.Clear();
        }

        public void SetAllObstaclesSpeed(float newSpeed)
        {
            foreach (var obstacle in this.obstacles)
            {
                obstacle.SetSpeed(newSpeed);
            }
        }

        public void Update(GameTime gameTime, Character mainCharacter)
        {
            foreach (var obstacle in this.obstacles)
            {
                obstacle.Update(gameTime);

                if (this.collisionManager.CheckCollision(mainCharacter, obstacle))
                {
                    this.CollisionDetected?.Invoke();
                    break;
                }
            }

            this.obstacles.RemoveAll(obstacle => obstacle.Position.X + obstacle.Width < 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var obstacle in this.obstacles)
            {
                obstacle.Draw(spriteBatch);
            }
        }
    }
}
