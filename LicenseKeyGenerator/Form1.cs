using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LicenseKeyGenerator
{
    public partial class Form1 : Form
    {
        static string alphanumericlower = "abcdefghijklmnopqrstuvwxyz01234567890";
        static string alphanumerichigher = "ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890";
        static string alphanumericany = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890";
        static string numeric = "01234567890";
        static string alphalower = "abcdefghijklmnopqrstuvwxyz";
        static string alphahigher = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static string alphaany = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string isError = "False";
        public static int dupl = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 2;
            comboBox2.SelectedIndex = 0;
        }
        static string Generate(string Charset, int chars, int groups, string separator)
        {
            if (chars == 0 && groups == 0)
            {
                isError = "True";
                return "Characters and groups cannot be 0.";
            }
            else if (chars == 0)
            {
                isError = "True";
                return "Characters cannot be 0.";
            }
            else if (groups == 0)
            {
                isError = "True";
                return "Groups cannot be 0.";
            }
            else
            {
                try
                {
                    Random random = new Random();
                    string final = "";
                    for (int i = 0; i < groups; i++)
                    {
                        for (int j = 0; j < chars; j++)
                        {
                            final = final + Charset.Substring(random.Next(0, Charset.Length), 1);
                        }
                        final = final + separator;
                    }
                    return final.Substring(0, final.Length - 1);
                }
                catch
                {
                    isError = "True";
                    return "Unknown error.";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(textBox1.Text == ""))
            {
                string chrset = "";
                list.Items.Clear();
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        switch(comboBox2.SelectedIndex)
                        {
                            case 0:
                                chrset = alphahigher;
                                break;
                            case 1:
                                chrset = alphalower;
                                break;
                            case 2:
                                chrset = alphaany;
                                break;
                            default:
                                return;
                        }
                        break;
                    case 1:
                        chrset = numeric;
                        break;
                    case 2:
                        switch (comboBox2.SelectedIndex)
                        {
                            case 0:
                                chrset = alphanumerichigher;
                                break;
                            case 1:
                                chrset = alphanumericlower;
                                break;
                            case 2:
                                chrset = alphanumericany;
                                break;
                            default:
                                return;
                        }
                        break;
                    default:
                        return;
                }
                for (int i = 0; i < Convert.ToInt32(amount.Value); i++)
                {
                    string toAdd = Generate(chrset, Convert.ToInt32(chars.Value), Convert.ToInt32(groups.Value), textBox1.Text);
                    if (!(list.Items.Contains(toAdd)))
                    {
                        list.Items.Add(toAdd);
                    }
                    else
                    {
                        //MessageBox.Show("discarded "+toAdd);
                        dupl++;
                    }
                    //System.Threading.Thread.Sleep(5);
                }
                if (dupl > 0)
                {
                    for (int i = 0; i < dupl; i++)
                    {
                        string toAdd = Generate(chrset, Convert.ToInt32(chars.Value), Convert.ToInt32(groups.Value), textBox1.Text);
                        if (!(list.Items.Contains(toAdd)))
                        {
                            list.Items.Add(toAdd);
                        }
                        else
                        {
                            dupl++;
                        }
                    }
                }
                dupl = 0;
                if (isError == "True")
                {
                    list.Items.Clear();
                    label6.Visible = true;
                    label6.Text = Generate(chrset, Convert.ToInt32(chars.Value), Convert.ToInt32(groups.Value), textBox1.Text);
                    button3.Visible = true;
                    isError = "False";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label6.Visible = false;
            button3.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            about about = new about();
            about.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string contents = "";
            foreach (string item in list.Items)
            {
                contents = contents + item + "\n";
            }
            s.DefaultExt = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (s.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(s.FileName, contents);
            }
        }
    }
}
