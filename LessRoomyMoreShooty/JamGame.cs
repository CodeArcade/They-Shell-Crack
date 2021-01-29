using LessRoomyMoreShooty.Manager;
using LessRoomyMoreShooty.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Unity;

namespace LessRoomyMoreShooty
{
    public class JamGame : Game
    {
        private readonly GraphicsDeviceManager Graphics;
        private SpriteBatch SpriteBatch;

        [Dependency]
        public StateManager StateManager { get; set; }

        public int ScreenWidth => 1024;
        public int ScreenHeight => 768;

        public JamGame()
        {
            Graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.Title = "They Shell Crack";
            IsMouseVisible = true;

            // Set Screen size to HD-READY
            Graphics.PreferredBackBufferWidth = ScreenWidth;
            Graphics.PreferredBackBufferHeight = ScreenHeight;
            Graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            StateManager.ChangeTo<MenuState>(MenuState.Name);
        }

        protected override void Update(GameTime gameTime)
        {
            StateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            SpriteBatch.Begin();
            StateManager.Draw(gameTime, SpriteBatch);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
