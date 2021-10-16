using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace WFA_20210929
{
    public partial class LWP02Main : Form
    {
        Color defColor;
        public LWP02Main()
        {
            InitializeComponent();

            String[] checkItems = {"Прозрачность", "Овальное окно"};
            checkedListBox1.Items.AddRange(checkItems);
            checkedListBox1.CheckOnClick = true;
        }

        private void exitStripMenuItem_Click(object sender, EventArgs e)
        {
            timerOneSecond.Start();
            statusProgressBar.Value = 100;
            //this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newButton newButton = new newButton();
            this.Controls.Add(newButton);
            newButton.Location = new Point(17, 70);
            newButton.BringToFront();
            newButton.Click += newButton_Click;
            button2.Enabled = true;
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            try
            {
                (Controls["newButton"] as Button).Text = "It's Alive!";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            foreach (Control control in Controls)
            {
                TextBox TB = control as TextBox;
                if (TB != null)
                {
                    if (TB.Name == "textBox1")
                    {
                        TB.Text = "REPLACED";
                    }

                }
            }
        }



        private void exitToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            statusLabel.Text = $"{exitToolStripMenuItem.Text} ({exitToolStripMenuItem.ShortcutKeyDisplayString})";
        }

        private void mouseLeave(object sender, EventArgs e)
        {
            statusLabel.Text = "";
            //statusProgressBar.Value = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Controls.Remove((Controls["newButton"] as Button));

            Controls.Remove(button4);
            button4.Dispose();
            button2.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            statusProgressBar.Value = 0;
            statusProgressBar.Step = 34;

            TextBox[] TB = new TextBox[Convert.ToInt32(numericUpDown1.Value)];

            for (int i = 0; i < TB.Length; i++)
            {
                statusProgressBar.PerformStep();
                TB[i] = new TextBox();
                TB[i].Name = $"textBox{i}";
                TB[i].Size = new Size(75, 23);
                TB[i].TabIndex = i;
                TB[i].Location = new Point(380 + i * 80, 50);
                Controls.Add(TB[i]);
                TB[i].BringToFront();
                statusLabel.Text = $"Массив элементов создан ({statusProgressBar.Value}%)";

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            //SaveFileDialog sf = new SaveFileDialog();
            if (selectColor.ShowDialog() == DialogResult.OK)
            {
                defColor = this.BackColor;
                this.BackColor = selectColor.Color;
                statusLabel.Text = $"Selected color: {selectColor.Color}";
                button6.Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //this.BackColor = DefaultBackColor;
            this.BackColor = defColor;
            //this.BackColor = Color.Empty;
            statusLabel.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (selectFont.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Font = selectFont.Font;
                    statusLabel.Text = $"Selected Font: {selectFont.Font}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Font = DefaultFont;
            statusLabel.Text = "";
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            Rectangle rectangle = new Rectangle(0, 0, this.Width, this.Height);

            if (checkedListBox1.CheckedItems.Contains("Прозрачность"))
            {
                this.Opacity = Convert.ToDouble(numericAlpha.Value) / 100;

            }
            else
            {
                this.Opacity = 1;
            }

            if(checkedListBox1.CheckedItems.Contains("Овальное окно"))
            {
                graphicsPath.AddEllipse(0, 0, this.Width, this.Height);
                this.Region = new Region(graphicsPath);
            }
            else
            {
                graphicsPath.AddRectangle(rectangle);
                this.Region = new Region(graphicsPath);
            }
        }

        private void numericAlpha_ValueChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Contains("Прозрачность"))
            {
                this.Opacity = Convert.ToDouble(numericAlpha.Value) / 100;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (selectBrowser.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = selectBrowser.SelectedPath;
            }
        }

        private void timerOneMinute_Tick(object sender, EventArgs e)
        {
            Byte seconds = Convert.ToByte(DateTime.Now.Second);
            progressBar1.Value = seconds;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                selectColor.ShowDialog();
                richTextBox1.SelectionColor = selectColor.Color;
            }
            catch
            {

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                selectFont.ShowDialog();
                richTextBox1.SelectionFont = selectFont.Font;
            }
            catch
            {

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                selectColor.ShowDialog();
                richTextBox1.SelectionBackColor = selectColor.Color;
            }
            catch
            {

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            richTextBox1.Focus();
            richTextBox1.SelectAll();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                fileOpen.ShowDialog();
                Stream stream = fileOpen.OpenFile();
                richTextBox1.LoadFile(stream, RichTextBoxStreamType.RichText);
                stream.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                Stream stream;

                if(fileSave.ShowDialog() == DialogResult.OK)
                {
                    stream = fileSave.OpenFile();
                    memoryStream.Position = 0;
                    richTextBox1.SaveFile(stream, RichTextBoxStreamType.RichText);
                    memoryStream.WriteTo(stream);
                    stream.Close();
                    statusLabel.Text = $"File Saved :: {fileSave.FileName}";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void выхлдToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveQuestion();
        }

        void saveQuestion()
        {
            DialogResult result =
                MessageBox.Show("Save?", "Save File",
                MessageBoxButtons.YesNo);
            switch (result)
            {
                case DialogResult.Yes:
                    saveAsToolStripMenuItem_Click(null, null);
                    Close();
                    break;
                case DialogResult.No:
                    {
                        Close();
                        return;
                    }
            }
        }
    }
}
