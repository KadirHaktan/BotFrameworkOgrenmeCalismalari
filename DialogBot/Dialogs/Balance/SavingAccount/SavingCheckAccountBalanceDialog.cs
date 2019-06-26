using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DialogBot.Dialogs.Balance.SavingAccount
{
    public class SavingCheckAccountBalanceDialog:WaterfallDialog
    {
        public static string ID => "SavingCheckAccountBalanceDialog";
        public static SavingCheckAccountBalanceDialog Instance = new SavingCheckAccountBalanceDialog(ID);

        public SavingCheckAccountBalanceDialog(string dialogID, IEnumerable<WaterfallStep> steps=null) : base(dialogID, steps)
        {
            AddStep(async (stepContext, cancellianToken) =>
            {
                await stepContext.Context.SendActivityAsync("Your savings balance is...");
                return await stepContext.EndDialogAsync();
            });
        }
    }
}
