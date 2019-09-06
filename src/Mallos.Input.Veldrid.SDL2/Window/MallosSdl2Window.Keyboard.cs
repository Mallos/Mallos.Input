namespace Mallos.Input.Window
{
    using System;
    using System.Runtime.CompilerServices;
    using Mallos.Input.Trackers;
    using Veldrid.Sdl2;

    public unsafe partial class MallosSdl2Window
    {
        IKeyboardTracker IDevice<IKeyboardTracker, KeyboardState>.CreateTracker()
        {
            throw new NotImplementedException();
        }

        KeyboardState IDevice<IKeyboardTracker, KeyboardState>.GetCurrentState()
        {
            throw new NotImplementedException();
        }

        private unsafe void HandleKeyboardEvent(SDL_Event* ev)
        {
            switch (ev->type)
            {
                case SDL_EventType.KeyDown:
                case SDL_EventType.KeyUp:
                    SDL_KeyboardEvent keyboardEvent = Unsafe.Read<SDL_KeyboardEvent>(ev);
                    this.HandleKeyboardEvent(keyboardEvent);
                    break;

                case SDL_EventType.TextEditing:
                    break;

                case SDL_EventType.TextInput:
                    SDL_TextInputEvent textInputEvent = Unsafe.Read<SDL_TextInputEvent>(ev);
                    this.HandleTextInputEvent(textInputEvent);
                    break;

                case SDL_EventType.KeyMapChanged:
                    break;
            }
        }

        private void HandleTextInputEvent(SDL_TextInputEvent textInputEvent)
        {
            // uint byteCount = 0;
            // // Loop until the null terminator is found or the max size is reached.
            // while (byteCount < SDL_TextInputEvent.MaxTextSize && textInputEvent.text[byteCount++] != 0)
            // {
            // }
            // 
            // if (byteCount > 1)
            // {
            //     // We don't want the null terminator.
            //     byteCount -= 1;
            //     int charCount = Encoding.UTF8.GetCharCount(textInputEvent.text, (int) byteCount);
            //     char* charsPtr = stackalloc char[charCount];
            //     Encoding.UTF8.GetChars(textInputEvent.text, (int) byteCount, charsPtr, charCount);
            //     for (int i = 0; i < charCount; i++)
            //     {
            //         _privateSnapshot.KeyCharPressesList.Add(charsPtr[i]);
            //     }
            // }
        }

        private void HandleKeyboardEvent(SDL_KeyboardEvent keyboardEvent)
        {
            // SimpleInputSnapshot snapshot = _privateSnapshot;
            // KeyEvent keyEvent = new KeyEvent(MapKey(keyboardEvent.keysym), keyboardEvent.state == 1, MapModifierKeys(keyboardEvent.keysym.mod));
            // snapshot.KeyEventsList.Add(keyEvent);
            // if (keyboardEvent.state == 1)
            // {
            //     KeyDown?.Invoke(keyEvent);
            // }
            // else
            // {
            //     KeyUp?.Invoke(keyEvent);
            // }
        }
    }
}
