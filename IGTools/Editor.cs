namespace IGTools
{
    public partial class EditorWindow : Form
    {
        // New Material
        public EditorWindow(string AssetName, string TemplatePath)
        {
            InitializeComponent();
            Material material = new Material();
            material.Name = AssetName;
            material.Template = TemplatePath;
            this.Text = material.Name + " - Editor";
        }

        // Open Material
        public EditorWindow(string AssetPath)
        {
            InitializeComponent();
            Material material = new Material();
            material.LoadMaterial(AssetPath);
            this.Text = material.Name + " - Editor";
        }

        private void ConfirmClose(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Do you want to save before closing?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            e.Cancel = result == DialogResult.Cancel;
        }
    }
}