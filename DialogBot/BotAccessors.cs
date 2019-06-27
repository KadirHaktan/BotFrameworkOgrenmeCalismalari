using DialogBot.Bots;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DialogBot
{
    public class BotAccessors
    {

        public ConversationState _state { get; set; }//Konuşma esnasında kullanılacak statelerin liste gibi bir yapıda
        //saklanabilmesi için kullanılacak liste yapılı state diyebiliriz.

        public BotAccessors(ConversationState _conversationState)
        {
            this._state = _conversationState ?? throw new ArgumentNullException(nameof(_state));
        }

        public  IStatePropertyAccessor<BankStateBot> BankStateBotAccessor { get; set; }
        public  IStatePropertyAccessor<DialogState> DialogStateBotAccessor { get; set; }

        public static string BankStateBotAccessorName { get; } = $"{nameof(BotAccessors)}.BankStateBotAccessor";
        public static string DialogStateBotAccessorName { get; } = $"{nameof(BotAccessors)}.DialogStateBotAccessor";
        //ConversationState nesnemize yeni özellik ya da state eklenirken ait olabileceği bir string tipinde özellikleri oluşturulmuş.
       


    }

    //Birden fazla bot state erişim sağlayabilmek için kullanılacak erişimci sınıfı.
}
