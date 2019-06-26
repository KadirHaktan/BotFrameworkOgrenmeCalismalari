using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace DialogBot.Dialogs
{
    public class MainDialog:WaterfallDialog
    {
        public MainDialog(string dialogID,IEnumerable<WaterfallStep> step = null):base(dialogID,step)
        {

        }
    }
}
