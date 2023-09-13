using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace InventoryMGMT
{
    public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();
            MainScreenFormLoad();
        }

        public void MainScreenFormLoad()
        {
            Inventory.PopulateDummyLists();
            var bindingPart = new BindingSource();
            bindingPart.DataSource = Inventory.AllParts;
            dataGridView1.DataSource = bindingPart;

            var bindingProduct = new BindingSource();
            bindingProduct.DataSource = Inventory.Products;
            dataGridView2.DataSource = bindingProduct;

        }

        private void add_part_launch(object sender, EventArgs e)
        {
            new Add_Modify_Part().ShowDialog();
        }

        private void modify_part_launch(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.DataBoundItem.GetType() == typeof(InventoryMGMT.InHousePart))
            {
                InHousePart inhousepart = (InHousePart)dataGridView1.CurrentRow.DataBoundItem;
                var modifypartwindow = new Add_Modify_Part(inhousepart);
                modifypartwindow.label1.Text = "Modify Part";
                modifypartwindow.ShowDialog();
            }
            else
            {
                OutsourcedPart outpart = (OutsourcedPart)dataGridView1.CurrentRow.DataBoundItem;
                var modifypartwindow = new Add_Modify_Part(outpart);
                modifypartwindow.label1.Text = "Modify Part";
                modifypartwindow.ShowDialog();
            }
        }

        private void add_product_launch(object sender, EventArgs e)
        {
            new Add_Modify_Product().ShowDialog();
        }

        private void modify_product_launch(object sender, EventArgs e)
        {
            Product currentProd = (Product)dataGridView2.CurrentRow.DataBoundItem;
            var modifyproductwindow = new Add_Modify_Product(currentProd);
            modifyproductwindow.label1.Text = "Modify Product";
            modifyproductwindow.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Yes)
            {
                Part parttodelete = (Part)dataGridView1.CurrentRow.DataBoundItem;
                Inventory.AllParts.Remove(parttodelete);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Yes)
            {
                Product prodtodelete = (Product)dataGridView2.CurrentRow.DataBoundItem;
                if (prodtodelete.AssociatedParts.Count > 0) 
                {
                    MessageBox.Show("Error: Product has associated parts, please remove all associated parts and re-try", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Inventory.Products.Remove(prodtodelete);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool isInt = !string.IsNullOrEmpty(textBox2.Text) && textBox2.Text.All(Char.IsDigit);
            int searchValue = 0;
            if (isInt)
            {
                searchValue = int.Parse(textBox2.Text);
            }
            else
            {
               searchValue = 0;
            }
            
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
        private void prodsearch(object sender, EventArgs e)
        {
            bool isInt = !string.IsNullOrEmpty(textBox1.Text) && textBox1.Text.All(Char.IsDigit);
            int searchValue = 0;
            if (isInt)
            {
                searchValue = int.Parse(textBox1.Text);
            }
            else
            {
                searchValue = 0;
            }
            Product match = Inventory.lookupProduct(searchValue);

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                Product prod = (Product)row.DataBoundItem;

                if (prod.ProductID == match.ProductID)
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
