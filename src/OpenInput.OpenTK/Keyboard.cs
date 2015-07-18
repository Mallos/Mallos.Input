using System;
using OpenTK.Input;
using tkKeyboard = OpenTK.Input.Keyboard;

namespace OpenInput.OpenTK
{
    /// <summary>
    /// 
    /// </summary>
    public class Keyboard : IKeyboard
    {
        /// <inheritdoc />
        public string Name => "Keyboard";

        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyDown;

        /// <inheritdoc />
        public event EventHandler<KeyEventArgs> KeyUp;

        /// <inheritdoc />
        public TextInput TextInput => textInput;
        private TextInput textInput = new Empty.EmptyTextInput();

        private static KeyboardState keyboardState = new KeyboardState();
        
        /// <inheritdoc />
        public KeyboardState GetCurrentState()
        {
            var state = tkKeyboard.GetState();

            // TODO: How can I get all the keys?

            return keyboardState;
        }
    }
}
