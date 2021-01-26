using LessRoomyMoreShooty.Component.Controls;
using LessRoomyMoreShooty.Component.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LessRoomyMoreShooty.Component.Effects
{
    public class DamageNumber : Label
    {
        private double TTL { get; set; }
        private double TimeLived { get; set; }

        public DamageNumber(int damage, Vector2 position) : base(null)
        {
            Font = ContentManager.KenneyMini;
            Text = damage.ToString();
            FontColor = Color.Red;
            TTL = 0.25;
            Position = new Vector2(position.X + new Random().Next(-5, 6), position.Y + 1);
            FontScale = 2;
        }

        public override void Update(GameTime gameTime)
        {
            TimeLived += gameTime.ElapsedGameTime.TotalSeconds;

            if (TimeLived >= TTL)
            {
                IsRemoved = true;
                return;
            }

            base.Update(gameTime);
        }

    }
}
