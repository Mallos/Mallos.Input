namespace Mallos.Input.Blazor.Components
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;
    
    public class MInputWrapperComponent : ComponentBase
    {
        protected readonly string id = Guid.NewGuid().ToString();

        protected ElementReference wrapperRef;

        public ElementReference WrapperReference => this.wrapperRef;

        [Parameter]
        public RenderFragment ChildContent { get; set; }
        
        [Inject]
        internal IJSRuntime JSRuntime { get; set; }

        protected Lazy<Task<IJSObjectReference>> moduleTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.moduleTask = new(() => this.JSRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/Mallos.Input.Blazor/JsInterop.js").AsTask());

            await base.OnInitializedAsync();
        }

        public async ValueTask<BoundingClientRect> GetBoundingClientRect()
        {
            IJSObjectReference module = await this.moduleTask.Value;
            return await module.InvokeAsync<BoundingClientRect>("getBoundingClientRect", this.id);
        }
    }
}
