namespace IGTools
{
    partial class LandingPage
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LandingPage));
            button1 = new Button();
            button2 = new Button();
            panel1 = new Panel();
            TitleBar = new Panel();
            Min = new Button();
            Max = new Button();
            Close = new Button();
            panel1.SuspendLayout();
            TitleBar.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.AutoSize = true;
            button1.BackColor = Color.FromArgb(40, 45, 50);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Ubuntu", 20F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = Color.FromArgb(235, 235, 250);
            button1.Location = new Point(3, 490);
            button1.Name = "button1";
            button1.Size = new Size(250, 70);
            button1.TabIndex = 0;
            button1.Text = "New Material";
            button1.UseVisualStyleBackColor = false;
            button1.Click += NewMaterial;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.AutoSize = true;
            button2.BackColor = Color.FromArgb(40, 45, 50);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Ubuntu", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point);
            button2.ForeColor = Color.FromArgb(235, 235, 250);
            button2.Location = new Point(601, 490);
            button2.Name = "button2";
            button2.Size = new Size(250, 70);
            button2.TabIndex = 1;
            button2.Text = "Open Material";
            button2.UseVisualStyleBackColor = false;
            button2.Click += OpenMaterial;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(30, 30, 30);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button2);
            panel1.ForeColor = Color.Black;
            panel1.Location = new Point(10, 41);
            panel1.Name = "panel1";
            panel1.Size = new Size(854, 563);
            panel1.TabIndex = 2;
            // 
            // TitleBar
            // 
            TitleBar.BackColor = Color.Transparent;
            TitleBar.Controls.Add(Min);
            TitleBar.Controls.Add(Max);
            TitleBar.Controls.Add(Close);
            TitleBar.Dock = DockStyle.Top;
            TitleBar.Location = new Point(0, 0);
            TitleBar.Name = "TitleBar";
            TitleBar.Size = new Size(876, 35);
            TitleBar.TabIndex = 3;
            TitleBar.MouseDown += MoveWindow;
            // 
            // Min
            // 
            Min.Dock = DockStyle.Right;
            Min.FlatAppearance.BorderSize = 0;
            Min.FlatStyle = FlatStyle.Flat;
            Min.ForeColor = Color.FromArgb(64, 64, 64);
            Min.Image = (Image)resources.GetObject("Min.Image");
            Min.Location = new Point(696, 0);
            Min.Name = "Min";
            Min.Size = new Size(60, 35);
            Min.TabIndex = 2;
            Min.UseVisualStyleBackColor = false;
            Min.Click += MinWindow;
            // 
            // Max
            // 
            Max.Dock = DockStyle.Right;
            Max.FlatAppearance.BorderSize = 0;
            Max.FlatStyle = FlatStyle.Flat;
            Max.ForeColor = Color.FromArgb(64, 64, 64);
            Max.Image = (Image)resources.GetObject("Max.Image");
            Max.Location = new Point(756, 0);
            Max.Name = "Max";
            Max.Size = new Size(60, 35);
            Max.TabIndex = 1;
            Max.UseVisualStyleBackColor = false;
            Max.Click += MaxWindow;
            // 
            // Close
            // 
            Close.Dock = DockStyle.Right;
            Close.FlatAppearance.BorderSize = 0;
            Close.FlatStyle = FlatStyle.Flat;
            Close.ForeColor = Color.FromArgb(64, 64, 64);
            Close.Image = (Image)resources.GetObject("Close.Image");
            Close.Location = new Point(816, 0);
            Close.Name = "Close";
            Close.Size = new Size(60, 35);
            Close.TabIndex = 0;
            Close.UseVisualStyleBackColor = false;
            Close.Click += CloseWindow;
            // 
            // LandingPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(876, 616);
            Controls.Add(TitleBar);
            Controls.Add(panel1);
            ForeColor = Color.Transparent;
            FormBorderStyle = FormBorderStyle.None;
            Name = "LandingPage";
            Text = "IGMaterialTool";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            TitleBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Panel panel1;
        private Panel TitleBar;
        private Button Close;
        private Button Max;
        private Button Min;
    }
}
