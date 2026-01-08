using ProductsApp.Forms;

namespace ProductsApp
{
    public partial class MainForm : Form
    {
        Context _context;
        public MainForm(Context context)
        {
            InitializeComponent();
            _context = context;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ProductForm form = ProductForm.GetOrCreateForm(_context);
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OrderForm form = OrderForm.GetOrCreateForm(_context);
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }
    }
}
