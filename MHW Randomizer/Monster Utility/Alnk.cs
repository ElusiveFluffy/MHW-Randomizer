using System.IO;
using System.Linq;
using System;
using Troschuetz.Random.Generators;

namespace MHW_Randomizer
{
    public class Alnk
    {
        public static void CreateAlnks()
        {
            XorShift128Generator r = new XorShift128Generator(IoC.Randomizer.Seed);
            Files[] alnkFiles = ChunkOTF.files.Values.Where(o => o.Name.Contains("alnk")).ToArray();
            alnkFiles = alnkFiles.OrderBy(x => x.Name).ToArray();

            File.Create(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Alnk Log.txt").Dispose();
            using (StreamWriter file = File.AppendText(IoC.Settings.SaveFolderPath + IoC.Randomizer.RandomizeRootFolder + @"\Alnk Log.txt"))
            {
                for (int a = 0; a < alnkFiles.Length; a++)
                {
                    Files alnk = alnkFiles[a];
                    //Don't change small monsters, safi, xeno, fatalis, or zorah
                    if (alnk.Name.Contains("ems") || alnk.Name.Contains("em104") || alnk.Name.Contains("em105") || alnk.Name.Contains("em013") || alnk.Name.Contains("em106"))
                        continue;

                    file.WriteLine(alnk.Name);

                    byte[] alnkBytes = alnk.Extract();
                    byte[] newAlnk = new byte[44];
                    //Copy the Header
                    Array.Copy(alnkBytes, newAlnk, 44);

                    int chosenAlnk = 0;
                    byte pathCount = 0;
                    int[] chosenIndexes = new int[7];
                    if (Alnks.IsGroundMonster[a])
                    {
                        for (int map = 0; map < 7; map++)
                        {
                            //Chose a alnk and extract it
                            chosenAlnk = r.Next(Alnks.GroundOffsets[map].Length);
                            byte[] chosenAlnkData = ChunkOTF.files[Alnks.GroundOffsets[map][chosenAlnk].TargetAlnk].Extract();
                            chosenIndexes[map] = chosenAlnk;

                            //Make a new byte array to be able to copy the path to
                            byte[] copyBuffer = new byte[Alnks.GroundOffsets[map][chosenAlnk].PathLength];
                            Array.Copy(chosenAlnkData, Alnks.GroundOffsets[map][chosenAlnk].PathOffset, copyBuffer, 0, Alnks.GroundOffsets[map][chosenAlnk].PathLength);

                            //Add the path count
                            pathCount += copyBuffer[4];

                            //Concat the path to the new alnk
                            newAlnk = newAlnk.Concat(copyBuffer).ToArray();
                        }
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

                    //Add map entries for the arenas so the monsters don't try to run...hopefully
                    //newAlnk = newAlnk.Concat(Properties.Resources.Iceborne_Arenas).ToArray();

                    //Add 5 just as a failsafe
                    //pathCount += 5;

                    //Something to do with path count maybe???
                    newAlnk[40] = pathCount;

                    file.WriteLine("Chosen Paths: " + string.Join(", ", chosenIndexes));
                    file.WriteLine("Path Count: " + pathCount);
                    file.WriteLine("\n--------------------------");

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
