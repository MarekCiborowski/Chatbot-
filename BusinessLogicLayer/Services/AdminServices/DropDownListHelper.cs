using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLogicLayer.Interfaces.IAdmin;

namespace BusinessLogicLayer.Services.AdminServices
{
    class DropDownListHelper : IDropDownListHelper
    {
        public IEnumerable<SelectListItem> GetNumericDropDownList(int number)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            for (int i = 0; i <= number; i++)
            {
                list.Add(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }
            SelectList selectlist = new SelectList(list, "Value", "Text");

            return selectlist;
        }
    }
}
