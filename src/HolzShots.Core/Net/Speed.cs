
namespace HolzShots.Net
{
    public struct Speed<T> where T : struct
    {
        public T ItemsPerSecond { get; }

        public Speed(T itemsPerSecond) => ItemsPerSecond = itemsPerSecond;

        public override string ToString() => ItemsPerSecond.ToString() + "/s";
    }
}
