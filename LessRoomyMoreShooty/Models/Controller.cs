using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LessRoomyMoreShooty.Models
{
    static class Controller
    {
        public static bool IsInUse()
        {
            GamePadState state = GamePad.GetState(0);
            GamePadDPad pad = state.DPad;
            GamePadButtons buttons = state.Buttons;
            GamePadThumbSticks sticks = state.ThumbSticks;


            if (IsNonDirectionButtonPressed()) return true;
            // dpad
            if (IsGamePadButtonPressed(pad.Down) || IsGamePadButtonPressed(pad.Up) || IsGamePadButtonPressed(pad.Left) || IsGamePadButtonPressed(pad.Right)) return true;
            // triggers
            if (state.Triggers.Left != 0 || state.Triggers.Right != 0) return true;
            // thumbstick buttons
            if (IsGamePadButtonPressed(buttons.LeftStick) || IsGamePadButtonPressed(buttons.RightStick)) return true;
            if (sticks.Left != Vector2.Zero || sticks.Right != Vector2.Zero) return true;

            return false;
        }

        public static bool IsNonDirectionButtonPressed()
        {
            GamePadState state = GamePad.GetState(0);
            GamePadButtons buttons = state.Buttons;

            // face buttons
            if (IsGamePadButtonPressed(buttons.A) || IsGamePadButtonPressed(buttons.B) || IsGamePadButtonPressed(buttons.X) || IsGamePadButtonPressed(buttons.Y)) return true;
            // special buttons
            if (IsGamePadButtonPressed(buttons.Start) || IsGamePadButtonPressed(buttons.Back) || IsGamePadButtonPressed(buttons.BigButton)) return true;
            // shoulder buttons
            if (IsGamePadButtonPressed(buttons.LeftShoulder) || IsGamePadButtonPressed(buttons.RightShoulder)) return true;

            return false;
        }

        private static bool IsGamePadButtonPressed(ButtonState state) => state == ButtonState.Pressed;
    }
}
