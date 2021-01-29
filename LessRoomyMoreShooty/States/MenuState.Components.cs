using Microsoft.Xna.Framework;
using LessRoomyMoreShooty.Component.Controls;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;

namespace LessRoomyMoreShooty.States
{
    public partial class MenuState : State
    {

        protected override void LoadComponents()
        {

            AddComponent(new Label(ContentManager.KenneyMini)
            {
                Position = new Vector2((JamGame.ScreenWidth / 2) - 330, JamGame.ScreenHeight / 4),
                Text = "They Shell Crack",
                FontColor = Color.White,
                FontScale = 5
            });


            Button button = new Button(ContentManager.ButtonTexture, ContentManager.KenneyMini)
            {
                Position = new Vector2(JamGame.ScreenWidth / 2 - 50, (JamGame.ScreenHeight / 2) + 50),
                Text = "Start",
                Size = new Size(100, 50)
            };
            button.OnClick += (sender, e) =>
            {
                StateManager.ChangeTo<GameState>(GameState.Name);
            };
            AddComponent(button);
        }

    }
}