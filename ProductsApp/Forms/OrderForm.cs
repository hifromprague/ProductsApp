using Microsoft.EntityFrameworkCore;
using ProductsApp.Models;
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
            orderBindingSource.DataSource = _context.Orders
                                                    .Include(x => x.Items)
                                                    .ThenInclude(y => y.Product)
                                                    .ToList();
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

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var order = (Order)dataGridView1.CurrentRow.DataBoundItem;
                var addEditForm = new AddEditOrderForm(_context, order);
                var dialogResult = addEditForm.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    RefreshDataSource();
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var orderToDelete = (Order)dataGridView1.CurrentRow.DataBoundItem;
                if (_context.Orders.Find(orderToDelete.Id) != null)
                {
                    _context.ProductItems.RemoveRange(orderToDelete.Items);
                    _context.Orders.Remove(orderToDelete);
                    _context.SaveChanges();
                    RefreshDataSource();
                }
            }
        }
    }
}
