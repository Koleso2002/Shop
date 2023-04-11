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
    public partial class FormShop : Form
    {
        Form _main;
        public FormShop(Form main)
        {
            InitializeComponent();
            _main = main;
            FormClosed += FormShop_FormClosed;
        }

        private void FormShop_FormClosed(object? sender, FormClosedEventArgs e)
        {
            _main.Visible = true;
        }

        public void ShowForm()
        {
            _main.Visible = false;
            ShowAllShop();
            this.ShowDialog();
        }
        private async void Add(string address)
        {
            using(Connection context=new Connection())
            {
                if ( context.Shop.Any(row => 
                String.Equals(row.Address,address))) { return; }
                
                
                var row = new Shop()
                {
                    Address = address
                };
                context.Shop.Add(row);
                await context.SaveChangesAsync();
                ShowAllShop();
            }
        }
        List<Shop> shops;
        private void ShowAllShop()
        {
            using(Connection connect=new Connection())
            {
                shops=connect.Shop.ToList();
                listBox1.Items.Clear();
                listBox1.Items.AddRange(shops.Select(row => row.Address).ToArray());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                Add(textBox1.Text);
            }
        }
    }
}
