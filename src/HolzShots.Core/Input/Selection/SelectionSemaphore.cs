namespace HolzShots.Input.Selection
{
    /// <summary> This isn't a semaphore and not even a thread-safe. We just use this for assertions.</summary>
    public static class SelectionSemaphore
    {
        public static bool IsInAreaSelection { get; set; }
    }
}
