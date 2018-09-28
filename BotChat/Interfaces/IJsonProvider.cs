using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChat.Interfaces
{
    public interface IJsonProvider
    {
        string GetJsonForTest(int testID);
        string GetValidationJson();
    }
}
