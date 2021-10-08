using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace HolzShots.Capture.Video.UI
{
    public partial class RecordingFrame : Form
    {
        const int FrameMarkerThickness = 6;
        const int FrameMarkerWidth = FrameMarkerThickness * 8;

        private static readonly Pen[] IndicationPens = new Pen[]
        {
            new(Color.Black, FrameMarkerThickness),
            new(Color.Red, FrameMarkerThickness),
            new(Color.White, FrameMarkerThickness),
        };

        private readonly Rectangle _indicatedRegion;
        private int CurrentPenIndex = 0;
        public RecordingFrame(Rectangle indicatedregion)
        {
            _indicatedRegion = indicatedregion;
            InitializeComponent();
            TopMost = true;
            Bounds = Rectangle.Inflate(indicatedregion, FrameMarkerThickness, FrameMarkerThickness);
        }

        public void StartIndicating(CancellationToken cancellationToken)
        {
            RecordingIndicatorTimer.Interval = 500;
            RecordingIndicatorTimer.Start();
            cancellationToken.Register(() =>
            {
                RecordingIndicatorTimer.Stop();
                Close();
            });
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            var currentPen = IndicationPens[CurrentPenIndex];
            var halfThickness = FrameMarkerThickness / 2;

            // Top left
            e.Graphics.DrawLine(
                currentPen,
                new Point(0, halfThickness),
                new Point(0 + FrameMarkerWidth, halfThickness)
            );
            e.Graphics.DrawLine(
                currentPen,
                new Point(halfThickness, 0),
                new Point(halfThickness, FrameMarkerWidth)
            );

            // Bottom left
            e.Graphics.DrawLine(
                currentPen,
                new Point(0, Bounds.Height - halfThickness),
                new Point(0 + FrameMarkerWidth, Bounds.Height - halfThickness)
            );
            e.Graphics.DrawLine(
                currentPen,
                new Point(halfThickness, Bounds.Height - FrameMarkerWidth),
                new Point(halfThickness, Bounds.Height)
            );

            // Top right
            e.Graphics.DrawLine(
                currentPen,
                new Point(Bounds.Width - FrameMarkerWidth, halfThickness),
                new Point(Bounds.Width, halfThickness)
            );
            e.Graphics.DrawLine(
                currentPen,
                new Point(Bounds.Width - halfThickness, 0),
                new Point(Bounds.Width - halfThickness, FrameMarkerWidth)
            );

            // Bottom right
            e.Graphics.DrawLine(
                currentPen,
                new Point(Bounds.Width - halfThickness, Bounds.Height - FrameMarkerWidth),
                new Point(Bounds.Width - halfThickness, Bounds.Height)
            );
            e.Graphics.DrawLine(
                currentPen,
                new Point(Bounds.Width - FrameMarkerWidth, Bounds.Height - halfThickness),
                new Point(Bounds.Width, Bounds.Height - halfThickness)
            );
        }

        private void RecordingIndicatorTimer_Tick(object sender, EventArgs e)
        {
            CurrentPenIndex = (CurrentPenIndex + 1) % IndicationPens.Length;

            Invalidate(false);
        }
    }
}
