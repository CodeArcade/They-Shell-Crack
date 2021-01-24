using Microsoft.Xna.Framework;
using System;
using System.Drawing;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = Microsoft.Xna.Framework.Color;
using System.Linq;
using LessRoomyMoreShooty.Component.Controls;
using LessRoomyMoreShooty.Component.Sprites.Environment;
using LessRoomyMoreShooty.Component.Sprites.Enemies;

namespace LessRoomyMoreShooty.States
{
    public partial class GameState : State
    {
        public static string Name = "Game";
        public int Level { get; set; }
        public int RemainingSeconds { get; set; }
        public double SecondTimer { get; set; }
        protected override void OnLoad()
        {
            Level = 1;
            RemainingSeconds = 60 * 10;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (SecondTimer >= 1) { RemainingSeconds--; SecondTimer = 0; }
            SecondTimer += gameTime.ElapsedGameTime.TotalSeconds;

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

            if (!AreEnemiesAlive())
            {
                TopDoor.IsOpen = true;
                BottomDoor.IsOpen = true;
                LeftDoor.IsOpen = true;
                RightDoor.IsOpen = true;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentManager.BackgroundTexture, new Rectangle(0, 0, 1024, 768), Color.White);
            base.Draw(gameTime, spriteBatch);
        }

        private void UpdateUi()
        {
            GetLabel("HealthLabel").Text = $"{Player.CurrentHealth}/{Player.MaxHealth}";
            GetLabel("AmmoLabel").Text = Player.CurrentAmmo > 0 ? $"{Player.CurrentAmmo}/{Player.MaxAmmo}" : "Reloading";
            GetLabel("LevelLabel").Text = $"{Level}";
            GetLabel("TimeLabel").Text = RemainingSeconds <= 60 ? $"{RemainingSeconds}" : $"{(RemainingSeconds / 60).ToString().PadLeft(2, '0')}:{(RemainingSeconds % 60).ToString().PadLeft(2, '0')}";
        }

        private Label GetLabel(string name) => (Label)Components.FirstOrDefault(x => x is Label && ((Label)x).Name == name);

        private bool AreEnemiesAlive() => Components.Any(x => x is Enemy);

        private void PlayerEnteredDoor(object sender, EventArgs e)
        {
            TopDoor.IsOpen = false;
            BottomDoor.IsOpen = false;
            LeftDoor.IsOpen = false;
            RightDoor.IsOpen = false;

            Door entry = (Door)sender;
            Player.Position = new Vector2(entry.Exit.Position.X - (entry == LeftDoor ? Player.Size.Width : 0), entry.Exit.Position.Y - (entry == TopDoor ? Player.Size.Height : 0));

            Level++;
            AddComponent(new TestDummy() { Position = new Vector2(new Random().Next(0, 800), new Random().Next(0, 500)) });
        }

    }
}