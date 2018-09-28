using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ViewModels
{
    public class TestVM
    {
        public int testID { get; set; }
        public string username { get; set; } //początkowo miało to być imię + nazwisko
        public string testKey { get; set; } // klucz dostępu do testu; ma być tworzony automatycznie wraz z instancją testu
        public System.Nullable<int> percentageScore { get; set; } // uzyskany wynik
        public int timeLimit { get; set; } // czas na wykonanie testu w minutach (?)
        public bool isCompleted { get; set; } = false; // ustawianie na true po ukończeniu testu
        public DateTime? completionDate { get; set; } // kiedy ukończono test
        public System.Nullable<TimeSpan> completionTime { get; set; } // jak długo trwało wykonywanie testu
        public DateTime expirationDate { get; set; } // określa do kiedy można podejść do testu       
        public int testTemplateID { get; set; }
        
    }
}
