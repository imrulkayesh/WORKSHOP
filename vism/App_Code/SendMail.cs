using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;

public class SendEmail
{

    private static string Username = "rsms";
    private static string Password = "mis@W3lc0m3";
    private const string SmtpServer = "172.17.2.29";
    private static string From = "Vehicle Workshop<rsms@prangroup.com>";

    public SendEmail()
    {

    }
    public static bool SendSingleMail(string mailTo, string emailSubject, string emailBody)
    {
        SmtpClient smtpClient = new SmtpClient();
        NetworkCredential basicCredential = new NetworkCredential(Username, Password);
        MailMessage message = new MailMessage();
        MailAddress fromAddress = new MailAddress(From, "Vehicle Workshop");

        smtpClient.Host = SmtpServer;

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = basicCredential;
        message.From = fromAddress;
        message.Subject = emailSubject;
        message.IsBodyHtml = true;
        message.Body = emailBody;
        message.To.Add(mailTo);

        try
        {
            smtpClient.Send(message);
            return true;
        }
        catch (Exception ex)
        {
            string err = ex.Message;
            return false;
        }
    }

    public static bool SendMultipleMail(List<string> mailTo, string emailSubject, string emailBody)
    {
        SmtpClient smtpClient = new SmtpClient();
        NetworkCredential basicCredential = new NetworkCredential(Username, Password);
        MailMessage message = new MailMessage();
        MailAddress fromAddress = new MailAddress(From);

        smtpClient.Host = SmtpServer;

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = basicCredential;
        message.From = fromAddress;
        message.Subject = emailSubject;
        message.IsBodyHtml = true;
        message.Body = emailBody;

        for (int i = 0; i < mailTo.Count; i++)
        {
            message.To.Add(mailTo[i]);
        }

        try
        {
            smtpClient.Send(message);
            return true;
        }
        catch (Exception ex)
        {
            string err = ex.Message;
            return false;
        }
    }

    public static bool SendMultipleMailWithPath(List<string> mailTo, string emailSubject, string emailBody,string path)
    {
        SmtpClient smtpClient = new SmtpClient();
        NetworkCredential basicCredential = new NetworkCredential(Username, Password);
        MailMessage message = new MailMessage();
        MailAddress fromAddress = new MailAddress(From);

        smtpClient.Host = SmtpServer;

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = basicCredential;
        message.From = fromAddress;
        message.Subject = emailSubject;
        message.IsBodyHtml = true;
        message.Body = emailBody;


        Attachment attachment = new Attachment(path);
        message.Attachments.Add(attachment);

        for (int i = 0; i < mailTo.Count; i++)
        {
            message.To.Add(mailTo[i]);
        }

        try
        {
            smtpClient.Send(message);
            return true;
        }
        catch (Exception ex)
        {
            string err = ex.Message;
            return false;
        }
    }
    public static bool SendSingleMailFrom(string from, string emailSubject, string emailBody)
    {
        SmtpClient smtpClient = new SmtpClient();
        NetworkCredential basicCredential = new NetworkCredential(Username, Password);
        MailMessage message = new MailMessage();
        MailAddress fromAddress = new MailAddress(from);

        smtpClient.Host = SmtpServer;

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = basicCredential;
        message.From = fromAddress;
        message.Subject = emailSubject;
        message.IsBodyHtml = true;
        message.Body = emailBody;
       // message.To.Add(mailTo);

        try
        {
            smtpClient.Send(message);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public static bool SendMultipleMailBcc(string from, List<string> mailToBcc, string emailSubject, string emailBody)
    {
        SmtpClient smtpClient = new SmtpClient();
        NetworkCredential basicCredential = new NetworkCredential(Username, Password);
        MailMessage message = new MailMessage();
        MailAddress fromAddress = new MailAddress(from);

        smtpClient.Host = SmtpServer;

        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = basicCredential;
        message.From = fromAddress;
        message.Subject = emailSubject;
        message.IsBodyHtml = true;
        message.Body = emailBody;
        for (int i = 0; i < mailToBcc.Count; i++)
        {
            message.Bcc.Add(mailToBcc[i]);
        }

        try
        {
            smtpClient.Send(message);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
