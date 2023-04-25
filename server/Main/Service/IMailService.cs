using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Main.Service
{
    public interface IMailService
    {
        /// <summary>
        /// This method sends a mail
        /// <param name = "receiver" >The email address of the receiver</param>
        /// <param name = "subject" >The subject of the mail</param>
        /// <param name = "htmlMessage" >The body of the mail in html</param>
        /// </summary>
        void sendMail(string receiver, string subject, string htmlMessage);
    }
}