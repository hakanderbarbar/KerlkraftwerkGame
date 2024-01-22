using KerlkraftwerkGame.Entities;
using Microsoft.Xna.Framework;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace KerlkraftwerkGame.TestEntities
{
    [TestClass]
    public class CharacterTest
    {
        Character character = new Character();

        [TestMethod]
        public void Jump()
        {
            character.Jump();

            Assert.IsTrue(character.IsJumping);
            Assert.AreEqual(1, character.JumpsRemaining);
            Assert.AreEqual(-character.JumpSpeed, character.Velocity.Y);

            //2. Sprung simulieren
            character.SetJumpsRemaining(1);
            character.SetIsJumping(false);
            character.Jump();

            Assert.AreEqual((-character.JumpSpeed / 2f), character.Velocity.Y);
        }

        [TestMethod]
        public void SetToStart() 
        {
            Vector2 startPos = new Vector2(100, 100);

            character.SetToStart(startPos);

            Assert.AreEqual(startPos, character.Position);
        }

        [TestMethod]
        public void SetGravity() 
        {
            float newGravity = 10;

            character.SetGravity(newGravity);

            Assert.AreEqual(newGravity, character.Gravity);
        }
    }
}