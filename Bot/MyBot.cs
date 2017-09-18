using Discord;
using Discord.Commands;
using System;

namespace Bot
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;
        Random rand;

        string[] meme;

        public MyBot()
        {
            rand = new Random();

            meme = new string[]
            {
                "mem/1.jpg",
                "mem/2.jpg",
                "mem/3.jpg",
                "mem/4.jpg",
                "mem/5.jpg",
                "mem/6.jpg",
                "mem/7.jpg",
                "mem/8.jpg",
                "mem/9.jpg",
                "mem/10.png",
                "mem/11.jpg",
                "mem/12.jpg",
                "mem/13.jpg",
                "mem/14.jpg",
                "mem/15.jpg",
                "mem/16.jpg",
                "mem/17.png",
                "mem/18.png",
                "mem/19.png",
                "mem/20.png",
                "mem/21.png",
                "mem/22.png",
                "mem/23.jpg",
                "mem/24.jpg",
                "mem/25.jpg",
                "mem/26.png",
                "mem/27.gif",
                "mem/28.jpg",
                "mem/29.jpg",
                "mem/30.png",
                "mem/31.jpg"
            };
        
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });
            discord.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
            });
                
            commands = discord.GetService<CommandService>();

            RegisterMemeCommand();
            RegisterPurgeCommand();

            commands.CreateCommand("Hello")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Why are you introducing your self to a bot.");
                });

            RegisterMemeCommand();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzIyNzk5ODE2NjYyODQzMzkz.DFCHmg.OGmgLOaMJ_M5OF2MYOl529AX42E", TokenType.Bot);
            });
        }

        private void RegisterMemeCommand()
        {
            commands.CreateCommand("post random meme")
                .Do(async (e) =>
                {
                   int randomMemeIndex = rand.Next(meme.Length);
                   string memeToPost = meme[randomMemeIndex];
                   await e.Channel.SendFile(memeToPost);
                });
        }

        private void RegisterPurgeCommand()
        {
            commands.CreateCommand("Purge")
                .Do(async (e) =>
                {
                    Message[] messagesToDelete;
                    messagesToDelete = await e.Channel.DownloadMessages(100);

                    await e.Channel.DeleteMessages(messagesToDelete);
                });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}