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
    public class CheckBalanceDialog : WaterfallDialog
    {
        public static string ID => "checkBalanceDialog";
        public static CheckBalanceDialog Instance { get; } = new CheckBalanceDialog(ID);


        public CheckBalanceDialog(string dialogID, IEnumerable<WaterfallStep> steps = null) : base(dialogID, steps)
        {
            AddStep(async (stepContext, cancellationToken) =>
            {
                return await stepContext.PromptAsync("choicePrompt",
                    new PromptOptions
                    {
                        Prompt = stepContext.Context.Activity.CreateReply($"[CheckBalanceDialog] Which account?"),
                        Choices = new[] { new Choice { Value = "Current" }, new Choice { Value = "Savings" } }.ToList()
                    });
            });

            AddStep(async (stepContext, cancellationToken) =>
            {
                var response = stepContext.Result as FoundChoice;

                if (response.Value == "Current")
                {
                    return await stepContext.BeginDialogAsync(CheckCurrentAccountBalanceDialog.ID);
                }

                if (response.Value == "Savings")
                {
                    return await stepContext.BeginDialogAsync(SavingCheckAccountBalanceDialog.ID);
                }

                return await stepContext.NextAsync();
            });
        }

    }

   

      

 }

