using System;

namespace HolzShots.Net
{
    public readonly struct Speed<T> : IEquatable<Speed<T>> where T : struct
    {
        public T ItemsPerSecond { get; }

        public Speed(T itemsPerSecond) => ItemsPerSecond = itemsPerSecond;

        public override string ToString() => ItemsPerSecond.ToString() + "/s";

        public override bool Equals(object? obj) => obj is Speed<T> s && (s == this);
        public bool Equals(Speed<T> other) => other == this;
        public override int GetHashCode() => ItemsPerSecond.GetHashCode();
        public static bool operator ==(Speed<T> left, Speed<T> right) => left.ItemsPerSecond.Equals(right.ItemsPerSecond);
        public static bool operator !=(Speed<T> left, Speed<T> right) => !(left == right);
    }
}
