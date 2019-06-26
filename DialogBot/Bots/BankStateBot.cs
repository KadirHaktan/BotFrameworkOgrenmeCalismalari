using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DialogBot.Bots
{
    public class BankStateBot
    {
        public int Amount { get; set; }
        public string Receipient { get; set; }
    }

    //BankStateBot sınıfını ASP.NET MVC 'de de yapıldığı gibi ilgili propertyleri bir yerde tutmak için
    //bir model yapısı olusturması gibi söyleyebiliriz.Yani state'imize ait özellikleri içinde barındıracak
    //bir model sınıfı olusturuyoruz.


    //State dediğimiz şey chatbot esnasında konuşmada geçen konuşmaları storage yapısı ile tutulan özel bilgileri denilebilir
    //State deki bilgiler de storage da tutulduğu içinde önceki konuşmalarda  chatbot da saklı kalacaktir.
}
