using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using Agiso.Object;

namespace Agiso.MessageSender
{
	// Token: 0x020000F0 RID: 240
	public abstract class MailSender
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x0004D9E4 File Offset: 0x0004BBE4
		public static void SendMail(MailSenderAccount msAccount, string mailTo, string subject, string body)
		{
			MailSender.SendMail(msAccount.SmtpHost, msAccount.SmtpPort, msAccount.Account, msAccount.Password, msAccount.MailFrom, msAccount.Nick, mailTo, subject, body, msAccount.EnableSsl);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0004DA24 File Offset: 0x0004BC24
		public static void SendMail(string smtpHost, int port, string account, string password, string mailFrom, string displayName, string mailTo, string subject, string body, bool enableSsl)
		{
			SmtpClient smtpClient = new SmtpClient(smtpHost, port);
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.EnableSsl = enableSsl;
			smtpClient.UseDefaultCredentials = true;
			smtpClient.Credentials = new NetworkCredential(account, password);
			MailMessage mailMessage = new MailMessage();
			mailMessage.Priority = MailPriority.High;
			MailAddress mailAddress = new MailAddress(mailFrom, displayName, MailSender.a);
			mailMessage.From = mailAddress;
			mailMessage.ReplyTo = mailAddress;
			mailMessage.Sender = mailAddress;
			mailMessage.To.Add(mailTo);
			subject = subject.Replace("\r\n", " ").Replace('\r', ' ').Replace('\n', ' ');
			mailMessage.Subject = subject;
			mailMessage.SubjectEncoding = MailSender.a;
			mailMessage.IsBodyHtml = false;
			mailMessage.BodyEncoding = MailSender.a;
			mailMessage.Body = body;
			smtpClient.Send(mailMessage);
		}

		// Token: 0x040004E1 RID: 1249
		private static Encoding a = Encoding.GetEncoding(936);

		// Token: 0x040004E2 RID: 1250
		private static Encoding b = Encoding.UTF8;
	}
}
