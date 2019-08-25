# OpenInput
A portable way of accessing HID devices cross-platform

## Supported Platforms
* Windows (RawInput)
* OpenTK
* Veldrid

## Features
* Event based input

### Supported Devices
* Mouse
* Keyboard
* ~~TouchDevice~~
* GamePad

### Mechanics
A lot of the simple things should be in the hands of the designers not the programmers, so lets make it easy to modify key features!

* [Combo Tracker](./docs/combo_tracker.md)
  
  Makes it super easy to handle key combos.

* [Input System](./docs/input_system.md)
  
  Bind keys to string keys, support both actions and axis events.

* [Layout](./docs/layout.md)
  
  Rebinding layout, get/modify all the possible rebindable keys and apply it to the *Input System* giving you a simple way to handle user rebindable keys while not making it hard to create new layouts.

## Contributing
Contributions are always welcome.

## License
The project is available as open source under the terms of the [MIT License](http://opensource.org/licenses/MIT).
