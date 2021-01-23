using Microsoft.Xna.Framework;
using System;
using System.Drawing;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;
using System.Linq;

namespace LessRoomyMoreShooty.States
{
    public partial class GameState : State
    {
        public static string Name = "Game";

        protected override void OnLoad()
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            if (!Components.Any(x => x is Component.Sprites.Player))
            {
                StateManager.ChangeTo<GameOverState>(GameOverState.Name);
                return;
            }

            base.PostUpdate(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

    }
}