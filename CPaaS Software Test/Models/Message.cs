namespace CPaaS_Software_Test;

public class Message
{
    public string MessageText { get; set; }
    public string SenderName { get; set; }
    public string RecipientPhonenumber { get; set; }
    public string Reference { get; set; }
    //optional
    public List<string> RecipientPhonenumbers { get; set; }

    // single recipient ctor
    public Message(string MessageText, string SenderName, string RecipientPhonenumber, string Reference)
    {
        this.MessageText = MessageText;
        this.SenderName = SenderName;
        this.RecipientPhonenumber = RecipientPhonenumber;
        this.Reference = Reference;
    }
    
    // multi recipient ctor
    public Message(string MessageText, string SenderName, List<string> RecipientPhonenumbers, string Reference)
    {
        this.MessageText = MessageText;
        this.SenderName = SenderName;
        this.RecipientPhonenumbers = RecipientPhonenumbers;
        this.Reference = Reference;
    }
}