using System.Collections.Generic;

public class Settings
{   
    public string OpenFiberUrl { get; set; }
    public string FWACoverageStringIdentifier { get; set; }
    public string FTTHCoverageStringIdentifier { get; set; }
    public string NOCoverageStringIdentifier { get; set; }
    public string AngleSharp_ParentElementIdentifier { get; set; }
    public string AngleSharp_CoverageStringElementIdentifier { get; set; }

}

public class SendGridSettings
{
    public string SendGrid_API_KEY { get; set; }
    public SendGridEmailUser SendGrid_Sender { get; set; }
    public List<SendGridEmailUser> SendGrid_Recipients { get; set; }
}
