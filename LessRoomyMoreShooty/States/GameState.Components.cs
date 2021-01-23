using Microsoft.Xna.Framework;
using LessRoomyMoreShooty.Component.Controls;

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
        }

    }
}