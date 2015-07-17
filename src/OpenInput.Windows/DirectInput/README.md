
# DirectInput

[Introduction to DirectInput](https://msdn.microsoft.com/en-us/library/windows/desktop/ee418273(v=vs.85).aspx)

DirectInput is outdated and is not recommended to use by Microsoft. 
Also SharpDX haven't implemented all the functionalities of DirectInput.

#### Pros

- High DPI mouse input
- Support for pre-Win2k machines

#### Cons

- Create a thread in the background that reads raw input
- No support for keyboard key repeat rate
- No support for capital letters and shifted characters
- No support for keymaps other than US-English
- No support for Input Method Editors
- No support for pointer ballistics
- No support for getting mouse position in pixel coordinates

## Supports

- Mouse
- Limited Keyboard
- GamePad

### Dependencies

- SharpDX
- SharpDX.DirectInput

---

##### References

- [Reasons not to use DirectInput for Keyboard Input](http://www.gamedev.net/blog/233/entry-1567278-reasons-not-to-use-directinput-for-keyboard-input/)
