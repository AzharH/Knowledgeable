using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Knowledgeable.Models
{
    public class Utility
    {


        public static void SendMail(string name, string To, string Subject, string MailContent)
        {
            string _sender = "pollhub@mdx.ac.mu";
            string _password = "M!ddle$ex2015";
            //easbzfarmpywfwds";

            string recipient = To;
            //string subject = "subjectTEst";
            //string message = "MessageTest";

            string welcome = "<p>Dear " + name + "</p>";
            MailContent = welcome + MailContent;


            SmtpClient client = new SmtpClient("smtp.office365.com");

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(_sender, _password);
            client.EnableSsl = true;
            client.Credentials = credentials;
            bool flag = false;
            while (!flag)
            {
                try
                {
                    var mail = new MailMessage(_sender.Trim(), recipient.Trim());
                    mail.Subject = Subject;
                    mail.IsBodyHtml = true;
                    mail.Body = MailContent;
                    client.Send(mail);
                    flag = true;

                }
                catch (Exception ex)
                {
                    flag = false;
                    //throw ex;
                }
            }

        }


    }
}