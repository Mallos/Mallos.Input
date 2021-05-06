namespace Mallos.Input.Blazor
{
    using Mallos.Input.Trackers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BlazorKeyboard : BlazorDevice, IKeyboard
    {
        /// <inheritdoc />
        public TextInput TextInput => throw new System.NotImplementedException();

        /// <inheritdoc />
        public string Name => "Blazor Keyboard";

        /// <inheritdoc />
        public IKeyboardTracker CreateTracker() => new BasicKeyboardTracker(this);

        /// <inheritdoc />
        public KeyboardState GetCurrentState() => BlazorKeyboardState.GetSnapshot();
    }

    public static class BlazorKeyboardState
    {
        private static readonly HashSet<Keys> pressedKeys = new();

        public static KeyboardState GetSnapshot()
        {
            return new KeyboardState(pressedKeys.ToArray());
        }

        public static ValueTask OnKeyDown(int keyCode)
        {
            var key = (Keys)keyCode;
            pressedKeys.Add(key);
            return ValueTask.CompletedTask;
        }

        public static ValueTask OnKeyUp(int keyCode)
        {
            var key = (Keys)keyCode;
            pressedKeys.Remove(key);
            return ValueTask.CompletedTask;
        }
    }
}
