using Microsoft.Xna.Framework;
using System;

namespace LessRoomyMoreShooty.Component.Sprites.Environment
{

    public class Door : Sprite
    {
        private bool isOpen;

        public Door Exit { get; set; }
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                isOpen = value;
                if (isOpen)
                    Texture = ContentManager.PlayerTexture;
                else
                    Texture = ContentManager.ButtonTexture;
            }
        }

        public event EventHandler PlayerEntered;

        public Door()
        {
            IsOpen = false;
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            if (!(sprite is Player)) return;
            if (!IsOpen) return;

            PlayerEntered?.Invoke(this, null);
        }

    }

}
