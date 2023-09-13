using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryMGMT
{
    public partial class Add_Modify_Product : Form
    {
        BindingList<Part> partstoAdd = new BindingList<Part>();
        public Add_Modify_Product()
        {
            InitializeComponent();
            textBox1.ReadOnly = true;
            textBox1.Text = (Inventory.Products.Count + 1).ToString();

            var bindingPart = new BindingSource();
            bindingPart.DataSource = Inventory.AllParts;
            dataGridView1.DataSource = bindingPart;
            dataGridView1.Columns["PartID"].HeaderText = "Part ID";
            dataGridView1.Columns["Name"].HeaderText = "Name";
            dataGridView1.Columns["Inventory"].HeaderText = "Inventory";
            dataGridView1.Columns["Price"].HeaderText = "Price";
            dataGridView1.Columns["Max"].HeaderText = "Max";
            dataGridView1.Columns["Min"].HeaderText = "Min";

            var bindingProduct = new BindingSource();
            bindingProduct.DataSource = partstoAdd;
            dataGridView2.DataSource = bindingProduct;
            dataGridView2.Columns["PartID"].HeaderText = "Part ID";
            dataGridView2.Columns["Name"].HeaderText = "Name";
            dataGridView2.Columns["Inventory"].HeaderText = "Inventory";
            dataGridView2.Columns["Price"].HeaderText = "Price";
            dataGridView2.Columns["Max"].HeaderText = "Max";
            dataGridView2.Columns["Min"].HeaderText = "Min";
        }
        public Add_Modify_Product(Product currentProd)
        {
            InitializeComponent();
            textBox1.Text = currentProd.ProductID.ToString();
            textBox2.Text = currentProd.Name;
            textBox3.Text = currentProd.Inventory.ToString();
            textBox4.Text = currentProd.Price.ToString();
            textBox5.Text = currentProd.Max.ToString();
            textBox7.Text = currentProd.Min.ToString();

            var bindingPart = new BindingSource();
            bindingPart.DataSource = Inventory.AllParts;
            dataGridView1.DataSource = bindingPart;
            dataGridView1.Columns["PartID"].HeaderText = "Part ID";
            dataGridView1.Columns["Name"].HeaderText = "Name";
            dataGridView1.Columns["Inventory"].HeaderText = "Inventory";
            dataGridView1.Columns["Price"].HeaderText = "Price";
            dataGridView1.Columns["Max"].HeaderText = "Max";
            dataGridView1.Columns["Min"].HeaderText = "Min";

            foreach (Part part in currentProd.AssociatedParts)
            {
                partstoAdd.Add(part);
            }

            var bindingProduct = new BindingSource();
            bindingProduct.DataSource = partstoAdd;
            dataGridView2.DataSource = bindingProduct;
            dataGridView2.Columns["PartID"].HeaderText = "Part ID";
            dataGridView2.Columns["Name"].HeaderText = "Name";
            dataGridView2.Columns["Inventory"].HeaderText = "Inventory";
            dataGridView2.Columns["Price"].HeaderText = "Price";
            dataGridView2.Columns["Max"].HeaderText = "Max";
            dataGridView2.Columns["Min"].HeaderText = "Min";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cancel_window(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Yes)
            {
                Part parttoRemove = (Part)dataGridView2.CurrentRow.DataBoundItem;
                partstoAdd.Remove(parttoRemove);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Part parttoAdd = (Part)dataGridView1.CurrentRow.DataBoundItem;
            partstoAdd.Add(parttoAdd);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var isNumeric1 = !string.IsNullOrEmpty(textBox1.Text) && textBox1.Text.All(Char.IsDigit);
            var isNumeric2 = !string.IsNullOrEmpty(textBox3.Text) && textBox3.Text.All(Char.IsDigit);
            var isNumeric3 = !string.IsNullOrEmpty(textBox5.Text) && textBox5.Text.All(Char.IsDigit);
            var isNumeric4 = !string.IsNullOrEmpty(textBox7.Text) && textBox7.Text.All(Char.IsDigit);

            bool addingprod = false;
            if (isNumeric1 && int.Parse(textBox1.Text) > Inventory.Products.Count)
            {
                addingprod = true;
            }

            

            if (isNumeric1 && isNumeric2 && isNumeric3 && isNumeric4)
            {
                if (int.Parse(textBox5.Text) > int.Parse(textBox7.Text))
                {
                    if (int.Parse(this.textBox3.Text) <= int.Parse(this.textBox5.Text) && int.Parse(this.textBox3.Text) >= int.Parse(this.textBox7.Text))
                    {
                        if (addingprod)
                        {
                            Product newProd = new Product(int.Parse(textBox1.Text), textBox2.Text, int.Parse(this.textBox3.Text), decimal.Parse(this.textBox4.Text, NumberStyles.Currency), int.Parse(this.textBox5.Text), int.Parse(this.textBox7.Text));
                            foreach (Part newPart in partstoAdd)
                            {
                                newProd.AddAssociatedPart(newPart);
                            }

                            Inventory.addProduct(newProd);
                            this.Close();
                        }
                        else
                        {
                            Product updatedProd = new Product(int.Parse(textBox1.Text), textBox2.Text, int.Parse(this.textBox3.Text), decimal.Parse(this.textBox4.Text, NumberStyles.Currency), int.Parse(this.textBox5.Text), int.Parse(this.textBox7.Text));
                            foreach (Part newPart in partstoAdd)
                            {
                                updatedProd.AddAssociatedPart(newPart);
                            }

                            Inventory.updateProduct(int.Parse(textBox1.Text), updatedProd);
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Inventory must be below max and above min", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   
                }
                else
                {
                    MessageBox.Show("Max must be greater than Min!", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Value Error - Check numeric Inputs (Inventory, Max, Min, MachineID, Part ID) !", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

            private void button1_Click(object sender, EventArgs e)
        {
            int searchValue = int.Parse(textBox6.Text);
            Part match = Inventory.lookupPart(searchValue);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Part part = (Part)row.DataBoundItem;

                if (part.PartID == match.PartID)
                {
                    row.Selected = true;
                    break;
                }
                else
                {
                    row.Selected = false;
                }
            }
        }
    }
}
