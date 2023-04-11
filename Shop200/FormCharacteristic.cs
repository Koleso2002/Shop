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
    public partial class FormCharacteristic : Form
    {
        List<Product> _products;
        object _locker = new object();
        List<RadioButton> _radioButtons;

        public FormCharacteristic()
        {
            InitializeComponent();
            Task.Run(ShowAllProduct);
            _radioButtons = new List<RadioButton>()
            {
                radioButton1,radioButton2,radioButton3,radioButton4
            };
            radioButton1.Text = "Черный";
            radioButton2.Text = "Печеный";
            radioButton3.Text = "Белый";
            radioButton4.Text = "Зеленый";
            radioButton1.Checked = true;
            numericUpDown1.Maximum = int.MaxValue;
            numericUpDown1.Minimum = 1;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            button1.Text = "Add item";

            button1.Click += AddItem;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            listBox1.DoubleClick += ListBox1_DoubleClick;
        }

        private void ListBox1_DoubleClick(object? sender, EventArgs e)
        {
            if (_characteristics != null && listBox1.SelectedIndex>-1)
            {
                var delete = _characteristics[listBox1.SelectedIndex];
                using (Connection conn=new Connection())
                {
                    conn.Characteristics.Remove(delete);
                    conn.SaveChanges();
                    ComboBox1_SelectedIndexChanged(null, null);
                }

            }
        }

        List<Characteristic> _characteristics;
        private void ComboBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex > -1)
            {
                using(Connection conn=new Connection())
                {
                    int id = _products[comboBox1.SelectedIndex].Id;
                    listBox1.Items.Clear();
                    _characteristics = conn.Characteristics.Where(c => c.ProductId == id).ToList();
                    foreach (var item in _characteristics)
                    {
                        listBox1.Items.Add($"{item.Name}|{item.Price}|{item.Date.ToLongDateString()}");
                    }
                }
            }
        }

        private async void ErroForm()
        {
            comboBox1.BackColor = Color.Tomato;
            textBox1.BackColor = Color.Tomato;
            numericUpDown1.BackColor = Color.Tomato;
            button1.BackColor = Color.Tomato;
            await Task.Delay(3000);
            comboBox1.BackColor = Color.WhiteSmoke;
            textBox1.BackColor = Color.WhiteSmoke;
            numericUpDown1.BackColor = Color.WhiteSmoke;
            button1.BackColor = Color.WhiteSmoke;


        }
        private async void AddItem(object? sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 ||
                textBox1.Text == String.Empty)
            {
                Task.Run(ErroForm);

                return;
            }
            using (Connection conn = new Connection())
            {
                var item = new Characteristic()
                {
                    Name = textBox1.Text,
                    Price = numericUpDown1.Value,
                    Date = dateTimePicker1.Value,
                    ProductId = _products[comboBox1.SelectedIndex].Id,
                    Color = _radioButtons.FirstOrDefault(row => row.Checked)?.Text

                };
                conn.Characteristics.Add(item);
                await conn.SaveChangesAsync();
                ComboBox1_SelectedIndexChanged(this, null);
            }

        }

        private void ShowAllProduct()
        {
            using (Connection conn = new Connection())
            {
                lock (_locker)
                {
                    _products = conn.Products.ToList();
                }
                comboBox1.Invoke(new Action(
                    () =>
                    {
                        comboBox1.Items.Clear();
                        comboBox1.Items.AddRange(_products.Select(x => x.Name).ToArray());
                    }
                    ));
            }
        }
    }
}
