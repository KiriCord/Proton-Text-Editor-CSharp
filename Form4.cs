using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proton_Text_Editor
{
    public partial class GoToForm : Form
    {
        int index;
        int lines1;
        public GoToForm()
        {
            InitializeComponent();
        }
        
        private void GoToForm_Load(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            index = main.richTextBox1.SelectionStart;
            lines1 = main.richTextBox1.GetLineFromCharIndex(index) + 1;
            label1.Text = "Номер строки (1-" + lines1.ToString() + ") :";

            int sel = main.richTextBox1.GetLineFromCharIndex(main.richTextBox1.SelectionStart) + 1;
            textBox1.Text = sel.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            int line = int.Parse(textBox1.Text);
            main.richTextBox1.SelectionStart = main.richTextBox1.GetFirstCharIndexFromLine(line - 1);
            main.richTextBox1.ScrollToCaret();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int sel;
            Form1 main = this.Owner as Form1;

            index = main.richTextBox1.SelectionStart;
            lines1 = main.richTextBox1.GetLineFromCharIndex(index) + 1;
            label1.Text = "Номер строки (1-" + lines1.ToString() + ") :";

            RichTextBox tb = new RichTextBox();
            tb.Text = main.richTextBox1.Text;
            int lines = tb.Lines.Length;

            if (textBox1.Text == "")
                button1.Enabled = false;
            else if (!int.TryParse(textBox1.Text, out sel))
            {
                button1.Enabled = false;
            }
            else if (int.Parse(textBox1.Text) > main.richTextBox1.Lines.Length)
            {
                button1.Enabled = false;
            }
            else if (textBox1.Text == "0")
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }
    }
}
