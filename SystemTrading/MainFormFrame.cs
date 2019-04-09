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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
            );

        private KiwoomAPI _kiwoomApi;
        public Main()
        {
            InitializeComponent();
            // TODO : Kiwoom API OCX Initialize
            _kiwoomApi = new KiwoomAPI(_openApi);
            _kiwoomApi.OnConnectEventHandler = OnConnectEventHandler;

            // Status Field Bindings.
            _kiwoomApi.OnRealTimeMessageCountModifiedHandler = RealTimeCounterModified;

        }

        private void RealTimeCounterModified(int newCount)
        {
            toolStripStatusLabelCount.Text = newCount.ToString();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loginToolStripMenuItem.Enabled = false;
            if(loginToolStripMenuItem.Checked)
            {
                // Login Status. Log out needed.
                MessageBox.Show("Kiwoom API can't support Disconnection from Server. If you want to reset channel, you should restart this application.",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.ServiceNotification);
                loginToolStripMenuItem.Enabled = true;
            }
            else
            {
                // Not Logined. So we should try to connect to server.
                _kiwoomApi.CommConnect();
            }
        }

        private void SetLoginStatus(bool flag)
        {
            if(flag)
            {
                loginToolStripMenuItem.Text = "Logout";
                loginToolStripMenuItem.Checked = true;
                loginToolStripMenuItem.Enabled = true;
                toolStripStatusLabelConnected.Text = "Connected";
                registerNRTInfoToolStripMenuItem.Enabled = true;
            }
            else
            {
                loginToolStripMenuItem.Text = "Login";
                loginToolStripMenuItem.Checked = false;
                loginToolStripMenuItem.Enabled = true;
                toolStripStatusLabelConnected.Text = "Disconnected";
                registerNRTInfoToolStripMenuItem.Enabled = false;
            }
        }

        private void OnConnectEventHandler(bool result)
        {
            if (result)
            {
                SetLoginStatus(true);
            }
            else
            {
                loginToolStripMenuItem.Enabled = true;
            }

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void RegisterNrtInfo()
        {
            if (_kiwoomApi.IsConnected())
            {
                _kiwoomApi.RegisterRealTime(GetRecommendedStocks());
                toolStripStatusLabelRegistered.Text = "Registered";
                registerNRTInfoToolStripMenuItem.Checked = true;
            }
        }

        private void UnregisterNrtInfo()
        {
            if (_kiwoomApi.IsConnected())
            {
                _kiwoomApi.UnRegisterRealTime();
                toolStripStatusLabelRegistered.Text = "Unregistered";
                registerNRTInfoToolStripMenuItem.Checked = false;
            }
        }

        private void registerNRTInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            registerNRTInfoToolStripMenuItem.Enabled = false;
            if(registerNRTInfoToolStripMenuItem.Checked)
            {
                // Registered status. 
                UnregisterNrtInfo();
            }
            else
            {
                // Unregistered status. Registration needed.
                RegisterNrtInfo();
            }
            registerNRTInfoToolStripMenuItem.Enabled = true;
        }

        private string[] GetRecommendedStocks()
        {
            // TODO : this function should be replaced with result of recommend engine.
            string[] rtn = {
                "003300",
                "003030",
                "005620",
                "009160",
                "029460",
                "033160",
                "004960",
                "194510",
                "023600",
                "004800",
                "127710",
                "074130",
                "122450",
                "005960",
                "900290",
                "013580",
                "023960",
                "012630",
                "005990",
                "086460",
                "036010",
                "013120",
                "900270",
                "900300",
                "034730",
                "006840",
                "002460",
                "003830",
                "042420",
                "030530",
                "054800",
                "039020",
                "034300",
                "016590",
                "001880",
                "006260",
                "008060",
                "900250",
                "009200",
                "900280",
                "121800",
                "138040",
                "006650",
                "036000",
                "016250",
                "005710",
                "140290",
                "042670",
                "123890",
                "009410",
                "032190",
                "065130",
                "007680",
                "011370",
                "004140",
                "218710",
                "034830",
                "159650",
                "006580",
                "088130",
                "024660",
                "007540",
                "017000",
                "076340",
                "175330",
                "267290",
                "079660",
                "002030",
                "001070",
                "043370",
                "083930",
                "900040",
                "012320",
                "045100",
                "900310",
                "078930",
                "067310",
                "000860",
                "091340",
                "114920",
                "018310",
                "049070",
                "000210",
                "088910",
                "002310",
                "009580",
                "016610",
                "002200",
                "010780",
                "095570",
                "139130",
                "000480",
                "049430",
                "007340",
                "004970",
                "006740",
                "115570",
                "037460",
                "072870",
                "010960",
                "012700",
                "900180",
                "004870",
                "007330",
                "042040",
                "097950",
                "090730",
                "036190",
                "023590",
                "140910",
                "036530",
                "017890",
                "024120",
                "025900",
                "001040",
                "030610",
                "081580",
                "192530",
                "009970",
                "051360",
                "079370",
                "037330",
                "095720",
                "001340",
                "194860",
                "031330",
                "005720",
                "000030",
                "000300",
                "024110",
                "037710",
                "165270",
                "035620",
                "017650",
                "161000",
                "006120",
                "000400",
                "037350",
                "138290",
                "015230",
                "003540",
                "092590",
                "001630",
                "002710",
                "155900",
                "008870",
                "002870",
                "011170",
                "002170",
                "001130",
                "042370",
                "011560",
                "052330",
                "086790",
                "094970",
                "000240",
                "035890",
                "140070",
                "010470",
                "067990",
                "003380",
                "000660",
                "004490",
                "007690",
                "004000",
                "054050",
                "016740",
                "003530",
                "138930",
                "067830",
                "123100",
                "900080",
                "267850",
                "006220",
                "001810",
                "017670",
                "007160",
                "003550",
                "009440",
                "221980",
                "084110",
                "006360",
                "189690",
                "017940",
                "030210",
                "053050",
                "101330",
                "005880",
                "073560",
                "003240",
                "004360",
                "009310",
                "039570",
                "005870",
                "083450",
                "058730",
                "038010",
                "000590",
                "224810",
                "224110",
                "290740",
                "267270",
                "105560",
                "067920",
                "003470",
                "008260",
                "109860",
                "063760",
                "057050",
                "007530",
                "192390",
                "024800",
                "005430",
                "001200",
                "264660",
                "045300",
                "083310",
                "039010",
                "016090",
                "046310",
                "031310",
                "053270",
                "021320",
                "900340",
                "092780",
                "000880",
                "000540",
                "012620",
                "032560",
                "001500",
                "016710",
                "010280",
                "131290",
                "152330",
                "034310",
                "014530",
                "005010",
                "069510",
                "031980",
                "000070",
                "092300",
                "153360",
                "013570",
                "141000",
                "115310",
                "069960",
                "093190",
                "055550",
                "094820",
                "049830",
                "001720",
                "042600",
                "021050",
                "212560",
                "104110",
                "248170",
                "072710",
                "079940",
                "000850",
                "045060",
                "001570",
                "047040",
                "163560",
                "281820",
                "033660",
                "014710",
                "001390",
                "271400",
                "023760",
                "036800",
                "092070",
                "077360",
                "000700",
                "060540",
                "060240",
                "101160",
                "000370",
                "015350",
                "002840",
                "011780",
                "084690",
                "299670",
                "000990",
                "044450",
                "001270",
                "004560",
                "005930",
                "012800",
                "005810",
                "016800",
                "005390",
                "003160",
                "004890",
                "104830",
                "066570",
                "002000",
                "040910",
                "058850",
                "208890",
                "290120",
                "066900",
                "035000",
                "120030",
                "120240",
                "024720",
                "002810",
                "086670",
                "036460",
                "068790",
                "035810",
                "241770",
                "064960",
                "126700",
                "053610",
                "214330",
                "014280",
                "090410",
                "001940",
                "263020",
                "161390",
                "003570",
                "071050",
                "008560",
                "107590",
                "260490",
                "155660",
                "004450",
                "093050",
                "023000",
                "294870",
                "024830",
                "028150",
                "108380",
                "092440",
                "003960",
                "077970",
                "088790",
                "008900",
                "036200",
                "039340",
                "002410",
                "210610",
                "030200",
                "017480",
                "094840",
                "004590",
                "210540",
                "017390",
                "032080",
                "085620",
                "104480",
                "119850",
                "073110",
                "059090",
                "088350",
                "095660",
                "025530",
                "272550",
                "039240",
                "041650",
                "002320",
                "151750",
                "102710",
                "066620",
                "040160",
                "100030",
                "139480",
                "001800",
                "900260",
                "058650",
                "282690",
                "900110",
                "091810",
                "035510",
                "160980",
                "011790",
                "005440",
                "069730",
                "256090",
                "036830",
                "213500",
                "096770",
                "006140",
                "036220",
                "042110",
                "093520",
                "025750",
                "032280",
                "045660",
                "065690",
                "145990",
                "053210",
                "056360",
                "006090",
                "002350",
                "010420",
                "042660",
                "069260",
                "041520",
                "054410",
                "264900",
                "172580",
                "079440",
                "126560",
                "003100",
                "052900",
                "032830",
                "015710",
                "003200",
                "038390",
                "011500",
                "041440",
                "010400",
                "003800",
                "035600",
                "058430",
                "021960",
                "180060",
                "149950",
                "001250",
                "215050",
                "016360",
                "036810",
                "056190",
                "085670",
                "300720",
                "008490",
                "091120",
                "079430",
                "058860",
                "003690",
                "187870",
                "203450",
                "023910",
                "035610",
                "025550",
                "250000",
                "078020",
                "006660",
                "005830",
                "004840",
                "004170",
                "012330",
                "019440",
                "208140",
                "130740",
                "095610",
                "024880",
                "112190",
                "219130",
                "001450",
                "000680",
                "204690",
                "015890",
                "138250",
                "036560",
                "110790",
                "001560",
                "039440",
                "011280",
                "049520",
                "000720",
                "005490",
                "268280",
                "051600",
                "003650",
                "244880",
                "104460",
                "122990",
                "012200",
                "002290",
                "208710",
                "123700",
                "232140",
                "014570",
                "101170",
                "078340",
                "028100",
                "086390",
                "029530",
                "221670",
                "298690",
                "000430",
                "256940",
                "010240",
                "058400",
                "241560",
                "009300",
                "036930",
                "029780",
                "095340",
                "004250",
                "014440",
                "130660",
                "042500",
                "014130",
                "014830",
                "255440",
                "034590",
                "074600",
            };
            return rtn;
        }

        private void toolStripStatusLabelConnected_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabelCount_Click(object sender, EventArgs e)
        {

        }
    }
}
