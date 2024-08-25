using MHW_Randomizer.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHW_Randomizer
{
    public class MonsterRewards
    {
        /// <summary>
        /// The key is the monster to give low rank drops to, the value is what monster to get the drops and chances from
        /// </summary>
        public static Dictionary<string, string> DropsForLowRank = new Dictionary<string, string>
        {
            { "em001_01.itlot", "em001.itlot" }, //Pink Rathian with Rathian
            { "em002_01.itlot", "em002.itlot" }, //Azure Rathalos with Rathalos
            { "em007_01.itlot", "em007.itlot" }, //Black Diablos with Diablos
            { "em024.itlot", "em111.itlot" }, //Kushalia with Legiana
            //{ "em026.itlot", "em011.itlot" }, //Lunastra with Kirin
            { "em026.itlot", "em011.itlot" },
        };

        //.itlot file key
        private static Cipher cipher = new Cipher("D7N88VEGEnRl0HEHTO0xMQkbeMb37arJF488lREp90WYojAONkLoxfMt");

        public static void test()
        {
            GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + "/em118.itlot", cipher.Decipher(ChunkOTF.files["em118.itlot"].Extract()));
            GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + "/em026.itlot", cipher.Decipher(ChunkOTF.files["em026.itlot"].Extract()));
            GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + "/em037.itlot", cipher.Decipher(ChunkOTF.files["em037.itlot"].Extract()));
        }
    }
}
