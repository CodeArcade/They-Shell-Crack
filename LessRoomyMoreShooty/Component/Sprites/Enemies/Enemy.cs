using Microsoft.Xna.Framework;

namespace LessRoomyMoreShooty.Component.Sprites.Enemies
{
    public abstract class Enemy: Entity
    {
        protected Player Player { get; set; }

        public bool IsActive { get; set; }

        // https://gamedev.stackexchange.com/questions/137305/need-help-with-getting-a-direction-vector-between-two-given-points
        protected Vector2 DistanceToPlayer => Player.Position - Position;
        protected Vector2 DirectionToPlayer => DistanceToPlayer / DistanceToPlayer.Length();

        public Enemy(Player player)
        {
            Player = player;
        }

    }
}
