using LessRoomyMoreShooty.Manager;
using LessRoomyMoreShooty.States;
using System;
using Unity;
using Unity.Injection;

namespace LessRoomyMoreShooty
{
    public static class Program
    {
        public static IUnityContainer UnityContainer = new UnityContainer();

        [STAThread]
        static void Main()
        {
            Register();

            using JamGame game = UnityContainer.Resolve<JamGame>();
            game.Run();
        }

        static void Register()
        {

            RegisterStates();
            RegisterManager();

            UnityContainer.RegisterSingleton<JamGame>();
        }

        static void RegisterManager()
        {
            UnityContainer.RegisterSingleton<StateManager>();
            UnityContainer.RegisterType<ParticleManager>();
            UnityContainer.RegisterType<AudioManager>();
            UnityContainer.RegisterType<ContentManager>();
            UnityContainer.RegisterType<AnimationManager>();
        }

        static void RegisterStates()
        {
            UnityContainer.RegisterSingleton<MenuState>(MenuState.Name);
            UnityContainer.RegisterSingleton<GameState>(GameState.Name);
            UnityContainer.RegisterSingleton<GameOverState>(GameOverState.Name);
        }

    }
}
