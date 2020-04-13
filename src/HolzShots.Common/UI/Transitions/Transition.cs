using HolzShots.UI.Transitions.ManagedTypes;
using HolzShots.UI.Transitions.TransitionTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Double = HolzShots.UI.Transitions.ManagedTypes.Double;
using Int32 = HolzShots.UI.Transitions.ManagedTypes.Int32;
using String = HolzShots.UI.Transitions.ManagedTypes.String;

namespace HolzShots.UI.Transitions
{
    /// <summary>
    /// Lets you perform animated transitions of properties on arbitrary objects. These 
    /// will often be transitions of UI properties, for example an animated fade-in of 
    /// a UI object, or an animated move of a UI object from one position to another.
    /// 
    /// Each transition can simulataneously change multiple properties, including properties
    /// across multiple objects.
    /// 
    /// Example transition
    /// ------------------
    /// a.      Transition t = new Transition(new TransitionMethods.Linear(500));
    /// b.      t.Add(form1, "Width", 500);
    /// c.      t.Add(form1, "BackColor", Color.Red);
    /// d.      t.Run();
    ///   
    /// Line a:         Creates a new transition. You specify the transition method.
    ///                 
    /// Lines b. and c: Set the destination values of the properties you are animating.
    /// 
    /// Line d:         Starts the transition.
    /// 
    /// Transition methods
    /// ------------------
    /// TransitionMethod objects specify how the transition is made. Examples include
    /// linear transition, ease-in-ease-out and so on. Different transition methods may
    /// need different parameters.
    /// 
    /// </summary>
    public class Transition
    {
        #region Registration

        /// <summary>
        /// You should register all managed-types here.
        /// </summary>
        static Transition()
        {
            RegisterType<int>(new Int32());
            RegisterType<float>(new Float());
            RegisterType<double>(new Double());
            RegisterType<string>(new String());
            RegisterType<System.Drawing.Color>(new Color());
            RegisterType<System.Drawing.Rectangle>(new Rectangle());
            RegisterType<System.Drawing.Point>(new Point());
        }

        #endregion

        #region Events

        /// <summary>
        /// Event raised when the transition hass completed.
        /// </summary>
        public event EventHandler<TransitionCompletedEventArgs> TransitionCompletedEvent;

        #endregion

        #region Public static methods

        /// <summary>
        /// Creates and immediately runs a transition on the property passed in.
        /// </summary>
        public static void Run<T>(object target, string propertyName, T destinationValue, ITransitionType transitionMethod)
        {
            var t = new Transition(transitionMethod);
            t.Add(target, propertyName, destinationValue);
            t.Run();
        }

        /// <summary>
        /// Sets the property passed in to the initial value passed in, then creates and 
        /// immediately runs a transition on it.
        /// </summary>
        public static void Run<T>(object target, string propertyName, T initialValue, T destinationValue, ITransitionType transitionMethod)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            Utility.SetValue(target, propertyName, initialValue);
            Run(target, propertyName, destinationValue, transitionMethod);
        }

        /// <summary>
        /// Creates a TransitionChain and runs it.
        /// </summary>
        public static void RunChain(params Transition[] transitions)
        {
            var c = new TransitionChain(transitions);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Constructor. You pass in the object that holds the properties 
        /// that you are performing transitions on.
        /// </summary>
        public Transition(ITransitionType transitionMethod)
        {
            _transitionMethod = transitionMethod;
        }

        /// <summary>
        /// Adds a property that should be animated as part of this transition.
        /// </summary>
        public void Add<T>(object target, string propertyName, T destinationValue)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            // We get the property info...
            Type targetType = target.GetType();
            PropertyInfo propertyInfo = targetType.GetProperty(propertyName);
            if (propertyInfo == null)
                throw new ArgumentException("Object: " + target + " does not have the property: " + propertyName);

            // We check that we support the property type...
            Type propertyType = propertyInfo.PropertyType;
            if (MapManagedTypes.ContainsKey(propertyType) == false)
                throw new NotSupportedException("Transition does not handle properties of type: " + propertyType);

            // We can only transition properties that are both getable and setable...
            if (propertyInfo.CanRead == false || propertyInfo.CanWrite == false)
                throw new NotSupportedException("Property is not both getable and setable: " + propertyName);

            IManagedType managedType = MapManagedTypes[propertyType];

            // We can manage this type, so we store the information for the
            // transition of this property...
            var info = new TransitionedPropertyInfo
                       {
                           EndValue = destinationValue,
                           Target = target,
                           PropertyInfo = propertyInfo,
                           ManagedType = managedType
                       };

            lock (_lock)
            {
                _listTransitionedProperties.Add(info);
            }
        }

        /// <summary>
        /// Starts the transition.
        /// </summary>
        public void Run()
        {
            // We find the current start values for the properties we 
            // are animating...
            foreach (var info in _listTransitionedProperties)
            {
                var value = info.PropertyInfo.GetValue(info.Target, null);
                info.StartValue = info.ManagedType.Copy(value);
            }

            // We start the stopwatch. We use this when the timer ticks to measure 
            // how long the transition has been runnning for...
            _stopwatch.Reset();
            _stopwatch.Start();

            // We register this transition with the transition manager...
            TransitionManager.GetInstance().Register(this);
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Property that returns a list of information about each property managed
        /// by this transition.
        /// </summary>
        internal IList<TransitionedPropertyInfo> TransitionedProperties { get { return _listTransitionedProperties; } }

        /// <summary>
        /// We remove the property with the info passed in from the transition.
        /// </summary>
        internal void RemoveProperty(TransitionedPropertyInfo info)
        {
            lock (_lock)
            {
                _listTransitionedProperties.Remove(info);
            }
        }

        /// <summary>
        /// Called when the transition timer ticks.
        /// </summary>
        internal void OnTimer()
        {
            // When the timer ticks we:
            // a. Find the elapsed time since the transition started.
            // b. Work out the percentage movement for the properties we're managing.
            // c. Find the actual values of each property, and set them.

            // a.
            int iElapsedTime = (int)_stopwatch.ElapsedMilliseconds;

            // b.
            double dPercentage;
            bool bCompleted = _transitionMethod.OnTimer(iElapsedTime, out dPercentage);

            // We take a copy of the list of properties we are transitioning, as
            // they can be changed by another thread while this method is running...
            IList<TransitionedPropertyInfo> listTransitionedProperties = new List<TransitionedPropertyInfo>();
            lock (_lock)
            {
                foreach (var info in _listTransitionedProperties)
                {
                    listTransitionedProperties.Add(info.Copy());
                }
            }

            // c. 
            foreach (TransitionedPropertyInfo info in listTransitionedProperties)
            {
                // We get the current value for this property...
                var value = info.ManagedType.GetIntermediateValue(info.StartValue, info.EndValue, dPercentage);

                // We set it...
                var args = new PropertyUpdateArgs(info.Target, info.PropertyInfo, value);
                SetProperty(this, args);
            }

            // Has the transition completed?
            if (bCompleted)
            {
                // We stop the stopwatch and the timer...
                _stopwatch.Stop();

                // We raise an event to notify any observers that the transition has completed...
                Utility.RaiseEvent(TransitionCompletedEvent, this, new TransitionCompletedEventArgs());
            }
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Sets a property on the object passed in to the value passed in. This method
        /// invokes itself on the GUI thread if the property is being invoked on a GUI 
        /// object.
        /// </summary>
        private void SetProperty(object sender, PropertyUpdateArgs args)
        {
            try
            {
                // If the target is a control that has been disposed then we don't 
                // try to update its proeprties. This can happen if the control is
                // on a form that has been closed while the transition is running...
                if (IsDisposed(args.Target))
                    return;

                var invokeTarget = args.Target as ISynchronizeInvoke;
                if (invokeTarget != null && invokeTarget.InvokeRequired)
                {
                    // There is some history behind the next two lines, which is worth
                    // going through to understand why they are the way they are.

                    // Initially we used BeginInvoke without the subsequent WaitOne for
                    // the result. A transition could involve a large number of updates
                    // to a property, and as this call was asynchronous it would send a 
                    // large number of updates to the UI thread. These would queue up at
                    // the GUI thread and mean that the UI could be some way behind where
                    // the transition was.

                    // The line was then changed to the blocking Invoke call instead. This 
                    // meant that the transition only proceded at the pace that the GUI 
                    // could process it, and the UI was not overloaded with "old" updates.

                    // However, in some circumstances Invoke could block and lock up the
                    // Transitions background thread. In particular, this can happen if the
                    // control that we are trying to update is in the process of being 
                    // disposed - for example, it is on a form that is being closed. See
                    // here for details: 
                    // http://social.msdn.microsoft.com/Forums/en-US/winforms/thread/7d2c941a-0016-431a-abba-67c5d5dac6a5

                    // To solve this, we use a combination of the two earlier approaches. 
                    // We use BeginInvoke as this does not block and lock up, even if the
                    // underlying object is being disposed. But we do want to wait to give
                    // the UI a chance to process the update. So what we do is to do the
                    // asynchronous BeginInvoke, but then wait (with a short timeout) for
                    // it to complete.
                    IAsyncResult asyncResult = invokeTarget.BeginInvoke(new EventHandler<PropertyUpdateArgs>(SetProperty), new[] { sender, args });
                    asyncResult.AsyncWaitHandle.WaitOne(50);
                }
                else
                {
                    // We are on the correct thread, so we update the property...
                    args.PropertyInfo.SetValue(args.Target, args.Value, null);
                }
            }
            catch (Exception)
            {
                // We silently catch any exceptions. These could be things like 
                // bounds exceptions when setting properties.
            }
        }

        /// <summary>
        /// Returns true if the object passed in is a Control and is disposed
        /// or in the process of disposing. (If this is the case, we don't want
        /// to make any changes to its properties.)
        /// </summary>
        private bool IsDisposed(object target)
        {
            // Is the object passed in a Control?
            var controlTarget = target as Control;
            if (controlTarget == null)
            {
                return false;
            }

            // Is it disposed or disposing?
            return controlTarget.IsDisposed || controlTarget.Disposing;
        }

        #endregion

        #region Private static functions

        /// <summary>
        /// Registers a transition-type. We hold them in a map.
        /// </summary>
        private static void RegisterType<T>(IManagedType transitionType)
        {
            MapManagedTypes[typeof(T)] = transitionType;
        }

        #endregion

        #region Private static data

        // A map of Type info to IManagedType objects. These are all the types that we
        // know how to perform transactions on...
        private static readonly IDictionary<Type, IManagedType> MapManagedTypes = new Dictionary<Type, IManagedType>();

        #endregion

        #region Private data

        // The transition method used by this transition...
        private readonly ITransitionType _transitionMethod;

        // Holds information about one property on one taregt object that we are performing
        // a transition on...
        internal class TransitionedPropertyInfo
        {
            public object StartValue;
            public object EndValue;
            public object Target;
            public PropertyInfo PropertyInfo;
            public IManagedType ManagedType;

            public TransitionedPropertyInfo Copy()
            {
                var info = new TransitionedPropertyInfo
                           {
                               StartValue = StartValue,
                               EndValue = EndValue,
                               Target = Target,
                               PropertyInfo = PropertyInfo,
                               ManagedType = ManagedType
                           };
                return info;
            }
        }

        // The collection of properties that the current transition is animating...
        private readonly IList<TransitionedPropertyInfo> _listTransitionedProperties = new List<TransitionedPropertyInfo>();

        // Helps us find the time interval from the time the transition starts to each timer tick...
        private readonly Stopwatch _stopwatch = new Stopwatch();

        // Event args used for the event we raise when updating a property...
        private class PropertyUpdateArgs : EventArgs
        {
            public PropertyUpdateArgs(object t, PropertyInfo pi, object v)
            {
                Target = t;
                PropertyInfo = pi;
                Value = v;
            }
            public readonly object Target;
            public readonly PropertyInfo PropertyInfo;
            public readonly object Value;
        }

        // An object used to lock the list of transitioned properties, as it can be 
        // accessed by multiple threads...
        private readonly object _lock = new object();

        #endregion
    }

    /// <summary>
    /// Args passed with the TransitionCompletedEvent.
    /// </summary>
    public class TransitionCompletedEventArgs : EventArgs
    {
    }
}
