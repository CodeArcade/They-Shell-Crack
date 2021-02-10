using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace LessRoomyMoreShooty.Component.Controls
{
    public class Button : Component
    {
        private Size InternalSize { get; set; }

        private MouseState CurrentMouse { get; set; }
        private MouseState PreviousMouse { get; set; }
        private bool IsMouseOver { get; set; }

        public Color FontColor { get; set; }
        public Color HoverColor { get; set; } = Color.Gray;
        public Color ForeColor { get; set; } = Color.White;
        public SpriteFont Font { get; set; }
        public string Text { get; set; }
        public bool Clicked { get; private set; }

        public Size Size
        {
            get
            {
                if (InternalSize == Size.Empty)
                {
                    if (AnimationManager.IsPlaying)
                        InternalSize = new Size(AnimationManager.AnimationRectangle.Width, AnimationManager.AnimationRectangle.Height);
                    else
                        InternalSize = new Size(Texture.Width, Texture.Height);
                }
                return InternalSize;
            }
            set
            {
                InternalSize = value;
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                if (AnimationManager.IsPlaying) return AnimationManager.AnimationRectangle;

                return new Rectangle((int)Position.X, (int)Position.Y, Size.Width, Size.Height);
            }
        }
        public Texture2D Texture { get; set; }

        public event EventHandler OnClick;

        public Button(Texture2D texture, SpriteFont font)
        {
            Texture = texture;
            Font = font;
            FontColor = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = ForeColor;

            if (IsMouseOver) color = HoverColor;

            spriteBatch.Draw(Texture, Rectangle, color);

            if (!string.IsNullOrEmpty(Text))
            {
                float x = (Rectangle.X + (Rectangle.Width / 2)) - (Font.MeasureString(Text).X / 2);
                float y = (Rectangle.Y + (Rectangle.Height / 2)) - (Font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(Font, Text, new Vector2(x, y), FontColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            PreviousMouse = CurrentMouse;
            CurrentMouse = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle(CurrentMouse.X, CurrentMouse.Y, 1, 1);
            Rectangle previousMouseRectangle = new Rectangle(CurrentMouse.X, CurrentMouse.Y, 1, 1);

            IsMouseOver = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                IsMouseOver = true;

                if (CurrentMouse.LeftButton == ButtonState.Released && PreviousMouse.LeftButton == ButtonState.Pressed)
                {
                    AudioManager.PlayEffect(ContentManager.ButtonClickSoundEffect, 0.25f);
                    OnClick?.Invoke(this, new EventArgs());
                }
            }

            if (Models.Controller.IsNonDirectionButtonPressed())
            {
                OnClick?.Invoke(this, new EventArgs());
            }
        }
    }
}