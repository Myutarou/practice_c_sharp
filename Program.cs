using YamlDotNet.RepresentationModel;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

/// <summary>
/// botを起動する名前空間
/// </summary>
namespace Program
{
    /// <summary>
    /// botを起動するMain関数を保持するクラス
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ymlファイルのURI
        /// </summary>
        private static string yamlPath = "data.yml";

        private DiscordSocketClient client;
        public static CommandService commands;
        public static IServiceProvider services;
        
        public static void Main()
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });
            client.Log += Log;
            commands = new CommandService();
            // services = new IServiceCollection().BuildServiceProvider();
            // client.MessageReceived += CommandRecieved;
            string token = GetYamlData("token");
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        /// <summary>
        /// yamlデータを抽出する関数
        /// </summary>
        /// <param name="data">ymlのデータ</param>
        /// <returns>指定されたデータを返す</returns>
        public static string GetYamlData(string data)
        {
            // ymlデータのロード
            var input = new StreamReader(yamlPath, Encoding.UTF8);
            var yaml = new YamlStream();
            yaml.Load(input);
            var rootNode = yaml.Documents[0].RootNode;

#pragma warning disable CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
#pragma warning disable CS8603 // Null 参照戻り値である可能性があります。
            return (string)rootNode[data];
#pragma warning restore CS8603 // Null 参照戻り値である可能性があります。
#pragma warning restore CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
        }
    }

}
