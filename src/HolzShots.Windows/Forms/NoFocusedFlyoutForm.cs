using System.Windows.Forms;
using HolzShots.Native;

namespace HolzShots.Windows.Forms
{
    public class NoFocusedFlyoutForm : FlyoutForm
    {
        protected override bool ShowWithoutActivation => true;

        /// <summary>
        /// We don't want this form to be focused when shown. This disrupts the user experience.
        /// Ref: https://stackoverflow.com/a/62739625
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= (int)ExtendedWindowStyleFlags.WS_EX_NOACTIVATE;
                return cp;
            }
        }
    }
}
