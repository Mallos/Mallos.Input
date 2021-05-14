# Events
Events are taken directly from the device set.

Events are built on top of a tracking system,
which can be specifically built for a platform to handle the raw events or
a real-time tracker that checks the difference each tick.
That will in turn invoke the events.

## Features
- Mouse Events
- Keyboard Events
- GamePad Events

### Input Tracker
Input Tracker is an extra feature that was built to track multiple types of devices.
Which will combine the events to just a very few, but still keep the same context.

This kind of tracker is used in the Combo system,
which supports multiple types of inputs from different devices.

## Getting Started
Make sure that the device set is getting updated on every tick.
This is required for all platforms that doesn't pull the events directly when they happen.

```cs
this.DeviceSet.Update(elapsedTime);
```

### Mouse Event Example
This is something that I used to test all the mouse events that were called by the blazor device set.

```cs
class MouseLogger
{
    public BlazorDeviceSet DeviceSet { get; }

    public MouseTestLogger(BlazorDeviceSet DeviceSet)
    {
        this.DeviceSet = DeviceSet;
        this.DeviceSet.MouseTracker.MouseDown += MouseTracker_MouseDown;
        this.DeviceSet.MouseTracker.MouseUp += MouseTracker_MouseUp;
        this.DeviceSet.MouseTracker.MouseWheel += MouseTracker_MouseWheel;
        this.DeviceSet.MouseTracker.Move += MouseTracker_Move;
    }

    private void MouseTracker_Move(object sender, Mallos.Input.MouseEventArgs e)
    {
        var mouseState = this.DeviceSet.Mouse.GetCurrentState();
        Console.WriteLine(mouseState);
    }

    private void MouseTracker_MouseWheel(object sender, Mallos.Input.MouseWheelEventArgs e)
    {
        var mouseState = this.DeviceSet.Mouse.GetCurrentState();
        Console.WriteLine(mouseState);
    }

    private void MouseTracker_MouseUp(object sender, Mallos.Input.MouseButtonEventArgs e)
    {
        var mouseState = this.DeviceSet.Mouse.GetCurrentState();
        Console.WriteLine(mouseState);
    }

    private void MouseTracker_MouseDown(object sender, Mallos.Input.MouseButtonEventArgs e)
    {
        var mouseState = this.DeviceSet.Mouse.GetCurrentState();
        Console.WriteLine(mouseState);
    }
}
```

### Keyboard Event Example
This is something that I used to test all the keyboard events that were called by the blazor device set.

```cs
class KeyboardTestLogger
{
    public BlazorDeviceSet DeviceSet { get; }

    public KeyboardTestLogger(BlazorDeviceSet DeviceSet)
    {
        this.DeviceSet = DeviceSet;
        this.DeviceSet.KeyboardTracker.KeyUp += KeyboardTracker_KeyUp;
        this.DeviceSet.KeyboardTracker.KeyDown += KeyboardTracker_KeyDown;
    }

    private void KeyboardTracker_KeyUp(object sender, Mallos.Input.KeyEventArgs e)
    {
        var keyboardState = this.DeviceSet.Keyboard.GetCurrentState();
        Console.WriteLine("KeyUp: " + keyboardState);
    }

    private void KeyboardTracker_KeyDown(object sender, Mallos.Input.KeyEventArgs e)
    {
        var keyboardState = this.DeviceSet.Keyboard.GetCurrentState();
        Console.WriteLine("KeyDown: " + keyboardState);
    }
}

```
