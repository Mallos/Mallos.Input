# [Veldrid](https://github.com/mellinoe/veldrid)
Veldrid is a low-level, portable graphics library for .NET.

## Samples
A sample can be found [here](https://github.com/Mallos/Mallos.Input/tree/cd01ff333c857f4ba2a9af7a6255fe65cf85c2db/samples/veldrid).

## Installing

```cs
class Game : BaseGame
{
    private readonly VeldridDeviceSet deviceSet;

    public Game()
    {
        // Create the device set for veldrid
        this.deviceSet = new VeldridDeviceSet();
    }

    protected override void Draw(Veldrid.CommandList cl)
    {
        float elapsedTime = 1f / 60;
        
        // Pull the input snapshot from Veldrid
        var lastInputSnapshot = this.Window.PumpEvents();

        // Pass the input snapshot to the device set
        this.deviceSet.UpdateSnapshot(lastInputSnapshot);
        this.deviceSet.Update(elapsedTime);
    }
}
```
