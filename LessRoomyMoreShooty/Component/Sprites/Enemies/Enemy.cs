using Microsoft.Xna.Framework;
using System.Collections.Generic;
using LessRoomyMoreShooty.Models;
using System;

namespace LessRoomyMoreShooty.Component.Sprites.Enemies
{
    public abstract class Enemy : Entity
    {
        protected Player Player { get; set; }
        protected Dictionary<string, Animation> Animations { get; set; }

        public bool DidPlaySpawnAnimation { get; set; }

        public bool IsActive { get; set; }

        // https://gamedev.stackexchange.com/questions/137305/need-help-with-getting-a-direction-vector-between-two-given-points
        protected float DistanceToPlayer => Vector2.Distance(Position, Player.Position);
        protected Vector2 DirectionToPlayer
        {
            get
            {
                Vector2 direction = Player.Position - Position;
                direction.Normalize();

                return direction;
            }

        }

        protected float AngleToPlayer
        {
            get
            {
                return (float)Math.Atan2(Player.Position.Y - Position.Y, Player.Position.X - Position.X);
            }
        }

        public Enemy(Player player)
        {
            Player = player;
        }

        public override void Update(GameTime gameTime)
        {
            if (!DidPlaySpawnAnimation && IsActive)
            {
                ParticleManager.GenerateNewParticle(Color.White, Position, ContentManager.ObstacleHitParticle, 50, 20);
                AudioManager.PlayEffect(ContentManager.ButtonClickSoundEffect, 0.15f, 0.1f);

                DidPlaySpawnAnimation = true;
            }

            base.Update(gameTime);
            if (!IsActive) Speed = 0;
        }

        public void LevelUp(int targetLevel)
        {
            OnLevelUp(targetLevel);
        }

        protected abstract void OnLevelUp(int level);

        protected void SetDirectionToPlayer()
        {
            if (IsActive)
                Direction = DirectionToPlayer;
            else Direction = Vector2.Zero;
        }

    }
}
