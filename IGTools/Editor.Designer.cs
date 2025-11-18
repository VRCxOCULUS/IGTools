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
            panel1 = new Panel();
            splitContainer1 = new SplitContainer();
            lblParams = new Label();
            listView1 = new ListView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(30, 30, 30);
            panel1.Controls.Add(splitContainer1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(976, 656);
            panel1.TabIndex = 5;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = Color.FromArgb(40, 40, 40);
            splitContainer1.Panel1.Controls.Add(listView1);
            splitContainer1.Panel1.Controls.Add(lblParams);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = Color.FromArgb(30, 30, 30);
            splitContainer1.Size = new Size(976, 656);
            splitContainer1.SplitterDistance = 271;
            splitContainer1.TabIndex = 0;
            // 
            // lblParams
            // 
            lblParams.AutoSize = true;
            lblParams.Font = new Font("Ubuntu", 16F, FontStyle.Regular, GraphicsUnit.Point);
            lblParams.Location = new Point(0, 0);
            lblParams.Name = "lblParams";
            lblParams.Size = new Size(155, 33);
            lblParams.TabIndex = 1;
            lblParams.Text = "Parameters";
            // 
            // listView1
            // 
            listView1.BackColor = Color.FromArgb(35, 35, 35);
            listView1.ForeColor = Color.White;
            listView1.Location = new Point(3, 36);
            listView1.Name = "listView1";
            listView1.Size = new Size(265, 617);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // EditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(1000, 680);
            Controls.Add(panel1);
            ForeColor = Color.Transparent;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "EditorWindow";
            Text = "Editor";
            FormClosing += ConfirmClose;
            panel1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private SplitContainer splitContainer1;
        private Label lblParams;
        private ListView listView1;
    }
}