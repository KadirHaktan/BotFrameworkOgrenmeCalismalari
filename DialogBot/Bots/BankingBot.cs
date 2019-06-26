using DialogBot.Dialogs;
using DialogBot.Dialogs.Balance;
using DialogBot.Dialogs.Balance.CurrentAccount;
using DialogBot.Dialogs.Balance.SavingAccount;
using DialogBot.Dialogs.Payment;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DialogBot.Bots
{
    public class BankingBot : IBot
    {

        private readonly DialogSet dialogs;

        public BotAccessors accessors { get; set; }

        public BankingBot(BotAccessors accessors)
        {
            dialogs = new DialogSet(accessors.DialogStateBotAccessor);

            dialogs.Add(MainDialog.Instance);
            dialogs.Add(MakePaymentDialog.Instance);
            dialogs.Add(CheckCurrentAccountBalanceDialog.Instance);
            dialogs.Add(SavingCheckAccountBalanceDialog.Instance);
            dialogs.Add(CheckBalanceDialog.Instance);

            dialogs.Add(new ChoicePrompt("choicePrompt"));
            dialogs.Add(new TextPrompt("textPrompt"));
            dialogs.Add(new NumberPrompt<int>("numberPrompt"));

            this.accessors = accessors;
        }


        


        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                var state = await accessors.BankStateBotAccessor.GetAsync(turnContext, () => new BankStateBot(), cancellationToken);

                turnContext.TurnState.Add("BotAccessors",accessors);


                var dialogCtx = await dialogs.CreateContextAsync(turnContext, cancellationToken);

                if (dialogCtx != null)
                {
                    await dialogCtx.BeginDialogAsync(MainDialog.Id, cancellationToken);
                }

                else
                {
                    await dialogCtx.ContinueDialogAsync(cancellationToken);
                }

                await accessors._state.SaveChangesAsync(turnContext);


            }
        }
    }
}
