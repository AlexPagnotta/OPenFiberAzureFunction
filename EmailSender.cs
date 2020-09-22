using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Linq;

public class EmailSender
{   
    private SendGridClient _client;
    private SendGridEmailUser _sender;
    private SendGridEmailUser _recipient;

    public EmailSender(
        string apiKey, 
        SendGridEmailUser sender,
        SendGridEmailUser recipient
    ){
        _sender = sender;
        _recipient = recipient;

        _client = new SendGridClient(apiKey);
    }

    public async Task SendEmail(
        string subject,
        string content
        ){

        var msg = new SendGridMessage();

        msg.SetFrom(new EmailAddress(_sender.Email, _sender.Name));

        var recipient = new EmailAddress(_recipient.Email, _recipient.Name);

        msg.AddTo(recipient);

        msg.SetSubject(subject);

        msg.AddContent(MimeType.Html, content);
        
        await _client.SendEmailAsync(msg);
    }  

}