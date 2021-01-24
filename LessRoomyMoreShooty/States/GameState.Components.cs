using Microsoft.Xna.Framework;
using LessRoomyMoreShooty.Component.Sprites.Item;
using LessRoomyMoreShooty.Component.Controls;
using LessRoomyMoreShooty.Component.Sprites;
using System.Drawing;

namespace LessRoomyMoreShooty.States
{
    public partial class GameState : State
    {
        private Player Player { get; set; }

        protected override void LoadComponents()
        {
            AddUi();

            Player = new Player()
            {
                Position = new Vector2(200, 200)
            };
            AddComponent(Player);

            AddComponent(new AmmoUpItem()
            {
                Position = new Vector2(500, 200)
            });

            AddComponent(new DamageUpItem()
            {
                Position = new Vector2(700, 200)
            });

            for (int i = 0; i < 5; i++)
            {
                AddComponent(new MovementSpeedUpItem()
                {
                    Position = new Vector2(200, 500)
                });
            }
        }

        private void AddUi()
        {
            AddComponent(new Sprite()
            {
                Position = new Vector2(180, 53),
                Texture = ContentManager.HeartTexture,
                Size = new Size(30, 30),
                Collide = false
            });

            AddComponent(new Label(ContentManager.KenneyMini)
            {
                Text = "X/X",
                Name = "HealthLabel",
                Position = new Vector2(220, 50),
                FontScale = 1.5f
            });

            AddComponent(new Sprite()
            {
                Position = new Vector2(350, 53),
                Texture = ContentManager.BulletsTexture,
                Size = new Size(30, 30),
                Collide = false
            });

            AddComponent(new Label(ContentManager.KenneyMini)
            {
                Text = "X/X",
                Name = "AmmoLabel",
                Position = new Vector2(390, 50),
                FontScale = 1.5f
            });

            AddComponent(new Sprite()
            {
                Position = new Vector2(510, 53),
                Texture = ContentManager.ArrowUpTexture,
                Size = new Size(30, 30),
                Collide = false
            });

            AddComponent(new Label(ContentManager.KenneyMini)
            {
                Text = "1",
                Name = "LevelLabel",
                Position = new Vector2(550, 50),
                FontScale = 1.5f
            });

            AddComponent(new Sprite()
            {
                Position = new Vector2(650, 53),
                Texture = ContentManager.ClockTexture,
                Size = new Size(30, 30),
                Collide = false
            });

            AddComponent(new Label(ContentManager.KenneyMini)
            {
                Text = "10:00",
                Name = "TimeLabel",
                Position = new Vector2(690, 50),
                FontScale = 1.5f
            });
        }

    }
}