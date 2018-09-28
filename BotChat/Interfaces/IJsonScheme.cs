using Microsoft.Bot.Builder.FormFlow;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotChat.Interfaces
{
    public interface IJsonScheme
    {
        IForm<JObject> ValidateUser();
        IForm<JObject> BuildJsonForm();
    }
}