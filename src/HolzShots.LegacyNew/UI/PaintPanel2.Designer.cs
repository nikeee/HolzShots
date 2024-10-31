namespace HolzShots.UI
{
    partial class PaintPanel2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            WholePanel = new Panel();
            RawBox = new PictureBox();
            TheFontDialog = new FontDialog();
            EckenTeil = new PictureBox();
            VerticalLinealBox = new PictureBox();
            HorizontalLinealBox = new PictureBox();
            WholePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RawBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EckenTeil).BeginInit();
            ((System.ComponentModel.ISupportInitialize)VerticalLinealBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HorizontalLinealBox).BeginInit();
            SuspendLayout();
            //
            // WholePanel
            //
            WholePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            WholePanel.AutoScroll = true;
            WholePanel.BackColor = Color.FromArgb(202, 212, 227);
            WholePanel.Controls.Add(RawBox);
            WholePanel.Location = new Point(23, 23);
            WholePanel.Margin = new Padding(4, 3, 4, 3);
            WholePanel.Name = "WholePanel";
            WholePanel.Size = new Size(623, 509);
            WholePanel.TabIndex = 17;
            //
            // RawBox
            //
            RawBox.BackColor = SystemColors.GradientActiveCaption;
            RawBox.Location = new Point(2, 7);
            RawBox.Margin = new Padding(4, 3, 4, 3);
            RawBox.Name = "RawBox";
            RawBox.Size = new Size(100, 50);
            RawBox.SizeMode = PictureBoxSizeMode.AutoSize;
            RawBox.TabIndex = 12;
            RawBox.TabStop = false;
            RawBox.MouseClick += DrawBoxMouseClick;
            RawBox.MouseDown += MouseLayerMouseDown;
            RawBox.MouseEnter += RawBoxMouseEnter;
            RawBox.MouseMove += MouseLayerMouseMove;
            RawBox.MouseUp += MouseLayerMouseUp;
            RawBox.Paint += RawBoxPaint;
            //
            // TheFontDialog
            //
            TheFontDialog.ShowColor = true;
            //
            // EckenTeil
            //
            EckenTeil.BackColor = Color.FromArgb(240, 241, 249);
            EckenTeil.Location = new Point(0, 0);
            EckenTeil.Margin = new Padding(4, 3, 4, 3);
            EckenTeil.Name = "EckenTeil";
            EckenTeil.Size = new Size(23, 23);
            EckenTeil.TabIndex = 18;
            EckenTeil.TabStop = false;
            //
            // VerticalLinealBox
            //
            VerticalLinealBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            VerticalLinealBox.Location = new Point(0, 23);
            VerticalLinealBox.Margin = new Padding(4, 3, 4, 3);
            VerticalLinealBox.Name = "VerticalLinealBox";
            VerticalLinealBox.Size = new Size(23, 509);
            VerticalLinealBox.TabIndex = 20;
            VerticalLinealBox.TabStop = false;
            VerticalLinealBox.Paint += VerticalLinealBoxPaint;
            //
            // HorizontalLinealBox
            //
            HorizontalLinealBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            HorizontalLinealBox.Location = new Point(23, 0);
            HorizontalLinealBox.Margin = new Padding(4, 3, 4, 3);
            HorizontalLinealBox.Name = "HorizontalLinealBox";
            HorizontalLinealBox.Size = new Size(623, 23);
            HorizontalLinealBox.TabIndex = 19;
            HorizontalLinealBox.TabStop = false;
            HorizontalLinealBox.Paint += HorizontalLinealBoxPaint;
            //
            // PaintPanel2
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            Controls.Add(WholePanel);
            Controls.Add(EckenTeil);
            Controls.Add(VerticalLinealBox);
            Controls.Add(HorizontalLinealBox);
            Margin = new Padding(4, 3, 4, 3);
            Name = "PaintPanel2";
            Size = new Size(646, 532);
            Load += PaintPanelLoad;
            Disposed += PaintPanelDisposed;
            WholePanel.ResumeLayout(false);
            WholePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)RawBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)EckenTeil).EndInit();
            ((System.ComponentModel.ISupportInitialize)VerticalLinealBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)HorizontalLinealBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        internal Panel WholePanel;
        internal PictureBox RawBox;
        internal FontDialog TheFontDialog;
        internal PictureBox EckenTeil;
        public PictureBox VerticalLinealBox;
        public PictureBox HorizontalLinealBox;
    }
}
