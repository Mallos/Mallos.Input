namespace OpenInput.Test
{
    using ImGuiNET;
    using OpenInput.Debug.Controls;
    using OpenInput.Mechanics;
    using OpenInput.Mechanics.Input;
    using OpenInput.Mechanics.Layout;
    using OpenInput.Mechanics.Combo;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    class Game : BaseGame
    {
        private readonly VeldridDeviceSet deviceSet;
        private readonly Layout layout;
        private readonly InputSystem InputSystem;
        private readonly ComboTracker ComboTracker;

        private ComboTrackerControl ComboTrackerControl;
        private InputSystemControl InputSystemControl;
        private DeviceSetControl DeviceSetControl;

        public Game()
        {
            this.deviceSet = new VeldridDeviceSet();

            // FIXME: How the generics was done is bad, lets fix that.
            var defaultSet = (IDeviceSet)deviceSet;

            this.DeviceSetControl = new DeviceSetControl(defaultSet);

            // Create a input system and register a few inputs.
            this.InputSystem = new InputSystem(
                defaultSet.Keyboard,
                defaultSet.Mouse);

            var layout = MyLayout.DefaultLayout;
            layout.Apply(InputSystem);

            // Create a combo tracker and register a few combos.
            this.ComboTracker = new ComboTracker(
                defaultSet.KeyboardTracker);

            this.ComboTracker.SequenceCombos.Add("Attack1", Keys.A, Keys.B, Keys.C);
            this.ComboTracker.SequenceCombos.Add("Attack2", Keys.A, Keys.C, Keys.B);
            this.ComboTracker.SequenceCombos.Add("Attack3", Buttons.A, Buttons.B, Buttons.X);
            this.ComboTracker.SequenceCombos.Add("Attack4", Keys.A, Keys.X, Keys.X);

            this.ComboTrackerControl = new ComboTrackerControl(this.ComboTracker);
            this.InputSystemControl = new InputSystemControl(this.InputSystem);
        }

        protected override void Draw(Veldrid.CommandList cl)
        {
            // FIXME: Handle elapsed time and not constant
            float elapsedTime = 1f / 60;

            // FIXME: Can I get the "last" state from Veldrid?
            this.deviceSet.UpdateSnapshot(this.LastInputSnapshot);

            this.deviceSet.Update(elapsedTime);
            this.InputSystem.Update(elapsedTime);
            this.ComboTracker.Update(elapsedTime);

            this.DeviceSetControl.DrawWindow();
            this.InputSystemControl.DrawWindow();
            this.ComboTrackerControl.DrawWindow();
        }
    }
}
