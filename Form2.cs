using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proton_Text_Editor
{
    public partial class FindForm : Form
    {
        //RichTextBox rtb;
        int findCutLength = 0;
        public FindForm()//RichTextBox _rtb)
        {
            InitializeComponent();
            //rtb = _rtb;
        }

        public int FindText(ref RichTextBox richtextbox, string findText, ref int findCutLength, bool reg)
        {
            if(reg == true)//регистр
            {
                if(richtextbox.Text.Contains(findText))
                {
                    string text = richtextbox.Text;
                    string nextText = text.Remove(0, findCutLength);
                    int resultPosition = nextText.IndexOf(findText);

                    if(resultPosition != -1)
                    {
                        richtextbox.Select(resultPosition + findCutLength, findText.Length);
                        richtextbox.ScrollToCaret();
                        richtextbox.Focus();
                        findCutLength += findText.Length + resultPosition;
                    }
                    else if (resultPosition == -1 && findCutLength != 0)
                    {
                        findCutLength = 0;
                        return FindText(ref richtextbox, findText, ref findCutLength, reg);
                    }
                }
                else
                {
                    findCutLength = 0;
                    MessageBox.Show("Не найдено.", "Proton", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if(richtextbox.Text.ToLower().Contains(findText.ToLower()))
                {
                    string text = richtextbox.Text.ToLower();
                    string nextText = text.Remove(0, findCutLength);
                    int resultPosition = nextText.IndexOf(findText.ToLower());

                    if (resultPosition != -1)
                    {
                        richtextbox.Select(resultPosition + findCutLength, findText.Length);
                        richtextbox.ScrollToCaret();
                        richtextbox.Focus();
                        findCutLength += findText.Length + resultPosition;
                    }
                    else if (resultPosition == -1 && findCutLength != 0)
                    {
                        findCutLength = 0;
                        return FindText(ref richtextbox, findText, ref findCutLength, reg);
                    }
                }
                else
                {
                    findCutLength = 0;
                    MessageBox.Show("Не найдено.", "Proton", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            if (main != null)
            {
                if (checkBox1.CheckState == CheckState.Checked)
                {
                    FindText(ref main.richTextBox1, textBox1.Text, ref findCutLength, true);
                }
                else
                {
                    FindText(ref main.richTextBox1, textBox1.Text, ref findCutLength, false);
                }
            }

        }

        private void FindForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            findCutLength = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            findCutLength = 0;
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            findCutLength = 0;
        }
    }
}
