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
using LessRoomyMoreShooty.Component.Sprites.Item;
using LessRoomyMoreShooty.Component.Sprites;
using System.Collections.Generic;

namespace LessRoomyMoreShooty.States
{
    public partial class GameState : State
    {
        public static string Name = "Game";
        public int Level { get; set; }
        public int RemainingSeconds { get; set; }
        public int RemainingLevelSeconds { get; set; }
        public double SecondTimer { get; set; }
        public bool IsGameOver { get; set; }

        readonly List<Enemy> EnemiesToSpawn = new List<Enemy>();
        List<Door> DoorsToSpawnAt = new List<Door>();

        public Rectangle GameArea { get; set; } = new Rectangle(90, 240, 840, 450);

        protected override void OnLoad()
        {
            Level = 0;
            RemainingSeconds = 60 * 1;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsGameOver)
            {
                AddGameOverUi();

                foreach (Component.Component component in Components)
                {
                    if (component is Projectile) component.IsRemoved = true;
                    if (component is Enemy enemy) enemy.IsActive = false;
                    if (component is Player) component.IsRemoved = true;
                }

                return;
            }

            if (SecondTimer >= 1 && Level > 0)
            {
                RemainingSeconds--;
                if (RemainingLevelSeconds > 0) RemainingLevelSeconds--;
                SecondTimer = 0;
            }
            SecondTimer += gameTime.ElapsedGameTime.TotalSeconds;

            UpdateUi();

            if (RemainingSeconds <= 0)
            {
                IsGameOver = true;
            }

            foreach (Component.Component component in Components)
                if (component is Enemy enemy) enemy.IsActive = true;
        }

        public override void PostUpdate(GameTime gameTime)
        {
            base.PostUpdate(gameTime);

            if (!Components.Any(x => x is Player))
            {
                IsGameOver = true;
                return;
            }

            if (!AreEnemiesAlive() && !AreItemsPresent())
            {
                if (EnemiesToSpawn.Count > 0)
                {
                    SpawnEnemies();
                    return;
                }

                LevelEnd();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentManager.BackgroundTexture, new Rectangle(0, 0, 1024, 768), Color.White);

            if (!AreEnemiesAlive() && !AreItemsPresent())
                if (EnemiesToSpawn.Count > 0)
                    spriteBatch.Draw(ContentManager.DoorsClosedTexture, new Rectangle(0, 0, 1024, 768), Color.White);
                else
                    spriteBatch.Draw(ContentManager.DoorsOpendTexture, new Rectangle(0, 0, 1024, 768), Color.White);
            else
                spriteBatch.Draw(ContentManager.DoorsClosedTexture, new Rectangle(0, 0, 1024, 768), Color.White);

            base.Draw(gameTime, spriteBatch);
        }

        private void UpdateUi()
        {
            GetLabel("HealthLabel").Text = $"{Player.CurrentHealth}/{Player.MaxHealth}";
            GetLabel("AmmoLabel").Text = Player.CurrentAmmo > 0 ? $"{Player.CurrentAmmo}/{Player.MaxAmmo}" : "Reloading";
            GetLabel("LevelLabel").Text = $"{Level}";
            GetLabel("TimeLabel").Text =
                RemainingSeconds <= 60 ?
                $"{RemainingSeconds}" : $"{(RemainingSeconds / 60).ToString().PadLeft(2, '0')}:" +
                $"{(RemainingSeconds % 60).ToString().PadLeft(2, '0')}";

            GetLabel("TimeLabel").Text += " | " + (RemainingLevelSeconds <= 60 ?
                $"{RemainingLevelSeconds}" : $"{(RemainingLevelSeconds / 60).ToString().PadLeft(2, '0')}:" +
                $"{(RemainingLevelSeconds % 60).ToString().PadLeft(2, '0')}");
        }

        private Label GetLabel(string name) => (Label)Components.FirstOrDefault(x => x is Label label && label.Name == name);

        private bool AreEnemiesAlive() => Components.Any(x => x is Enemy);
        private bool AreItemsPresent() => Components.Any(x => x is Item);

        private void PlayerEnteredDoor(object sender, EventArgs e)
        {
            foreach (Component.Component c in Components.Where(x => x is Projectile))
                c.IsRemoved = true;

            TopDoor.IsOpen = false;
            BottomDoor.IsOpen = false;
            LeftDoor.IsOpen = false;
            RightDoor.IsOpen = false;

            Door entry = (Door)sender;
            Player.Position = new Vector2(entry.Exit.Position.X - (entry == LeftDoor ? Player.Size.Width : 0), entry.Exit.Position.Y - (entry == TopDoor ? Player.Size.Height : 0));

            if (RemainingSeconds > 0)
                RemainingSeconds += RemainingLevelSeconds;

            Level++;
            RemainingLevelSeconds = 12;

            if (Level % 5 == 0)
            {
                AddUpgrades();
            }
            else
            {
                Random random = new Random();
                FillDoorList();
                DoorsToSpawnAt.Remove(entry.Exit);

                int enemyCount = (Level / 10) + 1;

                for (int i = 0; i < enemyCount; i++)
                {
                    switch (random.Next(0, 4))
                    {
                        case 0:
                            EnemiesToSpawn.Add(new Zombie(Player));
                            break;
                        case 1:
                            EnemiesToSpawn.Add(new Skeleton(Player));
                            break;
                        case 2:
                            EnemiesToSpawn.Add(new Creeper(Player));
                            break;
                        case 3:
                            EnemiesToSpawn.Add(new Blaze(Player));
                            break;
                    }
                }
            }
        }

        private void FillDoorList()
        {
            DoorsToSpawnAt = new List<Door>
                {
                    RightDoor,
                    LeftDoor,
                    TopDoor,
                    BottomDoor
                };
        }

        private void LevelEnd()
        {
            if (!TopDoor.IsOpen)
            {
                TopDoor.IsOpen = true;
                BottomDoor.IsOpen = true;
                LeftDoor.IsOpen = true;
                RightDoor.IsOpen = true;
            }
        }

        private void SpawnEnemies()
        {

            for (int i = 0; i < 3; i++)
            {
                if (EnemiesToSpawn.Count == 0) return;

                Random random = new Random();

                if (DoorsToSpawnAt.Count == 0) FillDoorList();

                int doorIndex = random.Next(0, DoorsToSpawnAt.Count);
                Door door = DoorsToSpawnAt[doorIndex];
                DoorsToSpawnAt.RemoveAt(doorIndex);
                Vector2 position = door.Position;
                Enemy enemy = EnemiesToSpawn[^1];

                enemy.LevelUp(Level);

                if (door == RightDoor)
                {
                    enemy.Position = new Vector2(position.X - (enemy.Size.Width * 1.5f), position.Y);
                }
                else if (door == LeftDoor)
                {
                    enemy.Position = new Vector2(position.X + (enemy.Size.Width * 0.5f), position.Y);
                }
                else if (door == TopDoor)
                {
                    enemy.Position = new Vector2(position.X, position.Y + (enemy.Size.Height * 0.5f));
                }
                else if (door == BottomDoor)
                {
                    enemy.Position = new Vector2(position.X, position.Y - (enemy.Size.Height * 1.5f));
                }
                AddComponent(enemy);
                EnemiesToSpawn.Remove(enemy);
            }

        }

    }
}