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
    {//Bir sınıfın bot olması için IBot interface'den implemente edilmesi gerekir

        private readonly DialogSet dialogs;
        //tüm diyaloglarımız birleştirmek ya da saklanması için gerekli bir sınıftan sadece okunabilir
        //olacak sekilde ve ayrıca private olarak belirleyerek bir nesne yapısı olusturduk.Ama nesneyi 
        //hafıza da yer almadı cünkü newlenmedi

        public BotAccessors accessors { get; set; }

        //Botumuza gerektiğinde statelerimize çağırmak istersek farklı state yapilarini içinde barındırabilen
        //bir aksesuar ya da model yapısı sınıfı diyebiliriz.Botumuz statelere erişebilmesi için farklı stateleri tutabilen model
        //class diyelim

        public BankingBot(BotAccessors accessors)
        {
            dialogs = new DialogSet(accessors.DialogStateBotAccessor);

            dialogs.Add(MainDialog.Instance);
            dialogs.Add(MakePaymentDialog.Instance);
            dialogs.Add(CheckBalanceDialog.Instance);
            dialogs.Add(CheckCurrentAccountBalanceDialog.Instance);
            dialogs.Add(SavingCheckAccountBalanceDialog.Instance);
            

            dialogs.Add(new ChoicePrompt("choicePrompt"));
            dialogs.Add(new TextPrompt("textPrompt"));
            dialogs.Add(new NumberPrompt<int>("numberPrompt"));

            this.accessors = accessors;
        }

        //BankingBot dan bir nesne olusturulduğunda yapıcı metot da dialoglar ve komutlar eklenecektir ve aynı zamanda durum
        // erişimci nesnemizi de tanımlama gereksinimliğine zorunlu hale getiriyor.


        


        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Aktivasyon tipi Mesajsa eğer
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {

                //Erişimciden durum kullanma gereği olursa diye bir durum nesnesi olusutruluyor ve sonrasında
                //dönen durumlara da durum botlara erişim sağlayan BotAccessors nesnesi ekleniyor
                var state = await accessors.BankStateBotAccessor.GetAsync(turnContext, () => new BankStateBot(), cancellationToken);

                turnContext.TurnState.Add("BotAccessors",accessors);


                
                var dialogCtx = await dialogs.CreateContextAsync(turnContext, cancellationToken);

                if (dialogCtx != null)
                {
                    await dialogCtx.BeginDialogAsync(MainDialog.ID, cancellationToken);
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
