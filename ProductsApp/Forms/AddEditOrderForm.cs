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

        public AddEditOrderForm(Context context)
        {
            InitializeComponent();
            _context = context;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedProduct = comboBox1.SelectedItem as Product;

            var productItem = new ProductItem();
            productItem.ProductId = selectedProduct.Id;
            productItem.Product = selectedProduct;
            productItem.Count = 1;

            _items.Add(productItem);

            var viewModels = _items.Select(x => new ProductItemViewModel()
            {
                Name = x.Product.Name,
                Count = 1,
                Price = 1 * x.Product.Price
            });

            bindingSource1.DataSource = viewModels;
            bindingSource1.ResetBindings(false);
        }

        private void AddEditOrderForm_Load(object sender, EventArgs e)
        {
            productBindingSource.DataSource = _context.Products.ToList();
            dataGridView1.DataSource = bindingSource1;
        }
    }
}
