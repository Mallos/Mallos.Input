namespace Mallos.Input.Blazor
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class BlazorKeyboardState
    {
        private readonly HashSet<Keys> pressedKeys = new();

        public KeyboardState GetSnapshot()
        {
            return new KeyboardState(pressedKeys.ToArray());
        }

        public ValueTask OnKeyDown(int keyCode)
        {
            var key = (Keys) keyCode;
            pressedKeys.Add(key);
            return ValueTask.CompletedTask;
        }

        public ValueTask OnKeyUp(int keyCode)
        {
            var key = (Keys) keyCode;
            pressedKeys.Remove(key);
            return ValueTask.CompletedTask;
        }
    }
}
