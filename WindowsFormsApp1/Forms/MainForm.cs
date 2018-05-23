using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Common;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private EmailClient.MessageType currentView = EmailClient.MessageType.Seen;

        public MainForm()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(MainForm_FormClosed);
            this.messages.MouseClick += MessageClick;
            this.messages.DoubleClick += ComposeMessage;
            EmailClient.Instance.SetUI(this);
        }

        private async void ComposeMessage(object sender, EventArgs e)
        {
            if (messages.SelectedItem != null && currentView == EmailClient.MessageType.Draft)
            {
                MessageBoxItem selected = messages.SelectedItem as MessageBoxItem;
                var data = await Task.Run(() => EmailClient.Instance.GetMessage(selected.id, currentView));

                SendForm f = new SendForm(data.Body, data.Subject, data.Attachments);
                f.Show();
            }
        }

        private async void MessageClick(object sender, EventArgs e)
        {

            CleanView();
            if (messages.SelectedItem != null)
            {
                MessageBoxItem selected = messages.SelectedItem as MessageBoxItem;
                messageLoading.Visible = true;
                deleteButton.Enabled = true;
                var view = currentView;
                var data = await Task.Run(() => EmailClient.Instance.GetMessage(selected.id, currentView));
                messageLoading.Visible = false;

                if (data == null || view != currentView)
                    return;

                browserView.DocumentText = data.Body;
                if (data.Headers["Date"] != null)
                {
                    messageDate.Text = data?.Headers["Date"].Split('-')[0].Split('+')[0];
                }

                int index = -1;
                attachmentsBox.Items.Clear();
                foreach (var a in data.Attachments)
                {
                    a.ContentId = a.ContentId;
                    attachmentsBox.Items.Add(new AttachmentsBoxItem(++index, a.Name, selected.id));
                }

                if (currentView == EmailClient.MessageType.Sending)
                    topLabel.Text = "To: " + data.To?.ToString();
                else
                    topLabel.Text = "From: " + data.From?.ToString();

            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoginForm f = new LoginForm();
            f.ShowLogin();
        }

        private void loginToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!LoginForm.isShowing)
            {
                LoginForm f = new LoginForm();
                f.ShowLogin();
            }
        }

        public void ChangeLoadLabel(string text)
        {
            loadLabel.Text = text;
        }

        private void sendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SendForm.isShowing)
            {
                SendForm f = new SendForm();
                f.Show();
            }
        }

        private void MainForm_FormClosed(object sender, EventArgs e)
        {
            EmailClient.Instance.SendMessagesForced();
        }

        private void seenButton_Click(object sender, EventArgs e)
        {
            Invoke(new Action(() => {
                if (currentView != EmailClient.MessageType.Seen)
                {
                    CleanView();
                }
                viewName.Text = "Seen";
                currentView = EmailClient.MessageType.Seen;
                ShowMessages(EmailClient.MessageType.Seen, filterBox.Text);
            }));
        }

        private void unseenButton_Click(object sender, EventArgs e)
        {
            Invoke(new Action(() => {
                if (currentView != EmailClient.MessageType.Unseen)
                {
                    CleanView();
                }
                viewName.Text = "Unseen";
                currentView = EmailClient.MessageType.Unseen;
                ShowMessages(EmailClient.MessageType.Unseen, filterBox.Text);
            }));
        }

        private void draftButton_Click(object sender, EventArgs e)
        {
            Invoke(new Action(() => {
                if (currentView != EmailClient.MessageType.Draft)
                {
                    CleanView();
                }
                viewName.Text = "Draft";
                currentView = EmailClient.MessageType.Draft;
                ShowMessages(EmailClient.MessageType.Draft, filterBox.Text);
            }));
        }

        private void sendingButton_Click(object sender, EventArgs e)
        {
            Invoke(new Action(() => {
                if (currentView != EmailClient.MessageType.Sending)
                {
                    CleanView();
                }
                viewName.Text = "Sending queue";
                currentView = EmailClient.MessageType.Sending;
                ShowMessages(EmailClient.MessageType.Sending, filterBox.Text);
            }));
        }

        private async void ShowMessages(EmailClient.MessageType type, string filter)
        {
            var list = await Task.Run(() => EmailClient.Instance.GetMessagesSorted(type, filter));
            Invoke(new Action(() => {
                messages.Items.Clear();
                if (list != null)
                {
                    foreach (var x in list)
                    {
                        messages.Items.Add(x.Value);
                    }
                }
            }));
        }

        private void CleanView()
        {
            Invoke(new Action(() => {
                attachmentsBox.Items.Clear();
                deleteButton.Enabled = false;
                messageLoading.Visible = false;
                topLabel.Text = "";
                messageDate.Text = "";
                browserView.DocumentText = "";
            }));
        }

        public void UpdateCurrentView()
        {
            switch (currentView)
            {
                case EmailClient.MessageType.Seen:
                    seenButton_Click(null, null);
                    break;
                case EmailClient.MessageType.Unseen:
                    unseenButton_Click(null, null);
                    break;
                case EmailClient.MessageType.Draft:
                    draftButton_Click(null, null);
                    break;
                case EmailClient.MessageType.Sending:
                    sendingButton_Click(null, null);
                    break;
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            CleanView();
            UpdateCurrentView();
        }

        private async void deleteButton_Click(object sender, EventArgs e)
        {
            if (messages.SelectedItem != null)
            {
                MessageBoxItem selected = messages.SelectedItem as MessageBoxItem;
                await Task.Run(() => EmailClient.Instance.DeleteMessage(selected.id, currentView));
                CleanView();
                UpdateCurrentView();
            }
        }

        private async void downloadButton_Click(object sender, EventArgs e)
        {
            if (attachmentsBox.SelectedItem != null)
            {
                AttachmentsBoxItem selected = attachmentsBox.SelectedItem as AttachmentsBoxItem;
                var attachment = await Task.Run(() => EmailClient.Instance.
                    DownloadAttachment(selected.index, selected.messageId, currentView));

                SaveFileDialog dialog = new SaveFileDialog();
                dialog.DefaultExt = attachment.Name.Split('.').Last();
                dialog.FileName = attachment.Name;
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var path = dialog.FileName;
                    await Task.Run(() => EmailClient.Instance.SaveAttachment(attachment, path));
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            CleanView();
            UpdateCurrentView();
        }
    }
}
