using Microsoft.Xna.Framework;
using System;
using System.Drawing;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;

namespace LessRoomyMoreShooty.States
{
    public partial class MenuState : State
    {
        public static string Name => "Menu";

        protected override void OnLoad()
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentManager.MenuBackgroundTexture, new Rectangle(0, 0, 1024, 768), Color.White);

            base.Draw(gameTime, spriteBatch);
        }

    }
}