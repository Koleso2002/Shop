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
    public partial class FormProduct : Form
    {
        private Form1 _form;
        private List<Product> _products;
        public FormProduct(Form1 form1)
        {
            InitializeComponent();
            _form = form1;
            this.FormClosed += FormProduct_FormClosed;
            button1.Click += Button1_Click;
            listBox1.DoubleClick += ListBox1_DoubleClick;
            ShowProduct();
            
        }

        private async void ListBox1_DoubleClick(object? sender, EventArgs e)
        {
            int index=listBox1.SelectedIndex;
            if (index !=-1)
            {
                if(MessageBox.Show("Удалить?",
                                    _products[index].Name,
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Warning)==DialogResult.OK)
                using(Connection connect = new())
                {
                    int id = _products[index].Id;
                    var row = connect.Products.Find(id);
                    connect.Products.Remove(row);
                    await connect.SaveChangesAsync();
                }
                ShowProduct();
            }
        }

        private async void Button1_Click(object? sender, EventArgs e)
        {
            if (textBox1.Text != String.Empty)
            {
                using(Connection connection=new Connection())
                {
                    var row = new Product()
                    {
                        Name = textBox1.Text
                    };
                    connection.Products.Add(row);
                    await connection.SaveChangesAsync();
                    textBox1.Text = String.Empty;
                }
                ShowProduct();
            }
        }

        private void ShowProduct()
        {
            using (Connection connection=new Connection())
            {
                _products = connection.Products.ToList();
            }
            listBox1.Items.Clear();
            foreach (var item in _products)
            {
                listBox1.Items.Add(item.Name);
            }
        }

        private void FormProduct_FormClosed(object? sender, FormClosedEventArgs e)
        {
            _form.Visible = true;
        }

        public void ShowForm()
        {
            _form.Visible = false;
            this.ShowDialog();
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var index=listBox1.IndexFromPoint(e.Location);
            listBox1.SelectedIndex = index;
            if (index != ListBox.NoMatches)
            {
                var id=_products[index].Id;
                contextMenuStrip1.Show(Cursor.Position);
                contextMenuStrip1.Visible = true;
            }

        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            int id=_products[index].Id;
            FormProductrename form = new FormProductrename(id);

            form.ShowDialog();
            ShowProduct();

        }
    }
}
