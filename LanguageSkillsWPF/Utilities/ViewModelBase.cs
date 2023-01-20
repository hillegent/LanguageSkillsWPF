using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LanguageSkillsWPF.ViewModel
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = null)
        { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property)); }
    }
}
