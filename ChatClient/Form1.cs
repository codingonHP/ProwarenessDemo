using Microsoft.AspNet.SignalR.Client;
using System;
using System.Windows.Forms;


namespace ChatClient
{
    public partial class Form1 : Form
    {
        HubConnection connection = new HubConnection("http://localhost:23124/signalr");
        IHubProxy hubProxy;

        public Form1()
        {
            InitializeComponent();
            SetupSigalR();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hubProxy.Invoke("updateUserCount", "hello from winforms");
           
        }

        private void SetupSigalR()
        {
            hubProxy = connection.CreateHubProxy("ChatHub");

            hubProxy.On("updateUserCount", (data) => {
                MessageBox.Show(data);
            });

            connection.Start().Wait();

        }
    }
}
