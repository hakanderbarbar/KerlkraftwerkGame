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
        private ObstacleManager obstacleManager;
        private GameState gameState = GameState.StartScreen;
        private Texture2D pressAnyKeyTexture;
        private GameEventHandler gameEventHandler;
        private MapManager mapManager;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this.obstacles = new List<Obstacle>();
            this.random = new Random();
            this.obstacleManager = new ObstacleManager();
            this.obstacleManager.CollisionDetected += this.OnCollisionDetected;  // Reagiert auf CollisionDetected-Event
            this.mapManager = new MapManager(this.background, this.mainCharacter, this.obstacleManager);
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

            this.background = new Background("background");

            this.mainCharacter = new Character(new Vector2(100, 300));

            // Lade die PressAnyKeyToStart-Grafik
            this.pressAnyKeyTexture = this.Content.Load<Texture2D>("PressAnyKeyToStart");

            this.inputController = new InputController();

            this.gameEventHandler = new GameEventHandler();

            this.mapManager = new MapManager(this.background, this.mainCharacter, this.obstacleManager);

            // Abonniere das MapChanged-Event
            this.gameEventHandler.MapChanged += this.HandleMapChanged;
        }

        protected override void Update(GameTime gameTime)
        {
            switch (this.gameState)
            {
                case GameState.StartScreen:
                    // Überprüfe, ob eine Taste gedrückt wurde, um das Spiel zu starten
                    if (this.inputController.PressedAnyKey())
                    {
                        this.gameState = GameState.Playing;

                        // Stelle sicher, dass der Charakter bei Spielstart an der richtigen Position ist
                        this.mainCharacter = new Character(new Vector2(100, 300));
                    }

                    break;
                case GameState.Playing:
                    // Update des Charakters
                    this.mainCharacter.Update(gameTime);

                    this.mapManager.UpdateBackground(gameTime);

                    Globals.Update(gameTime);

                    // Überprüfe, ob das Spiel beendet werden soll
                    if (this.inputController.ShouldExit())
                    {
                        this.Exit();
                    }

                    // Update des InputControllers
                    this.inputController.Update(gameTime, this.mainCharacter);

                    // Überprüfe Kollision mit den Hindernissen
                    this.obstacleManager.Update(gameTime, this.mainCharacter);

                    // Prüfe, ob ein neues Hindernis hinzugefügt werden soll
                    if (this.RandomShouldAddObstacle())
                    {
                        this.AddNewObstacle();
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
                Vector2 pressAnyKeyPosition = new (10, 50); // Passe die Position an
                this.spriteBatch.Draw(this.pressAnyKeyTexture, pressAnyKeyPosition, Color.White);
            }
            else if (this.gameState == GameState.Playing)
            {
                // Zeichnung des Hintergrunds
                this.background.Draw();

                // Zeichnung des Charakters
                this.mainCharacter.Draw();

                // Zeichnung der Hindernisse
                this.obstacleManager.Draw(this.spriteBatch);
            }

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        private void HandleMapChanged(string newMap)
        {
            this.mapManager.ChangeMap();
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
            this.obstacleManager.AddObstacle(newObstacle);

            // Passe die Geschwindigkeit des neuen Hindernisses basierend auf dem aktuellen Hintergrund an
            newObstacle.SetSpeed(this.mapManager.GetObstacleSpeedForCurrentBackground());
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

        private void RestartGame()
        {
            this.gameState = GameState.StartScreen;
            this.mainCharacter = new Character(new Vector2(100, 300));
            this.obstacleManager.Reset();
        }

        private void OnCollisionDetected()
        {
            this.RestartGame();
        }
    }
}