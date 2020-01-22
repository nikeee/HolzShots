using System;

namespace HolzShots.UI.Transitions.TransitionTypes
{
    /// <summary>
    /// This class manages a linear transition. The percentage complete for the transition
    /// increases linearly with time.
    /// </summary>
    public class Linear : ITransitionType
    {
        #region Public methods

        /// <summary>
        /// Constructor. You pass in the time (in milliseconds) that the
        /// transition will take.
        /// </summary>
        public Linear(int transitionTime)
        {
			if (transitionTime <= 0)
                throw new ArgumentException("Transition time must be greater than zero.");
			_transitionTime = transitionTime;
        }

        #endregion

		#region ITransitionMethod Members

		/// <summary>
		/// We return the percentage completed.
		/// </summary>
        public bool OnTimer(int time, out double percentage)
		{
			percentage = time / _transitionTime;
			if (percentage >= 1.0)
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
