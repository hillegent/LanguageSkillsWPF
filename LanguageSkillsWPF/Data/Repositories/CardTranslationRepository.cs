using LanguageSkillsWPF.Data.Repositories;
using LanguageSkillsWPF.Data.Entities;

namespace LanguageSkillsWPF.Data.Repositories
{
    public class CardTranslationRepository : BaseRepository<CardTranslation>, ICardTranslationRepository
    {
        public CardTranslationRepository(WPFContext context) : base(context) { }

        // функции нужные только для переводов карточек
    }
}
