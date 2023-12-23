using System;
using System.Collections.Generic;
using KerlkraftwerkGame.Entities;
using KerlkraftwerkGame.Global;
using KerlkraftwerkGame.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KerlkraftwerkGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D backgroundTexture;
        private Character mainCharacter;
        private InputController inputController;
        private List<Obstacle> obstacles;
        private Random random;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
            this.obstacles = new List<Obstacle>();
            this.random = new Random();
        }

        protected override void LoadContent()
        {
            Globals.Content = this.Content;

            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            Globals.SpriteBatch = this.spriteBatch;

            // Lade den Hintergrund
            this.backgroundTexture = this.Content.Load<Texture2D>("background");

            // Lade den Charakter
            this.mainCharacter = new Character(new Vector2(100, 345));

            // Initialisiere den InputController
            this.inputController = new InputController();
        }

        protected override void Update(GameTime gameTime)
        {
            this.mainCharacter.Update(gameTime);

            Globals.Update(gameTime);

            if (this.inputController.ShouldExit())
            {
                this.Exit();
            }

            this.inputController.Update(gameTime, this.mainCharacter);

            // Überprüfe Kollision mit den Hindernissen
            foreach (var obstacle in this.obstacles)
            {
                obstacle.Update(gameTime);

                if (this.mainCharacter.GetBoundingBox().Intersects(obstacle.GetBoundingBox()))
                {
                    // Hier kannst du den Code für das Neustarten des Spiels und das Entfernen von Hindernissen hinzufügen
                    this.RestartGame();
                    break; // Um sicherzustellen, dass das Spiel nur einmal neu gestartet wird, wenn eine Kollision auftritt
                }
            }

            // Entferne Hindernisse, die den Bildschirm verlassen haben
            this.obstacles.RemoveAll(obstacle => obstacle.Position.X + obstacle.Width < 0);

            // Prüfe, ob ein neues Hindernis hinzugefügt werden soll
            if (this.RandomShouldAddObstacle())
            {
                this.AddNewObstacle();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();

            this.spriteBatch.Draw(this.backgroundTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, this.CalculateBackgroundScale(), SpriteEffects.None, 0f);

            this.mainCharacter.Draw();

            // Zeichne die Hindernisse
            foreach (var obstacle in this.obstacles)
            {
                obstacle.Draw(this.spriteBatch);
            }

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        private void RestartGame()
        {
            this.mainCharacter = new Character(new Vector2(100, 345));
            this.obstacles.Clear();
        }

        private void AddNewObstacle()
        {
            // Lade die Hindernisse
            Texture2D obstacleTexture = this.Content.Load<Texture2D>("shuriken");

            // Erzeuge ein neues Hindernis
            Obstacle newObstacle = new Obstacle(
                obstacleTexture,  // Zufälliges Hindernisbild
                new Vector2(this.GraphicsDevice.Viewport.Width, this.GetRandomObstacleYPosition()));

            // Füge das Hindernis zur Liste hinzu
            this.obstacles.Add(newObstacle);
        }

        private int GetRandomObstacleYPosition()
        {
            // Gib eine zufällige Y-Position zurück (hier anpassen, je nachdem, wo du die Hindernisse haben möchtest)
            return this.random.Next(200, this.GraphicsDevice.Viewport.Height - 90);
        }

        private bool RandomShouldAddObstacle()
        {
            // Zufällig entscheiden, ob ein neues Hindernis hinzugefügt werden soll
            return this.random.Next(100) < 1; // Hier kannst du die Wahrscheinlichkeit anpassen (1% in diesem Beispiel)
        }

        private float CalculateBackgroundScale()
        {
            float scaleWidth = (float)this.GraphicsDevice.Viewport.Width / this.backgroundTexture.Width;
            float scaleHeight = (float)this.GraphicsDevice.Viewport.Height / this.backgroundTexture.Height;

            return Math.Max(scaleWidth, scaleHeight);
        }
    }
}