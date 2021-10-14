using System.Collections.Immutable;

namespace HolzShots.Input.Keyboard
{
    public abstract class HotkeyActionCollection
    {
        protected KeyboardHook Hook { get; }
        protected ImmutableArray<IHotkeyAction> Actions { get; }
        public int Count => Actions.Length;

        public HotkeyActionCollection(KeyboardHook hook, params IHotkeyAction[] actions)
        {
            Hook = hook ?? throw new ArgumentNullException(nameof(hook));
            Actions = actions?.ToImmutableArray() ?? throw new ArgumentNullException(nameof(actions));
        }

        public HotkeyActionCollection(KeyboardHook hook) => Hook = hook ?? throw new ArgumentNullException(nameof(hook));
        public abstract void Refresh();
    }
}
