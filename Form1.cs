using System;
using System.IO;
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
    public partial class Form1 : Form
    {

        Timer timer;
        string FilePath = null;
        bool isTextEdit = false;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Proton Text Editor - Безымянный";
            toolStripStatusLabel2.Text = " | Сохранён";
            timer = new Timer() { Interval = 100 };
            timer.Tick += timer_Tick;
            timer.Start();

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int index = richTextBox1.SelectionStart;
            int line = richTextBox1.GetLineFromCharIndex(index) + 1;
            int colum = richTextBox1.Text.Length;
            int selectlen = richTextBox1.SelectionLength;
            toolStripStatusLabel1.Text = "Стр " + line + " Симв " + colum + " Выдел " + selectlen;

        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "" && isTextEdit == true)
            {
                DialogResult SaveBeforClose = MessageBox.Show("Вы хотите сохранить изменения перед созданием нового файла?",
                                                                "Сохранение перед выходом",
                                                                MessageBoxButtons.YesNoCancel,
                                                                MessageBoxIcon.Warning,
                                                                MessageBoxDefaultButton.Button1,
                                                                MessageBoxOptions.DefaultDesktopOnly);
                if (SaveBeforClose == DialogResult.Yes)
                {
                    if (FilePath != null)
                        сохранитьToolStripMenuItem_Click(sender, e);
                    else
                        сохранитьКакToolStripMenuItem_Click(sender, e);
                    richTextBox1.Text = "";
                    isTextEdit = false;
                    toolStripStatusLabel2.Text = " | Сохранён";
                    FilePath = null;
                    this.Text = "Proton Text Editor - Безымянный";
                    this.Activate();
                }

                if (SaveBeforClose == DialogResult.No)
                {
                    richTextBox1.Text = "";
                    isTextEdit = false;
                    toolStripStatusLabel2.Text = " | Сохранён";
                    FilePath = null;
                    this.Text = "Proton Text Editor - Безымянный";
                    this.Activate();
                }

                if (SaveBeforClose == DialogResult.Cancel)
                    this.Activate();
            }
            else
            {
                this.Text = "Proton Text Editor - Безымянный";
                richTextBox1.Text = "";
                FilePath = null;
                isTextEdit = false;
                toolStripStatusLabel2.Text = " | Сохранён";
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isTextEdit == true && richTextBox1.Text != "")
            {
                DialogResult SaveBeforClose = MessageBox.Show("Вы хотите сохранить изменения перед открытием нового файла?",
                                                                "Сохранение перед выходом",
                                                                MessageBoxButtons.YesNoCancel,
                                                                MessageBoxIcon.Warning,
                                                                MessageBoxDefaultButton.Button1,
                                                                MessageBoxOptions.DefaultDesktopOnly);
                if (SaveBeforClose == DialogResult.Yes)
                {
                    if (FilePath != null)
                        сохранитьToolStripMenuItem_Click(sender, e);
                    else
                        сохранитьКакToolStripMenuItem_Click(sender, e);
                    //Текстовый файл (*.txt)|*.txt|Все файлы (*.*)|*.*"
                    openFileDialog1.Filter = "Текстовый файл (*.txt)|*.txt|Текст в формате (*.rtf)|*.rtf|Все файлы (*.*)|*.*";
                    openFileDialog1.FileName = "";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                        FilePath = openFileDialog1.FileName;
                        this.Text = "Proton Text Editor - " + openFileDialog1.FileName;
                        isTextEdit = false;
                        toolStripStatusLabel2.Text = " | Сохранён";
                    }

                }

                if (SaveBeforClose == DialogResult.No)
                {
                    openFileDialog1.Filter = "Текстовый файл (*.txt)|*.txt|Текст в формате (*.rtf)|*.rtf|Все файлы (*.*)|*.*";
                    openFileDialog1.FileName = "";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                        FilePath = openFileDialog1.FileName;
                        this.Text = "Proton Text Editor - " + openFileDialog1.FileName;
                        isTextEdit = false;
                        toolStripStatusLabel2.Text = " | Сохранён";
                    }
                }

                if (SaveBeforClose == DialogResult.Cancel)
                    this.Activate();

            }
            else
            {
                openFileDialog1.Filter = "Текстовый файл (*.txt)|*.txt|Текст в формате (*.rtf)|*.rtf|Все файлы (*.*)|*.*";
                openFileDialog1.FileName = "";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                    FilePath = openFileDialog1.FileName;
                    this.Text = "Proton Text Editor - " + openFileDialog1.FileName;
                    isTextEdit = false;
                    toolStripStatusLabel2.Text = " | Сохранён";
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FilePath != null)
            {
                StreamWriter streamWrt = new StreamWriter(FilePath);
                streamWrt.WriteLine(richTextBox1.Text);
                streamWrt.Close();
                isTextEdit = false;
                toolStripStatusLabel2.Text = " | Сохранён";
            }
            else
            {
                saveFileDialog1.Filter = "Текстовый файл (*.txt)|*.txt|Текст в формате (*.rtf)|*.rtf|Все файлы (*.*)|*.*";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                    FilePath = saveFileDialog1.FileName;
                    this.Text = "Proton Text Editor - " + saveFileDialog1.FileName;
                    isTextEdit = false;
                    toolStripStatusLabel2.Text = " | Сохранён";
                }
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Текстовый файл (*.txt)|*.txt|Текст в формате (*.rtf)|*.rtf|Все файлы (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                this.Text = "Proton Text Editor - " + saveFileDialog1.FileName;
                FilePath = saveFileDialog1.FileName;
                isTextEdit = false;
                toolStripStatusLabel2.Text = " | Сохранён";
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Сообщение о сохранении перед закрытием
            if (isTextEdit == true && richTextBox1.Text != "")
            {
                DialogResult SaveBeforClose = MessageBox.Show("Вы хотите сохранить изменения перед закрытием программы?",
                                                                "Сохранение перед выходом",
                                                                MessageBoxButtons.YesNoCancel,
                                                                MessageBoxIcon.Warning,
                                                                MessageBoxDefaultButton.Button1,
                                                                MessageBoxOptions.DefaultDesktopOnly);
                if (SaveBeforClose == DialogResult.Yes)
                {
                    if (FilePath != null)
                        сохранитьToolStripMenuItem_Click(sender, e);
                    else
                        сохранитьКакToolStripMenuItem_Click(sender, e);
                    Environment.Exit(0);
                }

                if (SaveBeforClose == DialogResult.No)
                    Environment.Exit(0);
                if (SaveBeforClose == DialogResult.Cancel)
                    this.Activate();
            }
            else
                Environment.Exit(0);
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
                richTextBox1.Undo();
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanRedo)
                richTextBox1.Redo();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void выделитьВсёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void найтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindForm Find = new FindForm();
            Find.Owner = this;
            Find.Show();

        }
        private void заменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceForm Replace = new ReplaceForm();
            Replace.Owner = this;
            Replace.Show();
        }

        private void перейтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToForm GoTo = new GoToForm();
            GoTo.Owner = this;
            GoTo.Show();
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }

        private void цветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
            }
        }

        //Меню с иконками
        private void toolStripButton1_Click(object sender, EventArgs e) => создатьToolStripMenuItem_Click(sender, e);
        private void toolStripButton2_Click(object sender, EventArgs e) => открытьToolStripMenuItem_Click(sender, e);
        private void toolStripButton3_Click(object sender, EventArgs e) => сохранитьToolStripMenuItem_Click(sender, e);
        private void toolStripButton4_Click(object sender, EventArgs e) => сохранитьКакToolStripMenuItem_Click(sender, e);
        private void toolStripButton7_Click(object sender, EventArgs e) => вырезатьToolStripMenuItem_Click(sender, e);
        private void toolStripButton8_Click(object sender, EventArgs e) => копироватьToolStripMenuItem_Click(sender, e);
        private void toolStripButton9_Click(object sender, EventArgs e) => вставитьToolStripMenuItem_Click(sender, e);
        private void toolStripButton10_Click(object sender, EventArgs e) => отменитьToolStripMenuItem_Click(sender, e);
        private void toolStripButton11_Click(object sender, EventArgs e) => повторитьToolStripMenuItem_Click(sender, e);
        private void toolStripButton12_Click(object sender, EventArgs e) => найтиToolStripMenuItem_Click(sender, e);
        private void toolStripButton13_Click(object sender, EventArgs e) => заменитьToolStripMenuItem_Click(sender, e);
        private void toolStripButton14_Click(object sender, EventArgs e) => перейтиToolStripMenuItem_Click(sender, e);

        private void toolStripButton15_Click(object sender, EventArgs e) => шрифтToolStripMenuItem_Click(sender, e);

        private void toolStripButton16_Click(object sender, EventArgs e) => цветToolStripMenuItem_Click(sender, e);

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            toolStripButton17.Checked = true;
            toolStripButton18.Checked = false;
            toolStripButton19.Checked = false;
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            toolStripButton17.Checked = false;
            toolStripButton18.Checked = true;
            toolStripButton19.Checked = false;
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
            toolStripButton17.Checked = false;
            toolStripButton18.Checked = false;
            toolStripButton19.Checked = true;
        }

        //Контекстное меню
        private void отменитьToolStripMenuItem1_Click(object sender, EventArgs e) => отменитьToolStripMenuItem_Click(sender, e);
        private void повторитьToolStripMenuItem1_Click(object sender, EventArgs e) => повторитьToolStripMenuItem_Click(sender, e);
        private void вырезатьToolStripMenuItem1_Click(object sender, EventArgs e) => вырезатьToolStripMenuItem_Click(sender, e);
        private void копироватьToolStripMenuItem1_Click(object sender, EventArgs e) => копироватьToolStripMenuItem_Click(sender, e);
        private void вставитьToolStripMenuItem1_Click(object sender, EventArgs e) => вставитьToolStripMenuItem_Click(sender, e);
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }
        private void выделитьВсёToolStripMenuItem1_Click(object sender, EventArgs e) => выделитьВсёToolStripMenuItem_Click(sender, e);

        private void Form1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            int index = richTextBox1.SelectionStart;
            int line = richTextBox1.GetLineFromCharIndex(index) + 1;
            int colum = richTextBox1.Text.Length;
            int selectlen = richTextBox1.SelectionLength;
            toolStripStatusLabel1.Text = "Стр " + line + " Симв " + colum + " Выдел " + selectlen;

            if (richTextBox1.CanUndo)
            {
                отменитьToolStripMenuItem.Enabled = true;
                отменитьToolStripMenuItem1.Enabled = true;
                toolStripButton10.Enabled = true;

            }
            else
            {
                отменитьToolStripMenuItem.Enabled = false;
                отменитьToolStripMenuItem1.Enabled = false;
                toolStripButton10.Enabled = false;
            }

            if (richTextBox1.CanRedo)
            {
                повторитьToolStripMenuItem.Enabled = true;
                повторитьToolStripMenuItem1.Enabled = true;
                toolStripButton11.Enabled = true;

            }
            else
            {
                повторитьToolStripMenuItem.Enabled = false;
                повторитьToolStripMenuItem1.Enabled = false;
                toolStripButton11.Enabled = false;
            }

            if (richTextBox1.Text == "")
            {
                //Меню               
                вырезатьToolStripMenuItem.Enabled = false;
                копироватьToolStripMenuItem.Enabled = false;
                очиститьToolStripMenuItem.Enabled = false;
                выделитьВсёToolStripMenuItem.Enabled = false;
                найтиToolStripMenuItem.Enabled = false;

                //Контекстное меню                              
                вырезатьToolStripMenuItem1.Enabled = false;
                копироватьToolStripMenuItem1.Enabled = false;
                удалитьToolStripMenuItem.Enabled = false;
                выделитьВсёToolStripMenuItem1.Enabled = false;

                //С иконками
                toolStripButton7.Enabled = false;
                toolStripButton8.Enabled = false;
                toolStripButton12.Enabled = false;
                //toolStripButton13.Enabled = false;
                //toolStripButton14.Enabled = false;
            }
            else
            {

                isTextEdit = true;
                //Меню               
                вырезатьToolStripMenuItem.Enabled = true;
                копироватьToolStripMenuItem.Enabled = true;
                очиститьToolStripMenuItem.Enabled = true;
                выделитьВсёToolStripMenuItem.Enabled = true;
                найтиToolStripMenuItem.Enabled = true;

                //Контекстное меню               
                вырезатьToolStripMenuItem1.Enabled = true;
                копироватьToolStripMenuItem1.Enabled = true;
                удалитьToolStripMenuItem.Enabled = true;
                выделитьВсёToolStripMenuItem1.Enabled = true;

                //С иконками
                toolStripButton7.Enabled = true;
                toolStripButton8.Enabled = true;
                toolStripButton12.Enabled = true;
                //toolStripButton13.Enabled = true;
                //toolStripButton14.Enabled = true;
            }

            if (isTextEdit == true)
                toolStripStatusLabel2.Text = " | Не сохранён";
            else
                toolStripStatusLabel2.Text = " | Сохранён";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isTextEdit == true && richTextBox1.Text != "")
            {
                DialogResult SaveBeforClose = MessageBox.Show("Вы хотите сохранить изменения перед закрытием программы?",
                                                                "Сохранение перед выходом",
                                                                MessageBoxButtons.YesNoCancel,
                                                                MessageBoxIcon.Warning,
                                                                MessageBoxDefaultButton.Button1,
                                                                MessageBoxOptions.DefaultDesktopOnly);
                if (SaveBeforClose == DialogResult.Yes)
                {
                    if (FilePath != null)
                        сохранитьToolStripMenuItem_Click(sender, e);
                    else
                        сохранитьКакToolStripMenuItem_Click(sender, e);
                    Environment.Exit(0);
                }

                if (SaveBeforClose == DialogResult.No)
                    Environment.Exit(0);
                if (SaveBeforClose == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    this.Activate();
                }
            }
            else
                Environment.Exit(0);
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Info = MessageBox.Show("Proton Text Editor \n Версия - 0.1 \n Сделал: Рахманов Артур",
                                                                "Справка",
                                                                MessageBoxButtons.OK,
                                                                MessageBoxIcon.Information,
                                                                MessageBoxDefaultButton.Button1,
                                                                MessageBoxOptions.DefaultDesktopOnly);
            if (Info == DialogResult.OK)
                this.Activate();
        }
    }
}
