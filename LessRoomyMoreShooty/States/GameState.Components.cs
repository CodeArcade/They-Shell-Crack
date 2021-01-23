using Microsoft.Xna.Framework;
using LessRoomyMoreShooty.Component.Sprites.Item;

namespace LessRoomyMoreShooty.States
{
    public partial class GameState : State
    {

        protected override void LoadComponents()
        {
            AddComponent(new Component.Sprites.Player()
            {
                Position = new Vector2(200, 200)
            });

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
                AddComponent(new AttackSpeedUpItem()
                {
                    Position = new Vector2(200, 500)
                });
            }
        }

    }
}