using KerlkraftwerkGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
