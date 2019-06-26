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

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(RootPath)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            services.AddSingleton(configuration);


            services.AddBot<BankingBot>(options =>
            {

                var conversationState = new ConversationState(new MemoryStorage());

                options.State.Add(conversationState);

                options.CredentialProvider = new ConfigurationCredentialProvider(configuration);

              
                
            });

            services.AddSingleton(servicesProvider =>
            {
                var options = servicesProvider.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;

                var conversationState = options.State.OfType<ConversationState>().FirstOrDefault();

                var accessors = new BotAccessors(conversationState)
                {
                    DialogStateBotAccessor = conversationState.CreateProperty<DialogState>(BotAccessors.DialogStateBotAccessorName),
                    BankStateBotAccessor = conversationState.CreateProperty<BankStateBot>(BotAccessors.BankStateBotAccessorName)
                };

                return accessors;
               
            });
                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
