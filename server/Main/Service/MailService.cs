using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace server.Main.Service
{
  public class MailService : IMailService
  {
    // INJECTIONS //
    private IConfiguration _config;
    public MailService(IConfiguration configuration)
    {
      _config = configuration;
    }

    // SEND MAIL //
    public void sendMail(string receiver, string subject, string htmlMessage)
    {
      string FromAddress = _config.GetValue<string>("Mail:Address")!;
      string Username = _config.GetValue<string>("Mail:Username")!;
      string Password = _config.GetValue<string>("Mail:Password")!;
      string Host = _config.GetValue<string>("Mail:Host")!;
      int Port = _config.GetValue<int>("Mail:Port")!;

      MailMessage message = new MailMessage();
      message.From = new MailAddress(FromAddress);
      message.To.Add(new MailAddress(receiver));
      message.Subject = subject;
      message.Body = htmlMessage;
      message.IsBodyHtml = true;
      SmtpClient client = new SmtpClient(Host)
      {
        Port = Port,
        Credentials = new NetworkCredential(Username, Password),
        EnableSsl = true,
      };
      client.Send(message);
      message.Dispose();
    }
  }
}