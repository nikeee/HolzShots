using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace HolzShots.Windows.Forms.Controls
{
    public class StackPanel : Panel
    {
        public StackPanel() => base.AutoScroll = true;

        [DefaultValue(true)]
        public new bool AutoScroll
        {
            get => base.AutoScroll;
            set => base.AutoScroll = value;
        }

        public override LayoutEngine LayoutEngine => StackLayout.Instance;

        private class StackLayout : LayoutEngine
        {
            internal static readonly StackLayout Instance = new StackLayout();

            public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
            {
                StackPanel stackPanel = container as StackPanel;
                if (stackPanel == null)
                    return false;

                // Use DisplayRectangle so that parent.Padding is honored.
                var displayRectangle = stackPanel.DisplayRectangle;
                var nextControlLocation = displayRectangle.Location;

                foreach (Control control in stackPanel.Controls)
                {
                    // Only apply layout to visible controls
                    if (control.Visible == false)
                        continue;

                    // Respect the margin of the control: shift over the left and the top.
                    nextControlLocation.Offset(control.Margin.Left, control.Margin.Top);

                    // Adjust control's Location and Size
                    control.Location = nextControlLocation;

                    var size = control.GetPreferredSize(displayRectangle.Size);
                    if (!control.AutoSize)
                        size.Width = displayRectangle.Width - control.Margin.Left - control.Margin.Right;
                    control.Size = size;

                    // Move X back to the display rectangle origin.
                    nextControlLocation.X = displayRectangle.X;

                    // Increment Y by the height of the control and the bottom margin.
                    nextControlLocation.Y += control.Height + control.Margin.Bottom;
                }

                // Adjust width of control to accomodate vertical scrollbar if necessary
                if (stackPanel.AutoScroll && nextControlLocation.Y > displayRectangle.Height)
                {
                    displayRectangle.Width -= SystemInformation.VerticalScrollBarWidth;
                    foreach (Control control in stackPanel.Controls)
                    {
                        if (control.Visible)
                        {
                            var size = control.GetPreferredSize(displayRectangle.Size);
                            if (!control.AutoSize)
                                size.Width = displayRectangle.Width - control.Margin.Left - control.Margin.Right;
                            control.Size = size;
                        }
                    }
                }

                return false;
            }
        }
    }
}
