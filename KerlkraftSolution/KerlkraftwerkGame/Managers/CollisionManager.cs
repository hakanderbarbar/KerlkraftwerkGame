using KerlkraftwerkGame.Entities;

namespace KerlkraftwerkGame.Managers
{
    public class CollisionManager
    {
        public bool CheckCollision(Character character, Obstacle obstacle)
        {
            return character.GetBoundingBox().Intersects(obstacle.GetBoundingBox());
        }
    }
}
