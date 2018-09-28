using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessLogicLayer.Interfaces.IAdmin
{
    public interface IDropDownListHelper
    {
        IEnumerable<SelectListItem> GetNumericDropDownList(int number);
    }
}
