using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DialogBot.Dialogs.Balance.SavingAccount
{
    public class SavingCheckAccountBalanceDialog:WaterfallDialog
    {
        public static string Id => "SavingCheckAccountBalanceDialog";

        public SavingCheckAccountBalanceDialog(string dialogID, IEnumerable<WaterfallStep> steps) : base(dialogID, steps)
        {

        }
    }
}
