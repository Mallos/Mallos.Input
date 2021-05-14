namespace Mallos.Input.Blazor.Components
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;

    public class MInputWrapperComponent : ComponentBase
    {
        protected readonly string id = Guid.NewGuid().ToString();

        protected ElementReference wrapperRef;

        public ElementReference WrapperReference => this.wrapperRef;

        [Parameter]
        public RenderFragment ChildContent
        {
            get; set;
        }

        [Inject]
        internal IJSRuntime JSRuntime
        {
            get; set;
        }

        protected Lazy<Task<IJSObjectReference>> ModuleTask
        {
            get; set;
        }

        internal BlazorMouseState mouseState = new();
        internal BlazorKeyboardState keyboardState = new();
        internal BlazorTouchDeviceState touchDeviceState = new();

        protected override async Task OnInitializedAsync()
        {
            this.ModuleTask = new(() => this.JSRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/Mallos.Input.Blazor/JsInterop.js").AsTask());

            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                IJSObjectReference module = await this.ModuleTask.Value;
                bool setupSuccess = await module.InvokeAsync<bool>("init", DotNetObjectReference.Create(this));
                Debug.Assert(setupSuccess);

                bool eventsSuccess = await module.InvokeAsync<bool>("addEvents", this.id);
                Debug.Assert(eventsSuccess);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        [JSInvokable]
        public ValueTask OnMouseMove(int mouseX, int mouseY)
            => this.mouseState.OnMouseMove(mouseX, mouseY);

        [JSInvokable]
        public ValueTask OnMouseWheel(int deltaMode)
            => this.mouseState.OnMouseWheel(deltaMode);

        [JSInvokable]
        public ValueTask OnMouseDown(int buttons)
            => this.mouseState.OnMouseToggle(buttons, true);

        [JSInvokable]
        public ValueTask OnMouseUp(int buttons)
            => this.mouseState.OnMouseToggle(buttons, false);

        [JSInvokable]
        public ValueTask OnKeyDown(int keyCode)
            => this.keyboardState.OnKeyDown(keyCode);

        [JSInvokable]
        public ValueTask OnKeyUp(int keyCode)
            => this.keyboardState.OnKeyUp(keyCode);

        [JSInvokable]
        public ValueTask OnTouch(BlazorTouchPoint[] points)
            => this.touchDeviceState.OnTouch(points);
    }
}
