using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DialogBot.Dialogs.Balance.CurrentAccount
{
    public class CheckCurrentAccountBalanceDialog:WaterfallDialog
    {
        public CheckCurrentAccountBalanceDialog(string dialogID,IEnumerable<WaterfallStep> steps) : base(dialogID, steps)
        {

        }
    }
}
