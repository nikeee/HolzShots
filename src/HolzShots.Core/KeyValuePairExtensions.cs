namespace HolzShots.Net.Custom
{
    static class KeyValuePairExtensions
    {
        public static void Deconstruct<T, K>(this KeyValuePair<T, K> kv, out T key, out K value)
        {
            key = kv.Key;
            value = kv.Value;
        }
    }

}
