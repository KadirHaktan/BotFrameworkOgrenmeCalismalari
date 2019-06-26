using DialogBot.Dialogs.Balance.CurrentAccount;
using DialogBot.Dialogs.Balance.SavingAccount;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DialogBot.Dialogs.Balance
{
    public class CheckBalanceDialog:WaterfallDialog
    {
        public CheckBalanceDialog(string dialogID,IEnumerable<WaterfallStep> steps=null) : base(dialogID, steps)
        {

            AddStep(async (stepContext, cancellianToken) =>
            {
                return await stepContext.PromptAsync(dialogID, new PromptOptions()
                {
                    Prompt = stepContext.Context.Activity.CreateReply("Which Account"),
                    Choices = new[]
                    {
                        new Choice
                        {
                            Value="Current"
                        },
                        new Choice
                        {
                            Value="Saving"
                        }
                    }.ToList()
                });
            
            });


            AddStep(async (stepContext, cancellianToken) =>
            {
                var response = stepContext.Result as FoundChoice;


                if (response.Value == "Current")
                {
                    return await stepContext.BeginDialogAsync(CheckCurrentAccountBalanceDialog.ID);
                }

                if (response.Value == "Saving")
                {
                    return await stepContext.BeginDialogAsync(SavingCheckAccountBalanceDialog.ID);
                }

                return await stepContext.ReplaceDialogAsync(Id);
            });

        }


        public static string ID => "CheckBalanceDialog";
        public static CheckBalanceDialog Instance = new CheckBalanceDialog(ID);
    }
}
