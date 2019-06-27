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
        //Diyaloglar başlatacağı zaman ID ye göre başlatılacak ve ayrıca Botumuza da hafızaya eklenecek diyaloglar
        // o ilgili diyalog bir nesne alacaktır.Tabi her seferinde ID ve nesneyi alabilmek yeni newleme yapmak yerine
        //onlarla ilgili bir statik yapı oluşturmak olacaktır.Çünkü bu özellikleri her yerde hafıza da fazladan
        //açmadan rahatlıkla kullanmamız gerekir ki bu yapıları statik olarak oluşturmamız mantıklı olacaktır


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
            //Bu adımda girilecek komutun bir seçenek komtu olacağını belirtiyoruz choicePrompt diyerek ve sonrasında
            //Komutu ve şıkları PromptOptions sınıfından türetilmiş nesneye tanımlıyoruz

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

            //Bu adımda da seçeneğimiz neyse onu o seçeneğe göre ilgili diyaloğu başlatıyoruz ve dönüş olarak da bir sonraki
            //geçişi döndürecektir bize yani NextAsync() dediğimiz yapıda.Middleware'deki next yapısı aklınıza gelebilir
        }

    }

   

      

 }

