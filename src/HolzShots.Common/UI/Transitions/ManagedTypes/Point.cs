using System;

namespace HolzShots.Common.UI.Transitions.ManagedTypes
{
    /// <summary>
    /// Class that manages transitions for Point properties.
    /// </summary>
    internal class Point : IManagedType
	{
		#region IManagedType Members

		/// <summary>
		/// Returns the type we are managing.
		/// </summary>
		public Type GetManagedType()
		{
            return typeof(System.Drawing.Point);
		}

		/// <summary>
        /// Returns a copy of the point object passed in.
		/// </summary>
		public object Copy(object o)
		{
            var c = (System.Drawing.Point)o;
            return new System.Drawing.Point(c.X, c.Y);
		}

		/// <summary>
		/// Creates an intermediate value for the points depending on the percentage passed in.
		/// </summary>
		public object GetIntermediateValue(object start, object end, double dPercentage)
		{
            var startPoint = (System.Drawing.Point)start;
            var endPoint = (System.Drawing.Point)end;

            int newX = Utility.Interpolate(startPoint.X, endPoint.X, dPercentage);
            int newY = Utility.Interpolate(startPoint.Y, endPoint.Y, dPercentage);

            return new System.Drawing.Point(newX, newY);
		}

		#endregion
	}
}
