//using BSU.BRSM.Bot.Core;
//using BSU.BRSM.Bot.Core.Models;

//namespace BSU.BRSM.Bot.Memory;

//public class UserRepository : IUserRepository
//{
//    private readonly IEnumerable<User> users = new User[]
//    {
//        new User(87235698347, "Гринь", "Максим"),
//        new User(87232694547, "Гринь", "Макс"),
//        new User(87235656834, "Green", "MaX"),
//    };
//    public User GetByChatId(long chatId)
//    {
//        return users.Single(user => user.ChatId == chatId);
//    }
//    public IEnumerable<User> GetByFirstOrLastName(string name)
//    {
//        return users.Where(user => user.FirstName == name 
//                                || user.UserName == name);                      
//    }
//    public IEnumerable<User> GetAll()
//    {
//        return users;
//    }
//}