using LanguageSkillsWPF.Data.Repositories;
using LanguageSkillsWPF.Data.Entities;

namespace LanguageSkillsWPF.Data.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(WPFContext context) : base(context) { }

        //Функции нужные только для карточек
    }
}
