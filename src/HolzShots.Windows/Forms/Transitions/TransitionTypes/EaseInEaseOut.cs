using System;

namespace HolzShots.UI.Transitions.TransitionTypes
{
    /// <summary>
    /// Manages an ease-in-ease-out transition. This accelerates during the first 
    /// half of the transition, and then decelerates during the second half.
    /// </summary>
    public class EaseInEaseOut : ITransitionType
	{
		#region Public methods

		/// <summary>
		/// Constructor. You pass in the time that the transition 
		/// will take (in milliseconds).
		/// </summary>
		public EaseInEaseOut(int transitionTime)
		{
			if (transitionTime <= 0)
                throw new ArgumentException("Transition time must be greater than zero.");
			_transitionTime = transitionTime;
		}

		#endregion

		#region ITransitionMethod Members

		/// <summary>
		/// Works out the percentage completed given the time passed in.
		/// This uses the formula:
		///   s = ut + 1/2at^2
		/// We accelerate as at the rate needed (a=4) to get to 0.5 at t=0.5, and
		/// then decelerate at the same rate to end up at 1.0 at t=1.0.
		/// </summary>
        public bool OnTimer(int time, out double percentage)
		{
			// We find the percentage time elapsed...
			double dElapsed = time / _transitionTime;
            percentage = Utility.ConvertLinearToEaseInEaseOut(dElapsed);

			if (dElapsed >= 1.0)
			{
                percentage = 1.0;
                return true;
            }
            return false;
		}

		#endregion

		#region Private data

		private readonly double _transitionTime;

		#endregion
	}
}
