using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Net.Http;
using Newtonsoft.Json;
using Esbotifun_Bot;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


internal class Program
{
    //Abolfazl Esmaeel Beigi
    //Tamrin 1 - Telegram Bot
    //Bot Id: @Esbotifun_Bot
    private static void Main(string[] args)
    {
        var receiverOptions = new ReceiverOptions()
        {
            AllowedUpdates = new UpdateType[]
            {
                UpdateType.Message,
            }
        };
        BotHandlers.botClient.StartReceiving(BotHandlers.updateHandler, BotHandlers.pollingErrorHandler, receiverOptions);
        Console.ReadKey();
    }


}
