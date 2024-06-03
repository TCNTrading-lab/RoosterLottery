using Client.Models;
using Client.Services;
using System.Text.RegularExpressions;

namespace Client
{
    public partial class Main : Form
    {
        private Player _player;
        public Main()
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
                clearDbGridView();
            }

        }
        async void  loadBoardBet()
        {
            PlayerService s = new PlayerService();
            var boardBets  = await s.LoadBoardBet(_player.id);
            dataGridView1.DataSource = boardBets;
        }
        void clearDbGridView()
        {
            dataGridView1.DataSource = null;
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


            //bool q = checkValidPhoneNumber(player.phoneNumber);
            //q = true;
            //if (q)
            //{
                
            //}

            bool p = await s.CreatePlayer(player);
            _player = await s.GetPlayer(player.phoneNumber);
            if (p == true)
            {
                setStateControl(false);
                setStateControlDatCuoc(true);
            }
        }
        bool checkValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^(0[3|5|7|8|9])+([0-9]{8})$";
            Regex regex = new Regex(pattern);
            bool isValid = regex.IsMatch(phoneNumber);
            if (isValid)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Số điện thoại không hợp lệ.");
                return false;
            }
        }
        private async void button3_Click(object sender, EventArgs e)
        {
            //var now = DateTime.Now;
            //if (now.Minute == 0)
            //{
            //    MessageBox.Show("Cược thất bại do đang ở phút đầu tiên của giờ");
            //    return;
            //}
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
