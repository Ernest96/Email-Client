using System.IO;

namespace WindowsFormsApp1
{
    class Settings
    {
        public static readonly string credentials = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\cred.json";
        public static readonly string archivedMessages = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\archive-";
        public static readonly string draftMessages = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\draft-";
        public static readonly string[] acceptedMails = { "@gmail.com" };
        public static readonly int SendTime = 10000; // 10 seconds
        public static readonly int GetTime = 3000; // 3 seconds
        public static readonly int CleanTime = 5000; // 5 seconds
        public static readonly int GetMessagesCount = 10;
        public static readonly int GetInitialMessagesCount = 100;
        public static readonly int CleanMessagesCount = 200;
    }
}
