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
    public partial class AddEditOrderForm : Form
    {
        Context _context;

        List<ProductItem> _items = new List<ProductItem>();

        Order _orderToEdit;

        public AddEditOrderForm(Context context)
        {
            InitializeComponent();
            _context = context;
        }

        public AddEditOrderForm(Context context, Order orderToEdit)
        {
            InitializeComponent();

            _context = context;
            _orderToEdit = orderToEdit;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedProduct = comboBox1.SelectedItem as Product;

            var productItem = new ProductItem();
            productItem.ProductId = selectedProduct.Id;
            productItem.Product = selectedProduct;
            productItem.Count = decimal.Parse(textBox2.Text);

            _items.Add(productItem);

            FillDataGridWithProductItems();
        }

        private void FillDataGridWithProductItems()
        {
            var viewModels = _items.Select(x => new ProductItemViewModel()
            {
                Name = x.Product.Name,
                Count = x.Count,
                Price = x.Count * x.Product.Price
            });

            label2.Text = $"Final sum: {viewModels.Sum(x => x.Price)}";

            bindingSource1.DataSource = viewModels;
            bindingSource1.ResetBindings(false);
        }

        private void AddEditOrderForm_Load(object sender, EventArgs e)
        {
            if (_orderToEdit != null)
            {
                textBox1.Text = _orderToEdit.Name;
                _items = _orderToEdit.Items.ToList();
                FillDataGridWithProductItems();
            }

            productBindingSource.DataSource = _context.Products.ToList();
            dataGridView1.DataSource = bindingSource1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_orderToEdit != null)
            {
                _orderToEdit.Name = textBox1.Text;
                _orderToEdit.FinalPrice = _items.Sum(x => x.Count * x.Product.Price);
                _orderToEdit.Items = _items;
            }
            else
            {
                var order = new Order();
                order.Name = textBox1.Text;
                order.FinalPrice = _items.Sum(x => x.Count * x.Product.Price);
                order.Items = _items;
                _context.Orders.Add(order);
            }

            _context.SaveChanges();

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
