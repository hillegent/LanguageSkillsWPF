using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSkillsWPF.Data.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string? Example { get; set; }
        public int Rating { get; set; }
        public string Language { get; set; }
        public DateTime? NextRepeat { get; set; }
        public int StudyProgress { get; set; }

        public void AddProgress(int attempt)
        {
            if (attempt == 0)
            {
                CalculateDateProgress(attempt);
            }
            else
            {
                CalculateDateRegress(attempt);
            }
        }

        private void CalculateDateProgress(int attempt)
        {
            switch (StudyProgress)
            {
                case 0:
                    StudyProgress = 0;
                    NextRepeat = DateTime.Now.AddYears(1);
                    break;
                case 1:
                    StudyProgress = 2;
                    NextRepeat = DateTime.Now.AddSeconds(30);
                    break;
                case 2:
                    StudyProgress += 1;
                    NextRepeat = DateTime.Now.AddSeconds(60);
                    break;
                case 3:
                    StudyProgress += 1;
                    NextRepeat = DateTime.Now.AddMinutes(5);
                    break;
                case 4:
                    StudyProgress += 1;
                    NextRepeat = DateTime.Now.AddMinutes(10);
                    break;
                case 5:
                    StudyProgress += 1;
                    NextRepeat = DateTime.Now.AddMinutes(30);
                    break;
                case 6:
                    StudyProgress += 1;
                    NextRepeat = DateTime.Now.AddHours(2);
                    break;
                case 7:
                    StudyProgress += 1;
                    NextRepeat = DateTime.Now.AddHours(8);
                    break;
                case 8:
                    StudyProgress += 1;
                    NextRepeat = DateTime.Now.AddDays(1);
                    break;
                case 9:
                    StudyProgress += 1;
                    NextRepeat = DateTime.Now.AddDays(4);
                    break;
                case 10:
                    StudyProgress += 1;
                    NextRepeat = DateTime.Now.AddDays(10);
                    break;
                case 11:
                    StudyProgress += 1;
                    NextRepeat = DateTime.Now.AddMonths(1);
                    break;
                case 12:
                    StudyProgress += 1;
                    NextRepeat = DateTime.Now.AddMonths(3);
                    break;
                case 13:
                    StudyProgress += 1;
                    NextRepeat = DateTime.Now.AddMonths(6);
                    break;
                case 14:
                    StudyProgress = 14;
                    NextRepeat = DateTime.Now.AddYears(1);
                    break;
                default:
                    StudyProgress = 4;
                    NextRepeat = DateTime.Now.AddMinutes(10);
                    break;
            }
        }

        private void CalculateDateRegress(int attempt)
        {
            switch (StudyProgress)
            {
                case 0:
                    StudyProgress = 3;
                    NextRepeat = DateTime.Now.AddMinutes(2);
                    break;
                case 1:
                    StudyProgress = 1;
                    NextRepeat = DateTime.Now.AddSeconds(30);
                    break;
                case 2:
                    StudyProgress -= 1;
                    NextRepeat = DateTime.Now.AddSeconds(60);
                    break;
                case 3:
                    StudyProgress -= 1;
                    NextRepeat = DateTime.Now.AddMinutes(3);
                    break;
                case 4:
                case 5:
                case 6:
                case 7:

                    StudyProgress = 3;
                    NextRepeat = DateTime.Now.AddMinutes(5);
                    break;

                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    StudyProgress = 5;
                    NextRepeat = DateTime.Now.AddMinutes(30);
                    break;

                default:
                    StudyProgress = 4;
                    NextRepeat = DateTime.Now.AddMinutes(10);
                    break;
            }
        }
    }
}
