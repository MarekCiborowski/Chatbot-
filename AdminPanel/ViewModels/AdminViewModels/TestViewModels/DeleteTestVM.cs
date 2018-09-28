using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.ViewModels.AdminViewModels.TestViewModels
{
    public class DeleteTestVM
    {
        public List<testInfo> testList { get; set; }

        public DeleteTestVM()
        {
            testList = new List<testInfo>();
        }

        public DeleteTestVM(List<testInfo> testList)
        {
            this.testList = new List<testInfo>();
            this.testList = testList;
        }
    }


    public class testInfo
    {
        public int testID { get; set; }

        public string userName { get; set; }

        public string templateName { get; set; }

        public bool isCompleted { get; set; }

        public DateTime expirationDate { get; set; }

        public bool isChecked { get; set; }

        public testInfo() { }

        public testInfo(int testID, string userName, string templateName, bool isCompleted, DateTime expirationDate, bool isChecked)
        {
            this.testID = testID;
            this.userName = userName;
            this.templateName = templateName;
            this.isCompleted = isCompleted;
            this.expirationDate = expirationDate;
            this.isChecked = isChecked;
        }
    }
}