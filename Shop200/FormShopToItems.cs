using SportLib;
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
    public partial class FormShopToItems : Form
    {
        public FormShopToItems()
        {
            InitializeComponent();
            ShowProduct();
            ShowShops();
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        private void ComboBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex > -1)
            {
                int fk = _product[comboBox1.SelectedIndex].Id;
                ShowItems(fk);
            }
        }

        List<Shop> _shops = null;
        List<Characteristic> _characteristics = null;
        private void ShowShops()
        {
            using(Connection conn=new Connection())
            {
                //var table = from item in conn.Characteristics
                //            join itemToShop in conn.ShopToCharacteristics
                //            on item.Id equals itemToShop.CharacteristicId
                //            join shop in conn.Shop on itemToShop.ShopId equals shop.Id
                //            select new { item, itemToShop, shop };


                _shops = conn.Shop.ToList();
                _characteristics = conn.Characteristics.ToList();

                listBox1.Items.AddRange(_shops.Select(row => row.Address).ToArray());
                                
            }
        }

        private void ShowItems(int fk)
        {
            using(Connection conn=new Connection())
            {
                _characteristics = conn.Characteristics.Where(
                    row=>row.ProductId==fk).ToList();
                    listBox2.Items.Clear();
                foreach (var item in _characteristics)
                {
                    listBox2.Items.Add($"{item.Name}|{item.Color}|{item.Price}");
                }
            }
        }

        List<Product> _product;
        private void ShowProduct()
        {
            using (Connection conn = new Connection())
            {
                _product=conn.Products.ToList();
                comboBox1.Items.AddRange(_product.Select(row=> row.Name).ToArray());
            }
        }

    }
}
