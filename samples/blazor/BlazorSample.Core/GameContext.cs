using System;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using Mallos.Input;
using Mallos.Input.Blazor;
using Mallos.Input.Blazor.Components;
using Mallos.Input.Mechanics;

namespace BlazorSample.Core
{
    public class GameContext
    {
        public GameTime GameTime { get; } = new();

        /// <summary>
        /// Gets the 
        /// </summary>
        public Display Display { get; } = new();

        /// <summary>
        /// Gets the Mallos Input system.
        /// </summary>
        public InputSystem InputSystem
        {
            get;
        }

        /// <summary>
        /// Gets the Mallos Input device set.
        /// </summary>
        public BlazorDeviceSet DeviceSet
        {
            get;
        }

        /// <summary>
        /// Gets the HTML Canvas render context.
        /// </summary>
        public Canvas2DContext Context
        {
            get;
        }

        public GameContext(Canvas2DContext context, MInputWrapperComponent component)
        {
            this.Context = context;
            this.DeviceSet = new BlazorDeviceSet(component);

            this.InputSystem = new InputSystem(
                this.DeviceSet.Keyboard,
                this.DeviceSet.Mouse
            );
        }

        public async ValueTask Step(float elapsedTime)
        {
            this.GameTime.TotalTime = elapsedTime;

            this.DeviceSet.Update(elapsedTime);
            this.InputSystem.Update(elapsedTime);

            await this.Render();
        }

        protected async ValueTask Render()
        {
            await this.Context.ClearRectAsync(0, 0, this.Display.Size.Width, this.Display.Size.Height);
            await this.Context.SetFontAsync("20px Courier");

            await this.Render_KeyboardState();
            await this.Render_MouseState();
        }

        private async ValueTask Render_KeyboardState()
        {
            KeyboardState keyboardState = this.DeviceSet.Keyboard.GetCurrentState();

            double currentX = 50;
            double currentY = 50;

            await this.Context.SetFillStyleAsync($"rgb(255,255,255)");

            await this.Context.FillTextAsync("Keys Down:", currentX, currentY);
            currentY += 24;

            foreach (Keys key in keyboardState.Keys)
            {
                string keyText = key.ToString();

                await this.Context.FillTextAsync(keyText, currentX, currentY);

                // TextMetrics messure = await this.Context.MeasureTextAsync(keyText);
                // double textHeight = (messure.EmHeightAscent + messure.EmHeightDescent) * 1.2f;
                // currentY += textHeight;
                // TODO: textHeight is always 0

                currentY += 16;
            }
        }

        private async ValueTask Render_MouseState()
        {
            MouseState mouseState = this.DeviceSet.Mouse.GetCurrentState();

            int radius = mouseState.LeftButton ? 12 : 16;

            await this.Context.SetStrokeStyleAsync($"rgb(255,0,0)");

            await this.Context.BeginPathAsync();
            await this.Context.SetLineWidthAsync(2);
            await this.Context.ArcAsync(mouseState.X, mouseState.Y, radius, 0, 2 * MathF.PI);
            await this.Context.StrokeAsync();

            await this.Context.SetFillStyleAsync($"rgb(0,255,0)>");
            await this.Context.FillTextAsync($"<{mouseState.X}, {mouseState.Y}>", mouseState.X + 32, mouseState.Y);
            await this.Context.FillTextAsync($"Wheel: {mouseState.ScrollWheelValue}", mouseState.X + 32, mouseState.Y + 24);
        }
    }
}
