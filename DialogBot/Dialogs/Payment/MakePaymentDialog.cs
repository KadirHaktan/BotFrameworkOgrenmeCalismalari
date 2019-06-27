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
                return await stepContext.PromptAsync("textPrompt", new PromptOptions
                {
                    Prompt=stepContext.Context.Activity.CreateReply("Who would like to pay?")
                });
                //Komutun bir yazı şeklinde bize emulatörde göstereceğini söylüyoruz textPrompt diyerek

            });


            AddStep(async (stepContext, cancellianToken) =>
            {

                var state = await (stepContext.Context.TurnState["BotAccessors"] as BotAccessors).BankStateBotAccessor.GetAsync(stepContext.Context);
                //Bot State erişimcimiz ile banka işlemleri ile ilgili bot state oluşturuyoruz


                state.Receipient = stepContext.Result.ToString();
                //State imizde saklı olan Receipient bilgisini konuşma esnasında bizim söylediğimiz bilgiyi sonucunu stringe çevirerek
                // bir sonraki yazışmalarda bir daha sorulmadan bize ekranımıza sonuca döndürebilmek için oluşturulmuş tanımlamadır



                return await stepContext.PromptAsync("numberPrompt", new PromptOptions
                {
                    Prompt = stepContext.Context.Activity.CreateReply($"{state.Receipient} got it{Environment.NewLine} How much should I pay"),
                    RetryPrompt = stepContext.Context.Activity.CreateReply("Sorry,please give a number")
                });
                //Önceki yazı ya da komuta sorulan soru üstüne verilecek cevap bir sayı olacağı için komut olarak numberPrompt olarak belirtiyoruz.
            });


            AddStep(async (stepContext, cancellianToken) =>
            {
                var state = await (stepContext.Context.TurnState["BotAccessors"] as BotAccessors).BankStateBotAccessor.GetAsync(stepContext.Context);
                state.Amount = Int16.Parse(stepContext.Result.ToString());
                //Sayı olarak verdiğimiz cevabı sayı olarak state de yer alan Amount propertysi ile eşleştiriyoruz ki 
                //fiyat değerinin tekrar tekrar sorulmadan state de propertyi değer ne olarak eşleştirildiyse onun sonucu 
                //bize sonradan döndürebilsin.


                await stepContext.Context.SendActivityAsync($" Thank you,I've paid {state.Amount} to {state.Receipient}");
                return await stepContext.EndDialogAsync();

            });
        }

        public static string ID => "MakePaymentDialog";
        public static MakePaymentDialog Instance = new MakePaymentDialog(ID);
    }
}
