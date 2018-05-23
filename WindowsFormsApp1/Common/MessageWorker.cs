using S22.Imap;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Tools;

namespace WindowsFormsApp1.Common
{
    class MessageWorker
    {
        public volatile ConcurrentDictionary<string, MailMessage> PendingMessages = new ConcurrentDictionary<string, MailMessage>();
        public volatile ConcurrentDictionary<string, MailMessage> SeenMessages = new ConcurrentDictionary<string, MailMessage>();
        public volatile ConcurrentDictionary<string, MailMessage> UnseenMessages = new ConcurrentDictionary<string, MailMessage>();
        public volatile ConcurrentDictionary<string, MailMessage> DraftMessages = new ConcurrentDictionary<string, MailMessage>();
        private readonly object _lockSend = new object();
        private readonly object _lockGet= new object();
        private string id;
        private bool workToken;
        private CountdownEvent countdownEvent;

        public void TryAddMessage(string id, MailMessage message)
        {
            if (message.To.ToString() == "draft@example.com")
            {
                DraftMessages.TryAdd(id, message);
            }
            else
            {
                PendingMessages.TryAdd(id, message);
            }
            EmailClient.Instance.mainForm.UpdateCurrentView();
        }

        public MessageWorker(string id)
        {
            this.id = id;
        }

        public void StartWorkers()
        {
            workToken = true;
            Task.Run(() => DoGetWork());
            Task.Run(() => DoSendWork());
            Task.Run(() => DoCleanWork());
        }

        public void Stop()
        {
            workToken = false;
        }

        private void DoSendWork()
        {
            while (workToken)
            {
                try
                {
                    SendAllMessages();
                    Thread.Sleep(Settings.SendTime);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error Sending Message: " + e.Message);
                }
            }
        }

        private void DoGetWork()
        {
            while (workToken)
            {
                try
                {
                    GetMessages(Settings.GetMessagesCount);
                    Thread.Sleep(Settings.GetTime);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error Sending Message: " + e.Message);
                }
            }
        }

        private void DoCleanWork()
        {
            while (workToken)
            {
                try
                {
                    CleanMessages(Settings.CleanMessagesCount);
                    Thread.Sleep(Settings.CleanTime);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error Sending Message: " + e.Message);
                }
            }
        }

        private void CleanMessages(int count)
        {
            try
            {
                if (EmailClient.Instance.clients.HasInternet && EmailClient.Instance.clients.imapClient != null)
                {
                    IEnumerable<uint> unseenIds = EmailClient.Instance.clients.imapClient.Search(SearchCondition.Unseen());
                    unseenIds = unseenIds.Skip(Math.Max(0, unseenIds.Count() - count));

                    foreach (var m in UnseenMessages)
                    {
                        if (!unseenIds.Contains(UInt32.Parse(m.Key)))
                        {
                            UnseenMessages.TryRemove(m.Key, out _);
                        }
                    }

                    IEnumerable<uint> seenIds = EmailClient.Instance.clients.imapClient.Search(SearchCondition.Seen());
                    seenIds = seenIds.Skip(Math.Max(0, seenIds.Count() - count));

                    foreach (var m in SeenMessages)
                    {
                        if (!seenIds.Contains(UInt32.Parse(m.Key)))
                        {
                            SeenMessages.TryRemove(m.Key, out _);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // ignore
            }
        }

        private void GetUnseenMessages(int count)
        {
            try
            {
                IEnumerable<uint> unseenIds = EmailClient.Instance.clients.imapClient.Search(SearchCondition.Unseen());
                unseenIds = unseenIds.Skip(Math.Max(0, unseenIds.Count() - count));
                IEnumerable<MailMessage> unseenMessages = EmailClient.Instance.clients.imapClient.GetMessages(unseenIds, FetchOptions.HeadersOnly, false);
                bool notify = false;

                for (int i = 0; i < unseenIds.Count(); i++)
                {
                    string key = unseenIds.ElementAt(i).ToString();
                    if (!UnseenMessages.ContainsKey(key))
                    {
                        UnseenMessages.TryAdd(key, unseenMessages.ElementAt(i));
                        EmailClient.Instance.mainForm.UpdateCurrentView();
                        notify = true;
                    }
                }

                if (notify)
                {
                    Task.Run(() => EmailClient.Instance.notification.Play());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Can't get unseen messages, {e.Message}");
            }
            finally
            {
                countdownEvent.Signal();
            }
        }

        private void GetSeenMessages(int count)
        {
            try
            {
                IEnumerable<uint> seenIds = EmailClient.Instance.clients.imapClient.Search(SearchCondition.Seen());
                seenIds = seenIds.Skip(Math.Max(0, seenIds.Count() - count));
                IEnumerable<MailMessage> seenMessages = EmailClient.Instance.clients.imapClient.GetMessages(seenIds, FetchOptions.HeadersOnly, false);

                for (int i = 0; i < seenIds.Count(); i++)
                {
                    string key = seenIds.ElementAt(i).ToString();

                    if (!SeenMessages.ContainsKey(key))
                    {
                        SeenMessages.TryAdd(key, seenMessages.ElementAt(i));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Can't get seen messages, {e.Message}");
            }
            finally
            {
                countdownEvent.Signal();
            }
        }

        public void GetMessages(int count)
        {
            try
            {
                lock(_lockGet)
                {
                    if (EmailClient.Instance.clients.HasInternet && EmailClient.Instance.clients.imapClient != null)
                    {
                        countdownEvent = new CountdownEvent(2);
                        Task.Run(() => GetSeenMessages(count));
                        Task.Run(() => GetUnseenMessages(count));
                        countdownEvent.Wait();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Could not get messages {e.Message}");
            }
        }

        public void SendAllMessages()
        {
            try
            {
                lock (_lockSend)
                {
                    if (EmailClient.Instance.clients.HasInternet && EmailClient.Instance.clients.smtpClient != null)
                    {
                        foreach (var m in PendingMessages)
                        {
                            try
                            {
                                if (m.Value.Attachments != null)
                                {
                                    foreach (var at in m.Value.Attachments)
                                        at.Name = at.Name.Split('\\').Last();
                                }
                                EmailClient.Instance.clients.smtpClient.Send(m.Value);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show($"Could not send message to {m.Value.To}, {e.Message}");
                            }
                        }
                        PendingMessages.Clear();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Could not send messages {e.Message}");
            }
        }

        public MailMessage GetMessage(string id, EmailClient.MessageType type)
        {
            MailMessage message = null;

            switch (type)
            {
                case EmailClient.MessageType.Seen:
                    SeenMessages.TryGetValue(id, out message);
                    message = EmailClient.Instance.clients.imapClient.GetMessage(UInt32.Parse(id), true);
                    break;
                case EmailClient.MessageType.Unseen:
                    MailMessage temp;
                    message = EmailClient.Instance.clients.imapClient.GetMessage(UInt32.Parse(id), true);
                    UnseenMessages.TryGetValue(id, out temp);
                    SeenMessages.TryAdd(id, temp);
                    UnseenMessages.TryRemove(id, out _);
                    break;
                case EmailClient.MessageType.Draft:
                    DraftMessages.TryGetValue(id, out message);
                    break;
                case EmailClient.MessageType.Sending:
                    PendingMessages.TryGetValue(id, out message);
                    break;
                default:
                    return message;
            }

            return message;
        }

        public void CleanAllMessages()
        {
            PendingMessages.Clear();
            DraftMessages.Clear();
            SeenMessages.Clear();
            UnseenMessages.Clear();
        }

        public void SaveUnsendMessages()
        {
            if (!EmailClient.Instance.clients.HasInternet)
            {
                MessageArchiver.ArchiveMessages(PendingMessages, Settings.archivedMessages + EmailClient.Instance.user.username);
            }
        }

        public void DeleteMessage(string id, EmailClient.MessageType type)
        {
            try
            {
                switch (type)
                {
                    case EmailClient.MessageType.Seen:
                        SeenMessages.TryRemove(id, out _);
                        EmailClient.Instance.clients.imapClient.DeleteMessage(UInt32.Parse(id));
                        break;
                    case EmailClient.MessageType.Unseen:
                        UnseenMessages.TryRemove(id, out _);
                        SeenMessages.TryRemove(id, out _);
                        EmailClient.Instance.clients.imapClient.DeleteMessage(UInt32.Parse(id));
                        break;
                    case EmailClient.MessageType.Draft:
                        DraftMessages.TryRemove(id, out _);
                        break;
                    case EmailClient.MessageType.Sending:
                        if (PendingMessages.TryRemove(id, out _))
                            MessageBox.Show("Your message was canceled");
                        break;
                    default:
                        return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Can't delete message {e.Message}");
            }
            
        }

        public Attachment GetAttachment(int index, string messageId, EmailClient.MessageType type)
        {
            try
            {
                var mess = GetMessage(messageId, type);
                var attachment = mess.Attachments[index];
                return attachment;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Can't get attachment {e.Message}");
                return null;
            }
        }

        public void SaveDraftMessages()
        {
            MessageArchiver.ArchiveMessages(DraftMessages, Settings.draftMessages + EmailClient.Instance.user.username);
        }

        public void GetArchivedMessages()
        {
            MessageArchiver.UnarchiveMessagesTo(PendingMessages, Settings.archivedMessages + EmailClient.Instance.user.username);
        }

        public void GetDraftMessages()
        {
            MessageArchiver.UnarchiveMessagesTo(DraftMessages, Settings.draftMessages + EmailClient.Instance.user.username);
        }
    }
}
