using LanguageSkillsWPF.Data.Repositories;
using LanguageSkillsWPF.Data.Entities;

namespace LanguageSkillsWPF.Data.Repositories
{
    public interface ICardRepository : IBaseRepository<Card>
    {
        // Функции нужные только для карточек
    }
}
