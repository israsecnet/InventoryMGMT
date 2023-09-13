using System.Globalization;

namespace InventoryMGMT
{
    public partial class Add_Modify_Part : Form
    {
        public Add_Modify_Part()
        {
            InitializeComponent();
            textBox1.ReadOnly = true;
            textBox1.Text = (Inventory.AllParts.Count + 1).ToString();
        }

        public Add_Modify_Part(InHousePart inhousepart)
        {
            InitializeComponent();
            textBox1.Text = inhousepart.PartID.ToString();
            textBox2.Text = inhousepart.Name;
            textBox3.Text = inhousepart.Inventory.ToString();
            textBox4.Text = inhousepart.Price.ToString();
            textBox5.Text = inhousepart.Max.ToString();
            textBox7.Text = inhousepart.Min.ToString();
            textBox6.Text = inhousepart.MachineID.ToString();
            radioButton1.Checked = true;

        }

        public Add_Modify_Part(OutsourcedPart outsourcedpart)
        {
            InitializeComponent();
            textBox1.Text = outsourcedpart.PartID.ToString();
            textBox2.Text = outsourcedpart.Name;
            textBox3.Text = outsourcedpart.Inventory.ToString();
            textBox4.Text = outsourcedpart.Price.ToString();
            textBox5.Text = outsourcedpart.Max.ToString();
            textBox7.Text = outsourcedpart.Min.ToString();
            textBox6.Text = outsourcedpart.CompanyName.ToString();
            radioButton2.Checked = true;
        }

        private void cancel_window(object sender, EventArgs e)
        {
            this.Close();
        }

        private void inhousepartdisplay(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                this.label6.Text = "Machine ID";
            }
        }

        private void outsourcedpartdisplay(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                this.label6.Text = "Company Name";
            }
        }
        private void savepart(object sender, EventArgs e)
        {
            var isNumeric1 = !string.IsNullOrEmpty(textBox2.Text);
            var isNumeric2 = !string.IsNullOrEmpty(textBox3.Text) && textBox3.Text.All(Char.IsDigit);
            var isNumeric3 = !string.IsNullOrEmpty(textBox5.Text) && textBox5.Text.All(Char.IsDigit);
            var isNumeric4 = !string.IsNullOrEmpty(textBox7.Text) && textBox7.Text.All(Char.IsDigit);
            var isNumeric5 = !string.IsNullOrEmpty(textBox6.Text);

            bool invcheck = false;
            if (int.Parse(textBox3.Text) >= int.Parse(textBox7.Text) && int.Parse(textBox3.Text) <= int.Parse(textBox5.Text))
            {
                invcheck = true;
            }
            else
            {
                MessageBox.Show("Inventory must be between Max and Min Values!", "Save Error");
            }

            if (isNumeric1 && isNumeric2 && isNumeric3 && isNumeric4 && isNumeric5)
            {
                if (radioButton1.Checked && label1.Text == "Modify Part")
                {
                    if (int.Parse(textBox5.Text) > int.Parse(textBox7.Text) && invcheck)
                    {
                        InHousePart inpart = new InHousePart(int.Parse(this.textBox1.Text), textBox2.Text, int.Parse(this.textBox3.Text), decimal.Parse(this.textBox4.Text, NumberStyles.Currency), int.Parse(this.textBox5.Text), int.Parse(this.textBox7.Text), int.Parse(this.textBox6.Text));
                        Inventory.updateinPart(int.Parse(this.textBox1.Text), inpart);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Max must be greater than Min!", "Save Error");
                    }
                }
                else if (radioButton2.Checked && label1.Text == "Modify Part")
                {
                    if (int.Parse(textBox5.Text) > int.Parse(textBox7.Text) && invcheck)
                    {
                        OutsourcedPart outpart = new OutsourcedPart(int.Parse(this.textBox1.Text), textBox2.Text, int.Parse(this.textBox3.Text), decimal.Parse(this.textBox4.Text, NumberStyles.Currency), int.Parse(this.textBox5.Text), int.Parse(this.textBox7.Text), this.textBox6.Text);
                        Inventory.updateoutPart(int.Parse(this.textBox1.Text), outpart);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Max must be greater than Min!");
                    }
                }
                else if (radioButton1.Checked && label1.Text == "Add Part")
                {
                    if (int.Parse(textBox5.Text) > int.Parse(textBox7.Text) && invcheck)
                    {
                        InHousePart inpart = new InHousePart((Inventory.AllParts.Count + 1), textBox2.Text, int.Parse(this.textBox3.Text), decimal.Parse(this.textBox4.Text, NumberStyles.Currency), int.Parse(this.textBox5.Text), int.Parse(this.textBox7.Text), int.Parse(this.textBox6.Text));
                        Inventory.addPart(inpart);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Max must be greater than Min!");
                    }
                }
                else
                {
                    if (int.Parse(textBox5.Text) > int.Parse(textBox7.Text) && invcheck)
                    {
                       
                        OutsourcedPart outpart = new OutsourcedPart((Inventory.AllParts.Count + 1), textBox2.Text, int.Parse(this.textBox3.Text), decimal.Parse(this.textBox4.Text, NumberStyles.Currency), int.Parse(this.textBox5.Text), int.Parse(this.textBox7.Text), this.textBox6.Text);
                        Inventory.addPart(outpart);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Max must be greater than Min!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Value Error - Check numeric Inputs (Inventory, Max, Min, MachineID, Part ID) !", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
