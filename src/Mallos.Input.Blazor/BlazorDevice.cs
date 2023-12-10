namespace Mallos.Input.Blazor
{
    using Mallos.Input.Blazor.Components;

    public abstract class BlazorDevice
    {
        internal MInputWrapperComponent component;

        protected BlazorDevice(MInputWrapperComponent component)
        {
            this.component = component;
        }
    }
}
