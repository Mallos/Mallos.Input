# Input System
Input System have 2 types of input modes, Action and Axis modes,
which can have "friendly names".

Actions are designed to only handle pressed and released states,
for example a key press, mouse button, or a gamepad button.
So an action is either True or False at all times.

Axes are designed to handle multiple input types that will
create a vector which is useful for handling movement input.
For example with a gamepad you can handle transitional movement.

## Features
- Friendly names for the input types
- Action input (True or False)
- Axis input (Single variable based on the defined modifiers)


## Getting Started

```cs
// Create the Input System using the Device Set
var inputSystem = new InputSystem(defaultSet.Keyboard, defaultSet.Mouse);

// Add a few actions, they will work as True or False.
inputSystem.Actions.Add("Jump", Keys.Space);
inputSystem.Actions.Add("Fire", Keys.F);
inputSystem.Actions.Add("Fire", MouseButtons.Left);

// Add a few axes, they will build a vector input based on all the inputs.
inputSystem.Axis.Add("MoveForward", Keys.W, 1.0f);
inputSystem.Axis.Add("MoveForward", Keys.S, -1.0f);
inputSystem.Axis.Add("MoveRight", Keys.D, 1.0f);
inputSystem.Axis.Add("MoveRight", Keys.A, -1.0f);

// ...

// Call update in the update step, which should be done on each update tick.
inputSystem.Update(elapsedTime);

// ...

// Now you are free to check whether the input action is True or False.
if (inputSystem.GetAction("Jump"))
{
  // do jump
}

// And create a input vector based on 2 axes.
var move = new Vector2(
  inputSystem.GetAxis("MoveForward"),
  inputSystem.GetAxis("MoveRight")
);

// Then handle the movement vector.

```
