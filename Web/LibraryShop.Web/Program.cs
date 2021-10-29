namespace LibraryShop.Web
{
    using System;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Twilio;
    using Twilio.Rest.Api.V2010.Account;

    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();

                        // I Used ENVIROMENT VERIABLES for accountSid and authToken

                        // string accountSid = Environment.GetEnvironmentVariable("Hakera");
                        // string authToken = Environment.GetEnvironmentVariable("twilioauth");
                        // TwilioClient.Init(accountSid, authToken);
                        // var message = MessageResource.Create(
                        //     body: "!!!!!!!!!!!!!!!",
                        //     from: new Twilio.Types.PhoneNumber("+13187668291"),
                        //     to: new Twilio.Types.PhoneNumber("+xxxxxxxx")
                        // );
                        // Console.WriteLine(message.Sid);
                    });
    }
}
