using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Net.Mail;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1.Common;

namespace WindowsFormsApp1.Tools
{
    class ArchivedMessage
    {
        public string id { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public List<string> Attachments = new List<string>();

        public ArchivedMessage()
        {

        }
    }

    class MessageArchiver
    {
        private static readonly object _lockArchive= new object();

        public static void ArchiveMessages(ConcurrentDictionary<string, MailMessage> messages, string path)
        {
            lock (_lockArchive)
            {
                List<ArchivedMessage> archivedMessages = new List<ArchivedMessage>();
                foreach (var m in messages)
                {
                    ArchivedMessage archived = new ArchivedMessage();

                    archived.To = m.Value.To.ToString();
                    archived.Subject = m.Value.Subject;
                    archived.Body = m.Value.Body;
                    archived.id = m.Key;
                    archived.From = m.Value.From.ToString();
                    foreach (var a in m.Value.Attachments)
                    {
                        archived.Attachments.Add(a.Name);
                    }

                    archivedMessages.Add(archived);
                }

                try
                {
                    JsonSerializer serializer = new JsonSerializer();

                    using (StreamWriter sw = new StreamWriter(path))
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, archivedMessages);
                    }

                    messages.Clear();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Can't save pending messages to local computer. {e.Message}");
                }
            }
        }

        public static void UnarchiveMessagesTo(ConcurrentDictionary<string, MailMessage> messages, string path)
        {
            List<ArchivedMessage> archivedMessages = new List<ArchivedMessage>();

            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader file = File.OpenText(path))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        archivedMessages = (List<ArchivedMessage>)serializer.Deserialize(file, typeof(List<ArchivedMessage>));

                        foreach (var m in archivedMessages)
                        {
                            try
                            {
                                MailMessage toAdd = new MailMessage(m.From, m.To);
                                toAdd.Body = m.Body;
                                toAdd.Subject = m.Subject;
                                foreach (var a in m.Attachments)
                                {
                                    var t = new Attachment(a);
                                    t.Name = a;
                                    toAdd.Attachments.Add(t);
                                }

                                messages.TryAdd(m.id, toAdd);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show($"Can't parse archived message. {e.Message}");
                            }
                        }
                    }
                    File.Delete(path);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Can't load archived messages from local computer. {e.Message}");
            }
        }
    }
}
