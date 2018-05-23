using System;
using System.Net.Mail;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class SendForm : Form
    {
        public static bool isShowing = false;
        private string selectedAtachment = "";

        public SendForm()
        {
            InitializeComponent();
            this.TopMost = true;
            this.FormClosed += new FormClosedEventHandler(f_FormClosed);
            this.attachmentsBox.MouseClick += ItemClick;
            isShowing = true;
        }

        public SendForm(string Body, string Subject, AttachmentCollection attachments) : this()
        {
            this.CCBox.Text = Subject;
            this.MessageBox.Text = Body;
            foreach(var a in attachments)
            {
                attachmentsBox.Items.Add(a.Name);
            }
        }

        void ItemClick(object o, MouseEventArgs e)
        {
            if (attachmentsBox.SelectedItem != null)
            {
                deleteButton.Enabled = true;
                selectedAtachment = attachmentsBox.SelectedItem.ToString();
            }
        }

        void f_FormClosed(object sender, FormClosedEventArgs e)
        {
            isShowing = false;
        }

        void HideSend()
        {
            this.Hide();
            isShowing = false;
        }

        public void ShowSend()
        {
            isShowing = true;
            this.Show();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            if (EmailClient.Instance.AddMessage(ToBox.Text, CCBox.Text, MessageBox.Text, attachmentsBox.Items))
            {
                isShowing = false;
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            DialogResult result = theDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                AddAttachment(theDialog.FileName);
            }
        }

        private void AddAttachment(string filename)
        {
            attachmentsBox.Items.Add(filename);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            attachmentsBox.Items.RemoveAt(attachmentsBox.Items.IndexOf(selectedAtachment));
            deleteButton.Enabled = false;
        }
    }
}
