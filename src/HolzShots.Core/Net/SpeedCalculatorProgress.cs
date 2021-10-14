using System.Diagnostics;

namespace HolzShots.Net
{
    // May move to other namespace?
    public class SpeedCalculatorProgress : Progress<TransferProgress>
    {
        private readonly object _lockObj = new object();

        private DateTime _start;
        private DateTime _end;
        private TimeSpan _eta;
        private Speed<MemSize> _speed;
        private bool _reportingEnabled;


        public TimeSpan ETA => _eta;
        public Speed<MemSize> CurrentSpeed => _speed;
        public bool HasStarted => _reportingEnabled;

        public void Start()
        {
            lock (_lockObj)
            {
                _start = DateTime.Now;
                _reportingEnabled = true;
            }
        }

        public void Stop()
        {
            lock (_lockObj)
            {
                _end = DateTime.Now;
                _reportingEnabled = false;
            }
        }

        protected override void OnReport(TransferProgress value)
        {
            base.OnReport(value);

            if (!_reportingEnabled)
                return;
            var now = DateTime.Now;
            var elapsedSeconds = (now - _start).TotalSeconds;

            // https://stackoverflow.com/a/4262301
            // Division by zero using floats does not throw.
            Debug.Assert(elapsedSeconds != 0.0);

            var bytesPerSecond = value.Current.ByteCount / elapsedSeconds;
            Debug.Assert(bytesPerSecond != 0.0);

            var secondsRemaining = (value.Total.ByteCount - value.Current.ByteCount) / bytesPerSecond;

            // Better check for infinity/NaN
            if (!double.IsNaN(secondsRemaining) && !double.IsInfinity(secondsRemaining))
                _eta = TimeSpan.FromSeconds(secondsRemaining);
            if (!double.IsNaN(bytesPerSecond) && !double.IsInfinity(bytesPerSecond))
                _speed = new Speed<MemSize>(new MemSize((long)bytesPerSecond));
        }
    }
}
