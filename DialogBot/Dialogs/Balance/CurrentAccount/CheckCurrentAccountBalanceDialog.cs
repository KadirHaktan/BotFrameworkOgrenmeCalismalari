using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DialogBot.Dialogs.Balance.CurrentAccount
{
    public class CheckCurrentAccountBalanceDialog:WaterfallDialog
    {
        public static string Id => "CheckCurrentAccountBalanceDialog";
        public static CheckCurrentAccountBalanceDialog Instance = new CheckCurrentAccountBalanceDialog(Id);



        public CheckCurrentAccountBalanceDialog(string dialogID,IEnumerable<WaterfallStep> steps=null) : base(dialogID, steps)
        {
            AddStep(async (stepContext, cancellianToken) =>
            {
                await stepContext.Context.SendActivityAsync("Your current balance is...");
                return await stepContext.EndDialogAsync();

            });

        }
    }
}
