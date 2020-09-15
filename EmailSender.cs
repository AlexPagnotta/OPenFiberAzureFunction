using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Linq;

public class EmailSender
{   
    private SendGridClient _client;
    private SendGridEmailUser _senderEmail;
    private List<SendGridEmailUser> _recipients;

    public EmailSender(
        string apiKey, 
        SendGridEmailUser senderEmail,
        List<SendGridEmailUser> recipients
    ){
        _senderEmail = senderEmail;
        _recipients = recipients;

        _client = new SendGridClient(apiKey);
    }

    public async Task SendEmail(
        string subject,
        string content
        ){

        var msg = new SendGridMessage();

        msg.SetFrom(new EmailAddress(_senderEmail.Email, _senderEmail.Name));

        var recipients = _recipients.Select(r => new EmailAddress(r.Email, r.Name)).ToList();

        msg.AddTos(recipients);

        msg.SetSubject(subject);

        msg.AddContent(MimeType.Html, content);
        
        await _client.SendEmailAsync(msg);
    }  

}