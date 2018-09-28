using AdminPanel.ViewModels.AdminViewModels.TestViewModels;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AdminPanel.ViewModels.AdminViewModels.TestSchemeViewModels
{
    public class DeleteTestSchemeVM
    {
        public List<templateInfo> schemaList { get; set; }

        public DeleteTestSchemeVM()
        {
            schemaList = new List<templateInfo>();
        }
        public DeleteTestSchemeVM(List<templateInfo> list)
        {
            schemaList = new List<templateInfo>();
            schemaList = list;
        }

    }


}