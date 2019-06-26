using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DialogBot.Dialogs.Balance;
using DialogBot.Dialogs.Payment;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;

namespace DialogBot.Dialogs
{
    public class MainDialog:WaterfallDialog
    {
        public MainDialog(string dialogID,IEnumerable<WaterfallStep> step = null):base(dialogID,step)
        {
            AddStep(async (stepContext, cancellianToken) =>
            {
                return await stepContext.PromptAsync("choicePrompt",
                    new PromptOptions()
                    {
                        Prompt=stepContext.Context.Activity.CreateReply("Hi!! I am Banking Bot.Would like to make payment or balance"),
                        Choices = new[] {new Choice("Check balance"),new Choice("Make Payment")}
                    });
            });

            AddStep(async (stepContext, cancellianToken) =>
            {
                var response = (stepContext.Result as FoundChoice)?.Value;

                if(response=="Check balance")
                {
                    return await stepContext.BeginDialogAsync(CheckBalanceDialog.Id);
                }

                if(response=="Make Payment")
                {
                    return await stepContext.BeginDialogAsync(MakePaymentDialog.Id);
                }

                return await stepContext.NextAsync();
            });
        }
    }
}
