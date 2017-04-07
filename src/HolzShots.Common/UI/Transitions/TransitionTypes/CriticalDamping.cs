using System;

namespace HolzShots.Common.UI.Transitions.TransitionTypes
{
    /// <summary>
    /// This transition animates with an exponential decay. This has a damping effect
    /// similar to the motion of a needle on an electomagnetically controlled dial.
    /// </summary>
	public class CriticalDamping : ITransitionType
	{
		#region Public methods

		/// <summary>
		/// Constructor. You pass in the time that the transition 
		/// will take (in milliseconds).
		/// </summary>
		public CriticalDamping(int transitionTime)
		{
			if (transitionTime <= 0)
				throw new ArgumentException("Transition time must be greater than zero.");
			_transitionTime = transitionTime;
		}

		#endregion

		#region ITransitionMethod Members

		public bool OnTimer(int time, out double percentage)
		{
			// We find the percentage time elapsed...
			var dElapsed = time / _transitionTime;
			percentage = (1.0 - Math.Exp(-1.0 * dElapsed * 5)) / 0.993262053;

            if (dElapsed < 1.0)
                return false;
            percentage = 1.0;
            return true;
		}

		#endregion

		#region Private data

		private readonly double _transitionTime;

		#endregion
	}
}
