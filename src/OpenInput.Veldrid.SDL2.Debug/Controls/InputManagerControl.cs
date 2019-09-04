namespace OpenInput.Debug.Controls
{
    using ImGuiNET;
    using System;
    using OpenInput.Mechanics;
    using OpenInput.Mechanics.Input;
    using OpenInput.Mechanics.Combo;
    using System.Collections.Generic;

    /// <summary>
    /// Debug Control for getting a insight into the input manager.
    /// </summary>
    public class InputManagerControl<TController> : Control
        where TController : IController
    {
        public InputManagerControl(InputManager<TController> inputManager)
        {

        }

        public InputManager<TController> InputManager { get; }

        public override void DrawControl()
        {
            foreach (var controller in this.InputManager)
            {
                ImGui.Text(controller);
            }
        }
    }
}