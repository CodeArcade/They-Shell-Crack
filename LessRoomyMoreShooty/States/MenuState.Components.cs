using Microsoft.Xna.Framework;
using LessRoomyMoreShooty.Component.Controls;

namespace LessRoomyMoreShooty.States
{
    public partial class MenuState : State
    {

        protected override void LoadComponents()
        {

            AddComponent(new Label(ContentManager.ButtonFont)
            {
                Position = new Vector2(JamGame.ScreenWidth / 2, 100),
                Text = "Gunfire Drier"
            });

            Button button = new Button(ContentManager.ButtonTexture, ContentManager.KenneyMini)
            {
                Position = new Vector2(JamGame.ScreenWidth / 2, 200),
                Text = "Start"
            };
            button.OnClick += (sender, e) =>
            {
                StateManager.ChangeTo<GameState>(GameState.Name);
            };
            AddComponent(button);
        }

    }
}