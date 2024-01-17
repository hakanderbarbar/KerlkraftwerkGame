using KerlkraftwerkGame.Entities;

namespace KerlkraftwerkGame.TestEntities
{
    [TestClass]
    public class JumpTest
    {
        [TestMethod]
        public void Jump()
        {
            // Erstelle eine echte Instanz der Character-Klasse

            Character character = new Character();

            // Act
            character.Jump();

            // Assert
            Assert.IsTrue(character.IsJumping);
            Assert.AreEqual(1, character.JumpsRemaining);
            Assert.AreEqual(-character.JumpSpeed, character.Velocity.Y);

        }
    }
}