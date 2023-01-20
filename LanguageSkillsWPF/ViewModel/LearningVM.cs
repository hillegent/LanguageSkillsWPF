using Jering.Javascript.NodeJS;
using LanguageSkillsWPF.Data;
using LanguageSkillsWPF.Data.Entities;
using LanguageSkillsWPF.Data.Repositories;
using LanguageSkillsWPF.Utilities;
using LanguageSkillsWPF.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using LanguageSkillsWPF.Infrastructure;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LanguageSkillsWPF.ViewModel
{
    internal class LearningVM : ViewModelBase
    {
        public Card cardCurrent;
        public CardTranslation cardTranslation;
        public string inputWord;
        private RelayCommand nextWord;
        private RelayCommand nextTranslation;
        WPFContext context;
        int attempt;


        public LearningVM()
        {
            context = new WPFContext();
            cardCurrent = GetCard();
            cardTranslation = new CardTranslation
            {
                Translate = "test",
            };
            inputWord = "";
            attempt = 0;
        }

        public RelayCommand NextWord
        {
            get
            {
                return nextWord ??
                    (nextWord = new RelayCommand(async obj =>
                    {
                        if (inputWord.ToLowerInvariant().CompareTo(cardCurrent.Word.ToLowerInvariant()) == 0)
                        {
                            cardCurrent.AddProgress(attempt);
                            context.Cards.Update(CardCurrent);
                            context.SaveChanges();
                            attempt = 0;
                            CardCurrent = GetCard();
                            CardTranslation = await GetTranslation();
                            InputWord = "";
                        }
                        else
                        {
                            attempt++;
                            InputWord = "";
                        }

                    }));
            }

        }

        public Card CardCurrent
        {
            get { return cardCurrent; }
            set
            {
                cardCurrent = value;
                OnPropertyChanged("CardCurrent");
            }
        }
        public CardTranslation CardTranslation
        {
            get { return cardTranslation; }
            set
            {
                cardTranslation = value;
                OnPropertyChanged("CardTranslation");
            }
        }
        public string InputWord
        {
            get { return inputWord; }
            set
            {
                inputWord = value;
                OnPropertyChanged("InputWord");
            }
        }

        private Card GetCard()
        {
            var date = DateTime.Now;
            Card toReturn = context.Cards.Where(c => c.NextRepeat < date).OrderBy(c=>c.NextRepeat).FirstOrDefault();
            if (toReturn == null)
            {
                toReturn = context.Cards.Where(c => c.NextRepeat == null).OrderByDescending(c => c.Rating).FirstOrDefault();
            }
            return toReturn; 
        }

        public RelayCommand NextTranslation
        {
            get
            {
                return nextTranslation ??
                    (nextTranslation = new RelayCommand(async obj =>
                    {
                        cardTranslation = await GetTranslation();
                    }));
            }

        }

        private async Task<CardTranslation> GetTranslation()
        {

            CardTranslation toReturn = context.CardTranslations.Where(c => c.CardId == cardCurrent.Id).FirstOrDefault();
            if (toReturn == null)
            {
                var node =  App.AppHost.Services.GetService<INodeJSService>();
                GoogleTraslator traslator = new GoogleTraslator(node);
                toReturn = await traslator.Translate(CardCurrent, "ru");
                context.CardTranslations.Add(toReturn);
                context.SaveChanges();
            }
            return toReturn;
        }
    }
}
