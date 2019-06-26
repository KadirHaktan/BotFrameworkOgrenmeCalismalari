using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DialogBot.Dialogs.Payment
{
    public class MakePaymentDialog:WaterfallDialog
    {
        public MakePaymentDialog(string dialogid,IEnumerable<WaterfallStep> steps = null) : base(dialogid, steps)
        {

            AddStep(async (stepContext, cancellianToken) =>
            {
                return await stepContext.PromptAsync(dialogid, new PromptOptions
                {
                    Prompt=stepContext.Context.Activity.CreateReply("Who would like to pay?")
                });

            });


            AddStep(async (stepContext, cancellianToken) =>
            {

                var state = await (stepContext.Context.TurnState["BotAccessors"] as BotAccessors).BankStateBotAccessor.GetAsync(stepContext.Context);

                state.Receipient = stepContext.Result.ToString();


                return await stepContext.PromptAsync(dialogid, new PromptOptions
                {
                    Prompt = stepContext.Context.Activity.CreateReply($"{state.Receipient} got it{Environment.NewLine} How much should I pay"),
                    RetryPrompt = stepContext.Context.Activity.CreateReply("Sorry,please give a number")
                });
            });


            AddStep(async (stepContext, cancellianToken) =>
            {
                var state = await (stepContext.Context.TurnState["BotAccessors"] as BotAccessors).BankStateBotAccessor.GetAsync(stepContext.Context);
                state.Amount = Int16.Parse(stepContext.Result.ToString());


                await stepContext.Context.SendActivityAsync($" Thank you,I've paid {state.Amount} to {state.Receipient}");
                return await stepContext.EndDialogAsync();

            });
        }

        public static string ID => "MakePaymentDialog";
        public static MakePaymentDialog Instance = new MakePaymentDialog(ID);
    }
}
