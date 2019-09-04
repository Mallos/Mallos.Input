# Input Manager

## TODO

- Request all controllers to do a action to continue (example ready screen)

## Example

```cs
class MyPlayer : IController {

}

class MyInputManager : InputManager<MyPlayer> {
  public MyInputManager(DeviceSet deviceSet)
    : base (deviceSet)
  {
    this.AllowRegister = false;
    this.MaxControllers = 4;
  }

  protected override void OnJoin(MyPlayer player) {

  }
}

// ...

var deviceSet = new VeldridDeviceSet(); /* your device set */
var manager = new MyInputManager(deviceSet);

// Allow controllers to join
manager.AllowRegister = true;

// ...



```
