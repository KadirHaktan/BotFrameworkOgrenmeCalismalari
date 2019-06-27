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

        //Botumuzda gerektiğinde statelerimizi çağırmak istersek farklı state yapilarini içinde barındırabilen
        //bir aksesuar ya da bot state erişimcisi diyebiliriz.MVC deki gibi model mantığı gibi düşenebiliriz.

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

                //Erişimciden state kullanma gereği olursa diye bir state erişimcimizden banka işlemleri alakalı spesifik bilgilerin
                //storage ile kaydedilecek olan state erişimcisinin get metodu ile bot state özelliklerin tutulduğu sınıftan
                //bir state yapısı olusturuluyor
                var state = await accessors.BankStateBotAccessor.GetAsync(turnContext, () => new BankStateBot(), cancellationToken);

                turnContext.TurnState.Add("BotAccessors",accessors);
                //Birden fazla state'in bulunduğu erişimciyi ekliyoruz



                var dialogCtx = await dialogs.CreateContextAsync(turnContext,cancellationToken);
                //diyalog ortamı nesnesi yaratılıyor

                if (dialogCtx.ActiveDialog==null)//eğer diyalog aktif olarak boşsa diyalog Ana diyalog başlayacak 
                {
                    await dialogCtx.BeginDialogAsync(MainDialog.ID,cancellationToken);
                }

                else//Aksi durumda da diyalog devam edecek
                {
                    await dialogCtx.ContinueDialogAsync(cancellationToken);
                }
                
                
                //State de olan değişiklikleri kaydedilecek fonksiyon yani sohbette geçen spesifik verileri state'imize kaydedecez.Tabi
                //Startup dosyamizda belirtiğimiz Storage yapısı ile birlikte değişiklikleri kaydedecez
                await accessors._state.SaveChangesAsync(turnContext,false,cancellationToken);


            }
        }
    }
}
