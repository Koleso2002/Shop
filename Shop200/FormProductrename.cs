using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SportLib;

namespace Shop200
{
    public partial class FormProductrename : Form
    {
        private Product _product;
        public FormProductrename(int id)
        {
            InitializeComponent();
            using(Connection connection = new())
            {
                _product=connection.Products.Find(id);
                if(_product == null)
                {
                    throw new ArgumentNullException("Product is null");
                }
                textBox1.Text = _product.Name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != String.Empty)
            {
                using (Connection connection = new())
                {
                    _product.Name = textBox1.Text;
                    connection.Update(_product);
                    connection.SaveChanges();
                    this.Close();
                }
            }
        }
    }
}
