using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.IO;



namespace Esbotifun_Bot
{
    public static class BotHandlers
    {
        public static TelegramBotClient botClient = new TelegramBotClient("6316353674:AAEzLtJ0jQiftDEXQaoyu3iMdygLahzH4EQ");
        public static Task pollingErrorHandler(ITelegramBotClient bot, Exception exp, CancellationToken ctoken)
        {
            throw new NotImplementedException();
        }

        public static async Task updateHandler(ITelegramBotClient bot, Update update, CancellationToken ctoken)
        {
            bot = botClient;
            string messageText = update.Message.Text;
            long chatId = update.Message.Chat.Id;
            long? userId = update.Message.From.Id;
            int messageId = Convert.ToInt32(update.Message.MessageId);
            string? userName = update.Message.From.Username;

            if (update.Type == UpdateType.Message)
            {
                if (update.Message.Type == MessageType.Text)
                {
                    if ((messageText.ToLower() == "/start") || (messageText.ToLower() == "/start@esbotifun_bot")) //Start Command 
                    {
                        await bot.SendTextMessageAsync(chatId, replyToMessageId: messageId,
                            text: $"Hello @{userName}. This bot is made to make you have some fun. \n" +
                                  "if you need any help with commands, use help command");

                    }
                    else if ((messageText.ToLower() == "/dadjoke") || (messageText.ToLower() == "/dadjoke@esbotifun_bot")) //Dad Joke command
                    {
                        string api = "https://api.api-ninjas.com/v1/dadjokes?limit=2";
                        using (HttpClient httpclient = new HttpClient())
                        {
                            httpclient.DefaultRequestHeaders.Add("X-Api-Key", "sIQIYVKdEkSr8XLi1R9myg==JQg3RCbMcosdFZjE");
                            HttpResponseMessage response = await httpclient.GetAsync(api);

                            if (response.IsSuccessStatusCode)
                            {
                                string responsecontent = await response.Content.ReadAsStringAsync();

                                List<Joke> list = JsonConvert.DeserializeObject<List<Joke>>(responsecontent);
                                foreach (var item in list)
                                {
                                    await bot.SendTextMessageAsync(chatId, replyToMessageId: messageId, text: item.joke.ToString());
                                }
                            }

                        }
                    }
                    else if ((messageText.ToLower() == "/randomquote") || (messageText.ToLower() == "/randomquote@esbotifun_bot")) //Quote Command code
                    {
                        string api = "https://api.quotable.io/random?maxLength=150&minLength=100";
                        using (HttpClient httpclient = new HttpClient())
                        {
                            HttpResponseMessage response = await httpclient.GetAsync(api);
                            if (response.IsSuccessStatusCode)
                            {
                                string responsecontent = await response.Content.ReadAsStringAsync();
                                Quote quotes = JsonConvert.DeserializeObject<Quote>(responsecontent);
                                string quote = $"{quotes.content} \n -{quotes.author}";
                                await bot.SendTextMessageAsync(chatId, replyToMessageId: messageId, text: quote);

                            }
                        }
                    }
                    else if ((messageText.ToLower() == "/gimmememe") || (messageText.ToLower() == "/gimmememe@esbotifun_bot")) //Meme Command code
                    {
                        try
                        {
                            string api = "https://meme-api.com/gimme";
                            using (HttpClient httpclient = new HttpClient())
                            {
                                HttpResponseMessage response = await httpclient.GetAsync(api);
                                if (response.IsSuccessStatusCode)
                                {
                                    string responsecontent = await response.Content.ReadAsStringAsync();
                                    Meme meme = JsonConvert.DeserializeObject<Meme>(responsecontent);
                                    string memeTemplate =
                                        $"[{meme.title}]({meme.url}) \n" +
                                        $"Author: {meme.author} \n" +
                                        $"NSFS: {meme.nsfw} \n" +
                                        $"Redit link: {meme.postLink}";
                                    await bot.SendTextMessageAsync(chatId, replyToMessageId: messageId, text: memeTemplate, parseMode: ParseMode.Markdown);

                                }
                            }
                        }
                        catch (Exception)
                        {
                            await bot.SendTextMessageAsync(chatId, replyToMessageId: messageId, text: "Oops! something happened. Try again please.");
                        }
                    }
                    else if ((messageText.ToLower() == "/help") || (messageText.ToLower() == "/help@esbotifun_bot")) //Help Command Code
                    {
                        string helptext =
                            "/start: To start the bot. \n" +
                            "/dadjoke: sends two dad jokes. \n " +
                            "/randomquote: A completely random quote. Don't take it personal \n" +
                            "/gimmememe: A random meme from reddit.";

                        await bot.SendTextMessageAsync(chatId, replyToMessageId: messageId, text: helptext);
                    }

                }

            }
        }
    }
}
