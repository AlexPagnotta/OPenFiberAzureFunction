

public class SendGridEmailUser
{
    public string Email { get; set; }
    public string Name { get; set; }

    public SendGridEmailUser(string email, string name){
        Email = email;
        Name = name;
    }
}