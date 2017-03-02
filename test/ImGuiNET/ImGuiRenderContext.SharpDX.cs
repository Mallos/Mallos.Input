namespace ImGuiNET
{
    using SharpDX.D3DCompiler;
    using SharpDX.Direct3D11;
    using System;
    using System.Windows.Forms;
    using Buffer = SharpDX.Direct3D11.Buffer;
    using Format = SharpDX.DXGI.Format;

    public class ImGuiRenderContext
    {
        private static readonly string vertexShaderCode = @"
cbuffer vertexBuffer : register(b0)
{
    float4x4 ProjectionMatrix;
};

struct VS_INPUT
{
    float2 pos : POSITION;
    float4 col : COLOR0;
    float2 uv  : TEXCOORD0;
};

struct PS_INPUT
{
    float4 pos : SV_POSITION;
    float4 col : COLOR0;
    float2 uv  : TEXCOORD0;
};

PS_INPUT main(VS_INPUT input)
{
    PS_INPUT output;
    output.pos = mul(ProjectionMatrix, float4(input.pos.xy, 0.f, 1.f));
    output.col = input.col;
    output.uv  = input.uv;
    return output;
}";

        private static readonly string pixelShaderCode = @"
struct PS_INPUT
{
    float4 pos : SV_POSITION;
    float4 col : COLOR0;
    float2 uv  : TEXCOORD0;
};

sampler sampler0;
Texture2D texture0;

float4 main(PS_INPUT input) : SV_Target
{
    float4 out_col = input.col * texture0.Sample(sampler0, input.uv); 
    return out_col; 
}";


        static readonly int FontTextureId = -12345;
        
        public readonly Form Window;
        public readonly Device Device;

        private Buffer vertexBuffer;
        private Buffer indexBuffer;

        private VertexShader vertexShader;
        private PixelShader pixelShader;
        private InputLayout inputLayout;

        private Buffer vertexConstantBuffer;
        private SamplerState fontSampler;

        private Texture2D fontTexture;
        private ShaderResourceView fontTextureView;

        private RasterizerState rasterizerState;
        private BlendState blendState;
        private DepthStencilState depthStencilState;
        
        private float wheelPosition;

        public ImGuiRenderContext(Form window, Device device)
        {
            this.Window = window ?? throw new ArgumentNullException(nameof(window));
            this.Device = device ?? throw new ArgumentNullException(nameof(device));

            window.KeyDown += OnKeyDown;
            window.KeyUp += OnKeyUp;
            window.KeyPress += OnKeyPress;

            ImGui.GetIO().FontAtlas.AddDefaultFont();

            SetOpenTKKeyMappings();
            
            CreateDeviceObjects();
        }
        
        public void BeginFrame(float elapsedTime = 1.0f / 60.0f)
        {
            IO io = ImGui.GetIO();
            io.DisplaySize = new System.Numerics.Vector2(Window.Width, Window.Height);
            io.DeltaTime = elapsedTime;

            UpdateImGuiInput(io);

            ImGui.NewFrame();
        }

        public unsafe void EndFrame()
        {
            ImGui.Render();

            DrawData* data = ImGui.GetDrawData();
            RenderImDrawData(data);
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            ImGui.AddInputCharacter(e.KeyChar);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            ImGui.GetIO().KeysDown[(int)e.KeyCode] = true;
            UpdateModifiers(e);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            ImGui.GetIO().KeysDown[(int)e.KeyCode] = false;
            UpdateModifiers(e);
        }

        private unsafe int OnTextEdited(TextEditCallbackData* data)
        {
            char currentEventChar = (char)data->EventChar;
            return 0;
        }

        private static void UpdateModifiers(KeyEventArgs e)
        {
            IO io = ImGui.GetIO();
            io.AltPressed = e.Alt;
            io.CtrlPressed = e.Control;
            io.ShiftPressed = e.Shift;
        }

        private unsafe void CreateDeviceObjects()
        {
            IO io = ImGui.GetIO();

            // Build texture atlas
            FontTextureData texData = io.FontAtlas.GetTexDataAsRGBA32();

            // Create DirectX Texture
            fontTexture = new Texture2D(Device, new Texture2DDescription()
            {
                Width = texData.Width,
                Height = texData.Height,
                MipLevels = 1,
                ArraySize = 1,
                Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 1),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.ShaderResource,
                CpuAccessFlags = 0
            }, new SharpDX.DataRectangle(new IntPtr(texData.Pixels), texData.Width));

            fontTextureView = new ShaderResourceView(Device, fontTexture, new ShaderResourceViewDescription()
            {
                Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm,
                Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D,
                Texture2D = new ShaderResourceViewDescription.Texture2DResource()
                {
                    MipLevels = 1,
                    MostDetailedMip = 0,
                }
            });

            io.FontAtlas.SetTexID(FontTextureId);

            // Create texture sampler
            fontSampler = new SamplerState(Device, new SamplerStateDescription()
            {
                Filter = Filter.MinMagMipLinear,
                AddressU = TextureAddressMode.Wrap,
                AddressV = TextureAddressMode.Wrap,
                AddressW = TextureAddressMode.Wrap,
                MipLodBias = 0.0f,
                ComparisonFunction = Comparison.Always,
                MinimumLod = 0.0f,
                MaximumLod = 0.0f
            });

            // Compile Shader
            var vertexShaderByteCode = ShaderBytecode.Compile(vertexShaderCode, "vs_4_0", ShaderFlags.None, EffectFlags.None);
            var vertexShader = new VertexShader(Device, vertexShaderByteCode);

            var pixelShaderByteCode = ShaderBytecode.Compile(pixelShaderCode, "ps_4_0", ShaderFlags.None, EffectFlags.None);
            var pixelShader = new PixelShader(Device, pixelShaderByteCode);

            inputLayout = new InputLayout(Device,
                ShaderSignature.GetInputSignature(vertexShaderByteCode),
                new[]
                {
                    new InputElement("POSITION", 0, Format.R32G32_Float,   0),
                    new InputElement("TEXCOORD", 0, Format.R32G32_Float,   1),
                    new InputElement("COLOR",    0, Format.R8G8B8A8_UNorm, 2)
                });

            vertexConstantBuffer = new Buffer(Device, SharpDX.Utilities.SizeOf<SharpDX.Matrix>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

            // Create the blending setup
            var blendStateDesc = new BlendStateDescription();
            blendStateDesc.AlphaToCoverageEnable = true;
            blendStateDesc.RenderTarget[0].IsBlendEnabled = true;
            blendStateDesc.RenderTarget[0].SourceAlphaBlend = BlendOption.SourceAlpha;
            blendStateDesc.RenderTarget[0].DestinationBlend = BlendOption.InverseSourceAlpha;
            blendStateDesc.RenderTarget[0].BlendOperation = BlendOperation.Add;
            blendStateDesc.RenderTarget[0].SourceBlend = BlendOption.InverseSourceAlpha;
            blendStateDesc.RenderTarget[0].DestinationAlphaBlend = BlendOption.Zero;
            blendStateDesc.RenderTarget[0].AlphaBlendOperation = BlendOperation.Add;
            blendStateDesc.RenderTarget[0].RenderTargetWriteMask = ColorWriteMaskFlags.All;
            blendState = new BlendState(Device, blendStateDesc);

            // Create the rasterizer state
            rasterizerState = new RasterizerState(Device, new RasterizerStateDescription()
            {
                FillMode = FillMode.Solid,
                CullMode = CullMode.None,
                IsScissorEnabled = true,
                IsDepthClipEnabled = true,
            });

            // Create depth-stencil State
            depthStencilState = new DepthStencilState(Device, new DepthStencilStateDescription()
            {
                IsDepthEnabled = true,
                DepthWriteMask = DepthWriteMask.All,
                DepthComparison = Comparison.Always,
                IsStencilEnabled = false,
                FrontFace = new DepthStencilOperationDescription()
                {
                    FailOperation = StencilOperation.Keep,
                    DepthFailOperation = StencilOperation.Keep,
                    PassOperation = StencilOperation.Keep
                },
                BackFace = new DepthStencilOperationDescription()
                {
                    FailOperation = StencilOperation.Keep,
                    DepthFailOperation = StencilOperation.Keep,
                    PassOperation = StencilOperation.Keep
                }
            });
        }

        private static void SetOpenTKKeyMappings()
        {
            IO io = ImGui.GetIO();
            io.KeyMap[GuiKey.Tab] = (int)Keys.Tab;
            io.KeyMap[GuiKey.LeftArrow] = (int)Keys.Left;
            io.KeyMap[GuiKey.RightArrow] = (int)Keys.Right;
            io.KeyMap[GuiKey.UpArrow] = (int)Keys.Up;
            io.KeyMap[GuiKey.DownArrow] = (int)Keys.Down;
            io.KeyMap[GuiKey.PageUp] = (int)Keys.PageUp;
            io.KeyMap[GuiKey.PageDown] = (int)Keys.PageDown;
            io.KeyMap[GuiKey.Home] = (int)Keys.Home;
            io.KeyMap[GuiKey.End] = (int)Keys.End;
            io.KeyMap[GuiKey.Delete] = (int)Keys.Delete;
            io.KeyMap[GuiKey.Backspace] = (int)Keys.Back;
            io.KeyMap[GuiKey.Enter] = (int)Keys.Enter;
            io.KeyMap[GuiKey.Escape] = (int)Keys.Escape;
            io.KeyMap[GuiKey.A] = (int)Keys.A;
            io.KeyMap[GuiKey.C] = (int)Keys.C;
            io.KeyMap[GuiKey.V] = (int)Keys.V;
            io.KeyMap[GuiKey.X] = (int)Keys.X;
            io.KeyMap[GuiKey.Y] = (int)Keys.Y;
            io.KeyMap[GuiKey.Z] = (int)Keys.Z;
        }

        private unsafe void UpdateImGuiInput(IO io)
        {
            //MouseState cursorState = Mouse.GetCursorState();
            //MouseState mouseState = Mouse.GetState();

            //if (Window.Bounds.Contains(cursorState.X, cursorState.Y))
            //{
            //    Point windowPoint = Window.PointToClient(new Point(cursorState.X, cursorState.Y));
            //    io.MousePosition = new System.Numerics.Vector2(windowPoint.X / io.DisplayFramebufferScale.X, windowPoint.Y / io.DisplayFramebufferScale.Y);
            //}
            //else
            //{
            //    io.MousePosition = new System.Numerics.Vector2(-1f, -1f);
            //}

            //io.MouseDown[0] = mouseState.LeftButton == ButtonState.Pressed;
            //io.MouseDown[1] = mouseState.RightButton == ButtonState.Pressed;
            //io.MouseDown[2] = mouseState.MiddleButton == ButtonState.Pressed;

            //float newWheelPos = mouseState.WheelPrecise;
            //io.MouseWheel = newWheelPos - wheelPosition;
            //wheelPosition = newWheelPos;
            throw new NotImplementedException();
        }

        private unsafe void RenderImDrawData(DrawData* draw_data)
        {
            var context = this.Device.ImmediateContext;

            throw new NotImplementedException();
        }
    }
}
