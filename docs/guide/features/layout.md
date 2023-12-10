# Layout
Handle input layouts easily for the Input System,
this makes it super easy to create multiple input layouts which can be serialized.

## Features
- Easy input layouts for input system
- Helper methods for creating a settings page
- Handle Mouse Buttons, Keyboard keys and GamePad buttons all in the same place

## Getting Started

Create the layout class which have all possible actions and axes that the user will have access too.
```cs
public class LayoutTest : Layout
{
  public LayoutTest(string layoutId)
    : base(layoutId)
  {
  }

  [LayoutItem("Move Forwards", group: "Movement")]
  [AxisTrigger("MoveForward", 1)]
  public InputKeys MoveForwards { get; set; }

  [LayoutItem("Move Backwards", group: "Movement")]
  [AxisTrigger("MoveForward", -1)]
  public InputKeys MoveBackwards { get; set; }

  [LayoutItem("Move Left", group: "Movement")]
  [AxisTrigger("MoveRight", -1)]
  public InputKeys MoveLeft { get; set; }

  [LayoutItem("Move Right", group: "Movement")]
  [AxisTrigger("MoveRight", 1)]
  public InputKeys MoveRight { get; set; }

  [LayoutItem("Jump", group: "Movement")]
  [ActionTrigger("Jump")]
  public InputKeys Jump { get; set; }

  [ActionTrigger("Pause", locked: true)]
  public InputKeys Pause { get; set; }

  public static readonly LayoutTest DefaultLayout = new LayoutTest("KeyboardLayout")
  {
    LayoutName      = "Default Keyboard Layout",
    LayoutDescription = "The default keyboard layout.",
    MoveForwards    = new InputKeys(Keys.W, Keys.Up),
    MoveBackwards   = new InputKeys(Keys.S, Keys.Down),
    MoveLeft        = new InputKeys(Keys.A, Keys.Left),
    MoveRight       = new InputKeys(Keys.D, Keys.Right),
    Jump            = new InputKeys(Keys.Space),
    Pause           = new InputKeys(Keys.Escape),
  };

  public static readonly LayoutTest DefaultGamePadLayout = new LayoutTest ("GamePadLayout")
  {
    LayoutName      = "Default GamePad Layout",
    LayoutDescription = "The default GamePad layout.",
    MoveForwards    = new InputKeys(Buttons.DPadUp),
    MoveBackwards   = new InputKeys(Buttons.DPadDown),
    MoveLeft        = new InputKeys(Buttons.DPadLeft),
    MoveRight       = new InputKeys(Buttons.DPadRight),
    Jump            = new InputKeys(Buttons.A),
    Pause           = new InputKeys(Buttons.Start),
  };
}
```

## Helper Methods

There are a few helper methods to make it easier to create a setting page.
```cs
var mylayout = LayoutTest.DefaultLayout;

// Returns the amount of settings that exist on this layout. 
var settingsCount = mylayout.SettingsCount();

// Returns a dictionary with the settings.
var settings = mylayout.GetSettings();

// Check if the "W" key is currently used anywhere.
if (mylayout.IsKeyUsed(Keys.W))
{
  // show alert are you sure you want to set this key.
}

// Update a setting direcly.
settings["Movement"][0].Set(new InputKeys(Keys.W, Keys.Up));

// if you have some way to save the layout you can do it now.
// There are future plans to build a save system for layouts.
```

## Applying a Layout to Input System

```cs
var mylayout = LayoutTest.DefaultLayout;

mylayout.Apply(inputSystem);
```
