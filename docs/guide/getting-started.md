# Getting Started
For each platform there are a few different steps that have to be taken.
It's better to follow the instructions under the "Platform" category,
it can be found in the sidebar.

## Device Sets
Device set is a set of all devices that allows for easier usage of the library.

_It's still possible to only use one type of device on it's own._

Device sets are created by each platform to collect all the types of devices,
which make it easy to handle multiple types of platform without writing a lot of code.
To handle the device sets in an abstract way there is IDeviceSet which is a shared
interface between them.
