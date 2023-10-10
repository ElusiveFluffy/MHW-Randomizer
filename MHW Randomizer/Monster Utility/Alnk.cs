using System.IO;
using System.Linq;
using System;

namespace MHW_Randomizer
{
    public class Alnk
    {
        public static void CreateAlnks()
        {
            {
                Files[] alnkFiles = ChunkOTF.files.Values.Where(o => o.Name.Contains("alnk")).ToArray();
                alnkFiles = alnkFiles.OrderBy(x => x.Name).ToArray();
                for (int a = 0; a < alnkFiles.Length; a++)
                {
                    Files alnk = alnkFiles[a];
                    if (alnk.Name.Contains("ems"))
                        continue;

                    byte[] alnkBytes = alnk.Extract();
                    byte[] newAlnk = new byte[44];
                    Array.Copy(alnkBytes, newAlnk, 44);
                    newAlnk[40] = 7;
                    if (QuestData.IsGroundMonster[a])
                    {
                        newAlnk = newAlnk.Concat(Properties.Resources.m101_Ground).ToArray();
                        newAlnk = newAlnk.Concat(Properties.Resources.m102_Ground).ToArray();
                        newAlnk = newAlnk.Concat(Properties.Resources.m103_Ground).ToArray();
                        newAlnk = newAlnk.Concat(Properties.Resources.m104_Ground).ToArray();
                        newAlnk = newAlnk.Concat(Properties.Resources.m105_Ground).ToArray();
                        newAlnk = newAlnk.Concat(Properties.Resources.m108_Ground).ToArray();
                        newAlnk = newAlnk.Concat(Properties.Resources.m109_Ground).ToArray();
                    }
                    else
                    {
                        newAlnk = newAlnk.Concat(Properties.Resources.m101_Flying).ToArray();
                        newAlnk = newAlnk.Concat(Properties.Resources.m102_Flying).ToArray();
                        newAlnk = newAlnk.Concat(Properties.Resources.m103_Flying).ToArray();
                        newAlnk = newAlnk.Concat(Properties.Resources.m104_Flying).ToArray();
                        newAlnk = newAlnk.Concat(Properties.Resources.m105_Flying).ToArray();
                        newAlnk = newAlnk.Concat(Properties.Resources.m108_Flying).ToArray();
                        newAlnk = newAlnk.Concat(Properties.Resources.m109_Flying).ToArray();
                    }

                    Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + alnk.EntireName.Truncate(alnk.EntireName.Length - 14));
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + alnk.EntireName, newAlnk);
                    //Add alnks for variant monsters
                    if (alnk.Name.Contains("em018"))
                    {
                        //Edit the monster ID
                        newAlnk[8] = 99;
                        Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"/em/em018/05/data");
                        File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"/em/em018/05/data/em018.dtt_alnk", newAlnk);
                    }

                    if (alnk.Name.Contains("em023"))
                    {
                        //Edit the monster ID
                        newAlnk[8] = 92;
                        Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"/em/em023/05/data");
                        File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"/em/em023/05/data/em023.dtt_alnk", newAlnk);
                    }
                }
                //Add custom think table for Shara to transform
                if (IoC.Settings.IncludeShara || IoC.Settings.ExpeditionIncludeShara)
                {
                    //Add custom think file
                    Directory.CreateDirectory(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"/em/em126/00/data");
                    File.WriteAllBytes(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"/em/em126/00/data/em126_00.thk", Properties.Resources.em126_00thk);
                }
            }
        }
    }
}
