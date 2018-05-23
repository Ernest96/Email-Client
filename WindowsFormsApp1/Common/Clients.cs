using S22.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Common
{
    class Clients
    {
        public volatile SmtpClient smtpClient;
        public volatile ImapClient imapClient;
        private User user;
        public bool HasInternet = false;

        public Clients()
        {
            NetworkChange.NetworkAvailabilityChanged += AvailabilityChanged;
        }

        public void Init()
        {
            this.user = EmailClient.Instance.user;
            HasInternet = NetworkInterface.GetIsNetworkAvailable();
            if (HasInternet)
            {
                try
                {
                    InitSmtpClient();
                    InitImapClient();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Can't connect to protocols. {e.Message}");
                }
            }
        }

        private void InitSmtpClient()
        {
            smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential(user.username, user.password);
            smtpClient.EnableSsl = true;
        }

        private void InitImapClient()
        {
            imapClient = new ImapClient("imap.gmail.com", 993,
                    user.username, user.password, AuthMethod.Login, true);
        }

        private void AvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable && user != null)
            {
                HasInternet = e.IsAvailable;
                this.Init();
            }
            else
            {
                HasInternet = e.IsAvailable;
            }
        }
    }
}
