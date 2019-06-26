using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DialogBot.Dialogs.Balance
{
    public class CheckBalanceDialog:WaterfallDialog
    {
        public CheckBalanceDialog(string dialogID,IEnumerable<WaterfallStep> steps) : base(dialogID, steps)
        {

        }
    }
}
