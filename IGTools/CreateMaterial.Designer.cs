namespace IGTools
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateMaterial));
            button2 = new Button();
            lblName = new Label();
            label2 = new Label();
            txtName = new TextBox();
            txtTemplate = new TextBox();
            btnSelectTemplatePath = new Button();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.AutoSize = true;
            button2.BackColor = Color.FromArgb(40, 45, 50);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(118, 178, 187);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Ubuntu", 10F, FontStyle.Regular, GraphicsUnit.Point);
            button2.ForeColor = Color.FromArgb(235, 235, 250);
            button2.Location = new Point(330, 82);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(89, 37);
            button2.TabIndex = 7;
            button2.Text = "Create";
            button2.UseVisualStyleBackColor = false;
            button2.Click += Create_Click;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblName.ForeColor = Color.FromArgb(235, 235, 250);
            lblName.Location = new Point(35, 9);
            lblName.Name = "lblName";
            lblName.Size = new Size(65, 23);
            lblName.TabIndex = 3;
            lblName.Text = "Name: ";
            lblName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(235, 235, 250);
            label2.Location = new Point(12, 35);
            label2.Name = "label2";
            label2.Size = new Size(88, 23);
            label2.TabIndex = 8;
            label2.Text = "Template: ";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(35, 35, 35);
            txtName.ForeColor = Color.White;
            txtName.Location = new Point(106, 5);
            txtName.Name = "txtName";
            txtName.Size = new Size(316, 27);
            txtName.TabIndex = 10;
            // 
            // txtTemplate
            // 
            txtTemplate.BackColor = Color.FromArgb(35, 35, 35);
            txtTemplate.ForeColor = Color.White;
            txtTemplate.Location = new Point(106, 31);
            txtTemplate.Name = "txtTemplate";
            txtTemplate.Size = new Size(258, 27);
            txtTemplate.TabIndex = 11;
            // 
            // btnSelectTemplatePath
            // 
            btnSelectTemplatePath.AutoSize = true;
            btnSelectTemplatePath.BackColor = Color.FromArgb(40, 45, 50);
            btnSelectTemplatePath.FlatAppearance.BorderSize = 0;
            btnSelectTemplatePath.FlatAppearance.MouseDownBackColor = Color.FromArgb(118, 178, 187);
            btnSelectTemplatePath.FlatStyle = FlatStyle.Flat;
            btnSelectTemplatePath.Font = new Font("Ubuntu", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnSelectTemplatePath.ForeColor = Color.FromArgb(235, 235, 250);
            btnSelectTemplatePath.Location = new Point(370, 31);
            btnSelectTemplatePath.Margin = new Padding(3, 4, 3, 4);
            btnSelectTemplatePath.Name = "btnSelectTemplatePath";
            btnSelectTemplatePath.Size = new Size(52, 31);
            btnSelectTemplatePath.TabIndex = 12;
            btnSelectTemplatePath.Text = "...";
            btnSelectTemplatePath.UseVisualStyleBackColor = false;
            btnSelectTemplatePath.Click += SelectTemplatePath;
            // 
            // CreateMaterial
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(431, 132);
            Controls.Add(btnSelectTemplatePath);
            Controls.Add(txtTemplate);
            Controls.Add(txtName);
            Controls.Add(label2);
            Controls.Add(lblName);
            Controls.Add(button2);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CreateMaterial";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Create Material";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button2;
        private Label lblName;
        private Label label2;
        private TextBox txtName;
        private TextBox txtTemplate;
        private Button btnSelectTemplatePath;
    }
}