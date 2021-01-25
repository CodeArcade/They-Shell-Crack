﻿using Microsoft.Xna.Framework;

namespace LessRoomyMoreShooty.Component.Sprites.Enemies
{
    public abstract class Enemy : Entity
    {
        protected Player Player { get; set; }

        public bool IsActive { get; set; }

        // https://gamedev.stackexchange.com/questions/137305/need-help-with-getting-a-direction-vector-between-two-given-points
        protected float DistanceToPlayer => Vector2.Distance(Position, Player.Position);
        protected Vector2 DirectionToPlayer
        {
            get
            {
                Vector2 direction =  Player.Position - Position;
                direction.Normalize();

                return direction;
            }

        }

        public Enemy(Player player)
        {
            Player = player;
        }

    }
}
