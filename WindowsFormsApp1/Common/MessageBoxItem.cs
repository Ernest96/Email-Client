using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Common
{
    class MessageBoxItem
    {
        public string subject { get; set; }
        public string id { get; set; }
        public AttachmentCollection attachments;

        public override string ToString()
        {
            return subject;
        }

        public MessageBoxItem(string subject, string id)
        {
            this.subject = subject;
            this.id = id;
        }
    }
}
