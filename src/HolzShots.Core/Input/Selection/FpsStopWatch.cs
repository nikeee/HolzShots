using System;
using System.Diagnostics;

namespace HolzShots.Input.Selection
{
    public class FpsStopWatch : IDisposable
    {
        private int _frameCount = 0;
        private Stopwatch _fpsTimer = new Stopwatch();
        public int FramesPerSecond { get; private set; } = 0;

        public void Start() => _fpsTimer.Start();
        public void Stop() => _fpsTimer.Stop();
        public void Update()
        {
            ++_frameCount;

            if (_fpsTimer.ElapsedMilliseconds >= 1000)
            {
                FramesPerSecond = (int)(_frameCount / _fpsTimer.Elapsed.TotalSeconds);
                _frameCount = 0;
                _fpsTimer.Reset();
                _fpsTimer.Start();
            }
        }
        public void Dispose() => _fpsTimer.Stop();
    }
}
