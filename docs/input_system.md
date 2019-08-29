# Input System

## Example

```cs
var inputSystem = new InputSystem(defaultSet.Keyboard, defaultSet.Mouse);
inputSystem.Actions.Add("Jump", Keys.Space);
inputSystem.Actions.Add("Fire", Keys.F);
inputSystem.Actions.Add("Fire", MouseButtons.Left);
inputSystem.Axis.Add("MoveForward", Keys.W, 1.0f);
inputSystem.Axis.Add("MoveForward", Keys.S, -1.0f);
inputSystem.Axis.Add("MoveRight", Keys.D, 1.0f);
inputSystem.Axis.Add("MoveRight", Keys.A, -1.0f);

// ...

inputSystem.Update(elapsedTime);

// ...

if (inputSystem.GetAction("Jump"))
{
  // do jump
}

var move = new Vector2(
  inputSystem.GetAxis("MoveForward"),
  inputSystem.GetAxis("MoveRight")
);

// handle move axis

```
