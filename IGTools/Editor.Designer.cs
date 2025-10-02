namespace MaterialEditor
{
    partial class Editor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            TitleBar = new Panel();
            Min = new Button();
            Max = new Button();
            Close = new Button();
            panel1 = new Panel();
            TitleBar.SuspendLayout();
            SuspendLayout();
            // 
            // TitleBar
            // 
            TitleBar.BackColor = Color.FromArgb(35, 35, 35);
            TitleBar.Controls.Add(Min);
            TitleBar.Controls.Add(Max);
            TitleBar.Controls.Add(Close);
            TitleBar.Dock = DockStyle.Top;
            TitleBar.Location = new Point(0, 0);
            TitleBar.Margin = new Padding(3, 4, 3, 4);
            TitleBar.Name = "TitleBar";
            TitleBar.Size = new Size(1000, 47);
            TitleBar.TabIndex = 4;
            TitleBar.MouseDown += MoveWindow;
            // 
            // Min
            // 
            Min.Dock = DockStyle.Right;
            Min.FlatAppearance.BorderSize = 0;
            Min.FlatAppearance.MouseDownBackColor = Color.FromArgb(118, 178, 187);
            Min.FlatAppearance.MouseOverBackColor = Color.FromArgb(118, 178, 187);
            Min.FlatStyle = FlatStyle.Flat;
            Min.ForeColor = Color.FromArgb(64, 64, 64);
            Min.Image = (Image)resources.GetObject("Min.Image");
            Min.Location = new Point(793, 0);
            Min.Margin = new Padding(3, 4, 3, 4);
            Min.Name = "Min";
            Min.Size = new Size(69, 47);
            Min.TabIndex = 2;
            Min.UseVisualStyleBackColor = false;
            Min.Click += MinWindow;
            // 
            // Max
            // 
            Max.Dock = DockStyle.Right;
            Max.FlatAppearance.BorderSize = 0;
            Max.FlatAppearance.MouseDownBackColor = Color.FromArgb(215, 215, 230);
            Max.FlatAppearance.MouseOverBackColor = Color.FromArgb(235, 235, 250);
            Max.FlatStyle = FlatStyle.Flat;
            Max.ForeColor = Color.FromArgb(64, 64, 64);
            Max.Image = (Image)resources.GetObject("Max.Image");
            Max.Location = new Point(862, 0);
            Max.Margin = new Padding(3, 4, 3, 4);
            Max.Name = "Max";
            Max.Size = new Size(69, 47);
            Max.TabIndex = 1;
            Max.UseVisualStyleBackColor = false;
            Max.Click += MaxWindow;
            // 
            // Close
            // 
            Close.Dock = DockStyle.Right;
            Close.FlatAppearance.BorderSize = 0;
            Close.FlatAppearance.MouseDownBackColor = Color.DarkRed;
            Close.FlatAppearance.MouseOverBackColor = Color.Red;
            Close.FlatStyle = FlatStyle.Flat;
            Close.ForeColor = Color.FromArgb(64, 64, 64);
            Close.Image = (Image)resources.GetObject("Close.Image");
            Close.Location = new Point(931, 0);
            Close.Margin = new Padding(3, 4, 3, 4);
            Close.Name = "Close";
            Close.Size = new Size(69, 47);
            Close.TabIndex = 0;
            Close.UseVisualStyleBackColor = false;
            Close.Click += CloseWindow;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(30, 30, 30);
            panel1.Location = new Point(12, 54);
            panel1.Name = "panel1";
            panel1.Size = new Size(976, 614);
            panel1.TabIndex = 5;
            // 
            // Editor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(1000, 680);
            Controls.Add(panel1);
            Controls.Add(TitleBar);
            ForeColor = Color.Transparent;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Editor";
            Text = "Editor";
            TitleBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel TitleBar;
        private Button Min;
        private Button Max;
        private Button Close;
        private Panel panel1;
    }
}