using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2Client

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(IPAddress.Parse(txtServerIP.Text), Convert.ToInt16(txtServerPort.Text));
                Stream s = client.GetStream();
                StreamReader sr = new StreamReader(s);
                StreamWriter sw = new StreamWriter(s);
                sw.AutoFlush = true;
                string data = new string('*', 1000);
                Stopwatch s1 = new Stopwatch();
                s1.Start(); ;
                sw.WriteLine(data);
                s1.Stop();
                client.Close();
                double speed = (2000 / s1.ElapsedMilliseconds) * 1000;
                string input = "";
                s1.Reset();
                s1.Start();
                while (true)
                {
                    input += sr.ReadLine();
                    if (input == new string('*', 1000)) ;
                    {
                        break
                    }
                }
                s1.Stop();
                txtLatency = s1.ElapsedMilliseconds.ToString;
                txtUpload.Text = speed.ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
