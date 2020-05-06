using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AliyunSendMail
{
    class Program
    {

        public static string from = ConfigurationManager.AppSettings["from"];
        public static string fromName = ConfigurationManager.AppSettings["fromName"];
        public static string password = ConfigurationManager.AppSettings["password"];
        public static string strHost = ConfigurationManager.AppSettings["strHost"];
        public static int iPort = 465;
        static void Main(string[] args)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.To.Add(ConfigurationManager.AppSettings["to"]);
            msg.From = new MailAddress(from, fromName, System.Text.Encoding.UTF8);

            msg.Subject = "邮件标题";//邮件标题    
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码   
            AlternateView htmlBody = AlternateView.CreateAlternateViewFromString("测试" + DateTime.Now.ToString("yyyyMMddHHssmm") + new Random().Next(10000), null, "text/html");
            msg.AlternateViews.Add(htmlBody);
            //msg.Body = body; //邮件内容    
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码    
            msg.IsBodyHtml = true;//,false;//是否是HTML邮件    
            msg.Priority = MailPriority.High;//邮件优先级 
            
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(from, password);

            client.Port = iPort;//25 465;//qqmail使用的端口    
            client.Host = strHost;
            client.EnableSsl = true;//经过ssl加密    
            object userState = msg;

            try
            {
                client.Send(msg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                throw ex;
            }
        }
    }
}
