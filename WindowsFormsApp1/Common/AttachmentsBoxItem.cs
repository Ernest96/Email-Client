namespace WindowsFormsApp1.Common
{
    class AttachmentsBoxItem
    {
        public string name { get; set; }
        public string messageId { get; set; }
        public int index { get; set; }

        public override string ToString()
        {
            return name;
        }

        public AttachmentsBoxItem(int index, string name, string messageId)
        {
            this.index = index;
            this.name = name;
            this.messageId = messageId;
        }
    }
}
