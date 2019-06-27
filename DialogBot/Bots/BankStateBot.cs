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


   //State dediğimiz şey konuşma esnasında storage yapısı ile chatbot'un hafızasını kaydedilecek olan önemli bilgilerin saklandığı
   //yapıdır.O saklanacak verilerin yapısal bir örneği olusturması sayesinde kaydedikten chatbot her seferinde kullanıcıya data
   //değerini sormadan verinin aitliğine de anlayabilecek.
}
