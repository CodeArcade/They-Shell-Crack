using Microsoft.Xna.Framework;
using LessRoomyMoreShooty.Component.Sprites.Item;
using LessRoomyMoreShooty.Component.Controls;
using LessRoomyMoreShooty.Component.Sprites;
using LessRoomyMoreShooty.Component.Sprites.Enemies;
using System.Drawing;
using LessRoomyMoreShooty.Component.Sprites.Environment;

namespace LessRoomyMoreShooty.States
{
    public partial class GameState : State
    {
        private Player Player { get; set; }
        private Door LeftDoor { get; set; }
        private Door RightDoor { get; set; }
        private Door TopDoor { get; set; }
        private Door BottomDoor { get; set; }

        protected override void LoadComponents()
        {
            AddUi();

            Player = new Player()
            {
                Position = new Vector2(200, 200)
            };
            AddComponent(Player);

            AddDoors();
            AddWalls();
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

        private void AddDoors()
        {
            LeftDoor = new Door()
            {
                Position = new Vector2(70, 404),
                Size = new Size(20, 100),
                IsOpen = true
            };

            RightDoor = new Door()
            {
                Position = new Vector2(JamGame.ScreenWidth - 90, 404),
                Size = new Size(20, 100),
                IsOpen = true
            };

            LeftDoor.Exit = RightDoor;
            RightDoor.Exit = LeftDoor;

            TopDoor = new Door() 
            { 
                Position = new Vector2((JamGame.ScreenWidth / 2) - 50, 220),
                Size = new Size(100, 20),
                IsOpen = true 
            };

            BottomDoor = new Door() 
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) - 50, 698),
                Size = new Size(100, 20),
                IsOpen = true 
            };

            TopDoor.Exit = BottomDoor;
            BottomDoor.Exit = TopDoor;

            LeftDoor.PlayerEntered += PlayerEnteredDoor;
            RightDoor.PlayerEntered += PlayerEnteredDoor;
            TopDoor.PlayerEntered += PlayerEnteredDoor;
            BottomDoor.PlayerEntered += PlayerEnteredDoor;

            AddComponent(LeftDoor);
            AddComponent(RightDoor);
            AddComponent(TopDoor);
            AddComponent(BottomDoor);
        }

        private void AddWalls()
        {
            // Wall behind left door
            AddComponent(new Sprite()
            {
                Position = new Vector2(50, 404),
                Size = new Size(20, 100),
                Texture = ContentManager.TransparentTexture
            });

            // Wall behind right door
            AddComponent(new Sprite()
            {
                Position = new Vector2(JamGame.ScreenWidth - 70, 404),
                Size = new Size(20, 100),
                Texture = ContentManager.TransparentTexture
            });

            // Wall behind top door
            AddComponent(new Sprite()
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) - 50, 200),
                Size = new Size(100, 20),
                Texture = ContentManager.TransparentTexture
            });

            // Wall behind bottom door
            AddComponent(new Sprite()
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) - 50, 698 + 20),
                Size = new Size(100, 20),
                Texture = ContentManager.TransparentTexture
            });

            // top left-> left middle
            AddComponent(new Sprite()
            {
                Position = new Vector2(0, 220),
                Size = new Size((JamGame.ScreenWidth / 2) - 50, 20),
                Texture = ContentManager.TransparentTexture
            });

            // left midlle -> top right
            AddComponent(new Sprite()
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) + 50, 220),
                Size = new Size((JamGame.ScreenWidth / 2) - 50, 20),
                Texture = ContentManager.TransparentTexture
            });

            // top right -> bottom middle
            AddComponent(new Sprite()
            {
                Position = new Vector2(JamGame.ScreenWidth - 90, 140),
                Size = new Size(20, ((JamGame.ScreenHeight - 140) / 2) - 50),
                Texture = ContentManager.TransparentTexture
            });

            // bottom middle -> bottom right
            AddComponent(new Sprite()
            {
                Position = new Vector2(JamGame.ScreenWidth - 90, (JamGame.ScreenHeight / 2) + 50 + 70),
                Size = new Size(20, ((JamGame.ScreenHeight - 70) / 2) - 50),
                Texture = ContentManager.TransparentTexture
            });

            // bottom left-> bottom middle
            AddComponent(new Sprite()
            {
                Position = new Vector2(0, JamGame.ScreenHeight - 70),
                Size = new Size((JamGame.ScreenWidth / 2) - 50, 20),
                Texture = ContentManager.TransparentTexture
            });

            // bottom midlle -> bottom right
            AddComponent(new Sprite()
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) + 50, JamGame.ScreenHeight - 70),
                Size = new Size((JamGame.ScreenWidth / 2) - 50, 20),
                Texture = ContentManager.TransparentTexture
            });

            // top left -> bottom middle
            AddComponent(new Sprite()
            {
                Position = new Vector2(70, 140),
                Size = new Size(20, ((JamGame.ScreenHeight - 140) / 2) - 50),
                Texture = ContentManager.TransparentTexture
            });

            // bottom middle -> bottom left 
            AddComponent(new Sprite()
            {
                Position = new Vector2(70, (JamGame.ScreenHeight / 2) + 50 + 70),
                Size = new Size(20, ((JamGame.ScreenHeight - 70) / 2) - 50),
                Texture = ContentManager.TransparentTexture
            });
        }

    }
}