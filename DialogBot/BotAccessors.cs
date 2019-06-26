using DialogBot.Bots;
using Microsoft.Bot.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DialogBot
{
    public class BotAccessors
    {

        public ConversationState _state { get; set; }

        public BotAccessors(ConversationState _conversationState)
        {
            this._state = _conversationState ?? throw new ArgumentNullException(nameof(_state));
        }

        public IStatePropertyAccessor<BankStateBot> BankStateBotAccessor { get; set; }

        public static string BankStateBotAccessorName { get; } = $"{nameof(BotAccessors)}.BankStateBotAccessor";


       

        //public static string AccessorName1
       


    }
}
