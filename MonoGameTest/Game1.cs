using System;
using Exam;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameTest.Metrics;

namespace MonoGameTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        Vector2 heroPosition = Vector2.Zero;

        private static string map = @"
WWWWWWWE
WEEEEEWE
WTTEEEEW
WPBEBEWW
WWWWWWWW";

        private static string crossMap = @"
EEEWEEE
EEWPWEE
EWBETWE
EEWEWEE
EEEWEEE";

        static Sokoban sokoban = new Sokoban(MapCreator.CreateMap(map));

        StepsCounter counter = new StepsCounter(sokoban);

        int pictureSize = 28;
        Texture2D texture;
        Texture2D wall;
        Texture2D box;
        Texture2D xMark;

        private double interval = 10;
        private double elapsedTime;

        SpriteFont textBlock;

        Vector2 position = Vector2.Zero;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("Nier");
            wall = Content.Load<Texture2D>("wall_2");
            box = Content.Load<Texture2D>("box");
            xMark = Content.Load<Texture2D>("X-mark");
            textBlock = Content.Load<SpriteFont>("text");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (elapsedTime > 64)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                    Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (keyboardState.IsKeyDown(Keys.Left))
                    sokoban.MakeTurn(Movement.Left);
                if (keyboardState.IsKeyDown(Keys.Right))
                    sokoban.MakeTurn(Movement.Right);
                if (keyboardState.IsKeyDown(Keys.Up))
                    sokoban.MakeTurn(Movement.Up);
                if (keyboardState.IsKeyDown(Keys.Down))
                    sokoban.MakeTurn(Movement.Down);
                if (keyboardState.IsKeyDown(Keys.R))
                    sokoban.RevertTurn();

                var adapter = new Adapter(sokoban.Map);
                var newMap = adapter.Map;
                map = newMap;
                elapsedTime = 0;
            }
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            var startPosition = Vector2.Zero;
            var rows = map.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var length = rows[0].Length;
            var centerPoint = new Vector2(
                (Window.ClientBounds.Width / 2) - (pictureSize / 2),
                (Window.ClientBounds.Height / 2) - (pictureSize / 2));

            if (length % 2 == 0)
            {
                startPosition.X = centerPoint.X - length / 2 * pictureSize;
            }
            else
            {
                startPosition.X = centerPoint.X - (int)Math.Ceiling(length / 2.0) * pictureSize;
            }

            var width = rows.Length;

            if (width % 2 != 0)
            {
                startPosition.Y = centerPoint.Y - (int) Math.Ceiling(width / 2.0) * pictureSize;
            }
            else
            {
                startPosition.Y = centerPoint.Y - length / 2 * pictureSize;
            }

            var startX = startPosition.X;

            spriteBatch.Begin();

            foreach (var row in rows)
            {
                foreach (var symbol in row)
                {
                    switch (symbol)
                    {
                        case 'P':
                            spriteBatch.Draw(texture, startPosition, null, Color.White, 0, Vector2.Zero, 0.125f, SpriteEffects.FlipHorizontally, 0);
                            break;
                        case 'W':
                            spriteBatch.Draw(wall, startPosition, null, Color.White, 0, Vector2.Zero, 0.125f, SpriteEffects.FlipHorizontally, 0);
                            break;
                        case 'B':
                            spriteBatch.Draw(box, startPosition, null, Color.White, 0, Vector2.Zero, 0.125f, SpriteEffects.FlipHorizontally, 0);
                            break;
                        case 'T':
                            spriteBatch.Draw(xMark, startPosition, null, Color.White, 0, Vector2.Zero, 0.125f, SpriteEffects.FlipHorizontally, 0);
                            break;
                    }

                    startPosition.X += pictureSize;
                }

                startPosition.X = startX;
                startPosition.Y += pictureSize;
            }

            if (sokoban.IsOver)
            {
                spriteBatch.DrawString(textBlock, "WE WON", new Vector2(200, 200), Color.Black);
            }
            spriteBatch.DrawString(textBlock, $"Step count: {counter.Count.ToString()}", new Vector2(100, 50), Color.Black);
            spriteBatch.DrawString(textBlock,
                $"Time: {gameTime.TotalGameTime.Minutes}:{gameTime.TotalGameTime.Seconds}", 
                new Vector2(100, 100), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
