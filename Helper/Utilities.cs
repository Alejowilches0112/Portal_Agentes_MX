using Entities;
using System;
using System.Web;
using System.Xml;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Collections.Generic;

namespace Helper
{
    public class Utilities
    {
        public static OutMailInformation GetMailInformation(string templatePath)
        {
            OutMailInformation mailInformation = new OutMailInformation();
            try
            {
                XmlDocument doc = new XmlDocument();
                string xmlTemplatePath = HttpContext.Current.Server.MapPath(templatePath);
                doc.Load(xmlTemplatePath);
                if (doc.GetElementsByTagName("to").Item(0) != null)
                    mailInformation.to = doc.GetElementsByTagName("to").Item(0).InnerText;
                if (doc.GetElementsByTagName("subject").Item(0) != null)
                    mailInformation.subject = doc.GetElementsByTagName("subject").Item(0).InnerText;
                if (doc.GetElementsByTagName("message").Item(0) != null)
                    mailInformation.message = doc.GetElementsByTagName("message").Item(0).InnerText;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("Helper", "Utilities", "GetMailRecipient", ex, "");
            }

            return mailInformation;
        }

        public static string ConvertToXml(object o)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Xml.XmlTextWriter tw = default(System.Xml.XmlTextWriter);
            try
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(o.GetType());
                tw = new System.Xml.XmlTextWriter(sw);
                serializer.Serialize(tw, o);
                return sw.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message + " " + ex.StackTrace;
            }
            finally
            {
                sw.Close();
                tw.Close();
            }
        }
        public bool sendEmailAuxliar(string folder, string[] emailAuxiliar, string[] pdf, string cuerpo)
        {
            List<string> emails = new List<string>();
            for (var i = 1; i < emailAuxiliar.Length; i++)
            {
                if (!"".Equals(emailAuxiliar[0]) && !"".Equals(emailAuxiliar[i]))
                {
                    emails.Add(emailAuxiliar[i]);
                }
            }
            InEmail email = new InEmail();
            email.server = ConfigurationManager.AppSettings["ServidorSaliente"];
            email.port = int.Parse(ConfigurationManager.AppSettings["port"]);
            email.EnableSsl = true;
            email.from = ConfigurationManager.AppSettings["emailAgente"];
            email.password = ConfigurationManager.AppSettings["passAgente"];
            email.Subject = ConfigurationManager.AppSettings["asuntoAgente"];
            email.to =  ("".Equals(emailAuxiliar[0])) ? emailAuxiliar[1] : emailAuxiliar[0];
            if (emails.Count > 0)
            {
                email.cc = emails;
            }
            email.Body = cuerpo;
            email.IsBodyHtml = true;
            if (!IsValidEmail(email.to))
            {
                LogHelper.WriteLog("Helper", "Utilities", "sendEmail", null, "Dirección de Correo no Valida => " + email.to);
                return false;
            }
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(email.from, email.displayName);
                mail.To.Add(email.to);
                if(email.cc.Count > 0)
                {
                    mail.CC.Add(emails[0]);
                }
                mail.Subject = email.Subject;
                mail.Body = email.Body;
                mail.IsBodyHtml = true;
                for(var i = 0; i < pdf.Length; i++)
                {
                    if ("".Equals(pdf[0]) || pdf[0] != null)
                        mail.Attachments.Add(new System.Net.Mail.Attachment(pdf[i]));
                }

                try
                {
                    using (SmtpClient smtp = new SmtpClient(email.server, email.port))
                    {
                        smtp.Credentials = new NetworkCredential(email.from, email.password);
                        smtp.EnableSsl = email.EnableSsl;
                        smtp.Send(mail);
                        LogHelper.WriteLog("Helper", "Utilities", "sendEmail", new Exception(), null);
                        return true;
                    }
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog("Helper", "Utilities", "sendEmail", e, null);
                    return false;
                }
            }
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
