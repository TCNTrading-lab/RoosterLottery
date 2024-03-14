using Client.Services;

namespace Client
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            GreetingService s = new GreetingService();
            string? message = await s.GetGreetingMessage();
            if (message != null)
            {
                MessageBox.Show(message);
            }
        }
    }
}
