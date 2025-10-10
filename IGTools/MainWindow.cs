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

        }

        private void OpenMaterial(object sender, EventArgs e)
        {

        }


        // Event Handlers for Title Bar Buttons

        private void CloseWindow(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void MaxWindow(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void MinWindow(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();



        private void MoveWindow(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}