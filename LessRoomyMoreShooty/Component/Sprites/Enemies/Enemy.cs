using Microsoft.Xna.Framework;

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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            IsActive = true; // is here so it does not shooting before on right coordinate in map
        }

        public void LevelUp(int targetLevel)
        {
              OnLevelUp(targetLevel);
        }

        protected abstract void OnLevelUp(int level);
    }
}
