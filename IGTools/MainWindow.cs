using System.Windows.Forms;

namespace IGTools
{
    public partial class LandingPage : Form
    {
        public LandingPage()
        {
            InitializeComponent();
        }


        private void NewMaterial(object sender, EventArgs e)
        {
            CreateMaterial createMaterial = new CreateMaterial();
            createMaterial.ShowDialog();

            if(!createMaterial.validParams)
                return;

            EditorWindow materialEditor = new EditorWindow(createMaterial.Name, createMaterial.Template);
            this.Hide();
            materialEditor.ShowDialog();
            this.Close();
        }

        private void OpenMaterial(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Material Asset (*.material)|*.material";
            openFileDialog.Title = "Open Material Asset";
            openFileDialog.ShowDialog();

            if(openFileDialog.FileName == "")
            {
                return;
            }

            string filePath = openFileDialog.FileName;
            EditorWindow materialEditor = new EditorWindow(filePath);
            this.Hide();
            materialEditor.ShowDialog();
            this.Close();
        }
    }
}