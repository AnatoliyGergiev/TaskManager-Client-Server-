using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form, IView
    {
        public Form1()
        {
            InitializeComponent();
            Presenter pr = new Presenter(this);
        }
        public event EventHandler<string> OnExchange;
        public event EventHandler OnReceive;
        public event EventHandler<string> OnConnect;
        public event EventHandler OnDisconnect;
        public ListBox ListBox1
        {
            get { return listBox1; }
        }
        public void Message(string msg)
        {
           MessageBox.Show(msg);
        }
        private void button_Get_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                if (OnExchange != null)
                    OnExchange(sender, button_Get.Text);
                //Exchange(button_Get.Text);
                if (OnReceive != null)
                    OnReceive(sender, null);
                //Receive(sock);
            }
            catch (Exception ex)
            {
                MessageBox.Show("\nКлиент: " + ex.Message + "\n");
                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close(); 
            }
        }

        private void button_Kill_Click(object sender, EventArgs e)
        {
            try
            {
                if (OnExchange != null)
                    OnExchange(sender, button_Kill.Text + (string)listBox1.SelectedItem);
                //Exchange(button_Kill.Text + (string)listBox1.SelectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show("\nКлиент: " + ex.Message + "\n");
                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close(); 
            }

        }

        private void button_Create_Click(object sender, EventArgs e)
        {
            try
            {
                if (OnExchange != null)
                    OnExchange(sender, button_Create.Text + textBox_Create.Text);
                //Exchange(button_Create.Text + textBox_Create.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("\nКлиент: " + ex.Message + "\n");
                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close(); 
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (OnDisconnect != null)
                    OnDisconnect(sender, null);
                //sock.Shutdown(SocketShutdown.Both); 
                //sock.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Клиент: " + ex.Message);
            }
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                if (button_Connect.Text == "Connect to server")
                {
                    if (OnConnect != null)
                        OnConnect(sender, textBox_IP.Text);
                    //Connect();
                    button_Connect.Text = "Disconnect";
                }
                else
                {
                    if (OnExchange != null)
                        OnExchange(sender, "Disconnect");
                    //Exchange("Disconnect");
                    button_Connect.Text = "Connect to server";
                    if (OnDisconnect != null)
                        OnDisconnect(sender, null);
                    //sock.Shutdown(SocketShutdown.Both); 
                    //sock.Close(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("\nКлиент: " + ex.Message + "\n");
                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close(); 
            }
        }
    }
}
