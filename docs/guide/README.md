# Introduction
## What is Mallos.Input?
Mallos.Input is a portable way of accessing HID (Human interface device) devices cross-platform.

The goal of Mallos.Input is to provide a simple and reusable input system,
with very limited amount of knowladge to get started.

Mallos.Input is built on top of __.NET Standard 2.0__,
while the platform specific code have more requirements.
By having an abstract and portable way of handling the input,
it is easier to create portable code.

# Getting Started
For each platform there are a few different steps that have to be taken.
It's better to follow the instructions under the "Platform" category,
it can be found in the sidebar.

## Currently Supported Platforms
- [Veldrid](platforms/veldrid)
- [ASP.NET Blazor](platforms/blazor)

## What is Device Sets?
Device set is a set of all devices that allows for easier usage of the library.

_It's still possible to only use one type of device on it's own._

Device sets are created by each platform to collect all the types of devices,
which make it easy to handle multiple types of platform without writing a lot of code.
To handle the device sets in an abstract way there is IDeviceSet which is a shared
interface between them.
