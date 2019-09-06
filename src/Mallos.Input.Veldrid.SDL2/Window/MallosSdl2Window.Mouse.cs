namespace Mallos.Input.Window
{
    using System;
    using System.Runtime.CompilerServices;
    using Mallos.Input.Trackers;
    using Veldrid.Sdl2;
    using static Veldrid.Sdl2.Sdl2Native;

    public unsafe partial class MallosSdl2Window
    {
        private int currentMouseX;
        private int currentMouseY;
        private bool[] currentMouseButtonStates = new bool[13];

        void IMouse.SetPosition(int x, int y)
        {
            if (this.Exists)
            {
                SDL_WarpMouseInWindow(this.window, x, y);
                this.currentMouseX = x;
                this.currentMouseY = y;
            }
        }

        void IMouse.GetPosition(out int x, out int y)
        {
            x = this.currentMouseX;
            y = this.currentMouseY;
        }

        IMouseTracker IDevice<IMouseTracker, Input.MouseState>.CreateTracker()
        {
            throw new NotImplementedException();
        }

        Input.MouseState IDevice<IMouseTracker, Input.MouseState>.GetCurrentState()
        {
            throw new NotImplementedException();
        }

        private unsafe void HandleMouseEvent(SDL_Event* ev)
        {
            switch (ev->type)
            {
                case SDL_EventType.MouseMotion:
                    SDL_MouseMotionEvent mouseMotionEvent = Unsafe.Read<SDL_MouseMotionEvent>(ev);
                    this.HandleMouseMotionEvent(mouseMotionEvent);
                    break;

                case SDL_EventType.MouseButtonDown:
                case SDL_EventType.MouseButtonUp:
                    SDL_MouseButtonEvent mouseButtonEvent = Unsafe.Read<SDL_MouseButtonEvent>(ev);
                    this.HandleMouseButtonEvent(mouseButtonEvent);
                    break;

                case SDL_EventType.MouseWheel:
                    SDL_MouseWheelEvent mouseWheelEvent = Unsafe.Read<SDL_MouseWheelEvent>(ev);
                    this.HandleMouseWheelEvent(mouseWheelEvent);
                    break;
            }
        }

        private void HandleMouseWheelEvent(SDL_MouseWheelEvent mouseWheelEvent)
        {
            //MouseWheel?.Invoke(new MouseWheelEventArgs(GetCurrentMouseState(), (float)mouseWheelEvent.y));
        }

        private void HandleMouseButtonEvent(SDL_MouseButtonEvent mouseButtonEvent)
        {
            //MouseButton button = MapMouseButton(mouseButtonEvent.button);
            //bool down = mouseButtonEvent.state == 1;
            //_currentMouseButtonStates[(int)button] = down;
            //_privateSnapshot.MouseDown[(int)button] = down;
            //MouseEvent mouseEvent = new MouseEvent(button, down);
            //_privateSnapshot.MouseEventsList.Add(mouseEvent);
            //if (down)
            //{
            //    MouseDown?.Invoke(mouseEvent);
            //}
            //else
            //{
            //    MouseUp?.Invoke(mouseEvent);
            //}
        }

        private void HandleMouseMotionEvent(SDL_MouseMotionEvent mouseMotionEvent)
        {
            //Vector2 mousePos = new Vector2(mouseMotionEvent.x, mouseMotionEvent.y);
            //Vector2 delta = new Vector2(mouseMotionEvent.xrel, mouseMotionEvent.yrel);
            //_currentMouseX = (int)mousePos.X;
            //_currentMouseY = (int)mousePos.Y;
            //_privateSnapshot.MousePosition = mousePos;

            //if (!_firstMouseEvent)
            //{
            //    _currentMouseDelta += delta;
            //    MouseMove?.Invoke(new MouseMoveEventArgs(GetCurrentMouseState(), mousePos));
            //}

            //_firstMouseEvent = false;
        }
    }
}
