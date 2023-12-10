# [ASP.NET Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
Blazor lets you build interactive web UIs using C# instead of JavaScript,
with __Mallos.Input__ we can make it easier to create complex and interactable elements for the web,
with a strong target for handling HTML Canvases.

## Samples
A sample can be found [here](https://github.com/Mallos/Mallos.Input/tree/4c7b80fa7e7f0c80a567b26ea47c3826de4d094a/samples/blazor) which is using Blazor HTML Canvas.

## Installing

1. In the `_Imports.razor` file, the wrapper component have to be included.

```cs
@* Import the Mallos.Input Blazor Component *@
@using Mallos.Input.Blazor.Components
```

2. In the `Pages/Index.razor` file, we add the wrapper to the HTML code.

For example in the sample, it looks like this.
```cs
<MInputWrapper @ref="inputWrapperComponent">
  <BECanvas @ref="canvasComponent"></BECanvas>
</MInputWrapper>

@code
{
    MInputWrapperComponent inputWrapperComponent;
    BECanvasComponent canvasComponent;

    // with the MInputWrapperComponent we can now create the device set.
    var deviceSet = new BlazorDeviceSet(inputWrapperComponent);
}
```
