using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop200
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Product_Click(object sender, EventArgs e)
        {
            FormProduct product=new FormProduct(this);
            product.ShowForm();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormShop formShop = new FormShop(this);
            formShop.ShowForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormCharacteristic formCharacteristic = new FormCharacteristic();
            formCharacteristic.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormShopToItems form = new FormShopToItems();
            form.ShowDialog();
        }
    }
}
