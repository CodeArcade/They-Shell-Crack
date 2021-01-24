using Microsoft.Xna.Framework;
using System;
using System.Drawing;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;
using System.Linq;
using LessRoomyMoreShooty.Component.Controls;

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

            UpdateUi();
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

        private void UpdateUi()
        {
            GetLabel("HealthLabel").Text = $"{Player.CurrentHealth}/{Player.MaxHealth}";
            GetLabel("AmmoLabel").Text = Player.CurrentAmmo > 0? $"{Player.CurrentAmmo}/{Player.MaxAmmo}" : "Reloading";
            GetLabel("LevelLabel").Text = $"1";
            GetLabel("TimeLabel").Text = $"{DateTime.Now}";
        }

        private Label GetLabel(string name)
        {
            return (Label)Components.FirstOrDefault(x => x is Label && ((Label)x).Name == name);
        }

    }
}