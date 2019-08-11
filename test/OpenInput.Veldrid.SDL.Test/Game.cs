using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace OpenInput.Test
{
    class Game
    {
        private readonly Sdl2Window window;
        private readonly GraphicsDevice graphicsDevice;
        private readonly ImGuiRenderer imGuiRenderer;
        private readonly CommandList commandList;
        private bool windowResized = false;

        private readonly VeldridDeviceSet deviceSet;
        private readonly TestContext testContext;

        public Game()
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
                GraphicsBackend.Vulkan,
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

            this.deviceSet = new VeldridDeviceSet();
            this.testContext = new TestContext(this.deviceSet);
        }

        public void Run()
        {
            while (this.window.Exists)
            {
                if (this.windowResized)
                {
                    this.windowResized = false;
                    this.graphicsDevice.ResizeMainWindow((uint)this.window.Width, (uint)this.window.Height);
                }

                InputSnapshot inputSnapshot = this.window.PumpEvents();

                // FIXME: Can I get the "last" state from Veldrid?
                this.deviceSet.UpdateSnapshot(inputSnapshot);

                this.testContext.Update(1f / 60);
                this.imGuiRenderer.Update(1f / 60, inputSnapshot);

                this.commandList.Begin();
                this.commandList.SetFramebuffer(this.graphicsDevice.MainSwapchain.Framebuffer);
                this.commandList.ClearColorTarget(0, RgbaFloat.CornflowerBlue);
                this.commandList.ClearDepthStencil(1f);

                this.testContext.AddImGuiStuff();

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
    }
}
