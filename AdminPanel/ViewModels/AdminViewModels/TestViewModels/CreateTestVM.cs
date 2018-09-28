using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.ViewModels.AdminViewModels.TestViewModels
{
    public class CreateTestVM
    {
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan timeLimit { get; set; }

        public string userName { get; set; }

        public List<templateInfo> schemaList { get; set; }

        public int selectedTemplateID { get; set; }

        public CreateTestVM()
        {
            schemaList = new List<templateInfo>();
            Deadline = DateTime.Now;
        }
        public CreateTestVM(List<templateInfo> list)
        {
            schemaList = new List<templateInfo>();
            schemaList = list;
            Deadline = DateTime.Now;
        }

    }

    public class templateInfo
    {
        public int templateID { get; set; }
        public string templateName { get; set; }
        public string templateDescription { get; set; }

        public bool isChecked { get; set; }

        public templateInfo() { }

        public templateInfo(int id, string name, string desc)
        {
            templateID = id;
            templateName = name;
            templateDescription = desc;
        }

        public templateInfo(int id, string name, string desc, bool isCheck)
        {
            templateID = id;
            templateName = name;
            templateDescription = desc;
            isChecked = isCheck;
        }
    }
}