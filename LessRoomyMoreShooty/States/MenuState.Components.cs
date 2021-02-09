using Microsoft.Xna.Framework;
using LessRoomyMoreShooty.Component.Controls;
using System.Drawing;

namespace LessRoomyMoreShooty.States
{
    public partial class MenuState : State
    {

        protected override void LoadComponents()
        {


            Button button = new Button(ContentManager.TitleTexture, ContentManager.KenneyMini)
            {
                Position = new Vector2(JamGame.ScreenWidth / 2 - 220, JamGame.ScreenHeight / 2 - (384 * 0.3f)),
                Size = new Size(512, 384)
            };

            button.OnClick += (sender, e) =>
            {
                StateManager.ChangeTo<GameState>(GameState.Name);
            };
            AddComponent(button, 1);
        }

    }
}