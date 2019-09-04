using ImGuiNET;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using Device = SharpDX.Direct3D11.Device;

namespace OpenInput.Test
{
    class Game
    {
        protected DeviceContext ImmediateContext
        {
            get { return device.ImmediateContext; }
        }

        private RenderForm form;
        private RenderLoop renderLoop;

        private Device device;
        private Factory factory;
        private SwapChain swapChain;

        private Texture2D renderTarget;
        private RenderTargetView renderTargetView;

        private ImGuiRenderContext imGuiRender;
        private TestContext testContext;

        public Game()
        {
            this.form = new RenderForm("OpenInput.Test");
            this.renderLoop = new RenderLoop(form);

            var desc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(form.ClientSize.Width, form.ClientSize.Height, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, desc, out device, out swapChain);

            factory = swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(form.Handle, WindowAssociationFlags.IgnoreAll);

            renderTarget = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
            renderTargetView = new RenderTargetView(device, renderTarget);

            this.imGuiRender = new ImGuiRenderContext(this.form, this.device);

            this.testContext = new TestContext(new RawDeviceSet());
        }

        public void Run()
        {
            this.form.Show();
            while (this.renderLoop.NextFrame())
            {
                this.ImmediateContext.ClearRenderTargetView(this.renderTargetView, Color.Black);

                this.imGuiRender.BeginFrame();
                this.testContext.AddImGuiStuff();
                this.imGuiRender.EndFrame();

                this.swapChain.Present(0, PresentFlags.None);
            }
        }
    }
}
