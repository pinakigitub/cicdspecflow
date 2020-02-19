using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CICDBDDRTODOTNETFramework.Utils
{
    public class Mailing
    {

        public static void MethodToSendEmail()
        {
            //    try
            //    {
            //Sender's Email,Sender's Password,To/Receiver's Email,subject,body,cc,attachment
            MailMessage mail = new MailMessage();
            mail.From = new System.Net.Mail.MailAddress("mbi-sgolyala@cfgo.com");
            mail.To.Add(new System.Net.Mail.MailAddress("sampath.golyala@m3bi.com"));
            mail.Subject = "Automation Smoke Suite Test Report";
            string dashboardmailAttachmentPath = Hooks.reportPath + "\\dashboard.html";
            string indexmailAttachmentPath = Hooks.reportPath + "\\index.html";
            mail.Attachments.Add(new Attachment(dashboardmailAttachmentPath));
            mail.Attachments.Add(new Attachment(indexmailAttachmentPath));
            //mail.Attachments.Add(new Attachment(@"C:\Users\mbi-sgolyala\source\repos\JarvisApps\Test\BDDwithSpecflowProject\Reports\dashboard.html"));
            //mail.Attachments.Add(new Attachment("C:\\Users\\mbi-sgolyala\\source\\repos\\JarvisApps\\Test\\BDDwithSpecflowProject\\Reports\\index.html"));
            SmtpClient smtp = new SmtpClient("SMTP.CORP.CFGO.COM");
            smtp.Send(mail);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

        }

    

    }
}

