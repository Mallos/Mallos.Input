using Mallos.Input.Window;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace Mallos.Input.Test
{
    abstract class BaseGame
    {
        public readonly MallosSdl2Window Window;
        public readonly GraphicsDevice GraphicsDevice;
        public readonly ImGuiRenderer ImGuiRenderer;
        public readonly CommandList CommandList;
        private bool windowResized = false;

        public BaseGame()
        {
            WindowCreateInfo windowCreateInfo = new WindowCreateInfo
            {
                X = 100,
                Y = 100,
                WindowWidth = 1280,
                WindowHeight = 720,
                WindowTitle = "Mallos.Input Veldrid SDL2 Test",
            };

            GraphicsDeviceOptions options = new GraphicsDeviceOptions(
                debug: false,
                swapchainDepthFormat: PixelFormat.R16_UNorm,
                syncToVerticalBlank: true,
                resourceBindingModel: ResourceBindingModel.Improved,
                preferDepthRangeZeroToOne: true,
                preferStandardClipSpaceYDirection: true
            );

#if DEBUG
            options.Debug = true;
#endif

            VeldridStartup.CreateWindowAndGraphicsDevice(
                windowCreateInfo,
                options,
                this.GetSupportedGraphicsBackend(),
                out this.Window,
                out this.GraphicsDevice);

            this.Window.Resized += () =>
            {
                this.ImGuiRenderer.WindowResized(this.Window.Width, this.Window.Height);
                this.windowResized = true;
            };

            this.CommandList = this.GraphicsDevice.ResourceFactory.CreateCommandList();

            this.ImGuiRenderer = new ImGuiRenderer(
                this.GraphicsDevice,
                this.GraphicsDevice.SwapchainFramebuffer.OutputDescription,
                this.Window.Width,
                this.Window.Height);
        }

        public void Run()
        {
            while (this.Window.Exists)
            {
                if (this.windowResized)
                {
                    this.windowResized = false;
                    this.GraphicsDevice.ResizeMainWindow((uint)this.Window.Width, (uint)this.Window.Height);
                }

                this.Window.PumpEvents();

                this.ImGuiRenderer.Update(1f / 60, this.LastInputSnapshot);

                this.CommandList.Begin();
                this.CommandList.SetFramebuffer(this.GraphicsDevice.MainSwapchain.Framebuffer);
                this.CommandList.ClearColorTarget(0, RgbaFloat.CornflowerBlue);
                this.CommandList.ClearDepthStencil(1f);

                this.Draw(this.CommandList);

                this.ImGuiRenderer.Render(this.GraphicsDevice, this.CommandList);

                this.CommandList.End();

                if (this.Window.Exists)
                {
                    this.GraphicsDevice.SubmitCommands(this.CommandList);
                    this.GraphicsDevice.SwapBuffers();
                    this.GraphicsDevice.WaitForIdle();
                }
            }
        }

        protected abstract void Draw(CommandList cl);

        private GraphicsBackend GetSupportedGraphicsBackend()
        {
#pragma warning disable IDE0022 // Use expression body for methods
#if DEBUG && WINDOWS
            return GraphicsBackend.Direct3D11;
#elif WINDOWS
            return GraphicsBackend.Vulkan;
#elif OSX
            return GraphicsBackend.Metal;
#else
            // FIXME: OSX DefineConstants
            return GraphicsBackend.Metal; // GraphicsBackend.OpenGL
#endif
#pragma warning restore IDE0022 // Use expression body for methods
        }
    }
}
