using YamlDotNet.RepresentationModel;
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
        public static void Main()
        {
            // ymlデータのロード
            var input = new StreamReader(yamlPath, Encoding.UTF8);
            var yaml = new YamlStream();
            yaml.Load(input);
            var rootNode = yaml.Documents[0].RootNode;

            Console.WriteLine(rootNode["api"]);
        }
    }

}
