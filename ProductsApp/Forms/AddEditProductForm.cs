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

        public AddEditProductForm(Context context)
        {
            InitializeComponent();

            _context = context;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var product = new Product();
            product.Name = textBox1.Text;
            product.Price = decimal.Parse(textBox2.Text);

            _context.Products.Add(product);
            _context.SaveChanges();

            DialogResult = DialogResult.OK;

            Close();
        }
    }
}
