using Microsoft.Xna.Framework;
using System;
using System.Drawing;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;

namespace LessRoomyMoreShooty.States
{
    public class GameOverState : State
    {
        public static string Name => "GameOver";

        protected override void OnLoad()
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

    }
}