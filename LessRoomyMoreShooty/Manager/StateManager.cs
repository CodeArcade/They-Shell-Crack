using LessRoomyMoreShooty.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LessRoomyMoreShooty.Manager
{
    public class StateManager
    {
        public static State CurrentState { get; private set; }
        private static State NextState { get; set; }
        private static string StateName { get; set; } 


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) => CurrentState.Draw(gameTime, spriteBatch);

        public void Update(GameTime gameTime)
        {
            ChangeState();
            if (CurrentState.HasLoaded)
            {
                CurrentState.Update(gameTime);
                CurrentState.PostUpdate(gameTime);
            }
        }

        private void ChangeState()
        {
            if (NextState is null) return;
            if (NextState == CurrentState) return;
            if (!NextState.HasLoaded) NextState.Load();

            CurrentState = NextState;
            NextState = null;
        }

        public void Reload()
        {
            NextState = (State)Program.UnityContainer.Resolve(CurrentState.GetType(), StateName);
        }

        public void ChangeTo<T>(string name) where T : State
        {
            NextState = (T)Program.UnityContainer.Resolve(typeof(T), name);
            StateName = name;
        }

    }
}