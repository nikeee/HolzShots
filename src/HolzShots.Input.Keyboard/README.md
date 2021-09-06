# `HolzShots.Input.Keyboard`
> Global Hotkey library used in [HolzShots](https://github.com/nikeee/HolzShots).


Usage of `Hotkey` primitives:
```csharp
var keyboardHook = KeyboardHookSelector.CreateHookForCurrentPlatform(someForm);
// someForm can be anything that implements ISynchronizeInvoke

var hk = new Hotkey(Input.Keyboard.ModifierKeys.Shift, Keys.F8);
hk.KeyPressed += (hook, h) => Console.WriteLine($"Hotkey pressed: {h}");

_keyboardHook.RegisterHotkey(hk);

var hk2 = Hotkey.Parse("Shift+F9");
hk2.KeyPressed += (hook, h) => Console.WriteLine($"Hotkey pressed: {h}");

_keyboardHook.RegisterHotkey(hk2);

// Clean up:
_keyboardHook.UnregisterHotkey(hk2);

_keyboardHook.UnregisterAllHotkeys();
```

The `Hotkey` class can be serialized to Windows Forms Settings as well as re-instantiated using `Hotkey.Parse(hk.ToString())`.
You may also implement a JSON converter to parse hotkey strings from JSON config files.
