using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;
using MailKit.Net.Smtp;
namespace HOSPITALMANAGEMENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

    }
}
//    {
//        [HttpPost]
//        //       public IActionResult SendEmail(string body)
//        //{
//        //    try
//        //    {
//        //        // Create a new MimeMessage
//        //        var email = new MimeMessage();
//        //        email.From.Add(MailboxAddress.Parse("ewell33@ethereal.email"));
//        //        email.To.Add(MailboxAddress.Parse("ewell3@ethereal.email"));
//        //        email.Subject = "Hi! How are you";
//        //        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

//        //        // Connect to the Ethereal SMTP server
//        //        //using var smtp = new SmtpClient();
//        //        //smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);

//        //        // Send the email
//        //        smtp.Send(email);

//        //        // Disconnect from the SMTP server
//        //        smtp.Disconnect(true);

//        //        return Content("Email sent successfully!");
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        // Handle any exceptions that may occur during the email sending process
//        //        return Content($"Failed to send email: {ex.Message}");
//        //    }
//        ////}

//    }
//}