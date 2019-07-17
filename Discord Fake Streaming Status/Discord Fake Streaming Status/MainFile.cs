using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus;
using System.IO;

namespace Discord_Fake_Streaming_Status
{

    class MainFile
    {
        static DiscordClient discord;

        static void Main(string[] args)
        {
            try
            {
                MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Token is wrong");
                Console.WriteLine("Token is wrong");
                Console.WriteLine("Token is wrong");
                Console.WriteLine("Token is wrong");
                Console.WriteLine("Token is wrong");
                Console.ResetColor();

            }
        }

        static async Task MainAsync(string[] args)
        {
            string location = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string[] array = File.ReadAllLines(location + @"\config.txt", Encoding.UTF8);
            string token = array[0].Split('=')[1].Replace(" ", "");
            string StreamName = array[1].Split('=')[1];
            string URL = array[2].Split('=')[1].Replace(" ", "");
            discord = new DiscordClient(new DiscordConfiguration
            {

                Token = $"{token}", 
                TokenType = TokenType.User
            });

            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("c!start"))
                {
                
                    if (e.Message.Author.Id == discord.CurrentUser.Id)
                    {
                        await e.Message.RespondAsync("started!");
                        DiscordGame game = new DiscordGame() { Name = $"{StreamName}", StreamType = GameStreamType.Twitch, Url = $"{URL}" };
                        await discord.UpdateStatusAsync(game, UserStatus.Online);
                        Console.WriteLine("Token: " + token + "\nStreamName: " + StreamName + "\nURL: " + URL);


                    }
                }
            };
            Console.WriteLine("Ready for fire");
            Console.WriteLine("Token: " + token + "\nStreamName: " + StreamName + "\nURL: " + URL);
            await discord.ConnectAsync();
            await Task.Delay(-1);

        }
    }
}
