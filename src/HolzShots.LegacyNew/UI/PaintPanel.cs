using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using HolzShots.Drawing.Tools;
using HolzShots.Drawing;

namespace HolzShots.UI
{
    public partial class PaintPanel : UserControl
    {

        public event EventHandler Initialized;
        public event EventHandler<Point> UpdateMousePosition;

        public PaintPanel()
        {
            InitializeComponent();
            CurrentToolChanged += CurrentToolChanged_Event;
        }

        private ShotEditorTool _currentTool;
        private Screenshot _screenshot;

        private bool _drawCursor;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DrawCursor
        {
            get
            {
                return _drawCursor;
            }
            set
            {
                _drawCursor = value;
                RawBox.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Screenshot Screenshot
        {
            get
            {
                return DesignMode ? null : _screenshot;
            }
            set
            {
                if (!DesignMode)
                    _screenshot = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image CurrentImage
        {
            get
            {
                return DesignMode ? new Bitmap(1, 1) : _undoStack.Count > 0 ? _undoStack.Peek() : Screenshot.Image;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image CombinedImage
        {
            get
            {
                if (!DesignMode)
                {
                    Image bmp = RawBox.Image;
                    if (_drawCursor)
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.CompositingQuality = CompositingQuality.HighQuality;
                            g.DrawImage(Properties.Resources.windowsCursorMedium, Screenshot.CursorPosition.OnImage);
                        }
                    }
                    return bmp;
                }
                return null/* TODO Change to default(_) if this is not a reference type */;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ShotEditorTool CurrentTool
        {
            get
            {
                return _currentTool;
            }
            set
            {
                _currentTool = value;
                switch (value)
                {
                    case ShotEditorTool.None:
                        Cursor = System.Windows.Forms.Cursors.Default;
                        break;
                    case ShotEditorTool.Crop:

                        CurrentToolObject = new Crop();
                        break;
                    case ShotEditorTool.Marker:

                        CurrentToolObject = new Marker();
                        break;
                    case ShotEditorTool.Censor:

                        CurrentToolObject = new Censor();
                        break;
                    case ShotEditorTool.Eraser:

                        CurrentToolObject = new Eraser();
                        break;
                    case ShotEditorTool.Blur:

                        CurrentToolObject = new Blur();
                        break;
                    case ShotEditorTool.Ellipse:

                        CurrentToolObject = new Ellipse();
                        break;
                    case ShotEditorTool.Eyedropper:

                        CurrentToolObject = new Eyedropper();
                        break;
                    case ShotEditorTool.Brighten:

                        CurrentToolObject = new Brighten();
                        break;
                    case ShotEditorTool.Arrow:

                        CurrentToolObject = new Arrow();
                        break;
                    default:

                        CurrentToolObject = null/* TODO Change to default(_) if this is not a reference type */;
                        break;
                }
            }
        }


        private event EventHandler<ITool<ToolSettingsBase>> CurrentToolChanged;

        private ITool<ToolSettingsBase> _currentToolObject;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ITool<ToolSettingsBase> CurrentToolObject
        {
            get
            {
                return _currentToolObject;
            }
            set
            {
                if (value != _currentToolObject)
                {
                    _currentToolObject = value;
                    CurrentToolChanged?.Invoke(this, _currentToolObject);
                }
            }
        }

        private void CurrentToolChanged_Event(object? sender, ITool<ToolSettingsBase> tool)
        {
            if (tool != null)
                Cursor = tool.Cursor;
        }


        public void Initialize(Screenshot shot)
        {
            _screenshot = shot;

            RawBox.BringToFront();
            RawBox.SizeMode = PictureBoxSizeMode.AutoSize;
            RawBox.Location = new Point(0, 0);

            UpdateRawBox();

            RawBox.Focus();

            Initialized?.Invoke(this, new EventArgs());
        }

        private void InvokeFinalRender(ITool<ToolSettingsBase> tool)
        {
            var img = (Image)CurrentImage.Clone();
            Debug.Assert(tool != null);
            Debug.Assert(img != null);
            if (img == null)
                return;

            // Dim oldImageRef = img
            tool.RenderFinalImage(ref img);
            // If ReferenceEquals(oldImageRef, img) Then
            // RawBox.Image = oldImageRef
            // End If

            _undoStack.Push(img);
            UpdateRawBox();
        }

        public void RunDialogTool(IDialogTool tool)
        {
            var img = (Image)CurrentImage.Clone();
            if (img == null)
                return;

            tool.ShowToolDialog(ref img, Screenshot, this);
            _undoStack.Push(img);
            UpdateRawBox();
        }

        private void UpdateRawBox()
        {
            RawBox.Image = CurrentImage;
            RawBox.Invalidate();
        }

        private static class Localization
        {
            public const string SizeInfo = "{0}x{1}px, creation date: {2}";
            public const string SizeInfoText = "The image is {0}-by-{1}px. Left click to copy the image size. Right click to copy creation date. Middle click to copy both.";
        }

        public string SizeInfo => string.Format(Localization.SizeInfo, _screenshot.Size.Width, _screenshot.Size.Height, _screenshot.Timestamp);

        public string SizeInfoText => string.Format(Localization.SizeInfoText, _screenshot.Size.Width, _screenshot.Size.Height);


        private void DrawBoxMouseClick(object sender, MouseEventArgs e)
        {
            var cursor = Cursor;
            CurrentToolObject?.MouseClicked(CurrentImage, e.Location.ToVector2(), ref cursor, this);
            RawBox.Invalidate();
        }

        private void MouseLayerMouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            if (_currentTool == ShotEditorTool.None)
                return;

            var startPosition = e.Location.ToVector2();
            CurrentToolObject.BeginCoordinates = startPosition;
            CurrentToolObject.EndCoordinates = startPosition;
            RawBox.Invalidate();
        }

        private void RawBoxMouseEnter(object sender, EventArgs e)
        {
            if (!RawBox.Focused)
                RawBox.Focus();
        }

        private void MouseLayerMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CurrentToolObject != null)
                {
                    CurrentToolObject.EndCoordinates = e.Location.ToVector2();
                    RawBox.Invalidate();
                }
            }

            SaveLinealStuff(e);

            if (!RawBox.Focused)
                RawBox.Focus();
        }

        private void MouseLayerMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (CurrentToolObject is not Eyedropper)
                InvokeFinalRender(CurrentToolObject);
            else
            {
                var img = CurrentImage;
                if (img == null)
                    return;

                CurrentToolObject.RenderFinalImage(ref img);
            }
            _mouseDown = false;
        }

        private bool _mouseDown = false;

        private void RawBoxPaint(object sender, PaintEventArgs e)
        {
            if (_mouseDown == true)
            {
                if (_currentToolObject != null)
                {
                    CurrentToolObject.RenderPreview((Bitmap)RawBox.Image, e.Graphics);
                    return;
                }
            }
            if (_drawCursor && Screenshot != null)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            }
        }


        private readonly Stack<Image> _undoStack = new();

        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                _undoStack.Pop();
                UpdateRawBox();
                GC.Collect();
            }
        }

        private void PaintPanelDisposed(object sender, EventArgs e)
        {
            foreach (var i in _undoStack)
            {
                try
                {
                    i.Dispose();
                }
                catch (Exception ex)
                {
                    Debugger.Break();
                    Debug.Fail("Failed to dispose undoStack");
                }
            }
        }

        private void PaintPanelLoad(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(207, 217, 231);
            RawBox.Focus();
        }


        private void SaveLinealStuff(MouseEventArgs e)
        {
            var hRect = new Rectangle(_currentMousePosition.X < e.X ? _currentMousePosition.X : e.X - 1, 0, Math.Abs(_currentMousePosition.X - e.X) + 2, HorizontalLinealBox.DisplayRectangle.Height);
            var vRect = new Rectangle(0, _currentMousePosition.Y < e.Y ? _currentMousePosition.Y : e.Y - 1, VerticalLinealBox.DisplayRectangle.Width, Math.Abs(_currentMousePosition.Y - e.Y) + 2);

            _currentMousePosition = e.Location;

            HorizontalLinealBox.Invalidate(hRect, false);
            VerticalLinealBox.Invalidate(vRect, false);

            UpdateMousePosition?.Invoke(this, e.Location);

            if (CurrentTool == ShotEditorTool.Eyedropper)
            {
                var cursor = Cursor;
                CurrentToolObject.MouseOnlyMoved(CurrentImage, ref cursor, e);
            }
        }

        private Point _currentMousePosition;
        private static readonly Font RulerFont = new Font("Verdana", 8, FontStyle.Regular);
        private static readonly SolidBrush FontBrush = new SolidBrush(Color.FromArgb(255, 51, 75, 106)); // (255, 51, 75, 106))
        private static readonly SolidBrush LinearBackgroundBrush = new SolidBrush(Color.FromArgb(255, 241, 243, 248)); // (255, 240, 241, 249))
        private static readonly Pen LinePen = new Pen(Color.FromArgb(255, 142, 156, 175)); // (255, 137, 146, 179))

        private void HorizontalLinealBoxPaint(object sender, PaintEventArgs e)
        {
            {
                var withBlock = e.Graphics;
                withBlock.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                withBlock.SmoothingMode = SmoothingMode.HighSpeed;
                withBlock.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                withBlock.CompositingQuality = CompositingQuality.HighSpeed;

                withBlock.FillRectangle(LinearBackgroundBrush, HorizontalLinealBox.DisplayRectangle);
                withBlock.DrawLine(LinePen, 0, HorizontalLinealBox.DisplayRectangle.Height - 2, HorizontalLinealBox.DisplayRectangle.Width - 1, HorizontalLinealBox.DisplayRectangle.Height - 2);

                int xPos;
                var offset = WholePanel.HorizontalScroll.Value;

                for (int i = 0; i <= WholePanel.Width + offset; i += 10)
                {
                    xPos = i - offset;
                    if (xPos < 0 || xPos > WholePanel.Width + offset)
                        continue;

                    if (i % 100 == 0)
                    {
                        withBlock.DrawLine(LinePen, xPos, 0, xPos, HorizontalLinealBox.Height - 2);
                        withBlock.DrawString(i.ToString(), RulerFont, FontBrush, xPos, 0);
                    }
                    else
                        withBlock.DrawLine(LinePen, xPos, HorizontalLinealBox.Height - 6, xPos, HorizontalLinealBox.Height - 2);
                }

                withBlock.DrawLine(Pens.Red, _currentMousePosition.X - offset, 0, _currentMousePosition.X - offset, 20);
                withBlock.DrawLine(BorderLinePen, 0, 0, Width, 0);
            }
        }
        private void VerticalLinealBoxPaint(object sender, PaintEventArgs e)
        {
            {
                var withBlock = e.Graphics;
                withBlock.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                withBlock.SmoothingMode = SmoothingMode.HighSpeed;
                withBlock.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                withBlock.CompositingQuality = CompositingQuality.HighSpeed;

                withBlock.FillRectangle(LinearBackgroundBrush, VerticalLinealBox.DisplayRectangle);
                withBlock.DrawLine(LinePen, VerticalLinealBox.DisplayRectangle.Width - 2, 0, VerticalLinealBox.DisplayRectangle.Width - 2, VerticalLinealBox.DisplayRectangle.Height - 1);

                int yPos;
                var offset = WholePanel.VerticalScroll.Value;

                for (int i = 0; i <= WholePanel.Height + offset; i += 10)
                {
                    yPos = i - offset;
                    if (yPos < 0 || yPos > WholePanel.Height + offset)
                        continue;

                    if (i % 100 == 0)
                    {
                        withBlock.DrawLine(LinePen, 0, yPos, VerticalLinealBox.Width - 2, yPos);
                        withBlock.TranslateTransform(0, yPos);
                        withBlock.RotateTransform(-90);
                        withBlock.DrawString(i.ToString(UIConfig.Culture), RulerFont, FontBrush, -withBlock.MeasureString(i.ToString(UIConfig.Culture), RulerFont, 100).Width, 0);
                        withBlock.ResetTransform();
                    }
                    else
                        withBlock.DrawLine(LinePen, VerticalLinealBox.Width - 6, yPos, VerticalLinealBox.Width - 2, yPos);
                }

                withBlock.DrawLine(Pens.Red, 0, _currentMousePosition.Y - offset, 20, _currentMousePosition.Y - offset);
            }
        }


        private void WholePanelScroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                HorizontalLinealBox.Invalidate();
            else
                VerticalLinealBox.Invalidate();
        }

        private void EckenTeilPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(LinePen, EckenTeil.Width - 2, EckenTeil.Height - 2, 2, 2);
            e.Graphics.DrawLine(BorderLinePen, 0, 0, Width, 0);
        }

        private static readonly Pen BorderLinePen = new(Color.FromArgb(218, 219, 220));
    }
}
