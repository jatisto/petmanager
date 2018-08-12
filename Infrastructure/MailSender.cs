using System.Net;
using System.Net.Mail;

namespace pet_manager.Infrastructure{
    public static class MailSender{
    public static void SendMail(string receiverEmail, string buyerEmail, int petId, string userId){
        var link = "<a href='http://localhost:5000/Pet/ConfirmSelling?Id="+petId+"&&userId="+userId+'>'+"Click Here</a>";
        SmtpClient client = new SmtpClient("smtp.gmail.com");
        client.Port = 587;
        client.EnableSsl=true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential("nepdealnepal@gmail.com", "mBhadrA929fuckyou");
        
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress("nepdealnepal@gmail.com");
        mailMessage.To.Add(receiverEmail);
        mailMessage.Body = @"You have received this email. The "+buyerEmail+
        "has completed the payment process and is requesting the ownership transfer"+
        "of the pet. Please click on this link "+
        link +" to complete the selling process"+
        "Thanks";
        mailMessage.Subject = "Request for pet ownership transfer";
        client.Send(mailMessage);
        }
    }
}