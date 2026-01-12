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
    public partial class AddEditProductForm : Form
    {
        Context _context;
        Product _productToEdit;

        public AddEditProductForm(Context context)
        {
            InitializeComponent();

            _context = context;
        }

        public AddEditProductForm(Context context, Product productToEdit)
        {
            InitializeComponent();

            _context = context;
            _productToEdit = productToEdit;

        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            var product = new Product();

            if (_productToEdit != null)
            {
                Product? foundProduct = _context.Products.Find(_productToEdit.Id);
                if (foundProduct != null)
                {
                    product = foundProduct;
                }
                product.Name = textBox1.Text;
                product.Price = decimal.Parse(textBox2.Text);
                _context.SaveChanges();
            }
            else 
            {
                product.Name = textBox1.Text;
                product.Price = decimal.Parse(textBox2.Text);

                _context.Products.Add(product);
                _context.SaveChanges();
            }


            DialogResult = DialogResult.OK;

            Close();
        }

        private void AddEditForm_load(object sender, EventArgs e)
        {
            if (_productToEdit != null) 
            {
               textBox1.Text = _productToEdit.Name;
                textBox2.Text = _productToEdit.Price.ToString();

            }
           

        }
    }
}
