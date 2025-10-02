namespace MaterialEditor
{
    partial class CreateMaterial
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateMaterial));
            TitleBar = new Panel();
            Close = new Button();
            panel2 = new Panel();
            MaterialPathTextBox = new Label();
            button1 = new Button();
            label1 = new Label();
            openFileDialog1 = new OpenFileDialog();
            contextMenuStrip1 = new ContextMenuStrip(components);
            testToolStripMenuItem = new ToolStripMenuItem();
            TitleBar.SuspendLayout();
            panel2.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // TitleBar
            // 
            TitleBar.BackColor = Color.FromArgb(35, 35, 35);
            TitleBar.Controls.Add(Close);
            TitleBar.Dock = DockStyle.Top;
            TitleBar.Location = new Point(0, 0);
            TitleBar.Margin = new Padding(3, 4, 3, 4);
            TitleBar.Name = "TitleBar";
            TitleBar.Size = new Size(800, 40);
            TitleBar.TabIndex = 4;
            TitleBar.MouseDown += MoveWindow;
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
            Close.Location = new Point(731, 0);
            Close.Margin = new Padding(3, 4, 3, 4);
            Close.Name = "Close";
            Close.Size = new Size(69, 40);
            Close.TabIndex = 0;
            Close.UseVisualStyleBackColor = false;
            Close.Click += CloseWindow;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(30, 30, 30);
            panel2.Controls.Add(MaterialPathTextBox);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(12, 47);
            panel2.Name = "panel2";
            panel2.Size = new Size(776, 60);
            panel2.TabIndex = 6;
            // 
            // MaterialPathTextBox
            // 
            MaterialPathTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            MaterialPathTextBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            MaterialPathTextBox.ForeColor = Color.FromArgb(235, 235, 250);
            MaterialPathTextBox.Location = new Point(80, 19);
            MaterialPathTextBox.Name = "MaterialPathTextBox";
            MaterialPathTextBox.Size = new Size(551, 23);
            MaterialPathTextBox.TabIndex = 2;
            MaterialPathTextBox.Text = "test";
            MaterialPathTextBox.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(40, 45, 50);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(118, 178, 187);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 50, 55);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Ubuntu", 10F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = Color.FromArgb(235, 235, 250);
            button1.Location = new Point(637, 10);
            button1.Name = "button1";
            button1.Size = new Size(129, 40);
            button1.TabIndex = 1;
            button1.Text = "Browse";
            button1.UseVisualStyleBackColor = false;
            button1.Click += SetMaterialPath;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(235, 235, 250);
            label1.Location = new Point(21, 19);
            label1.Name = "label1";
            label1.Size = new Size(53, 23);
            label1.TabIndex = 0;
            label1.Text = "Path: ";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { testToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(103, 28);
            // 
            // testToolStripMenuItem
            // 
            testToolStripMenuItem.Name = "testToolStripMenuItem";
            testToolStripMenuItem.Size = new Size(102, 24);
            testToolStripMenuItem.Text = "test";
            // 
            // CreateMaterial
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Controls.Add(panel2);
            Controls.Add(TitleBar);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CreateMaterial";
            Text = "CreateMaterial";
            TopMost = true;
            TitleBar.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel TitleBar;
        private Button Close;
        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private OpenFileDialog openFileDialog1;
        private Button button1;
        private Label MaterialPathTextBox;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem testToolStripMenuItem;
    }
}