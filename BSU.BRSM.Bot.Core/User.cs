namespace BSU.BRSM.Bot.Core;
public class User
{
    public long ChatId { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public User(long chatId, string firstName, string lastName)
    {
        ChatId = chatId;
        FirstName = firstName;
        LastName = lastName;
    }
}