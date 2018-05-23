using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Common;

namespace WindowsFormsApp1
{
    public partial class LoginForm : Form
    {
        public static bool isShowing;

        public LoginForm()
        {
            InitializeComponent();
            this.TopMost = true;
            this.FormClosed += new FormClosedEventHandler(f_FormClosed);
            isShowing = true;
        }

        private async void LoginClick(object sender, EventArgs e)
        {
            string login = loginBox.Text;
            string password = passBox.Text;
            bool success = await EmailClient.Instance.LoginUser(login, password);
            if (!success)
            {
                MessageBox.Show("This email is not supported or is not in valid format");
                return;
            }

            this.HideLogin();

            if (RememberMe.Checked)
            {
                Task.Run(() => User.CacheUser(login, password));
            }

            EmailClient.Instance.mainForm.ChangeLoadLabel("Loading messages...");

            await Task.Run(() => {
                EmailClient.Instance.StartClient();
                EmailClient.Instance.GetMessagesForced();
            });

            EmailClient.Instance.mainForm.ChangeLoadLabel("Message list");
        }

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            User user = await EmailClient.Instance.LoadUser();

            if (user != null)
            {
                loginBox.Text = user.username;
                passBox.Text = user.password;
                this.RememberMe.Checked = true;
            }
            
        }

        void f_FormClosed(object sender, FormClosedEventArgs e)
        {
            isShowing = false;
        }

        void HideLogin()
        {
            this.Hide();
            isShowing = false;
        }

        public void ShowLogin()
        {
            isShowing = true;
            this.Show();
        }

    }
}
