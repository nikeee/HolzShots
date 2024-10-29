namespace HolzShots.Drawing.Tools.UI
{
    partial class ScaleWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            UnitLabel1 = new Label();
            UnitLabel2 = new Label();
            Label3 = new Label();
            Label2 = new Label();
            KeepAspectRatio = new CheckBox();
            Pixel = new RadioButton();
            Percent = new RadioButton();
            PictureBox3 = new PictureBox();
            PictureBox2 = new PictureBox();
            okButton = new Button();
            cancelButton = new Button();
            ExplorerInfoPanel1 = new Windows.Forms.ExplorerInfoPanel();
            HeightBox = new Windows.Forms.Controls.NumericTextBox();
            WidthBox = new Windows.Forms.Controls.NumericTextBox();
            ((System.ComponentModel.ISupportInitialize)PictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).BeginInit();
            ExplorerInfoPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // UnitLabel1
            // 
            UnitLabel1.AutoSize = true;
            UnitLabel1.Location = new Point(240, 82);
            UnitLabel1.Name = "UnitLabel1";
            UnitLabel1.Size = new Size(17, 15);
            UnitLabel1.TabIndex = 32;
            UnitLabel1.Text = "%";
            // 
            // UnitLabel2
            // 
            UnitLabel2.AutoSize = true;
            UnitLabel2.Location = new Point(240, 37);
            UnitLabel2.Name = "UnitLabel2";
            UnitLabel2.Size = new Size(17, 15);
            UnitLabel2.TabIndex = 31;
            UnitLabel2.Text = "%";
            // 
            // Label3
            // 
            Label3.AutoSize = true;
            Label3.Location = new Point(75, 82);
            Label3.Name = "Label3";
            Label3.Size = new Size(48, 15);
            Label3.TabIndex = 30;
            Label3.Text = "Vertical:";
            // 
            // Label2
            // 
            Label2.AutoSize = true;
            Label2.Location = new Point(58, 37);
            Label2.Name = "Label2";
            Label2.Size = new Size(65, 15);
            Label2.TabIndex = 29;
            Label2.Text = "Horizontal:";
            // 
            // KeepAspectRatio
            // 
            KeepAspectRatio.AutoSize = true;
            KeepAspectRatio.Checked = true;
            KeepAspectRatio.CheckState = CheckState.Checked;
            KeepAspectRatio.FlatStyle = FlatStyle.System;
            KeepAspectRatio.Location = new Point(52, 117);
            KeepAspectRatio.Name = "KeepAspectRatio";
            KeepAspectRatio.Size = new Size(129, 20);
            KeepAspectRatio.TabIndex = 28;
            KeepAspectRatio.Text = "Keep aspect ration";
            KeepAspectRatio.UseVisualStyleBackColor = true;
            KeepAspectRatio.CheckedChanged += KeepAspectRatio_CheckedChanged;
            // 
            // Pixel
            // 
            Pixel.AutoSize = true;
            Pixel.FlatStyle = FlatStyle.System;
            Pixel.Location = new Point(177, 8);
            Pixel.Name = "Pixel";
            Pixel.Size = new Size(56, 20);
            Pixel.TabIndex = 24;
            Pixel.TabStop = true;
            Pixel.Text = "Pixel";
            Pixel.UseVisualStyleBackColor = true;
            // 
            // Percent
            // 
            Percent.AutoSize = true;
            Percent.Checked = true;
            Percent.FlatStyle = FlatStyle.System;
            Percent.Location = new Point(88, 8);
            Percent.Name = "Percent";
            Percent.Size = new Size(71, 20);
            Percent.TabIndex = 25;
            Percent.TabStop = true;
            Percent.Text = "Percent";
            Percent.UseVisualStyleBackColor = true;
            // 
            // PictureBox3
            // 
            PictureBox3.Image = Properties.Resources.verticalMedium;
            PictureBox3.Location = new Point(18, 70);
            PictureBox3.Name = "PictureBox3";
            PictureBox3.Size = new Size(34, 31);
            PictureBox3.SizeMode = PictureBoxSizeMode.AutoSize;
            PictureBox3.TabIndex = 27;
            PictureBox3.TabStop = false;
            // 
            // PictureBox2
            // 
            PictureBox2.Image = Properties.Resources.horizontalMedium;
            PictureBox2.Location = new Point(21, 29);
            PictureBox2.Name = "PictureBox2";
            PictureBox2.Size = new Size(31, 31);
            PictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            PictureBox2.TabIndex = 26;
            PictureBox2.TabStop = false;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.BackColor = SystemColors.Control;
            okButton.FlatStyle = FlatStyle.System;
            okButton.Location = new Point(170, 15);
            okButton.Name = "okButton";
            okButton.Size = new Size(87, 27);
            okButton.TabIndex = 21;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = false;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.FlatStyle = FlatStyle.System;
            cancelButton.Location = new Point(77, 15);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(87, 27);
            cancelButton.TabIndex = 22;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // ExplorerInfoPanel1
            // 
            ExplorerInfoPanel1.Controls.Add(okButton);
            ExplorerInfoPanel1.Controls.Add(cancelButton);
            ExplorerInfoPanel1.Dock = DockStyle.Bottom;
            ExplorerInfoPanel1.Font = new Font("Segoe UI", 9F);
            ExplorerInfoPanel1.ForeColor = Color.FromArgb(30, 57, 91);
            ExplorerInfoPanel1.Location = new Point(0, 157);
            ExplorerInfoPanel1.Name = "ExplorerInfoPanel1";
            ExplorerInfoPanel1.Size = new Size(274, 54);
            ExplorerInfoPanel1.TabIndex = 35;
            // 
            // HeightBox
            // 
            HeightBox.Location = new Point(129, 78);
            HeightBox.Name = "HeightBox";
            HeightBox.Size = new Size(103, 23);
            HeightBox.TabIndex = 34;
            HeightBox.Text = "100";
            HeightBox.TextChanged += HeightBox_TextChanged;
            // 
            // WidthBox
            // 
            WidthBox.Location = new Point(129, 34);
            WidthBox.Name = "WidthBox";
            WidthBox.Size = new Size(103, 23);
            WidthBox.TabIndex = 33;
            WidthBox.Text = "100";
            WidthBox.TextChanged += WidthBox_TextChanged;
            // 
            // ScaleWindow
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            CancelButton = cancelButton;
            ClientSize = new Size(274, 211);
            Controls.Add(UnitLabel1);
            Controls.Add(UnitLabel2);
            Controls.Add(Label3);
            Controls.Add(Label2);
            Controls.Add(KeepAspectRatio);
            Controls.Add(Pixel);
            Controls.Add(Percent);
            Controls.Add(PictureBox3);
            Controls.Add(PictureBox2);
            Controls.Add(ExplorerInfoPanel1);
            Controls.Add(HeightBox);
            Controls.Add(WidthBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "ScaleWindow";
            Text = "Scale Image";
            Load += ScaleWindow_Load;
            ((System.ComponentModel.ISupportInitialize)PictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox2).EndInit();
            ExplorerInfoPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal Label UnitLabel1;
        internal Label UnitLabel2;
        internal Label Label3;
        internal Label Label2;
        internal CheckBox KeepAspectRatio;
        internal RadioButton Pixel;
        internal RadioButton Percent;
        internal PictureBox PictureBox3;
        internal PictureBox PictureBox2;
        internal Button okButton;
        internal Button cancelButton;
        internal Windows.Forms.ExplorerInfoPanel ExplorerInfoPanel1;
        internal Windows.Forms.Controls.NumericTextBox HeightBox;
        internal Windows.Forms.Controls.NumericTextBox WidthBox;
    }
}