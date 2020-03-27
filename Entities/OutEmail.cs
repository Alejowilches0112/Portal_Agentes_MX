using System.Collections.Generic;

namespace Entities
{
    public class OutEmail
    {
        public string SERVER { get; set; }
        public string PORT { get; set; }
        public bool SSL { get; set; }
        public string EMAILFROM { get; set; }
        public string PASSWORD { get; set; }
        public string SUBJECT { get; set; }
        public string DISPLAYNAME { get; set; }
        public string BODY { get; set; }
        public string EMAILTO { get; set; }
        public string[] EMAIL_CC { get; set; }
        public string[] EMAIL_BCC { get; set; }
        public string filePass { get; set; }
        public string fileName { get; set; }
        public string attachment1 { get; set; }
        public string attachment2 { get; set; }
        public string attachment3 { get; set; }
        public string attachment4 { get; set; }
        public string attachment5 { get; set; }
        public string mailJson { get; set; }
    }

    public class InEmail
    {
        public string server { get; set; }
        public int port { get; set; }
        public bool EnableSsl { get; set; }
        public string from { get; set; }
        public string password { get; set; }
        public string to { get; set; }
        public List<string> cc { get; set; } = new List<string>();
        public List<string> bcc { get; set; } = new List<string>();
        public string Subject { get; set; }
        public string Body { get; set; }
        public string displayName { get; set; }
        public bool IsBodyHtml { get; set; }
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
    public class Attachment
    {
        public Attachment(string n, string f)
        {
            name = n;
            file = f;
        }
        public string name { get; set; }
        public string file { get; set; }
    }
}
