using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulatePro1.Pinyin;

namespace SimulatePro1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PYTool pytool = PYTool.GetInstance();

            string pinyin = pytool.GetPY(sourceTextBox.Text);
            targetTextLabel.Text = pinyin;
        }
    }
}
