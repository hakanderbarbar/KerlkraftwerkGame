using System;
using System.Collections.Generic;
using KerlkraftwerkGame.Entities;
using KerlkraftwerkGame.Global;
using KerlkraftwerkGame.Managers;
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
        private Background background;
        private Character mainCharacter;
        private InputController inputController;
        private List<Obstacle> obstacles;
        private Random random;
        private GameState gameState = GameState.StartScreen;
        private Texture2D pressAnyKeyTexture;
        private GameEventHandler gameEventHandler;
        private float elapsedTime = 0f;
        private float backgroundChangeInterval = 5f;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this.obstacles = new List<Obstacle>();
            this.random = new Random();
        }

        private enum GameState
        {
            StartScreen,
            Playing,
        }

        protected override void LoadContent()
        {
            Globals.Content = this.Content;
            Globals.GraphicsDevice = this.GraphicsDevice;
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            Globals.SpriteBatch = this.spriteBatch;

            // Lade den Hintergrund mit dem Anfangsbild "background"
            this.background = new Background("background");

            // Lade den Charakter
            this.mainCharacter = new Character(new Vector2(100, 300));

            // Lade die PressAnyKeyToStart-Grafik
            this.pressAnyKeyTexture = this.Content.Load<Texture2D>("PressAnyKeyToStart");

            // Initialisiere den InputController
            this.inputController = new InputController();

            // Initialisiere den GameEventHandler
            this.gameEventHandler = new GameEventHandler();

            // Abonniere das MapChanged-Event
            this.gameEventHandler.MapChanged += this.HandleMapChanged;
        }

        protected override void Update(GameTime gameTime)
        {
            switch (this.gameState)
            {
                case GameState.StartScreen:
                    // Überprüfe, ob eine Taste gedrückt wurde, um das Spiel zu starten
                    if (Keyboard.GetState().GetPressedKeys().Length > 0)
                    {
                        this.gameState = GameState.Playing;

                        // Stelle sicher, dass der Charakter bei Spielstart an der richtigen Position ist
                        this.mainCharacter = new Character(new Vector2(100, 300));
                    }

                    break;
                case GameState.Playing:
                    // Update des Charakters
                    this.mainCharacter.Update(gameTime);

                    Globals.Update(gameTime);

                    // Überprüfe, ob das Spiel beendet werden soll
                    if (this.inputController.ShouldExit())
                    {
                        this.Exit();
                    }

                    // Update des InputControllers
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

                    // Überwache die Zeit und ändere den Hintergrund nach einer bestimmten Zeitspanne
                    this.elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (this.elapsedTime >= this.backgroundChangeInterval)
                    {
                        // Ändere den Hintergrund
                        this.ChangeBackground();

                        // Setze die Zeit zurück !!!ERKLÄREN BITTE WAS IST ELAPSED TIME
                        this.elapsedTime = 0f;
                    }

                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();

            if (this.gameState == GameState.StartScreen)
            {
                // Zeichne den Charakter und die Map im StartScreen
                this.background.Draw();
                this.mainCharacter.Draw();

                // Zeichne die PressAnyKeyToStart-Grafik
                Vector2 pressAnyKeyPosition = new Vector2(10, 50); // Passe die Position an
                this.spriteBatch.Draw(this.pressAnyKeyTexture, pressAnyKeyPosition, Color.White);
            }
            else if (this.gameState == GameState.Playing)
            {
                // Zeichnung des Hintergrunds
                this.background.Draw();

                // Zeichnung des Charakters
                this.mainCharacter.Draw();

                // Zeichnung der Hindernisse
                foreach (var obstacle in this.obstacles)
                {
                    obstacle.Draw(this.spriteBatch);
                }
            }

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        private void HandleMapChanged(string newMap)
        {
            this.ChangeBackground();
        }

        private void RestartGame()
        {
            this.gameState = GameState.StartScreen;
            this.mainCharacter = new Character(new Vector2(100, 300));
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

        private void ChangeBackground()
        {
            // Random Zahl wird berechnet um den Hintergrund zu ändenr
            int randomBackground = this.random.Next(3); // Es gibt 3 verschieden Hintergründe zwischen denen gewechselt wird
            switch (randomBackground)
            {
                case 0:
                    this.background.ChangeTexture("spaceBackground");
                    break;

                case 1:
                    this.background.ChangeTexture("gravityMachineBackground");
                    break;

                case 2:
                    this.background.ChangeTexture("background");
                    break;
            }
        }
    }
}