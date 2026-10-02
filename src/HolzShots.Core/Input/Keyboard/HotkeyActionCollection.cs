using System.Collections.Immutable;

namespace HolzShots.Input.Keyboard;

public abstract class HotkeyActionCollection
{
    protected KeyboardHook Hook { get; }
    protected ImmutableArray<IHotkeyAction> Actions { get; }
    public int Count => Actions.Length;

    /// <summary>Creates a collection with the given hook and actions.</summary>
    /// <exception cref="System.ArgumentNullException"><paramref name="hook" /> or <paramref name="actions" /> is <see langword="null" />.</exception>
    public HotkeyActionCollection(KeyboardHook hook, params IHotkeyAction[] actions)
    {
        Hook = hook ?? throw new ArgumentNullException(nameof(hook));
        Actions = actions?.ToImmutableArray() ?? throw new ArgumentNullException(nameof(actions));
    }

    /// <summary>Creates a collection with the given hook.</summary>
    /// <exception cref="System.ArgumentNullException"><paramref name="hook" /> is <see langword="null" />.</exception>
    public HotkeyActionCollection(KeyboardHook hook) => Hook = hook ?? throw new ArgumentNullException(nameof(hook));
    public abstract void Refresh();
}
