using IGTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGTools
{
    public partial class CreateMaterial : Form
    {
        public string Name;
        public string Template;

        public bool validParams = false;

        public CreateMaterial()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, EventArgs e)
        {
            Name = txtName.Text;
            Template = txtTemplate.Text;
            if (Path.GetExtension(Name) != ".material")
            {
                MessageBox.Show("Invalid material name. Make sure name ends with \".material\".", "Error Creating Material", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Path.GetExtension(Template) != ".materialgraph")
            {
                MessageBox.Show("Selected path is not a material template.", "Error Creating Material", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            validParams = true;
            this.Close();
        }

        private void SelectTemplatePath(object sender, EventArgs e)
        {
            OpenFileDialog selectTemplate = new OpenFileDialog();
            selectTemplate.Filter = "Material Template (*.materialgraph)|*.materialgraph";
            selectTemplate.Title = "Select Material Template";
            selectTemplate.ShowDialog();

            txtTemplate.Text = selectTemplate.FileName;
        }
    }
}
