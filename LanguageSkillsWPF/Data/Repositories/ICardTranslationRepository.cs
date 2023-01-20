using LanguageSkillsWPF.Data.Repositories;
using LanguageSkillsWPF.Data.Entities;

namespace LanguageSkillsWPF.Data.Repositories
{
    public interface ICardTranslationRepository : IBaseRepository<CardTranslation>
    {
        //функции нужные только для переводов карт
    }
}
