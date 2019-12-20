using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClient
{
   
    public partial class Form1 : Form
    { 
        HubConnection connection;
        public Form1()
        {
            InitializeComponent();

            label1.Text = "";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44305/eventHub")
                .Build();

            connection.On<string>("SendNoticeEventToClient", (message) =>
            {
                MessageBox.Show(message);
            });

            try
            {
                await connection.StartAsync();
                label1.Text += "Connection started";
            }
            catch (Exception ex)
            {
                label1.Text += ex.Message;
            }
        }
    }
}
