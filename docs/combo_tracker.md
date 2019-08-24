# Combo Tracker

## Example

```cs
var comboTracker = new ComboTracker(defaultSet.KeyboardTracker);

// Register events
ComboTracker.OnComboCalled += (SequenceCombo combo) =>
{
  // Combo was called
};

// Register combos
ComboTracker.SequenceCombos.Add("Attack1", Keys.A, Keys.B, Keys.C);
ComboTracker.SequenceCombos.Add("Attack2", Keys.A, Keys.C, Keys.B);
ComboTracker.SequenceCombos.Add("Attack3", Buttons.A, Buttons.B, Buttons.X);
```
