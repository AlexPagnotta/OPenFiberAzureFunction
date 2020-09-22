using System.Collections.Generic;

public class Settings
{   
    public string OpenFiberAddress { get; set; }
    public string OpenFiberAddressUrl { get; set; }
    public string FWACoverageStringIdentifier { get; set; }
    public string FTTHCoverageStringIdentifier { get; set; }
    public string NOCoverageStringIdentifier { get; set; }
    public string ParentElementIdentifier { get; set; }
    public string CoverageStringElementIdentifier { get; set; }

}

public class SendGridSettings
{
    public string API_KEY { get; set; }
    public SendGridEmailUser Sender { get; set; }
    public SendGridEmailUser Recipient { get; set; }
}
