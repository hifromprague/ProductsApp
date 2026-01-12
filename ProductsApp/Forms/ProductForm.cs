using ProductsApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductsApp.Forms
{
    public partial class ProductForm : Form
    {
        Context _context;

        private static ProductForm _instance;

        public static ProductForm GetOrCreateForm(Context context)
        {
            if (_instance == null || _instance.IsDisposed)
                _instance = new ProductForm(context);

            return _instance;
        }

        public ProductForm(Context context)
        {
            InitializeComponent();
            _context = context;
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            RefreshDataSource();
        }

        private void RefreshDataSource()
        {
            productBindingSource.DataSource = _context.Products.ToList();
            productBindingSource.ResetBindings(false);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var addEditForm = new AddEditProductForm(_context);
            var dialogResult = addEditForm.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                RefreshDataSource();
            }
        }

        private void toolStripEditButton_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow != null)
            {
                var product = (Product)dataGridView1.CurrentRow.DataBoundItem;
                var addEditForm = new AddEditProductForm(_context, product);
                var dialogResult = addEditForm.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    RefreshDataSource();
                }

            }


        }

        private void ToolStripDeleteButton_click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var productToDelete = (Product)dataGridView1.CurrentRow.DataBoundItem;
                if (_context.Products.Find(productToDelete.Id) != null) 
                {
                    _context.Products.Remove(productToDelete);
                    _context.SaveChanges();
                    RefreshDataSource();
                }
                

            }
        }
    }
}
