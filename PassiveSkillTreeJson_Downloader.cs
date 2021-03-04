using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PassiveSkillTreePlanter
{
    //Many thanks to https://github.com/EmmittJ/PoESkillTree

    public class PassiveSkillTreeJson_Downloader
    {
        private const string TreeAddress = "https://www.pathofexile.com/passive-skill-tree/";

        public static async Task<string> DownloadSkillTreeToFileAsync(string filePath)
        {
            var code = await new HttpClient().GetStringAsync(TreeAddress);
            var start = "var passiveSkillTreeData = ";
            var regex = new Regex($"{start}{{(?>[^{{}}]|(?<open>){{|(?<-open>)}})*}}(?(o)(?!))");
            var skillTreeObj = regex.Match(code).Value.Replace("\\/", "/");
            skillTreeObj = skillTreeObj.Substring(start.Length, skillTreeObj.Length - start.Length);
            File.WriteAllText(filePath, skillTreeObj);
            return skillTreeObj;
        }
    }
}