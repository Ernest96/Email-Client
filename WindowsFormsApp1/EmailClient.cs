using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Common;

namespace WindowsFormsApp1
{
    class EmailClient
    {
        private static readonly object _mutex = new object();
        private static volatile EmailClient _instance = null;
        private MessageWorker messageWorker;
        public volatile Clients clients;
        public User user;
        public SoundPlayer notification;
        public MainForm mainForm;
        public enum MessageType { Seen, Unseen, Draft, Sending };

        private EmailClient()
        {
            notification = new System.Media.SoundPlayer(Properties.Resources.notification);
            clients = new Clients();
        }

        public static EmailClient Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_mutex)
                    {
                        if (_instance == null)
                        {
                            _instance = new EmailClient();
                        }
                    }
                }
                return _instance;
            }
        }

        public void SetUI(MainForm f)
        {
            mainForm = f;
        }

        public void StartClient()
        {
            if (messageWorker != null)
            {
                messageWorker.Stop();
            }
            clients.Init();
            messageWorker = new MessageWorker(user.username);
        }

        public async Task<bool> LoginUser(string login, string pass)
        {
            if (messageWorker != null)
            {
                SendMessagesForced();
            }
            
            var tempUser = new User { username = login, password = pass };

            if (!tempUser.IsAcceptableEmail())
            {
                return false;
            }

            user = tempUser;
            return true;
        }

        public async Task<User> LoadUser()
        {
            user = User.LoadUser();
            return user;
        }

        public bool AddMessage(string to, string cc, string messageText, ListBox.ObjectCollection attachments)
        {
            string[] destination = to.Split(',');

            foreach (var d in destination)
            {
                try
                {
                    string dest = String.IsNullOrWhiteSpace(d) == true ? "draft@example.com" : d;
                    MailMessage message = new MailMessage(user.username, dest);
                    message.Subject = String.IsNullOrWhiteSpace(cc) ? "(No subject)" : cc;
                    message.Body = messageText;

                    foreach (var a in attachments)
                    {
                        var at = new Attachment(a.ToString());
                        at.Name = a.ToString();
                        message.Attachments.Add(at);
                    }

                    Guid id = Guid.NewGuid();
                    messageWorker.TryAddMessage(id.ToString(), message);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error Adding Message: " + e.Message);
                    return false;
                }
            }

            MessageBox.Show("Your message is sending");
            return true;
        }

        public void SendMessagesForced()
        {
            if (messageWorker != null)
            {
                messageWorker.SendAllMessages();
                messageWorker.SaveUnsendMessages();
                messageWorker.SaveDraftMessages();
                MessageBox.Show("Your client will send all messages before closing. Please wait.");
            }
        }

        public async Task<MailMessage> GetMessage(string id, MessageType type)
        {
            return messageWorker.GetMessage(id, type);
        }

        public void GetMessagesForced()
        {
            if (messageWorker != null)
            {
                messageWorker.CleanAllMessages();
                EmailClient.Instance.mainForm.UpdateCurrentView();
                messageWorker.GetMessages(Settings.GetInitialMessagesCount);
                messageWorker.GetArchivedMessages();
                messageWorker.GetDraftMessages();
                mainForm.UpdateCurrentView();
                messageWorker.StartWorkers();
            }
        }

        public IEnumerable<KeyValuePair<string, MessageBoxItem>> GetMessagesSorted(EmailClient.MessageType type, string filter)
        {
            if (messageWorker == null)
                return null;

            SortedList<string, MessageBoxItem> list = new SortedList<string, MessageBoxItem>();
            ConcurrentDictionary<string, MailMessage> store;

            switch (type)
            {
                case EmailClient.MessageType.Seen:
                    store = messageWorker.SeenMessages;
                    break;
                case EmailClient.MessageType.Unseen:
                    store = messageWorker.UnseenMessages;
                    break;
                case EmailClient.MessageType.Draft:
                    store = messageWorker.DraftMessages;
                    break;
                case EmailClient.MessageType.Sending:
                    store = messageWorker.PendingMessages;
                    break;
                default:
                    return null;
            }

            if (store != null)
            {
                foreach (var x in store)
                {
                    if (x.Value != null &&
                       (x.Value.Subject.ToLower().Contains(filter.ToLower()) ||
                       x.Value.From.ToString().ToLower().Contains(filter.ToLower())))
                    {
                        if (String.IsNullOrEmpty(x.Value.Subject))
                        {
                            x.Value.Subject = "(No Subject)";
                        }
                        var temp = new MessageBoxItem(x.Value.Subject, x.Key);
                        temp.attachments = x.Value.Attachments;
                        list.Add(temp.id, temp);
                    }
                }
            }

            var res = list.Reverse();
            return res;
        }

        public async Task SaveAttachment(Attachment attachment, string path)
        {
            try
            {
                using (var fileStream = File.Create(path))
                {
                    attachment.ContentStream.Seek(0, SeekOrigin.Begin);
                    attachment.ContentStream.CopyTo(fileStream);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Can't save attachment {e.Message}");
            }
        }

        public async Task<Attachment> DownloadAttachment(int index, string messageId, MessageType type)
        {
            return messageWorker.GetAttachment(index, messageId, type);
        }

        public async Task DeleteMessage(string id, MessageType type)
        {
            messageWorker.DeleteMessage(id, type);
        }
    }
}