using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentManagementProvider.Helpers
{
    public static class EmailHelper
    {
        public static async Task<bool> SendMailWithCalendarInvite(EmailTemplate emailTemplate)
        {
            if (emailTemplate != null && emailTemplate.ToEmailAddress.Length > 0)
            {
                LoadEmailSettings(ref emailTemplate);
                System.Net.Mail.MailMessage msg = new MailMessage();
                foreach (string recepient in emailTemplate.ToEmailAddress)
                {
                    msg.To.Add("manjunath.g@ecanarys.com");
                }
                msg.From = new MailAddress("chandrashekhara@ecanarys.com", "Manjunath G");
                msg.Body = emailTemplate.Body;
                msg.Subject = emailTemplate.Subject;
                //MailAddress bcc = new MailAddress("");
                //msg.Bcc.Add(bcc);
                StringBuilder str = new StringBuilder();
                str.AppendLine("BEGIN:VCALENDAR");
                str.AppendLine("PRODID:-//Schedule a Meeting");
                str.AppendLine("VERSION:2.0");
                str.AppendLine("METHOD:REQUEST");
                str.AppendLine("BEGIN:VEVENT");
                str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", emailTemplate.InvitationStartTime));
                str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
                str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", emailTemplate.InvitationEndTime));
                str.AppendLine("LOCATION: " + emailTemplate.Location);
                str.AppendLine(string.Format("UID:{0}", emailTemplate.UniqueIdentifier));
                //str.AppendLine(string.Format("DESCRIPTION:{0}", appointment.Body));
                str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", emailTemplate.Body));
                str.AppendLine(string.Format("SUMMARY:{0}", emailTemplate.Subject));
                str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", emailTemplate.EmailFromAddress));

                str.AppendLine("BEGIN:VALARM");
                str.AppendLine("TRIGGER:-PT15M");
                str.AppendLine("ACTION:DISPLAY");
                str.AppendLine("DESCRIPTION:Reminder");
                str.AppendLine("END:VALARM");
                str.AppendLine("END:VEVENT");
                str.AppendLine("END:VCALENDAR");


                byte[] byteArray = Encoding.ASCII.GetBytes(str.ToString());
                MemoryStream stream = new MemoryStream(byteArray);


                System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(stream, "Calendar.ics");


                msg.Attachments.Add(attach);


                System.Net.Mime.ContentType contype = new System.Net.Mime.ContentType("text/calendar");
                contype.Parameters.Add("method", "REQUEST");
                //  contype.Parameters.Add("name", "Meeting.ics");
                AlternateView avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), contype);
                msg.AlternateViews.Add(avCal);


                try
                {
                    //Now sending a mail with attachment ICS file.                     
                    System.Net.Mail.SmtpClient smtpclient = new System.Net.Mail.SmtpClient();
                    smtpclient.Host = emailTemplate.Host;
                    smtpclient.Port = emailTemplate.Port;
                    smtpclient.EnableSsl = emailTemplate.EnableSsl;
                    smtpclient.Credentials = new System.Net.NetworkCredential(emailTemplate.EmailFromAddress, emailTemplate.EmailFromPassword);
                    await smtpclient.SendMailAsync(msg);
                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public static async Task<bool> SendCancelCalendarInvite(EmailTemplate emailTemplate)
        {
            if (emailTemplate != null && emailTemplate.ToEmailAddress.Length > 0)
            {
                LoadEmailSettings(ref emailTemplate);
                System.Net.Mail.MailMessage msg = new MailMessage();
                foreach (string recepient in emailTemplate.ToEmailAddress)
                {
                    msg.To.Add("manjunath.g@ecanarys.com");
                }
                msg.From = new MailAddress("chandrashekhara@ecanarys.com", "Manjunath G");
                msg.Body = emailTemplate.Body;
                msg.Subject = emailTemplate.Subject;
                //MailAddress bcc = new MailAddress("");
                //msg.Bcc.Add(bcc);
                StringBuilder str = new StringBuilder();
                str.AppendLine("BEGIN:VCALENDAR");
                str.AppendLine("PRODID:-//Schedule a Meeting");
                str.AppendLine("VERSION:2.0");
                str.AppendLine("METHOD:CANCEL");
                str.AppendLine("BEGIN:VEVENT");
                str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", emailTemplate.InvitationStartTime));
                str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
                str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", emailTemplate.InvitationEndTime));
                str.AppendLine("LOCATION: " + emailTemplate.Location);
                str.AppendLine(string.Format("UID:{0}", emailTemplate.UniqueIdentifier));            
                str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", emailTemplate.Body));
                str.AppendLine(string.Format("SUMMARY:{0}", emailTemplate.Subject));
                str.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", emailTemplate.EmailFromAddress));
                str.AppendLine("END:VEVENT");
                str.AppendLine("END:VCALENDAR");


                byte[] byteArray = Encoding.ASCII.GetBytes(str.ToString());
                MemoryStream stream = new MemoryStream(byteArray);


                System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(stream, "Calendar.ics");


                msg.Attachments.Add(attach);


                System.Net.Mime.ContentType contype = new System.Net.Mime.ContentType("text/calendar");
                contype.Parameters.Add("method", "REQUEST");
                //  contype.Parameters.Add("name", "Meeting.ics");
                AlternateView avCal = AlternateView.CreateAlternateViewFromString(str.ToString(), contype);
                msg.AlternateViews.Add(avCal);


                try
                {
                    //Now sending a mail with attachment ICS file.                     
                    System.Net.Mail.SmtpClient smtpclient = new System.Net.Mail.SmtpClient();
                    smtpclient.Host = emailTemplate.Host;
                    smtpclient.Port = emailTemplate.Port;
                    smtpclient.EnableSsl = emailTemplate.EnableSsl;
                    smtpclient.Credentials = new System.Net.NetworkCredential(emailTemplate.EmailFromAddress, emailTemplate.EmailFromPassword);
                    await smtpclient.SendMailAsync(msg);
                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public static async Task<bool> SendMailWithoutCalendarInvite(EmailTemplate emailTemplate)
        {
            if (emailTemplate != null && emailTemplate.ToEmailAddress.Length > 0)
            {
                LoadEmailSettings(ref emailTemplate);
                System.Net.Mail.MailMessage msg = new MailMessage();
                foreach (string recepient in emailTemplate.ToEmailAddress)
                {
                    msg.To.Add(recepient);
                }
                msg.From = new MailAddress(emailTemplate.EmailFromAddress, emailTemplate.EmailFromDisplayName);
                msg.Body = emailTemplate.Body;
                msg.Subject = emailTemplate.Subject;
                try
                {
                                         
                    System.Net.Mail.SmtpClient smtpclient = new System.Net.Mail.SmtpClient();
                    smtpclient.Host = emailTemplate.Host;
                    smtpclient.Port = emailTemplate.Port;
                    smtpclient.EnableSsl = emailTemplate.EnableSsl;
                    smtpclient.Credentials = new System.Net.NetworkCredential(emailTemplate.EmailFromAddress, emailTemplate.EmailFromPassword);
                    await smtpclient.SendMailAsync(msg);
                    return true;


                }
                catch (System.Exception ex)
                {
                    return false;
                }
            }

            return false;
        }
        private static void LoadEmailSettings(ref EmailTemplate emailTemplate)
        {
            emailTemplate.EmailFromAddress = "manjunathg351@gmail.com";
            emailTemplate.EmailFromPassword = "Sigmatree@1987";
            emailTemplate.EnableSsl = true;
            emailTemplate.Port = 587;
            emailTemplate.Host = "smtp.gmail.com";
        }

    }

    public class EmailTemplate
    {
        public string EmailFromAddress { get; set; }
        public string EmailFromDisplayName { get; set; }
        public string EmailFromPassword { get; set; }
        public string[] ToEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public DateTime InvitationStartTime { get; set; }
        public DateTime InvitationEndTime { get; set; }
        public string Location { get; set; }

        public Guid? UniqueIdentifier { get; set; }
       

    }
}
