using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Common
{
    class User
    {
        public string username { get; set; }
        public string password { get; set; }

        public static void CacheUser(string login, string pass)
        {
            try
            {
                User user = new User { username = login, password = pass };
                JsonSerializer serializer = new JsonSerializer();

                using (StreamWriter sw = new StreamWriter(Settings.credentials))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    user.password = EncryptUtil.Encrypt(user.password);
                    serializer.Serialize(writer, user);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error Remembering User {e.Message}");
            }
        }

        public bool IsAcceptableEmail()
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(username);
                if (!(addr.Address == username))
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            foreach (var s in Settings.acceptedMails)
            {
                if (username.Contains(s))
                {
                    return true;
                }
            }

            return false;
        }

        public static User LoadUser()
        {
            User user = null;

            if (File.Exists(Settings.credentials))
            {
                using (StreamReader file = File.OpenText(Settings.credentials))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    user = (User)serializer.Deserialize(file, typeof(User));
                    user.password = EncryptUtil.Decrypt(user.password);
                }
            }

            if (user == null)
            {
                return null;
            }
            else if (String.IsNullOrWhiteSpace(user.password) ||  String.IsNullOrWhiteSpace(user.username))
            {
                return null;
            }

            return user;
        }
    }
}
