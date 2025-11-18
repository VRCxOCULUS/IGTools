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
            label1 = new Label();
            Title = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.AutoSize = true;
            button1.BackColor = Color.FromArgb(40, 45, 50);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(118, 178, 187);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Ubuntu", 20F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = Color.FromArgb(235, 235, 250);
            button1.Location = new Point(79, 449);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(286, 93);
            button1.TabIndex = 0;
            button1.Text = "New Material";
            button1.UseVisualStyleBackColor = false;
            button1.Click += NewMaterial;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.None;
            button2.AutoSize = true;
            button2.BackColor = Color.FromArgb(40, 45, 50);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(118, 178, 187);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Ubuntu", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point);
            button2.ForeColor = Color.FromArgb(235, 235, 250);
            button2.Location = new Point(609, 449);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(286, 93);
            button2.TabIndex = 1;
            button2.Text = "Open Material";
            button2.UseVisualStyleBackColor = false;
            button2.Click += OpenMaterial;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(30, 30, 30);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(Title);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button2);
            panel1.ForeColor = Color.Black;
            panel1.Location = new Point(12, 13);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(975, 649);
            panel1.TabIndex = 2;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Ubuntu", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(118, 178, 187);
            label1.Location = new Point(424, 179);
            label1.Name = "label1";
            label1.Size = new Size(123, 35);
            label1.TabIndex = 3;
            label1.Text = "IG Tools";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Title
            // 
            Title.Anchor = AnchorStyles.None;
            Title.AutoSize = true;
            Title.Font = new Font("Ubuntu", 40F, FontStyle.Regular, GraphicsUnit.Point);
            Title.ForeColor = Color.FromArgb(235, 235, 250);
            Title.Location = new Point(246, 59);
            Title.Name = "Title";
            Title.Size = new Size(490, 79);
            Title.TabIndex = 2;
            Title.Text = "Material Editor";
            Title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LandingPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(1000, 675);
            Controls.Add(panel1);
            ForeColor = Color.Transparent;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "LandingPage";
            Text = "IGMaterialTool";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Panel panel1;
        private Label Title;
        private Label label1;
    }
}
