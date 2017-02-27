using ImGuiNET;
using ImGuiNET.OpenTK;
using OpenInput.Mechanics;
using OpenInput.Trackers;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenInput.Test
{
    class Program : GameWindow
    {
        private readonly RenderContext renderContext;

        private readonly List<InputContext> inputContexts;
        private readonly StringBuilder sb = new StringBuilder();

        private InputSystem inputSystem;

        private IKeyboardTracker keyboardTracker;
        private ComboTracker comboTracker;

        private List<string> comboHistory = new List<string>();

        public Program()
            : base(1280, 800, null, "OpenInput.Test")
        {
            Keyboard.KeyDown += Keyboard_KeyDown;

            // Add the different types of input context.
            this.inputContexts = new List<InputContext>(new[]
            {
                InputContext.CreateOpenTK(this),
                InputContext.CreateEmpty(),
            });

            keyboardTracker = inputContexts[0].Keyboard.CreateTracker();

            // Create a input system and register a few inputs.
            inputSystem = new InputSystem(inputContexts[0].Keyboard, inputContexts[0].Mouse);
            inputSystem.Actions.Add(new InputAction("Jump", Keys.Space));
            inputSystem.Axis.Add(new InputAxis("MoveForward", Keys.W, 1.0f));
            inputSystem.Axis.Add(new InputAxis("MoveForward", Keys.S, -1.0f));

            // Create a combo tracker and register a few combos.
            comboTracker = new ComboTracker(keyboardTracker, 0.25f);
            comboTracker.OnComboCalled += ComboTracker_OnComboCalled;
            comboTracker.SequenceCombos.Add(new SequenceCombo("Attack1", Keys.A, Keys.B, Keys.C));
            comboTracker.SequenceCombos.Add(new SequenceCombo("Attack2", Keys.A, Keys.C, Keys.B));
            comboTracker.SequenceCombos.Add(new SequenceCombo("Attack3", Buttons.A, Buttons.B, Buttons.X));

            // Initialize ImGui render context.
            this.renderContext = new RenderContext(this);
        }

        private void ComboTracker_OnComboCalled(SequenceCombo obj)
        {
            comboHistory.Add(obj.Name);

            if (comboHistory.Count > 10)
            {
                comboHistory.RemoveRange(10, comboHistory.Count - 10);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(114f / 255f, 144f / 255f, 154f / 255f, 1.0f);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            inputSystem.Update((float)e.Time);
            keyboardTracker.Update((float)e.Time);
            comboTracker.Update((float)e.Time);

            // Begin the frame
            GL.Clear(ClearBufferMask.ColorBufferBit);
            renderContext.BeginFrame((float)e.Time);

            // Add all the ImGui items
            {
                foreach (var item in inputContexts)
                {
                    TestWindwow_Input(item);
                }

                TestWindow_InputSystem();
                TestWindow_ComboTracker();
            }

            // End the frame
            renderContext.EndFrame();
            this.SwapBuffers();
        }
        
        private void TestWindwow_Input(InputContext inputContext)
        {
            string windowTitle = inputContext.Name + " Input";
            ImGui.BeginWindow(windowTitle);
            {
                if (inputContext.Keyboard != null)
                {
                    var keyboardState = inputContext.Keyboard.GetCurrentState();
                    if (ImGui.CollapsingHeader($"Keyboard (Name: \"{ inputContext.Keyboard.Name }\")", TreeNodeFlags.CollapsingHeader))
                    {
                        foreach (var item in keyboardState.Keys)
                        {
                            ImGui.Text(item.ToString());
                        }
                    }
                }

                if (inputContext.Mouse != null)
                {
                    var mouseState = inputContext.Mouse.GetCurrentState();
                    if (ImGui.CollapsingHeader($"Mouse (Name: \"{ inputContext.Mouse.Name }\")", TreeNodeFlags.CollapsingHeader))
                    {
                        sb.AppendLine($"Position: { mouseState.X }, { mouseState.Y }");
                        sb.AppendLine($"MouseWheel: { mouseState.ScrollWheelValue }");
                        sb.AppendLine();
                        sb.AppendLine($"Left Button: { mouseState.LeftButton }");
                        sb.AppendLine($"Middle Button: { mouseState.MiddleButton }");
                        sb.AppendLine($"Right Button: { mouseState.RightButton }");
                        sb.AppendLine($"XButton1: { mouseState.XButton1 }");
                        sb.AppendLine($"XButton2: { mouseState.XButton2 }");

                        ImGui.Text(sb.ToString());

                        sb.Clear();
                    }
                }

                //if (inputContext.GamePad != null)
                //{
                //    if (ImGui.CollapsingHeader("GamePad", TreeNodeFlags.CollapsingHeader))
                //    {
                //        ImGui.Text("Hello World!");
                //    }
                //}
                //
                //if (ImGui.CollapsingHeader("Touch", TreeNodeFlags.CollapsingHeader))
                //{
                //    ImGui.Text("Hello World!");
                //}
            }
            ImGui.EndWindow();
        }

        private void TestWindow_InputSystem()
        {
            ImGui.BeginWindow("InputSystem");
            {
                foreach (var item in inputSystem.Actions)
                {
                    ImGui.Text($"{ item.Name } = { inputSystem.GetAction(item.Name) }");
                }

                ImGui.Separator();
                foreach (var item in inputSystem.Axis)
                {
                    ImGui.Text($"{ item.Name } = { inputSystem.GetAxis(item.Name) }");
                }
            }
            ImGui.EndWindow();
        }

        private void TestWindow_ComboTracker()
        {
            ImGui.BeginWindow("ComboTracker");
            {
                foreach (var item in comboHistory)
                {
                    ImGui.Text(item);
                }
            }
            ImGui.EndWindow();
        }

        private void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
            {
                this.WindowState = (this.WindowState == WindowState.Fullscreen) ? WindowState.Normal : WindowState.Fullscreen;
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (var game = new Program())
                game.Run(30.0);
        }
    }
}