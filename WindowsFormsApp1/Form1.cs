using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public List<MyProcess> processes = new List<MyProcess>();
        public Form1()
        {
            InitializeComponent();

            TimerCallback timerCallback = new TimerCallback(Method);
            System.Threading.Timer timer = new System.Threading.Timer(timerCallback);
            timer.Change(500, 10000);

            TimerCallback timerForProcess = new TimerCallback(Meth);
            System.Threading.Timer timerSecund = new System.Threading.Timer(timerForProcess);
            timerSecund.Change(500, 1000);

            //Thread thread = new Thread(Method);
            //thread.Start();
        }

        public void Meth(object state)
        {
            for (int i = 0; i < processes.Count; i++)
            {
                if (DateTime.Now >= processes[i].StartTime && !processes[i].IsWorking)
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = processes[i].Path;
                    if (!String.IsNullOrEmpty(processes[i].Arguments))
                        proc.StartInfo.Arguments = processes[i].Arguments;
                    proc.Start();
                    processes[i].ID = proc.Id;
                    processes[i].IsWorking = true;
                    using (StreamWriter sw = new StreamWriter("info.txt", true)) 
                    {
                        sw.WriteLine(proc.Id.ToString() + " " + processes[i].Path + " Start time: " + proc.StartTime.ToString());
                    }
                    //using (FileStream fileStream = new FileStream("info.txt", FileMode.Append, FileAccess.Write)) 
                    //{
                    //    byte[] array = System.Text.Encoding.Default.GetBytes(proc.Id.ToString() + " " + processes[i].Path + " Start time: " + proc.StartTime.ToString());
                    //    fileStream.Write(array, 0, array.Length);
                    //}
                }
            }
            for (int i = 0; i < processes.Count; i++)
            {
                if (DateTime.Now >= processes[i].EndTime)
                {
                    try
                    {
                        Process proc = Process.GetProcessById(processes[i].ID);
                        proc.CloseMainWindow();
                        proc.Close();
                        using (StreamWriter sw = new StreamWriter("info.txt", true))
                        {
                            sw.WriteLine(processes[i].ID.ToString() + " " + processes[i].Path + " End time: " + DateTime.Now.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        processes.Remove(processes[i]);
                    }
                }
            }
        }

        public void Method(object state)
        {
            Process[] processes = Process.GetProcesses();
            listView1.Invoke(new Action(() => listView1.Items.Clear()));
            listView1.Invoke(new Action(() => listView1.Columns.Clear()));
            listView1.Invoke(new Action(() => listView1.View = View.Details));
            listView1.Invoke(new Action(() => listView1.Columns.Add("pId")));
            listView1.Invoke(new Action(() => listView1.Columns.Add("pName")));
            listView1.Invoke(new Action(() => listView1.Columns.Add("pPriority")));
            for (int i = 0; i < processes.Length; i++) 
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.SubItems.Add(processes[i].Id.ToString());
                listViewItem.SubItems.Add(processes[i].ProcessName);
                listViewItem.SubItems.Add(processes[i].BasePriority.ToString());

                listViewItem.SubItems.Remove(listViewItem.SubItems[0]);
                listView1.Invoke(new Action(() => listView1.Items.Add(listViewItem)));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
        }
    }
}
