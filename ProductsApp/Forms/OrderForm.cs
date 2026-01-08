using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductsApp.Forms
{
    public partial class OrderForm : Form
    {
        Context _context;

        private static OrderForm _instance;

        public static OrderForm GetOrCreateForm(Context context)
        {
            if (_instance == null || _instance.IsDisposed)
                _instance = new OrderForm(context);

            return _instance;
        }

        public OrderForm(Context context)
        {
            InitializeComponent();
            _context = context;
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            RefreshDataSource();
        }

        private void RefreshDataSource()
        {
            orderBindingSource.DataSource = _context.Orders.ToList();
            orderBindingSource.ResetBindings(false);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var addEditForm = new AddEditOrderForm(_context);
            var dialogResult = addEditForm.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                RefreshDataSource();
            }
        }
    }
}
