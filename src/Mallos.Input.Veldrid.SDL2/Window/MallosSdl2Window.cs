namespace Mallos.Input.Window
{
    using System;
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Veldrid;
    using Veldrid.Sdl2;
    using static Veldrid.Sdl2.Sdl2Native;

    public unsafe partial class MallosSdl2Window : IWindow, IMouse, IKeyboard
    {
        private IntPtr window;

        // Threaded Sdl2Window flags
        private readonly bool threadedProcessing;
        private bool shouldClose;

        // Cached Sdl2Window state (for threaded processing)
        private BufferedValue<Point> cachedPosition = new BufferedValue<Point>();
        private BufferedValue<Point> cachedSize = new BufferedValue<Point>();
        private string cachedWindowTitle;
        private bool newWindowTitleReceived;

        /// <summary>
        /// Initializes a new instance of the <see cref="MallosSdl2Window"/> class.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="flags"></param>
        /// <param name="threadedProcessing"></param>
        public MallosSdl2Window(string title, int x, int y, int width, int height, SDL_WindowFlags flags, bool threadedProcessing)
        {
            this.threadedProcessing = threadedProcessing;
            if (threadedProcessing)
            {
                using (ManualResetEvent mre = new ManualResetEvent(false))
                {
                    WindowParams wp = new WindowParams()
                    {
                        Title = title,
                        X = x,
                        Y = y,
                        Width = width,
                        Height = height,
                        WindowFlags = flags,
                        ResetEvent = mre
                    };

                    Task.Factory.StartNew(this.WindowOwnerRoutine, wp, TaskCreationOptions.LongRunning);
                    mre.WaitOne();
                }
            }
            else
            {
                this.window = SDL_CreateWindow(title, x, y, width, height, flags);
                this.WindowID = SDL_GetWindowID(this.window);
                Sdl2WindowRegistry.RegisterWindow(this);
                this.PostWindowCreated(flags);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MallosSdl2Window"/> class.
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="threadedProcessing"></param>
        public MallosSdl2Window(IntPtr windowHandle, bool threadedProcessing)
        {
            this.threadedProcessing = threadedProcessing;
            if (threadedProcessing)
            {
                using (ManualResetEvent mre = new ManualResetEvent(false))
                {
                    WindowParams wp = new WindowParams()
                    {
                        WindowHandle = windowHandle,
                        WindowFlags = 0,
                        ResetEvent = mre
                    };

                    Task.Factory.StartNew(this.WindowOwnerRoutine, wp, TaskCreationOptions.LongRunning);
                    mre.WaitOne();
                }
            }
            else
            {
                this.window = SDL_CreateWindowFrom(windowHandle);
                this.WindowID = SDL_GetWindowID(this.window);
                Sdl2WindowRegistry.RegisterWindow(this);
                this.PostWindowCreated(0);
            }
        }

        string IDevice.Name => "Mallos Sdl2 Window";

        /// <inheritdoc />
        public int X
        {
            get => this.cachedPosition.Value.X;
            set => this.SetWindowPosition(value, this.Y);
        }

        /// <inheritdoc />
        public int Y
        {
            get => this.cachedPosition.Value.Y;
            set => this.SetWindowPosition(this.X, value);
        }

        /// <inheritdoc />
        public int Width
        {
            get => this.cachedSize.Value.X;
            set => this.SetWindowSize(value, this.Height);
        }

        /// <inheritdoc />
        public int Height
        {
            get => this.cachedSize.Value.Y;
            set => this.SetWindowSize(this.Width, value);
        }

        /// <inheritdoc />
        public IntPtr Handle => this.GetUnderlyingWindowHandle();

        /// <inheritdoc />
        public string Title
        {
            get => this.cachedWindowTitle;
            set
            {
                this.cachedWindowTitle = value;
                this.newWindowTitleReceived = true;
            }
        }

        /// <inheritdoc />
        public bool LimitPollRate
        {
            get; set;
        }

        /// <inheritdoc />
        public float PollIntervalInMs
        {
            get; set;
        }

        /// <inheritdoc />
        public WindowState WindowState
        {
            get
            {
                SDL_WindowFlags flags = SDL_GetWindowFlags(this.window);
                if (((flags & SDL_WindowFlags.FullScreenDesktop) == SDL_WindowFlags.FullScreenDesktop)
                    || ((flags & (SDL_WindowFlags.Borderless | SDL_WindowFlags.Fullscreen)) == (SDL_WindowFlags.Borderless | SDL_WindowFlags.Fullscreen)))
                {
                    return WindowState.BorderlessFullScreen;
                }
                else if ((flags & SDL_WindowFlags.Minimized) == SDL_WindowFlags.Minimized)
                {
                    return WindowState.Minimized;
                }
                else if ((flags & SDL_WindowFlags.Fullscreen) == SDL_WindowFlags.Fullscreen)
                {
                    return WindowState.FullScreen;
                }
                else if ((flags & SDL_WindowFlags.Maximized) == SDL_WindowFlags.Maximized)
                {
                    return WindowState.Maximized;
                }
                else if ((flags & SDL_WindowFlags.Hidden) == SDL_WindowFlags.Hidden)
                {
                    return WindowState.Hidden;
                }

                return WindowState.Normal;
            }
            set
            {
                switch (value)
                {
                    case WindowState.Normal:
                        SDL_SetWindowFullscreen(this.window, SDL_FullscreenMode.Windowed);
                        break;
                    case WindowState.FullScreen:
                        SDL_SetWindowFullscreen(this.window, SDL_FullscreenMode.Fullscreen);
                        break;
                    case WindowState.Maximized:
                        SDL_MaximizeWindow(this.window);
                        break;
                    case WindowState.Minimized:
                        SDL_MinimizeWindow(this.window);
                        break;
                    case WindowState.BorderlessFullScreen:
                        SDL_SetWindowFullscreen(this.window, SDL_FullscreenMode.FullScreenDesktop);
                        break;
                    case WindowState.Hidden:
                        SDL_HideWindow(this.window);
                        break;
                    default:
                        throw new InvalidOperationException("Illegal WindowState value: " + value);
                }
            }
        }

        /// <inheritdoc />
        public bool Exists
        {
            get; private set;
        }

        /// <inheritdoc />
        public bool Visible
        {
            get => (SDL_GetWindowFlags(this.window) & SDL_WindowFlags.Shown) != 0;
            set
            {
                if (value)
                {
                    SDL_ShowWindow(this.window);
                }
                else
                {
                    SDL_HideWindow(this.window);
                }
            }
        }

        /// <inheritdoc />
        public Vector2 ScaleFactor => Vector2.One;

        /// <inheritdoc />
        public Rectangle Bounds => new Rectangle(this.cachedPosition, this.cachedSize.Value);

        /// <inheritdoc />
        public bool CursorVisible
        {
            get => SDL_ShowCursor(SDL_QUERY) == 1;
            set => SDL_ShowCursor(value ? SDL_ENABLE : SDL_DISABLE);
        }

        /// <inheritdoc />
        public float Opacity
        {
            get
            {
                float opacity = float.NaN;
                return (SDL_GetWindowOpacity(this.window, &opacity) == 0) ? opacity : float.NaN;
            }
            set => SDL_SetWindowOpacity(this.window, value);
        }

        /// <inheritdoc />
        public bool Focused => (SDL_GetWindowFlags(this.window) & SDL_WindowFlags.InputFocus) != 0;

        /// <inheritdoc />
        public bool Resizable
        {
            get => (SDL_GetWindowFlags(this.window) & SDL_WindowFlags.Resizable) != 0;
            set => SDL_SetWindowResizable(this.window, value ? 1u : 0u);
        }

        /// <inheritdoc />
        public bool BorderVisible
        {
            get => (SDL_GetWindowFlags(this.window) & SDL_WindowFlags.Borderless) == 0;
            set => SDL_SetWindowBordered(this.window, value ? 1u : 0u);
        }

        /// <inheritdoc />
        public uint WindowID
        {
            get; private set;
        }

        public TextInput TextInput => throw new NotImplementedException();

        TextInput IKeyboard.TextInput => throw new NotImplementedException();

        /// <inheritdoc />
        public event Action Closing;

        /// <inheritdoc />
        public event Action Closed;

        /// <inheritdoc />
        public event Action Resized;

        /// <inheritdoc />
        public event Action FocusLost;

        /// <inheritdoc />
        public event Action FocusGained;

        /// <inheritdoc />
        public event Action Shown;

        /// <inheritdoc />
        public event Action Hidden;

        /// <inheritdoc />
        public event Action MouseEntered;

        /// <inheritdoc />
        public event Action MouseLeft;

        /// <inheritdoc />
        public event Action Exposed;

        /// <inheritdoc />
        public event Action<DragDropEvent> DragDrop;

        /// <inheritdoc />
        public Point ClientToScreen(Point p)
        {
            Point position = this.cachedPosition;
            return new Point(p.X + position.X, p.Y + position.Y);
        }

        /// <inheritdoc />
        public Point ScreenToClient(Point p)
        {
            Point position = this.cachedPosition;
            return new Point(p.X - position.X, p.Y - position.Y);
        }

        /// <inheritdoc />
        public void Close()
        {
            if (this.threadedProcessing)
            {
                this.shouldClose = true;
            }
            else
            {
                this.CloseCore();
            }
        }

        private void CloseCore()
        {
            Sdl2WindowRegistry.RemoveWindow(this);
            this.Closing?.Invoke();
            SDL_DestroyWindow(this.window);
            this.Exists = false;
            this.Closed?.Invoke();
        }

        private void HandleResizedMessage()
        {
            this.RefreshCachedSize();
            this.Resized?.Invoke();
        }

        private void RefreshCachedSize()
        {
            int w, h;
            SDL_GetWindowSize(this.window, &w, &h);
            this.cachedSize.Value = new Point(w, h);
        }

        private void RefreshCachedPosition()
        {
            int x, y;
            SDL_GetWindowPosition(this.window, &x, &y);
            this.cachedPosition.Value = new Point(x, y);
        }

        private void SetWindowPosition(int x, int y)
        {
            SDL_SetWindowPosition(this.window, x, y);
            this.cachedPosition.Value = new Point(x, y);
        }

        private void SetWindowSize(int width, int height)
        {
            SDL_SetWindowSize(this.window, width, height);
            this.cachedSize.Value = new Point(width, height);
        }

        private void CheckNewWindowTitle()
        {
            if (this.WindowState != WindowState.Minimized && this.newWindowTitleReceived)
            {
                this.newWindowTitleReceived = false;
                SDL_SetWindowTitle(this.window, this.cachedWindowTitle);
            }
        }

        private void PostWindowCreated(SDL_WindowFlags flags)
        {
            this.RefreshCachedPosition();
            this.RefreshCachedSize();

            if ((flags & SDL_WindowFlags.Shown) == SDL_WindowFlags.Shown)
            {
                SDL_ShowWindow(this.window);
            }

            this.Exists = true;
        }

        private IntPtr GetUnderlyingWindowHandle()
        {
            SDL_SysWMinfo wmInfo;
            SDL_GetVersion(&wmInfo.version);
            SDL_GetWMWindowInfo(this.window, &wmInfo);
            if (wmInfo.subsystem == SysWMType.Windows)
            {
                Win32WindowInfo win32Info = Unsafe.Read<Win32WindowInfo>(&wmInfo.info);
                return win32Info.Sdl2Window;
            }

            return this.window;
        }
    }
}
