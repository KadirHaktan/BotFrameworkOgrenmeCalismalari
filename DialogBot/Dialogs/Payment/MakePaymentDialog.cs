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

        }

        public static string Id { get; set; }
    }
}
