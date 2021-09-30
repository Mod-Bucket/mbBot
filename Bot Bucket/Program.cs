using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Bot_Bucket {
	public class Program {
		public static void Main(string[] args)
			=> new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync() {
            DiscordSocketClient _client = new DiscordSocketClient();

            _client.Log += Log;

            var token = JsonConvert.DeserializeObject<BotConfig>(File.ReadAllText("config.json")).Token;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            var config = new CommandServiceConfig();
            if (File.Exists("command-config.json"))
            {
                await Log(new LogMessage(LogSeverity.Info, "Startup", "Loading configuration from file"));
                config = JsonConvert.DeserializeObject<CommandServiceConfig>(File.ReadAllText("command-config.json"));
            }
            
            var handler = new CommandHandler(_client, new CommandService(config));
            await handler.InstallCommandsAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg) {
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
	}
}
