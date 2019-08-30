namespace OpenInput.Test
{
    using ImGuiNET;
    using OpenInput.Mechanics;
    using OpenInput.Mechanics.Input;
    using OpenInput.Mechanics.Layout;
    using OpenInput.Mechanics.Combo;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    using OpenInput.Debug.Controls;

    class TestContext
    {
        public readonly List<IDeviceSet> DeviceSets;

        public readonly Layout layout;
        public readonly InputSystem InputSystem;
        public readonly ComboTracker ComboTracker;

        public ComboTrackerControl ComboTrackerControl;
        public InputSystemControl InputSystemControl;

        public TestContext(IDeviceSet defaultSet, MyLayout layout)
        {
            this.layout = layout;

            // Add the different types of input context.
            DeviceSets = new List<IDeviceSet>(new[]
            {
                defaultSet,
                // new OpenInput.Dummy.DummyDeviceSet(),
            });

            // Create a input system and register a few inputs.
            InputSystem = new InputSystem(defaultSet.Keyboard, defaultSet.Mouse);
            layout.Apply(InputSystem);

            // Create a combo tracker and register a few combos.
            ComboTracker = new ComboTracker(defaultSet.KeyboardTracker);
            ComboTracker.SequenceCombos.Add("Attack1", Keys.A, Keys.B, Keys.C);
            ComboTracker.SequenceCombos.Add("Attack2", Keys.A, Keys.C, Keys.B);
            ComboTracker.SequenceCombos.Add("Attack3", Buttons.A, Buttons.B, Buttons.X);
            ComboTracker.SequenceCombos.Add("Attack4", Keys.A, Keys.X, Keys.X);

            ComboTrackerControl = new ComboTrackerControl(ComboTracker);
            InputSystemControl = new InputSystemControl(InputSystem);
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
            InputSystemControl.DrawWindow();
            ComboTrackerControl.DrawWindow();
        }
    }
}