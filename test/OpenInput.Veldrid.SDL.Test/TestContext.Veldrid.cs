namespace OpenInput.Test
{
    using ImGuiNET;
    using OpenInput.Mechanics;
    using OpenInput.Mechanics.Input;
    using OpenInput.Mechanics.Layout;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    class TestContext
    {
        public readonly List<DeviceSet> DeviceSets;

        public readonly Layout layout;

        public readonly InputSystem InputSystem;
        public readonly ComboTracker ComboTracker;

        public readonly List<string> ComboHistory = new List<string>();
        public readonly int ComboHistoryMax = 10;

        private readonly StringBuilder sb = new StringBuilder();

        public TestContext(DeviceSet defaultSet, MyLayout layout)
        {
            this.layout = layout;

            // Add the different types of input context.
            DeviceSets = new List<DeviceSet>(new[]
            {
                defaultSet,
                // new OpenInput.Dummy.DummyDeviceSet(),
            });

            // Create a input system and register a few inputs.
            InputSystem = new InputSystem(defaultSet.Keyboard, defaultSet.Mouse);
            layout.Apply(InputSystem);

            // Create a combo tracker and register a few combos.
            ComboTracker = new ComboTracker(defaultSet.KeyboardTracker);
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
            ImGui.Begin(windowTitle);
            {
                if (inputContext.Keyboard != null)
                {
                    var keyboardState = inputContext.Keyboard.GetCurrentState();
                    if (ImGui.CollapsingHeader($"Keyboard (Name: \"{ inputContext.Keyboard.Name }\")", ImGuiTreeNodeFlags.CollapsingHeader))
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
                    if (ImGui.CollapsingHeader($"Mouse (Name: \"{ inputContext.Mouse.Name }\")", ImGuiTreeNodeFlags.CollapsingHeader))
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

                if (ImGui.CollapsingHeader("GamePads", ImGuiTreeNodeFlags.CollapsingHeader))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (ImGui.CollapsingHeader($"GamePad [{ i }]", ImGuiTreeNodeFlags.CollapsingHeader))
                        {
                            var gamepadState = inputContext.GamePads[i].GetCurrentState();

                            ImGui.Text($"Name: { inputContext.GamePads[i].Name }");
                            ImGui.Text($"IsConnected: { gamepadState.IsConnected }");

                            ImGui.BeginChild($"GamePad.{i}.Child1");
                            if (ImGui.CollapsingHeader("Buttons", ImGuiTreeNodeFlags.CollapsingHeader))
                            {
                                var buttonValues = System.Enum.GetValues(typeof(Buttons));
                                for (int i2 = 0; i2 < buttonValues.Length; i2++)
                                {
                                    Buttons button = (Buttons)buttonValues.GetValue(i2);
                                    var buttonDown = gamepadState.Buttons.IsButtonDown(button);
                                    ImGui.Text($"{ button } = { buttonDown }");
                                }
                            }

                            if (ImGui.CollapsingHeader("ThumbSticks", ImGuiTreeNodeFlags.CollapsingHeader))
                            {
                                ImGui.Text($"Left: { gamepadState.ThumbSticks.LeftThumbstick.X }, { gamepadState.ThumbSticks.LeftThumbstick.Y }");
                                ImGui.Text($"Right: { gamepadState.ThumbSticks.RightThumbstick.X }, { gamepadState.ThumbSticks.RightThumbstick.Y }");
                            }

                            if (ImGui.CollapsingHeader("Triggers", ImGuiTreeNodeFlags.CollapsingHeader))
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
            ImGui.End();
        }

        private void TestWindow_InputSystem(InputSystem inputSystem)
        {
            ImGui.Begin("InputSystem");
            {
                foreach (var item in inputSystem.Actions.GetValues())
                {
                    ImGui.Text($"{item.Key} = {item.Value}");
                }

                ImGui.Separator();
                foreach (var item in inputSystem.Axis.GetValues())
                {
                    ImGui.Text($"{item.Key} = {item.Value}");
                }

                ImGui.Separator();

                if (ImGui.CollapsingHeader("Layout", ImGuiTreeNodeFlags.CollapsingHeader))
                {
                    var settings = this.layout.GetSettings();
                    var settingsKeys = settings.Keys.ToArray();
                    for (var gi = 0; gi < settingsKeys.Length; gi++)
                    {
                        var groupKey = settingsKeys[gi];
                        var group = settings[groupKey];

                        ImGui.Text($"- {groupKey} -");

                        ImGui.Separator();
                        ImGui.Columns(3, null, true);
                        for (var i = 0; i < group.Count; i++)
                        {
                            var setting = group[i];
                            if (setting.IsReadOnly)
                            {
                                ImGui.Text($"[ReadOnly] {setting.Name}");
                            }
                            else
                            {
                                ImGui.Text($"{setting.Name}");
                            }

                            if (ImGui.IsItemHovered() && !string.IsNullOrWhiteSpace(setting.Description))
                            {
                                ImGui.SetTooltip(setting.Description);
                            }

                            ImGui.NextColumn();
                            if (setting.Keys.Keys.Length > 0)
                            {
                                ImGui.Text($"{setting.Keys.Keys[0]}");
                            }
                            ImGui.NextColumn();
                            if (setting.Keys.Keys.Length > 1)
                            {
                                ImGui.Text($"{setting.Keys.Keys[1]}");
                            }
                            ImGui.NextColumn();
                        }
                        ImGui.Columns(1);
                        ImGui.Separator();
                    }
                }
            }
            ImGui.End();
        }

        private void TestWindow_ComboTracker(ComboTracker comboTracker, IList<string> comboHistory)
        {
            ImGui.Begin("ComboTracker");
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

                ImGui.BeginChild("ComboTrackerChild1");
                {
                    foreach (var item in comboHistory)
                    {
                        ImGui.Text(item);
                    }
                }
                ImGui.EndChild();
            }
            ImGui.End();
        }
    }
}