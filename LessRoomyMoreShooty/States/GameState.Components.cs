﻿using Microsoft.Xna.Framework;
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
using LessRoomyMoreShooty.Component.Effects;

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

            AddComponent(Player, 0);

            AddDoors();
            AddWalls();
        }

        private void AddUi()
        {
            int xOffset = 25;

            AddComponent(new Sprite()
            {
                Position = new Vector2(180 + xOffset, 53),
                Texture = ContentManager.HeartTexture,
                Size = new Size(30, 30),
                Collide = false
            }, 1);

            AddComponent(new Label(ContentManager.KenneyMini)
            {
                Text = "X/X",
                Name = "HealthLabel",
                Position = new Vector2(220 + xOffset, 50),
                FontScale = 1.5f,
                FontColor = Color.White
            }, 1);

            xOffset = 25;

            AddComponent(new Sprite()
            {
                Position = new Vector2(350 + xOffset, 53),
                Texture = ContentManager.BulletsTexture,
                Size = new Size(30, 30),
                Collide = false
            }, 1);

            AddComponent(new Label(ContentManager.KenneyMini)
            {
                Text = "X/X",
                Name = "AmmoLabel",
                Position = new Vector2(390 + xOffset, 50),
                FontScale = 1.5f,
                FontColor = Color.White
            }, 1);

            xOffset = 100;

            AddComponent(new Label(ContentManager.KenneyMini)
            {
                Text = "1",
                Name = "LevelLabel",
                Position = new Vector2(535 + xOffset, 50),
                FontScale = 1.5f,
                FontColor = Color.White
            }, 1);

            xOffset = 175;

            AddComponent(new Sprite()
            {
                Position = new Vector2(650 + xOffset, 53),
                Texture = ContentManager.ClockTexture,
                Size = new Size(30, 30),
                Collide = false
            }, 1);

            AddComponent(new Label(ContentManager.KenneyMini)
            {
                Text = "10:00",
                Name = "TimeLabel",
                Position = new Vector2(690 + xOffset, 50),
                FontScale = 1.5f,
                FontColor = Color.White
            }, 1);
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
                Position = new Vector2((JamGame.ScreenWidth / 2) - 50, 220 - Player.Size.Height * 0.5f),
                Size = new Size(100, 20),
            };

            BottomDoor = new Door()
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) - 50, 698 - 20),
                Size = new Size(100, 20),
            };

            TopDoor.Exit = BottomDoor;
            BottomDoor.Exit = TopDoor;

            LeftDoor.PlayerEntered += PlayerEnteredDoor;
            RightDoor.PlayerEntered += PlayerEnteredDoor;
            TopDoor.PlayerEntered += PlayerEnteredDoor;
            BottomDoor.PlayerEntered += PlayerEnteredDoor;

            AddComponent(LeftDoor, 0);
            AddComponent(RightDoor, 0);
            AddComponent(TopDoor, 0);
            AddComponent(BottomDoor, 0);
        }

        private void AddWalls()
        {
            // Wall behind left door
            AddComponent(new Obstacle()
            {
                Position = new Vector2(50, 404),
                Size = new Size(20, 100),
                Texture = ContentManager.TransparentTexture
            }, 0);

            // Wall behind right door
            AddComponent(new Obstacle()
            {
                Position = new Vector2(JamGame.ScreenWidth - 70, 404),
                Size = new Size(20, 100),
                Texture = ContentManager.TransparentTexture
            }, 0);

            // Wall behind top door
            AddComponent(new Obstacle()
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) - 50, 200 - Player.Size.Height * 0.5f),
                Size = new Size(100, 20),
                Texture = ContentManager.TransparentTexture
            }, 0);

            // Wall behind bottom door
            AddComponent(new Obstacle()
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) - 50, 698 + 40),
                Size = new Size(100, 20),
                Texture = ContentManager.TransparentTexture
            }, 0);

            // top left-> left middle
            AddComponent(new Obstacle()
            {
                Position = new Vector2(0, 220 - Player.Size.Height * 0.5f),
                Size = new Size((JamGame.ScreenWidth / 2) - 50, 20),
                Texture = ContentManager.TransparentTexture
            }, 0);

            // left midlle -> top right
            AddComponent(new Obstacle()
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) + 50, 220 - Player.Size.Height * 0.5f),
                Size = new Size((JamGame.ScreenWidth / 2) - 50, 20),
                Texture = ContentManager.TransparentTexture
            }, 0);

            // top right -> bottom middle
            AddComponent(new Obstacle()
            {
                Position = new Vector2(JamGame.ScreenWidth - 90, 140),
                Size = new Size(20, ((JamGame.ScreenHeight - 140) / 2) - 50),
                Texture = ContentManager.TransparentTexture
            }, 0);

            // bottom middle -> bottom right
            AddComponent(new Obstacle()
            {
                Position = new Vector2(JamGame.ScreenWidth - 90, (JamGame.ScreenHeight / 2) + 50 + 70),
                Size = new Size(20, ((JamGame.ScreenHeight - 70) / 2) - 50),
                Texture = ContentManager.TransparentTexture
            }, 0);

            // bottom left-> bottom middle
            AddComponent(new Obstacle()
            {
                Position = new Vector2(0, JamGame.ScreenHeight - 90),
                Size = new Size((JamGame.ScreenWidth / 2) - 50, 20),
                Texture = ContentManager.TransparentTexture
            }, 0);

            // bottom midlle -> bottom right
            AddComponent(new Obstacle()
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) + 50, JamGame.ScreenHeight - 90),
                Size = new Size((JamGame.ScreenWidth / 2) - 50, 20),
                Texture = ContentManager.TransparentTexture
            }, 0);

            // top left -> bottom middle
            AddComponent(new Obstacle()
            {
                Position = new Vector2(70, 140),
                Size = new Size(20, ((JamGame.ScreenHeight - 140) / 2) - 50),
                Texture = ContentManager.TransparentTexture
            }, 0);

            // bottom middle -> bottom left 
            AddComponent(new Obstacle()
            {
                Position = new Vector2(70, (JamGame.ScreenHeight / 2) + 50 + 70),
                Size = new Size(20, ((JamGame.ScreenHeight - 70) / 2) - 50),
                Texture = ContentManager.TransparentTexture
            }, 0);
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
                Layers[0].Remove(statUp1);
                Layers[0].Remove(statUp2);
            };

            statUp1.OnItemPickUp += (sender, e) =>
            {
                Layers[0].Remove(pickUp);
                Layers[0].Remove(statUp2);
            };

            statUp2.OnItemPickUp += (sender, e) =>
            {
                Layers[0].Remove(statUp1);
                Layers[0].Remove(pickUp);
            };

            statUp1.Position = new Vector2(GameArea.X + (GameArea.Width / 2) - (statUp1.Size.Width * 3), GameArea.Y + (GameArea.Height / 2) - statUp1.Size.Height * 0.7f);
            statUp2.Position = new Vector2(GameArea.X + (GameArea.Width / 2) - statUp2.Size.Width / 2, GameArea.Y + (GameArea.Height / 2) - statUp2.Size.Height * 0.7f);
            pickUp.Position = new Vector2(GameArea.X + (GameArea.Width / 2) + (pickUp.Size.Width * 2), GameArea.Y + (GameArea.Height / 2) - pickUp.Size.Height * 0.7f);

            AddComponent(statUp1, 0);
            AddComponent(statUp2, 0);
            AddComponent(pickUp, 0);
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

            Button button = new Button(ContentManager.GameOverTexture, ContentManager.KenneyMini)
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) - 220, GameArea.Y - 40)
            };
            button.OnClick += (sender, e) =>
            {
                StateManager.Reload();
            };
            AddComponent(button, 1);

            AddComponent(new Sprite()
            {
                Texture = ContentManager.GameOverAgainTexture,
                Position = new Vector2((JamGame.ScreenWidth / 2) + 220, GameArea.Y )
            }, 1);

            AddComponent(new Label(ContentManager.KenneyMini)
            {
                Position = new Vector2(JamGame.ScreenWidth / 2 - 225, GameArea.Y * 2.4f),
                Text =  Level == 1? $"You reentered {Level} time": $"You reentered {Level} times",
                Name = "GameOverLevelLabel",
                FontColor = Color.White,
                FontScale = 3
            }, 1);


        }

    }
}