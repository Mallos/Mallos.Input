namespace OpenInput.Test
{
    using ImGuiNET;
    using OpenInput.Mechanics;
    using System.Collections.Generic;
    using System.Text;

    class TestContext
    {
        public readonly List<DeviceSet> DeviceSets;

        public readonly InputSystem InputSystem;
        public readonly ComboTracker ComboTracker;

        public readonly List<string> ComboHistory = new List<string>();
        public readonly int ComboHistoryMax = 10;

        private readonly StringBuilder sb = new StringBuilder();
        
        public TestContext(DeviceSet defaultSet)
        {
            //new OpenInput.RawDeviceSet(windowHandle.Value), // TODO: Window Handle
            
            // Add the different types of input context.
            DeviceSets = new List<DeviceSet>(new[]
            {
                defaultSet,
                new OpenInput.Dummy.DummyDeviceSet(),
            });
            
            // Create a input system and register a few inputs.
            InputSystem = new InputSystem(defaultSet.Keyboard, defaultSet.Mouse);
            InputSystem.Actions.Add(new InputAction("Jump", Keys.Space));
            InputSystem.Axis.Add(new InputAxis("MoveForward", Keys.W, 1.0f));
            InputSystem.Axis.Add(new InputAxis("MoveForward", Keys.S, -1.0f));

            // Create a combo tracker and register a few combos.
            ComboTracker = new ComboTracker(defaultSet.KeyboardTracker, 0.5f, 4);
            ComboTracker.OnComboCalled += ComboTracker_OnComboCalled;
            ComboTracker.SequenceCombos.Add(new SequenceCombo("Attack1", Keys.A, Keys.B, Keys.C));
            ComboTracker.SequenceCombos.Add(new SequenceCombo("Attack2", Keys.A, Keys.C, Keys.B));
            ComboTracker.SequenceCombos.Add(new SequenceCombo("Attack3", Buttons.A, Buttons.B, Buttons.X));
        }

        public void Update(float elapsedTime)
        {
            foreach (var item in DeviceSets)
            {
                item.Update(elapsedTime);
            }

            InputSystem.Update(elapsedTime);
            ComboTracker.Update(elapsedTime);
        }

        public void AddImGuiStuff()
        {
            foreach (var item in DeviceSets)
            {
                TestWindwow_Input(item);
            }

            TestWindow_InputSystem(InputSystem);
            TestWindow_ComboTracker(ComboTracker, ComboHistory);
        }

        private void ComboTracker_OnComboCalled(SequenceCombo obj)
        {
            ComboHistory.Add(obj.Name);
            if (ComboHistory.Count > ComboHistoryMax)
            {
                ComboHistory.RemoveRange(0, ComboHistory.Count - ComboHistoryMax);
            }
        }

        private void TestWindwow_Input(DeviceSet inputContext)
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

                if (ImGui.CollapsingHeader("GamePads", TreeNodeFlags.CollapsingHeader))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (ImGui.CollapsingHeader($"GamePad [{ i }]", TreeNodeFlags.CollapsingHeader))
                        {
                            var gamepadState = inputContext.GamePads[i].GetCurrentState();

                            ImGui.Text($"Name: { inputContext.GamePads[i].Name }");
                            ImGui.Text($"IsConnected: { gamepadState.IsConnected }");

                            ImGui.BeginChild($"GamePad.{i}.Child1", true);
                            if (ImGui.CollapsingHeader("Buttons", TreeNodeFlags.CollapsingHeader))
                            {
                                var buttonValues = System.Enum.GetValues(typeof(Buttons));
                                for (int i2 = 0; i2 < buttonValues.Length; i2++)
                                {
                                    Buttons button = (Buttons)buttonValues.GetValue(i2);
                                    var buttonDown = gamepadState.Buttons.IsButtonDown(button);
                                    ImGui.Text($"{ button } = { buttonDown }");
                                }
                            }

                            if (ImGui.CollapsingHeader("ThumbSticks", TreeNodeFlags.CollapsingHeader))
                            {
                                ImGui.Text($"Left: { gamepadState.ThumbSticks.LeftThumbstick.X }, { gamepadState.ThumbSticks.LeftThumbstick.Y }");
                                ImGui.Text($"Right: { gamepadState.ThumbSticks.RightThumbstick.X }, { gamepadState.ThumbSticks.RightThumbstick.Y }");
                            }

                            if (ImGui.CollapsingHeader("Triggers", TreeNodeFlags.CollapsingHeader))
                            {
                                ImGui.Text($"Left: { gamepadState.Triggers.Left }");
                                ImGui.Text($"Right: { gamepadState.Triggers.Right }");
                            }
                            ImGui.EndChild();
                        }
                    }
                }

                //if (ImGui.CollapsingHeader("Touch", TreeNodeFlags.CollapsingHeader))
                //{
                //    ImGui.Text("Hello World!");
                //}
            }
            ImGui.EndWindow();
        }

        private void TestWindow_InputSystem(InputSystem inputSystem)
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

        private void TestWindow_ComboTracker(ComboTracker comboTracker, IList<string> comboHistory)
        {
            ImGui.BeginWindow("ComboTracker");
            {
                ImGui.Text("# Combos:");
                foreach (var item in comboTracker.SequenceCombos)
                {
                    ImGui.Text($"{ item.Name } = [{ string.Join(", ", item.Keys) }]");
                }

                ImGui.Separator();
                ImGui.Text($"Current: { comboTracker.GetHistoryString() }");

                ImGui.Separator();
                ImGui.Text("# History:");

                ImGui.BeginChild("ComboTrackerChild1", true);
                {
                    foreach (var item in comboHistory)
                    {
                        ImGui.Text(item);
                    }
                }
                ImGui.EndChild();
            }
            ImGui.EndWindow();
        }
    }
}