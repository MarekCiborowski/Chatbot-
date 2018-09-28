using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminPanel.ViewModels.AdminViewModels.TestSchemeViewModels
{
    public class CreateTestSchemeVM
    {
        public List<questionInfo> questionList { get; set; }
        public string testSchemeName { get; set; }
        public string testSchemeDescription { get; set; }
    }

    public class questionInfo
    {
        public int questionID { get; set; }
        public string Text { get; set; }

        public string Type { get; set; }

        public bool isChecked { get; set; }
    }
}