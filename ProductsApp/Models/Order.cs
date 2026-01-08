using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductItem> Items { get; set; } = new List<ProductItem>();
        public decimal FinalPrice { get; set; }
    }
}
