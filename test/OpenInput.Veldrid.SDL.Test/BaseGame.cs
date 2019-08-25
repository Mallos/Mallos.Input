using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace OpenInput.Test
{
    abstract class BaseGame
    {
        public readonly Sdl2Window window;
        public readonly GraphicsDevice graphicsDevice;
        public readonly ImGuiRenderer imGuiRenderer;
        public readonly CommandList commandList;
        private bool windowResized = false;

        public BaseGame()
        {
            var windowCreateInfo = new WindowCreateInfo
            {
                X = 100,
                Y = 100,
                WindowWidth = 1280,
                WindowHeight = 720,
                WindowTitle = "OpenInput Veldrid SDL2 Test",
            };

            var options = new GraphicsDeviceOptions(
                debug: false,
                swapchainDepthFormat: PixelFormat.R16_UNorm,
                syncToVerticalBlank: true,
                resourceBindingModel: ResourceBindingModel.Improved,
                preferDepthRangeZeroToOne: true,
                preferStandardClipSpaceYDirection: true);
#if DEBUG
            options.Debug = true;
#endif

            VeldridStartup.CreateWindowAndGraphicsDevice(
                windowCreateInfo,
                options,
                GetSupportedGraphicsBackend(),
                out this.window,
                out this.graphicsDevice);

            this.window.Resized += () =>
            {
                this.imGuiRenderer.WindowResized(this.window.Width, this.window.Height);
                this.windowResized = true;
            };

            this.commandList = this.graphicsDevice.ResourceFactory.CreateCommandList();

            this.imGuiRenderer = new ImGuiRenderer(
                this.graphicsDevice,
                this.graphicsDevice.SwapchainFramebuffer.OutputDescription,
                this.window.Width,
                this.window.Height);
        }

        public InputSnapshot LastInputSnapshot { get; private set; }

        public void Run()
        {
            while (this.window.Exists)
            {
                if (this.windowResized)
                {
                    this.windowResized = false;
                    this.graphicsDevice.ResizeMainWindow((uint)this.window.Width, (uint)this.window.Height);
                }

                this.LastInputSnapshot = this.window.PumpEvents();

                this.imGuiRenderer.Update(1f / 60, this.LastInputSnapshot);

                this.commandList.Begin();
                this.commandList.SetFramebuffer(this.graphicsDevice.MainSwapchain.Framebuffer);
                this.commandList.ClearColorTarget(0, RgbaFloat.CornflowerBlue);
                this.commandList.ClearDepthStencil(1f);

                this.Draw(this.commandList);

                this.imGuiRenderer.Render(this.graphicsDevice, this.commandList);

                this.commandList.End();

                if (this.window.Exists)
                {
                    this.graphicsDevice.SubmitCommands(this.commandList);
                    this.graphicsDevice.SwapBuffers();
                    this.graphicsDevice.WaitForIdle();
                }
            }
        }

        protected abstract void Draw(CommandList cl);

        private GraphicsBackend GetSupportedGraphicsBackend()
        {
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
        }
    }
}
