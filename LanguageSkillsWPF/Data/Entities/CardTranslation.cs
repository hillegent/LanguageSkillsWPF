using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSkillsWPF.Data.Entities
{
    public class CardTranslation
    {
        public int Id { get; set; }
        public string? ExampleTranslation { get; set; }
        public string Language { get; set; }
        public string LanguageFrom { get; set; }
        public string? Translate { get; set; }
        public int CardId { get; set; }
    }
}
