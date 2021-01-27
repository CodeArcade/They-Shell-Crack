using Microsoft.Xna.Framework;
using LessRoomyMoreShooty.Component.Sprites.Item;
using LessRoomyMoreShooty.Component.Controls;
using LessRoomyMoreShooty.Component.Sprites;
using LessRoomyMoreShooty.Component.Sprites.Enemies;
using System.Drawing;
using LessRoomyMoreShooty.Component.Sprites.Environment;
using System;
using LessRoomyMoreShooty.Component.Sprites.Item.PickUpItems;
using LessRoomyMoreShooty.Component.Sprites.Item.SpecialItems;
using Color = Microsoft.Xna.Framework.Color;

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

            Player = new Player();
            Player.Position = new Vector2(GameArea.X + GameArea.Width / 2 - Player.Size.Width / 2, GameArea.Y + GameArea.Height / 2 - Player.Size.Height / 2);

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
            };

            RightDoor = new Door()
            {
                Position = new Vector2(JamGame.ScreenWidth - 90, 404),
                Size = new Size(20, 100),
            };

            LeftDoor.Exit = RightDoor;
            RightDoor.Exit = LeftDoor;

            TopDoor = new Door()
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) - 50, 220),
                Size = new Size(100, 20),
            };

            BottomDoor = new Door()
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) - 50, 698),
                Size = new Size(100, 20),
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
            AddComponent(new Obstacle()
            {
                Position = new Vector2(50, 404),
                Size = new Size(20, 100),
                Texture = ContentManager.TransparentTexture
            });

            // Wall behind right door
            AddComponent(new Obstacle()
            {
                Position = new Vector2(JamGame.ScreenWidth - 70, 404),
                Size = new Size(20, 100),
                Texture = ContentManager.TransparentTexture
            });

            // Wall behind top door
            AddComponent(new Obstacle()
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) - 50, 200),
                Size = new Size(100, 20),
                Texture = ContentManager.TransparentTexture
            });

            // Wall behind bottom door
            AddComponent(new Obstacle()
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) - 50, 698 + 20),
                Size = new Size(100, 20),
                Texture = ContentManager.TransparentTexture
            });

            // top left-> left middle
            AddComponent(new Obstacle()
            {
                Position = new Vector2(0, 220),
                Size = new Size((JamGame.ScreenWidth / 2) - 50, 20),
                Texture = ContentManager.TransparentTexture
            });

            // left midlle -> top right
            AddComponent(new Obstacle()
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) + 50, 220),
                Size = new Size((JamGame.ScreenWidth / 2) - 50, 20),
                Texture = ContentManager.TransparentTexture
            });

            // top right -> bottom middle
            AddComponent(new Obstacle()
            {
                Position = new Vector2(JamGame.ScreenWidth - 90, 140),
                Size = new Size(20, ((JamGame.ScreenHeight - 140) / 2) - 50),
                Texture = ContentManager.TransparentTexture
            });

            // bottom middle -> bottom right
            AddComponent(new Obstacle()
            {
                Position = new Vector2(JamGame.ScreenWidth - 90, (JamGame.ScreenHeight / 2) + 50 + 70),
                Size = new Size(20, ((JamGame.ScreenHeight - 70) / 2) - 50),
                Texture = ContentManager.TransparentTexture
            });

            // bottom left-> bottom middle
            AddComponent(new Obstacle()
            {
                Position = new Vector2(0, JamGame.ScreenHeight - 70),
                Size = new Size((JamGame.ScreenWidth / 2) - 50, 20),
                Texture = ContentManager.TransparentTexture
            });

            // bottom midlle -> bottom right
            AddComponent(new Obstacle()
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) + 50, JamGame.ScreenHeight - 70),
                Size = new Size((JamGame.ScreenWidth / 2) - 50, 20),
                Texture = ContentManager.TransparentTexture
            });

            // top left -> bottom middle
            AddComponent(new Obstacle()
            {
                Position = new Vector2(70, 140),
                Size = new Size(20, ((JamGame.ScreenHeight - 140) / 2) - 50),
                Texture = ContentManager.TransparentTexture
            });

            // bottom middle -> bottom left 
            AddComponent(new Obstacle()
            {
                Position = new Vector2(70, (JamGame.ScreenHeight / 2) + 50 + 70),
                Size = new Size(20, ((JamGame.ScreenHeight - 70) / 2) - 50),
                Texture = ContentManager.TransparentTexture
            });
        }

        private void AddUpgrades()
        {
            Item pickUp, statUp1, statUp2;
            Random random = new Random();

            switch (random.Next(0, 10))
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    pickUp = new SmallHealItem();
                    break;
                case 5:
                case 6:
                case 7:
                    pickUp = new MediumHealItem();
                    break;
                default:
                    pickUp = new LargeHealItem();
                    break;
            }

            statUp1 = GetRandomStatUpItem();
            while ((statUp2 = GetRandomStatUpItem()).GetType() == statUp1.GetType()) { }

            pickUp.OnItemPickUp += (sender, e) =>
            {
                Components.Remove(statUp1);
                Components.Remove(statUp2);
            };

            statUp1.OnItemPickUp += (sender, e) =>
            {
                Components.Remove(pickUp);
                Components.Remove(statUp2);
            };

            statUp2.OnItemPickUp += (sender, e) =>
            {
                Components.Remove(statUp1);
                Components.Remove(pickUp);
            };

            statUp1.Position = new Vector2(GameArea.X + (GameArea.Width / 2) - (statUp1.Size.Width * 1.5f), GameArea.Y + (GameArea.Height / 2));
            statUp2.Position = new Vector2(GameArea.X + (GameArea.Width / 2), GameArea.Y + (GameArea.Height / 2));
            pickUp.Position = new Vector2(GameArea.X + (GameArea.Width / 2) + (statUp1.Size.Width * 0.5f) + statUp2.Size.Width, GameArea.Y + (GameArea.Height / 2));

            AddComponent(statUp1);
            AddComponent(statUp2);
            AddComponent(pickUp);
        }

        private Item GetRandomStatUpItem()
        {
            Random random = new Random();
            switch (random.Next(0, 10))
            {
                case 0:
                case 1:
                    return new AmmoUpItem();
                case 2:
                case 3:
                    return new AttackSpeedUpItem();
                case 4:
                case 5:
                    return new DamageUpItem();
                case 6:
                case 7:
                    return new HealthUpItem();
                default:
                    return new MovementSpeedUpItem();
            }

        }

        private void AddGameOverUi()
        {
            if (GetLabel("GameOverLevelLabel") != null) return;

            AddComponent(new Label(ContentManager.KenneyMini)
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) - 220, GameArea.Y * 1.5f),
                Text = "Game Over",
                Name = "GameOverLabel",
                FontColor = Color.White,
                FontScale = 5
            });

            AddComponent(new Label(ContentManager.KenneyMini)
            {
                Position = new Vector2(JamGame.ScreenWidth / 2 - 260, GameArea.Y * 1.8f),
                Text = $"You reachead level {Level}",
                Name = "GameOverLevelLabel",
                FontColor = Color.White,
                FontScale = 3
            });

            Button button = new Button(ContentManager.ButtonTexture, ContentManager.KenneyMini)
            {
                Position = new Vector2(JamGame.ScreenWidth / 2 - 35 , GameArea.Y * 2.1f),
                Text = "Restart"
            };
            button.OnClick += (sender, e) =>
            {
                StateManager.Reload();
            };
            AddComponent(button);
        }

    }
}