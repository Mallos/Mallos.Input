namespace Mallos.Input
{
    using Mallos.Input.Trackers;
    using OpenTK.Input;

    public class OpenTKGamePad : IGamePad
    {
        public int Index { get; private set; }
        public string Name { get; private set; }

        public bool IsConnected
        {
            get { return OpenTK.Input.GamePad.GetState(Index).IsConnected; }
        }

        public OpenTKGamePad(int index)
        {
            this.Index = index;
            this.Name = OpenTK.Input.GamePad.GetName(Index);
        }

        public IGamePadTracker CreateTracker()
        {
            return new BasicGamePadTracker(this);
        }

        public GamePadState GetCurrentState()
        {
            var state = OpenTK.Input.GamePad.GetState(Index);

            if (!state.IsConnected)
                return GamePadState.Empty;

            Buttons buttonsFlags = 0;
            if (state.Buttons.A == ButtonState.Pressed) buttonsFlags |= Buttons.A;
            if (state.Buttons.B == ButtonState.Pressed) buttonsFlags |= Buttons.B;
            if (state.Buttons.Back == ButtonState.Pressed) buttonsFlags |= Buttons.Back;
            if (state.Buttons.BigButton == ButtonState.Pressed) buttonsFlags |= Buttons.BigButton;
            if (state.Buttons.LeftShoulder == ButtonState.Pressed) buttonsFlags |= Buttons.LeftShoulder;
            if (state.Buttons.LeftStick == ButtonState.Pressed) buttonsFlags |= Buttons.LeftStick;
            if (state.Buttons.RightShoulder == ButtonState.Pressed) buttonsFlags |= Buttons.RightShoulder;
            if (state.Buttons.RightStick == ButtonState.Pressed) buttonsFlags |= Buttons.RightStick;
            if (state.Buttons.Start == ButtonState.Pressed) buttonsFlags |= Buttons.Start;
            if (state.Buttons.X == ButtonState.Pressed) buttonsFlags |= Buttons.X;
            if (state.Buttons.Y == ButtonState.Pressed) buttonsFlags |= Buttons.Y;

            var thumbSticks = new GamePadThumbSticks(
                state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, 
                state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);

            var triggers = new GamePadTriggers(
                state.Triggers.Left, 
                state.Triggers.Right);

            var buttons = new GamePadButtons(buttonsFlags);

            var dpad = new GamePadDPad(
                state.DPad.IsUp, 
                state.DPad.IsDown, 
                state.DPad.IsLeft, 
                state.DPad.IsRight);

            return new GamePadState(state.IsConnected, thumbSticks, triggers, buttons, dpad);
        }
    }
}
