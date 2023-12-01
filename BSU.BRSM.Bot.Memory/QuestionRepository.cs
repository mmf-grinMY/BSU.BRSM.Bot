//using BSU.BRSM.Bot.Core;
//using BSU.BRSM.Bot.Core.Models;

namespace BSU.BRSM.Bot.Memory;
//{
//    public class QuestionRepository : IQuestionRepository
//    {
//        private readonly List<Question> questions = new()
//        {
//            new Question(87235698347, true, "Как дела?", DateTime.Now),
//            new Question(87235698347, false, "Как твои", DateTime.Now),
//            new Question(87232694547, true, "Что делаешь?", DateTime.Now),
//            new Question(87232694547, false, "Какая сегодня", DateTime.Now),
//            new Question(87235656834, true, "Ты покушал?", DateTime.Now),
//            new Question(87235656834, false, "Ты пок", DateTime.Now),
//        };

//        public IEnumerable<Question> GetByChatId(long chatId)
//        {
//            return questions.Where(q => q.ChatId == chatId);
//        }

//        public Task<IEnumerable<Question>> GetByChatIdAsync(long chatId)
//        {
//            throw new NotImplementedException();
//        }

//        public Question GetByGuid(Guid id)
//        {
//            return questions.Single(q => q.Id == id);
//        }

//        public void RemoveByGuid(Guid guid)
//        {
//            questions.Remove(GetByGuid(guid));
//        }
//    }
//}
