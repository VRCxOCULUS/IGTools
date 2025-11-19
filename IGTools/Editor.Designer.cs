namespace IGTools
{
    partial class EditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorWindow));
            splitContainer1 = new SplitContainer();
            lstParams = new ListBox();
            lblParams = new Label();
            label = new Label();
            textBox1 = new TextBox();
            TextureViewer = new PictureBox();
            lblPropertyName = new Label();
            checkBox1 = new CheckBox();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TextureViewer).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(9, 48);
            splitContainer1.Margin = new Padding(3, 2, 3, 2);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = Color.FromArgb(40, 40, 40);
            splitContainer1.Panel1.Controls.Add(lstParams);
            splitContainer1.Panel1.Controls.Add(lblParams);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = Color.FromArgb(30, 30, 30);
            splitContainer1.Panel2.Controls.Add(label);
            splitContainer1.Panel2.Controls.Add(textBox1);
            splitContainer1.Panel2.Controls.Add(TextureViewer);
            splitContainer1.Panel2.Controls.Add(lblPropertyName);
            splitContainer1.Panel2.Controls.Add(checkBox1);
            splitContainer1.Size = new Size(566, 451);
            splitContainer1.SplitterDistance = 191;
            splitContainer1.TabIndex = 0;
            // 
            // lstParams
            // 
            lstParams.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstParams.BackColor = Color.FromArgb(35, 35, 35);
            lstParams.BorderStyle = BorderStyle.FixedSingle;
            lstParams.ForeColor = Color.White;
            lstParams.FormattingEnabled = true;
            lstParams.ItemHeight = 15;
            lstParams.Items.AddRange(new object[] { "Test1", "2883245952", "Test2" });
            lstParams.Location = new Point(3, 29);
            lstParams.Name = "lstParams";
            lstParams.Size = new Size(185, 422);
            lstParams.TabIndex = 2;
            // 
            // lblParams
            // 
            lblParams.AutoSize = true;
            lblParams.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point);
            lblParams.Location = new Point(0, 0);
            lblParams.Name = "lblParams";
            lblParams.Size = new Size(125, 26);
            lblParams.TabIndex = 1;
            lblParams.Text = "Parameters";
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label.Location = new Point(6, 62);
            label.Name = "label";
            label.Size = new Size(51, 21);
            label.TabIndex = 4;
            label.Text = "Value:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(63, 60);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(300, 23);
            textBox1.TabIndex = 3;
            // 
            // TextureViewer
            // 
            TextureViewer.BackColor = Color.FromArgb(25, 25, 25);
            TextureViewer.Image = (Image)resources.GetObject("TextureViewer.Image");
            TextureViewer.InitialImage = null;
            TextureViewer.Location = new Point(3, 88);
            TextureViewer.Name = "TextureViewer";
            TextureViewer.Size = new Size(360, 360);
            TextureViewer.SizeMode = PictureBoxSizeMode.StretchImage;
            TextureViewer.TabIndex = 2;
            TextureViewer.TabStop = false;
            // 
            // lblPropertyName
            // 
            lblPropertyName.AutoSize = true;
            lblPropertyName.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            lblPropertyName.Location = new Point(3, 1);
            lblPropertyName.Name = "lblPropertyName";
            lblPropertyName.Size = new Size(112, 25);
            lblPropertyName.TabIndex = 1;
            lblPropertyName.Text = "2883245952";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox1.Location = new Point(3, 29);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(86, 25);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Exposed";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(563, 31);
            panel1.TabIndex = 1;
            // 
            // EditorWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(587, 510);
            Controls.Add(panel1);
            Controls.Add(splitContainer1);
            ForeColor = Color.Transparent;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "EditorWindow";
            Text = "Editor";
            FormClosing += ConfirmClose;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TextureViewer).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private SplitContainer splitContainer1;
        private Label lblParams;
        private CheckBox checkBox1;
        private Panel panel1;
        private Label lblPropertyName;
        private PictureBox TextureViewer;
        private ListBox lstParams;
        private Label label;
        private TextBox textBox1;
    }
}