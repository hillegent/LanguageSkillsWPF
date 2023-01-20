using GongSolutions.Wpf.DragDrop;
using LanguageSkillsWPF.Data;
using LanguageSkillsWPF.Data.Entities;
using LanguageSkillsWPF.Infrastructure;
using LanguageSkillsWPF.Model;
using LanguageSkillsWPF.Utilities;
using LanguageSkillsWPF.Web;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace LanguageSkillsWPF.ViewModel
{
    internal class WordsSourceVM : ViewModelBase, IDropTarget
    {
        private string fileName;
        private Visibility listBoxVisibility;
        private Visibility loadingAnimation;
        private RelayCommand sendRequest;
        public ObservableCollection<CheckboxInformation> CheckboxesInformation { get; set; }

        public WordsSourceVM()
        {
            //SearchEntries = new ObservableCollection<SearchEntryModel>();
            CheckboxesInformation = new ObservableCollection<CheckboxInformation>();
            FileName = "Предпросмотр";
            LoadingAnimation = Visibility.Hidden;
            ListBoxVisibility = Visibility.Hidden;
            
        }

        #region Properties
        public ObservableCollection<SearchEntryModel> SearchEntries { get; set; }

        public Visibility ListBoxVisibility
        {
            get { return listBoxVisibility; }
            set
            {
                listBoxVisibility = value;
                OnPropertyChanged("ListBoxVisibility");
            }
        }

        public Visibility LoadingAnimation
        {
            get { return loadingAnimation; }
            set
            {
                loadingAnimation = value;
                OnPropertyChanged("LoadingAnimation");
            }
        }

        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                OnPropertyChanged("FileName");
            }
        }
        #endregion

        public RelayCommand SendRequest
        {
            get
            {
                return sendRequest ??
                    (sendRequest = new RelayCommand(async obj =>
                    {
                        RequestManager manager = RequestManager.Get();

                        WPFContext context = new WPFContext();

                        Collection<SearchEntryModel> sendingInfo = new Collection<SearchEntryModel>();
                        foreach (var checkbox in CheckboxesInformation)
                        {
                            if (checkbox.IsChecked == true)
                            {
                                sendingInfo.Add(checkbox.SearchEntry);
                                Card card = new Card()
                                {
                                    Word = checkbox.SearchEntry.Word,
                                    NextRepeat = null,
                                    StudyProgress = 0, 
                                    Rating = checkbox.SearchEntry.Count,
                                    Language = "sr"
                                };   
                                context.Add(card);
                            }
                        }
                        context.SaveChanges();
                        context.Dispose();
                        string json = JsonConvert.SerializeObject(sendingInfo);
                        StringContent body = new StringContent(json);

                        LoadingAnimation = Visibility.Visible;
                        RequestManager.Response response = await manager.SendRequest("https://7ccf-109-225-0-248.eu.ngrok.io", RequestMethod.POST, body);
                        LoadingAnimation = Visibility.Hidden;

                        //contentOutput.Text = response.content;
                        FileName = $"Response Code: {response.statusCodeName} ({response.statusCode}).";
                    }));
            }

        }

        #region Drag&Drop Handlers
        public async void Drop(IDropInfo dropInfo)
        {
            var dataObject = dropInfo.Data as DataObject;

            if (dataObject != null && dataObject.ContainsFileDropList())
            {
                string[] files = (string[])dataObject.GetData(DataFormats.FileDrop);

                if (FileName != files[0])
                {
                    //SearchEntries.Clear();
                    FileName = files[0];
                    ListBoxVisibility = Visibility.Hidden;
                    LoadingAnimation = Visibility.Visible;

                    var orderedWords = await Task.Run(() => TextAnalyze.GetTopNWords(FileName, 100));

                    foreach (var word in orderedWords)
                    {
                        //SearchEntries.Add(word);

                        CheckboxesInformation.Add( new ViewModel.CheckboxInformation(word));
                    }
                    ListBoxVisibility = Visibility.Visible;
                    LoadingAnimation = Visibility.Hidden;
                }
            }
            else
            {
                GongSolutions.Wpf.DragDrop.DragDrop.DefaultDropHandler.Drop(dropInfo);
            }
        }

        public void DragEnter(IDropInfo dropInfo)
        {
            //throw new NotImplementedException();
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo != null)
            {
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        public void DragLeave(IDropInfo dropInfo)
        {
            //throw new NotImplementedException();
        }
        #endregion
    }

    public class CheckboxInformation
    {
        public SearchEntryModel SearchEntry { get; set; }
        public bool IsChecked { get; set; }

        public CheckboxInformation (SearchEntryModel searchEntry)
        {
            SearchEntry = searchEntry;
            IsChecked = true;
        }
    }
}
