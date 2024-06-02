using Client.Models;
using Client.Services;
using static Client.Services.PlayerService;

namespace Client
{
    public partial class Login : Form
    {
        private Player _player;
        public Login()
        {
            InitializeComponent();
            setStateControl(false);
            setStateControlDatCuoc(false);
        }

        private void Login_Load(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        void setStateControl(bool state)
        {
            textBox2.Enabled = state;
            button2.Enabled = state;
            dateTimePicker1.Enabled = state;
        }
        void setStateControlDatCuoc(bool state)
        {
            button3.Enabled = state;
      
            numericUpDown1.Enabled = state;
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            string phoneNumber = textBox1.Text;
            PlayerService s = new PlayerService();
            _player = await s.GetPlayer(phoneNumber);
            
            if (_player != null)
            {
                textBox2.Text = _player.fullName;
                dateTimePicker1.Value = _player.dateOfBirth;
                setStateControl(false);
                setStateControlDatCuoc(true);
                loadBoardBet();
            }
            else
            {
                _player = null;
                textBox2.Text = "";

                dateTimePicker1.Value = DateTime.Now;
                setStateControl(true);
                setStateControlDatCuoc(false);
            }

        }
        async void  loadBoardBet()
        {
            PlayerService s = new PlayerService();
            var boardBets  = await s.LoadBoardBet(_player.id);
            dataGridView1.DataSource = boardBets;
        }
        Player getPlayerFromUI()
        {
            return new Player
            {
                fullName = textBox2.Text,
                phoneNumber = textBox1.Text,
                dateOfBirth = dateTimePicker1.Value,
            };
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            PlayerService s = new PlayerService();
            Player? player =  getPlayerFromUI();
            bool p = await s.CreatePlayer(player);
            _player = await s.GetPlayer(player.phoneNumber);
            if (p == true)
            {
                setStateControl(false);
                setStateControlDatCuoc(true);
            }

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            PlayerService s = new PlayerService();
            decimal betNumber = numericUpDown1.Value;
            var i = (int) betNumber;
            var result = await s.PlayerBet(_player.id, i);
            if (result)
            {
                MessageBox.Show("Cược thành công");
            }
            else
            {
                MessageBox.Show("Cược thất bại");
            }
        }

    }
}
