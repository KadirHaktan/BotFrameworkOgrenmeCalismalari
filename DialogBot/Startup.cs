using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DialogBot.Bots;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Integration;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DialogBot
{
    public class Startup
    {
        public string RootPath { get; set; }
        public Startup(IHostingEnvironment env)
        {
            RootPath = env.ContentRootPath;
        }
        //Kök dizinimize ya da yolumuz için aslında burda Depency Injection yaptık diyebiliriz.Çünkü
        // RootPath imize atadığımız tanımlama da bir interface ait property ile eşleştiriyoruz.Interface olduğu için de
        // aslında IHostingEnvironment dan implemente olmuş herhangi bir class'ı da aslında kök yolumuza atanabilecektir.Bir yerlerde
        // tabi Startup class'ından bir nesne oluşturursak.Yani IHostingEnvironment dan destek alan classdan faydalanılabilecektir.
        // Bu da bir class'a olan bağımlılıktan kurtarıyor.İşte bu yüzden aslında burda küçük de olsa bir Depency Injection uygulanmış.

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(RootPath)
                .AddEnvironmentVariables();

            //Konfigurasyon için gerekli nesnenin sonradan ayağa kalkmasını sağlayacak altyapı olusturluyor


            var configuration = builder.Build();
            //Build design patternda olduğu yukarıdaki kodda parça parça konfigürasyon için gerekli yapılar olusturluyor
            //daha sonrasında parçalar birleşip aslında Build() metodu ile nesneyi ayağa kaldırıyoruz.Build Design Pattern mantığı




            services.AddSingleton(configuration);
            //Konfigürasyon nesnesinin program çalıştığı sürece tek sefer de newlenip her seferinde bu tek 
            //nesne üzerinde işlemlerin devam etmesine katkıda sağlıyor.Singleton Design Pattern'dan geliyor

            
            services.AddBot<BankingBot>(options =>
            {

                var conversationState = new ConversationState(new MemoryStorage());
                //Sohbet state nesnesi tanımlanıyor ve stateleri de saklanması için de aynı zamanda
                //yapıcı metounda da bir IStorage'den türeyecek bir Storage class'ı tanımlanması gerekir.

                options.State.Add(conversationState);

                //BotFramework Opsiyonları durumu olarak da sohbet state'i ekleniyor

                options.CredentialProvider = new ConfigurationCredentialProvider(configuration);
                //Opsiyonları kimlik tanımlayıcısı da konfigürasyon olarak tanımlanıyor.Verilerin storage da tutmak için
                // bir yetkilendirme yapısı gerekir(Authentication). O da bizim olusturdugumuz konfigürasyonu kimlik
                // ataması ile yetki sahip olabileceğimizi söylüyoruz.

              
                
            });

            services.AddSingleton(servicesProvider =>
            {
                var options = servicesProvider.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;
                //Seçenekler olarak Opsiyonlardan Bot Framework'le ile ilgili olan opsiyonun değeri

                var conversationState = options.State.OfType<ConversationState>().FirstOrDefault();
                //Opsiyonumuza storage da tutulacak özel bilgiler yani state tipi bir sohbetten geçen bilgilerden
                //saklanacağı için ConversationState tipinde olup ilk bulduğu veya geçerli olan değeri döndürecek bir
                // tanımlama  yapilmis

                var accessors = new BotAccessors(conversationState)
                {
                    DialogStateBotAccessor = conversationState.CreateProperty<DialogState>(BotAccessors.DialogStateBotAccessorName),
                    BankStateBotAccessor = conversationState.CreateProperty<BankStateBot>(BotAccessors.BankStateBotAccessorName)

                    //ConversationState yeni stateleri özellik yaratarak eklenmesi ya da üye edilmesi diyebiliriz.Conversationstate
                    //konuşma esnasında kullanılacak olan statelerin listesi gibi düşünelebilir.Yapıcı metota da tanımlanma nedeni 
                    // de erişimci sayesinde dediğimiz gibi konuşma esnasında kullanılacak statelerin hafıza da bir listelenebilecek
                    // yapıya eklenmesi diyebiliriz.
                };

                

                return accessors;
               
            });
                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            //app.UseMvc();

            app.UseStaticFiles();

            app.UseBotFramework();
                //uygulamamıza BotFramework unu kullanacaz diyoruz
        }
    }
}
