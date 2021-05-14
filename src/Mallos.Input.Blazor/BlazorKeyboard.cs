namespace Mallos.Input.Blazor
{
    using Mallos.Input.Blazor.Components;
    using Mallos.Input.Trackers;

    public class BlazorKeyboard : BlazorDevice, IKeyboard
    {
        /// <inheritdoc />
        public TextInput TextInput => throw new System.NotImplementedException();

        /// <inheritdoc />
        public string Name => "Blazor Keyboard";

        public BlazorKeyboard(MInputWrapperComponent component)
            : base(component)
        {
        }

        /// <inheritdoc />
        public IKeyboardTracker CreateTracker()
            => new BasicKeyboardTracker(this);

        /// <inheritdoc />
        public KeyboardState GetCurrentState()
            => this.component.keyboardState.GetSnapshot();
    }
}
