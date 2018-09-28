using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.ViewModels.AdminViewModels.TestViewModels
{
    public class ShowKeyVM
    {
        public string testKey { get; set; }

        public ShowKeyVM() {}

        public ShowKeyVM(string key)
        {
            testKey = key;
        }
    }
}