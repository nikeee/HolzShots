namespace HolzShots;

internal static class BoolEx
{
    /// <summary>
    /// Helper function that returns "a -> b".
    /// See: https://en.wikipedia.org/wiki/Modus_ponens#Justification_via_truth_table
    /// </summary>
    public static bool Implies(this bool p, bool q) => !p || q;
}
