namespace Mallos.Input.Window
{
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using Mallos.Input.Trackers;
    using Mallos.Input.Trackers.Smart;
    using Veldrid.Sdl2;
    using static Veldrid.Sdl2.Sdl2Native;

    public unsafe partial class MallosSdl2Window
    {
        private readonly MouseStateTracker mouseTracker = new MouseStateTracker();

        void IMouse.SetPosition(int x, int y)
        {
            if (this.Exists)
            {
                SDL_WarpMouseInWindow(this.window, x, y);
                this.mouseTracker.OnMove(new Vector2(x, y), Vector2.Zero);
            }
        }

        void IMouse.GetPosition(out int x, out int y)
        {
            x = this.mouseTracker.MouseState.X;
            y = this.mouseTracker.MouseState.Y;
        }

        IMouseTracker IDevice<IMouseTracker, Input.MouseState>.CreateTracker()
            => this.mouseTracker;

        Input.MouseState IDevice<IMouseTracker, Input.MouseState>.GetCurrentState()
            => this.mouseTracker.MouseState;

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
            => this.mouseTracker.OnMouseWheel(mouseWheelEvent.y);

        private void HandleMouseButtonEvent(SDL_MouseButtonEvent mouseButtonEvent)
        {
            if (mouseButtonEvent.state == 1)
            {
                this.mouseTracker.OnButtonDown(mouseButtonEvent.button.Convert());
            }
            else
            {
                this.mouseTracker.OnButtonUp(mouseButtonEvent.button.Convert());
            }
        }

        private void HandleMouseMotionEvent(SDL_MouseMotionEvent mouseMotionEvent)
        {
            Vector2 mousePos = new Vector2(mouseMotionEvent.x, mouseMotionEvent.y);
            Vector2 delta = new Vector2(mouseMotionEvent.xrel, mouseMotionEvent.yrel);
            this.mouseTracker.OnMove(mousePos, delta);
        }
    }
}
