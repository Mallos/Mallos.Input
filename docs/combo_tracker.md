# Combo Tracker

## Example

```cs
// You can pass any trackers or multiple trackers at once.
var comboTracker = new ComboTracker(defaultSet.KeyboardTracker);

// Register events
comboTracker.OnComboCalled += (SequenceCombo combo) =>
{
  // Combo was called
};

// Register combos
comboTracker.SequenceCombos.Add("Attack1", Keys.A, Keys.B, Keys.C);
comboTracker.SequenceCombos.Add("Attack2", Keys.A, Keys.C, Keys.B);
comboTracker.SequenceCombos.Add("Attack3", Buttons.A, Buttons.B, Buttons.X);
```

### Combo Searching

A feature useful for displaying possible combos.

```cs
comboTracker.SequenceCombos.Search(Keys.A, Keys.B);
// Result:
// [Keys.A, Keys.B, Keys.C]

comboTracker.SequenceCombos.FuzzySearch(2, Keys.A, Keys.B);
// Result:
// [Keys.A, Keys.B, Keys.C]
// [Keys.A, Keys.C, Keys.B]
```
