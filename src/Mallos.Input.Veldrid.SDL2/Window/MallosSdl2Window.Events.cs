namespace Mallos.Input.Window
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Veldrid;
    using Veldrid.Sdl2;
    using static Veldrid.Sdl2.Sdl2Native;

    public delegate void SDLEventHandler(ref SDL_Event ev);

    public unsafe partial class MallosSdl2Window
    {
        private readonly List<SDL_Event> events = new List<SDL_Event>();

        /// <summary>
        /// Called by <see cref="Sdl2WindowRegistry"/>
        /// </summary>
        internal void AddEvent(SDL_Event ev) => this.events.Add(ev);

        public bool PumpEvents()
        {
            if (!this.Exists)
            {
                return false;
            }

            if (!this.threadedProcessing)
            {
                this.ProcessEvents(null);
            }

            return true;
        }

        private void WindowOwnerRoutine(object state)
        {
            WindowParams wp = (WindowParams) state;
            this.window = wp.Create();
            this.WindowID = SDL_GetWindowID(this.window);

            Sdl2WindowRegistry.RegisterWindow(this);
            this.PostWindowCreated(wp.WindowFlags);

            wp.ResetEvent.Set();

            double previousPollTimeMs = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (this.Exists)
            {
                if (this.shouldClose)
                {
                    this.CloseCore();
                    return;
                }

                double currentTick = sw.ElapsedTicks;
                double currentTimeMs = sw.ElapsedTicks * (1000.0 / Stopwatch.Frequency);
                if (this.LimitPollRate && currentTimeMs - previousPollTimeMs < this.PollIntervalInMs)
                {
                    Thread.Sleep(0);
                }
                else
                {
                    previousPollTimeMs = currentTimeMs;
                    this.ProcessEvents(null);
                }
            }
        }

        private void ProcessEvents(SDLEventHandler eventHandler)
        {
            this.CheckNewWindowTitle();

            Sdl2Events.ProcessEvents();
            for (int i = 0; i < this.events.Count; i++)
            {
                SDL_Event ev = this.events[i];
                if (eventHandler == null)
                {
                    this.HandleEvent(&ev);
                }
                else
                {
                    eventHandler(ref ev);
                }
            }

            this.events.Clear();
        }

        private unsafe void HandleEvent(SDL_Event* ev)
        {
            switch (ev->type)
            {
                /*
                 * Window Events
                 */
                case SDL_EventType.Quit:
                case SDL_EventType.Terminating:
                    this.Close();
                    break;

                case SDL_EventType.WindowEvent:
                    SDL_WindowEvent windowEvent = Unsafe.Read<SDL_WindowEvent>(ev);
                    this.HandleWindowEvent(windowEvent);
                    break;

                case SDL_EventType.DropFile:
                case SDL_EventType.DropBegin:
                case SDL_EventType.DropTest:
                    break;

                /* Keyboard Events */
                case SDL_EventType.KeyDown:
                case SDL_EventType.KeyUp:
                case SDL_EventType.TextEditing:
                case SDL_EventType.TextInput:
                case SDL_EventType.KeyMapChanged:
                    this.HandleKeyboardEvent(ev);
                    break;

                /* Mouse Events */
                case SDL_EventType.MouseMotion:
                case SDL_EventType.MouseButtonDown:
                case SDL_EventType.MouseButtonUp:
                case SDL_EventType.MouseWheel:
                    this.HandleMouseEvent(ev);
                    break;

                /* GamePad Events */
                case SDL_EventType.ControllerAxisMotion:
                case SDL_EventType.ControllerButtonDown:
                case SDL_EventType.ControllerButtonUp:
                case SDL_EventType.ControllerDeviceAdded:
                case SDL_EventType.ControllerDeviceRemapped:
                case SDL_EventType.ControllerDeviceRemoved:
                    break;

                default:
                    break;
            }
        }

        private void HandleWindowEvent(SDL_WindowEvent windowEvent)
        {
            switch (windowEvent.@event)
            {
                case SDL_WindowEventID.Resized:
                case SDL_WindowEventID.SizeChanged:
                case SDL_WindowEventID.Minimized:
                case SDL_WindowEventID.Maximized:
                case SDL_WindowEventID.Restored:
                    this.HandleResizedMessage();
                    break;
                case SDL_WindowEventID.FocusGained:
                    this.FocusGained?.Invoke();
                    break;
                case SDL_WindowEventID.FocusLost:
                    this.FocusLost?.Invoke();
                    break;
                case SDL_WindowEventID.Close:
                    this.Close();
                    break;
                case SDL_WindowEventID.Shown:
                    this.Shown?.Invoke();
                    break;
                case SDL_WindowEventID.Hidden:
                    this.Hidden?.Invoke();
                    break;
                case SDL_WindowEventID.Enter:
                    this.MouseEntered?.Invoke();
                    break;
                case SDL_WindowEventID.Leave:
                    this.MouseLeft?.Invoke();
                    break;
                case SDL_WindowEventID.Exposed:
                    this.Exposed?.Invoke();
                    break;
                case SDL_WindowEventID.Moved:
                    this.cachedPosition.Value = new Point(windowEvent.data1, windowEvent.data2);
                    // this.Moved?.Invoke(new Point(windowEvent.data1, windowEvent.data2));
                    break;
                default:
                    Debug.WriteLine("Unhandled SDL WindowEvent: " + windowEvent.@event);
                    break;
            }
        }
    }
}
