using LanguageSkillsWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LanguageSkillsWPF.ViewModel
{
    internal class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand WordsSourceCommand { get; set; }
        public ICommand StatsCommand { get; set; }
        public ICommand LearnCommand { get; set; }


        private void WordsSource(object obj) => CurrentView = new WordsSourceVM();
        private void Stats(object obj) => CurrentView = new StatsVM();
        private void Learn(object obj) => CurrentView = new LearningVM();

        public NavigationVM()
        {
            WordsSourceCommand = new RelayCommand(WordsSource);
            StatsCommand = new RelayCommand(Stats);
            LearnCommand = new RelayCommand(Learn);

            CurrentView = new StatsVM();
        }
    }
}
