using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2Server
{
    public partial class Form1 : Form
    {
        TcpListener listener;
        bool running = false;
        Thread t;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtServerStatus.Text = "Not Running";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (running == false)
            {

                try
                {
                    listener = new TcpListener(IPAddress.Parse(txtServerIP.Text), Convert.ToInt16(txtPort.Text));
                    listener.Start();
                    txtServerStatus.Text = "Listening";
                     t = new Thread(new ThreadStart(Service));
                    t.IsBackground = true;
                    t.Start();
                    Service();
                    running = true;
                    button1.Text = "Stop";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    running = false;
                    button1.Text = "Start";
                    if (t != null && t.IsAlive == true)
                    {
                        t.Abort();
                    }
                    listener.Stop();
                }
                catch (Exception ex)
                {
                  //  MessageBox.Show(ex.Message);
                }

            }

        }
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            running = false;
        }

        public void Service()
        {
            while (running == true)
            {
                Socket soc = listener.AcceptSocket();
                try
                {
                    Invoke(new Action(() => { txtServerStatus.Text = "Connected"; }));
                    Stream s = new NetworkStream(soc);
                    StreamReader sr = new StreamReader(s);
                    StreamWriter sw = new StreamWriter(s);
                    sw.AutoFlush = true;
                    String text = ""; 
                    while (running == true)
                    {
                        string line = sr.ReadLine();
                        if(text == new string('*', 1000))
                        {
                            break;
                        }
                    }

                    Stopwatch stop = new Stopwatch();
                    string data = new string('*', Convert.ToInt16(50) * 500);
                    stop.Start();
                    sw.WriteLine(data);
                    stop.Stop();
                    txtDownload.Text = ((Convert.ToInt16(50) * 500) / stop.ElapsedMilliseconds).ToString();
                    s.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                soc.Close();
            }

        }

        private void txtDownload_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
