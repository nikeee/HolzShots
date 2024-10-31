using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HolzShots.Drawing.Tools;
using HolzShots.Drawing;

namespace HolzShots.UI
{
    public partial class PaintPanel : UserControl
    {
        public PaintPanel()
        {
            InitializeComponent();
        }

        private ShotEditorTool _currentTool;
        private Screenshot _screenshot;

        internal event InitializedEventHandler Initialized;

        internal delegate void InitializedEventHandler();

        internal event UpdateMousePositionEventHandler UpdateMousePosition;

        internal delegate void UpdateMousePositionEventHandler(Point e);

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
                return DesignMode ? null : _undoStack.Count > 0 ? _undoStack.Peek() : Screenshot.Image;
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
                            g.DrawImage(My.Resources.windowsCursorMedium, Screenshot.CursorPosition.OnImage);
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
                    case object _ when ShotEditorTool.None:
                        {
                            Cursor = Cursors.Default;
                            break;
                        }

                    case object _ when ShotEditorTool.Text:
                        {
                            CurrentToolObject = null/* TODO Change to default(_) if this is not a reference type */;
                            Cursor = new Cursor(My.Resources.textCursor.Handle);
                            break;
                        }

                    case object _ when ShotEditorTool.Crop:
                        {
                            CurrentToolObject = new Crop();
                            break;
                        }

                    case object _ when ShotEditorTool.Marker:
                        {
                            CurrentToolObject = new Marker();
                            break;
                        }

                    case object _ when ShotEditorTool.Censor:
                        {
                            CurrentToolObject = new Censor();
                            break;
                        }

                    case object _ when ShotEditorTool.Eraser:
                        {
                            CurrentToolObject = new Eraser();
                            break;
                        }

                    case object _ when ShotEditorTool.Blur:
                        {
                            CurrentToolObject = new Blur();
                            break;
                        }

                    case object _ when ShotEditorTool.Ellipse:
                        {
                            CurrentToolObject = new Ellipse();
                            break;
                        }

                    case object _ when ShotEditorTool.Eyedropper:
                        {
                            CurrentToolObject = new Eyedropper();
                            break;
                        }

                    case object _ when ShotEditorTool.Brighten:
                        {
                            CurrentToolObject = new Brighten();
                            break;
                        }

                    case object _ when ShotEditorTool.Arrow:
                        {
                            CurrentToolObject = new Arrow();
                            break;
                        }

                    default:
                        {
                            CurrentToolObject = null/* TODO Change to default(_) if this is not a reference type */;
                            break;
                        }
                }
            }
        }


        private event CurrentToolChangedEventHandler CurrentToolChanged;

        private delegate void CurrentToolChangedEventHandler(object sender, ITool<ToolSettingsBase> tool);

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

        private void CurrentToolChanged_Event(object sender, ITool<ToolSettingsBase> tool)
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

            Initialized?.Invoke();
        }

        private void InvokeFinalRender(ITool<ToolSettingsBase> tool)
        {
            var img = (Image)CurrentImage.Clone();
            Debug.Assert(tool != null);

            // Dim oldImageRef = img
            tool.RenderFinalImage(img);
            // If ReferenceEquals(oldImageRef, img) Then
            // RawBox.Image = oldImageRef
            // End If

            _undoStack.Push(img);
            UpdateRawBox();
        }

        public void RunDialogTool(IDialogTool tool)
        {
            var img = (Image)CurrentImage.Clone();
            tool.ShowToolDialog(img, Screenshot, this);
            _undoStack.Push(img);
            UpdateRawBox();
        }

        private void UpdateRawBox()
        {
            RawBox.Image = CurrentImage;
            RawBox.Invalidate();
        }

        private class Localization
        {
            private Localization()
            {
            }
            public const var SizeInfo = "{0}x{1}px, creation date: {2}";
            public const var SizeInfoText = "The image is {0}-by-{1}px. Left click to copy the image size. Right click to copy creation date. Middle click to copy both.";
        }

        public string SizeInfo
        {
            get
            {
                return string.Format(Localization.SizeInfo, _screenshot.Size.Width, _screenshot.Size.Height, _screenshot.Timestamp);
            }
        }

        public string SizeInfoText
        {
            get
            {
                return string.Format(Localization.SizeInfoText, _screenshot.Size.Width, _screenshot.Size.Height);
            }
        }



        private void DrawBoxMouseClick(object sender, MouseEventArgs e)
        {
            CurrentToolObject?.MouseClicked(CurrentImage, e.Location.ToVector2(), Cursor, this);
            RawBox.Invalidate();
        }

        private void MouseLayerMouseDown(object sender, MouseEventArgs e)
        {
            _mousedown = true;
            if (_currentTool == ShotEditorTool.None || _currentTool == ShotEditorTool.Text)
                return;

            CurrentToolObject.BeginCoordinates = e.Location.ToVector2();
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
            if (e.Button == MouseButtons.Left)
            {
                if (CurrentTool == ShotEditorTool.Text)
                {
                    TextPanel.BringToFront();
                    TextPanel.Location = new Point(e.Location.X + RawBox.Location.X, e.Location.Y + RawBox.Location.Y);
                    TextPanel.Visible = true;
                }
                else if (CurrentToolObject != null && CurrentToolObject.GetType != typeof(Scale))
                {
                    if (!CurrentToolObject is Eyedropper)
                        InvokeFinalRender(CurrentToolObject);
                    else
                        CurrentToolObject.RenderFinalImage(CurrentImage);
                }
                _mousedown = false;
            }
        }

        private bool _mousedown = false;

        private void RawBoxPaint(object sender, PaintEventArgs e)
        {
            if (_mousedown == true)
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


        private readonly Stack<Image> _undoStack = new Stack<Image>();

        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                _undoStack.Pop();
                UpdateRawBox();
                GC.Collect();
            }
        }

        private void PaintPanelDisposed()
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

        private void PaintPanelLoad()
        {
            BackColor = Color.FromArgb(207, 217, 231);
            RawBox.Focus();
        }

        private void TextOkClick()
        {
            TextPanel.Visible = false;

            using (RichTextBox rtb = new RichTextBox() { Location = RawBox.Location, Parent = RawBox.Parent, Font = RawBox.Font, BorderStyle = BorderStyle.None })
            {
                var charLocation = rtb.GetPositionFromCharIndex(0);

                Point location = new Point(TextPanel.Location.X - charLocation.X, TextPanel.Location.Y - charLocation.Y);

                var img = (Image)CurrentImage.Clone();

                using (Graphics g = Graphics.FromImage(img))
                {
                    g.TextRenderingHint = TextRenderingHint.AntiAlias;
                    g.DrawString(TextInput.Text, TextInput.Font, new SolidBrush(TextInput.ForeColor), location);
                }

                _undoStack.Push(img);
                UpdateRawBox();
                RawBox.Focus();
            }
        }


        private bool _moverMouseDown = false;
        private Point _dragPointMover;
        private Point _startPOsitionMover;

        private void PictureBox2MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _moverMouseDown = true;
                _startPOsitionMover = new Point(TextPanel.Location.X + EckenTeil.Location.X, TextPanel.Location.Y + EckenTeil.Location.Y);
                _dragPointMover = EckenTeil.PointToScreen(new Point(TextPanel.Location.X + e.X, TextPanel.Location.Y + e.Y));
            }
        }

        private void PictureBox2MouseMove(object sender, MouseEventArgs e)
        {
            if (_moverMouseDown == true)
            {
                Point nCurPos = EckenTeil.PointToScreen(new Point(TextPanel.Location.X + e.X, TextPanel.Location.Y + e.Y));
                TextPanel.Location = new Point(_startPOsitionMover.X + nCurPos.X - _dragPointMover.X, _startPOsitionMover.Y + nCurPos.Y - _dragPointMover.Y);
            }
        }

        private void PictureBox2MouseUp(object sender, MouseEventArgs e)
        {
            _moverMouseDown = false;
        }

        private void ChangeFontClick(object sender, EventArgs e)
        {
            if (TheFontDialog.ShowDialog() == DialogResult.OK)
            {
                TextInput.Font = TheFontDialog.Font;
                TextInput.ForeColor = TheFontDialog.Color;
            }
        }

        private void TextPanelVisibleChanged(object sender, EventArgs e)
        {
            ChangeFont.Parent = tools_bg;
            ChangeFont.Location = new Point(3, 2);

            MoverBox.Parent = tools_bg;
            MoverBox.Location = new Point(178, 2);

            SelectAll.Parent = tools_bg;
            SelectAll.Location = new Point(41, 2);

            InsertDate.Parent = tools_bg;
            InsertDate.Location = new Point(75, 2);

            CancelButton.Parent = tools_bg;
            CancelButton.Location = new Point(117, 2);
        }

        private void SelectAllClick(object sender, EventArgs e)
        {
            TextInput.Focus();
            TextInput.SelectAll();
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            TextPanel.Visible = false;
        }

        private void InsertDateClick(object sender, EventArgs e)
        {
            TextInput.Paste(_screenshot.Timestamp.ToString());
        }



        private void SaveLinealStuff(MouseEventArgs e)
        {
            var hRect = new Rectangle(_currentMousePosition.X < e.X ? _currentMousePosition.X : e.X - 1, 0, Math.Abs(_currentMousePosition.X - e.X) + 2, HorizontalLinealBox.DisplayRectangle.Height);
            var vRect = new Rectangle(0, _currentMousePosition.Y < e.Y ? _currentMousePosition.Y : e.Y - 1, VerticalLinealBox.DisplayRectangle.Width, Math.Abs(_currentMousePosition.Y - e.Y) + 2);

            _currentMousePosition = e.Location;

            HorizontalLinealBox.Invalidate(hRect, false);
            VerticalLinealBox.Invalidate(vRect, false);

            UpdateMousePosition?.Invoke(e.Location);

            if (CurrentTool == ShotEditorTool.Eyedropper)
                CurrentToolObject.MouseOnlyMoved(CurrentImage, Cursor, e);
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
                withBlock.SmoothingMode = SmoothingMode.HighSpeedx;
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

        private static readonly Pen BorderLinePen = new Pen(Color.FromArgb(218, 219, 220));

        private void PaintPanel_Load(object sender, EventArgs e)
        {

        }
    }
}
