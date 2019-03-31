using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemTrading
{
    public partial class Main : Form
    {
        private KiwoomAPI _kiwoomApi;
        public Main()
        {
            InitializeComponent();
            // TODO : Kiwoom API OCX Initialize
            _kiwoomApi = new KiwoomAPI(_openApi);
            _kiwoomApi.OnConnectEventHandler = OnConnectEventHandler;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginToolStripMenuItem.Enabled = false;
            _kiwoomApi.CommConnect();
        }

        private void OnConnectEventHandler(bool result)
        {
            if(result)
            {

            }
            else
            {
                loginToolStripMenuItem.Enabled = true;
            }

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

    }
}
