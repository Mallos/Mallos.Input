# OpenInput [![NuGet Version](http://img.shields.io/nuget/v/OpenInput.svg)](https://www.nuget.org/packages/OpenInput)
A portable way of accessing HID devices cross-platform

> This project is still in development!

> I have put this project to rest, will 
> probably pick it up once I have a reason to use it.

## Supported Platforms

* Windows 
    * [RawInput](src/OpenInput.Windows/RawInput/README.md)
* OpenTK

### Supported Devices

* Mouse
* Keyboard
* TouchDevice
* GamePad

### Getting Started

OpenInput is designed for loc libraries, which makes it easy for 
numerous reasons when implementation and handling crossplatform code.

In this example I am going to show how to use [Nine.Injection](https://github.com/studio-nine/Nine.Injection) which is a lightwight loc library.

First you have to map the devices you wish to use.

```c#
// You can read more on how to map object on Nine.Injection's page.
var container = new Container();
container
    .Map<IMouse>(new RawInput.Mouse(this.Handle))
    .Map<IKeyboard>(new RawInput.Keyboard(this.Handle))
    .Map<IGamePad>(new RawInput.GamePad(this.Handle));
```

Now when you have mapped the devices you can start using them.

```c#
var keyboardState = container.Get<IKeyboard>().GetCurrentState();
if (keyboardState.IsKeyDown(Keys.Space))
{
    /// Executed if Space is down.
}
```

