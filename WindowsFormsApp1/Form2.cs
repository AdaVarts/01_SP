using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        Form1 form1;

        public Form2(Form1 form)
        {
            InitializeComponent();
            form1 = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                form1.processes.Add(new MyProcess { IsWorking = false, Path = textBox1.Text, Arguments = textBox4.Text, StartTime = dateTimePicker1.Value, EndTime = dateTimePicker2.Value });
            }
            Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fileDialog.FileName;
                button1.Enabled = true;
            }

        }
    }
}
