using LanguageSkillsWPF.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jering.Javascript.NodeJS;

namespace LanguageSkillsWPF.Web
{
    internal class GoogleTraslator
    {
        public INodeJSService NodeServices;

        public GoogleTraslator(INodeJSService nodeServices)
        {
            NodeServices = nodeServices;
        }

        public async Task<CardTranslation> Translate(Card card, string targetLang)
        {

            object[] arr = { card.Word, card.Language, targetLang };

            var result = await NodeServices.InvokeFromFileAsync<string>("C:\\Users\\Home\\source\\repos\\LanguageSkillsWPF\\GoogleTranslator\\Translator.js", args: arr);

            var translatedCard = new CardTranslation
            {
                Translate = result,
                Language = targetLang,
                LanguageFrom = card.Language,
                CardId= card.Id,
            };
            return translatedCard;
        }
    }
}
