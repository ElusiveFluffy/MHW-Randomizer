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

                    string[] folderNames = alnk.EntireName.Split('\\');
                    file.WriteLine(QuestData.MonsterNames[Array.IndexOf(QuestData.MonsterEmNumber, folderNames[2] + "_" + folderNames[3]) + 1] + ": " + folderNames[2] + "_" + folderNames[3]);
                    file.WriteLine(Alnks.IsGroundMonster[a]);
                    file.WriteLine("Chosen Paths:");


                    byte[] alnkBytes = alnk.Extract();
                    byte[] newAlnk = new byte[44];
                    //Copy the Header
                    Array.Copy(alnkBytes, newAlnk, 44);

                    AlnkPaths chosenAlnk = new AlnkPaths();
                    byte pathCount = 0;
                    int[] chosenIndexes = new int[7];
                    if (Alnks.IsGroundMonster[a])
                    {
                        for (int map = 0; map < 7; map++)
                        {
                            //Chose a alnk and extract it
                            chosenAlnk = Alnks.GroundOffsets[map][r.Next(Alnks.GroundOffsets[map].Length)];
                            byte[] chosenAlnkData = ChunkOTF.files[chosenAlnk.TargetAlnk].Extract();
                            file.WriteLine(chosenAlnk.TargetAlnk);
                            chosenIndexes[map] = Array.IndexOf(Alnks.GroundOffsets[map], chosenAlnk);

                            //Make a new byte array to be able to copy the path to
                            byte[] copyBuffer = new byte[chosenAlnk.PathLength];
                            Array.Copy(chosenAlnkData, chosenAlnk.PathOffset, copyBuffer, 0, chosenAlnk.PathLength);

                            //Check if the first path has been cut off (due to it causing the path to not work)
                            if (chosenAlnk.TrimmedMapIdentifier)
                            {
                                copyBuffer = AddIdentifier(chosenAlnk, copyBuffer);
                            }

                            //Add the path count
                            pathCount += copyBuffer[4];

                            //Concat the path to the new alnk
                            newAlnk = newAlnk.Concat(copyBuffer).ToArray();
                        }
                    }
                    else
                    {
                        for (int map = 0; map < 7; map++)
                        {
                            //Chose a alnk and extract it
                            chosenAlnk = Alnks.FlyingOffsets[map][r.Next(Alnks.FlyingOffsets[map].Length)];
                            byte[] chosenAlnkData = ChunkOTF.files[chosenAlnk.TargetAlnk].Extract();
                            file.WriteLine(chosenAlnk.TargetAlnk);
                            chosenIndexes[map] = Array.IndexOf(Alnks.FlyingOffsets[map], chosenAlnk);

                            //Make a new byte array to be able to copy the path to
                            byte[] copyBuffer = new byte[chosenAlnk.PathLength];
                            Array.Copy(chosenAlnkData, chosenAlnk.PathOffset, copyBuffer, 0, chosenAlnk.PathLength);

                            //Check if the first path has been cut off (due to it causing the path to not work)
                            if (chosenAlnk.TrimmedMapIdentifier)
                            {
                                copyBuffer = AddIdentifier(chosenAlnk, copyBuffer);
                            }

                            //Add the path count
                            pathCount += copyBuffer[4];

                            //Concat the path to the new alnk
                            newAlnk = newAlnk.Concat(copyBuffer).ToArray();
                        }
                    }

                    //Add map entries for the arenas so the monsters don't try to run...hopefully
                    //newAlnk = newAlnk.Concat(Properties.Resources.Iceborne_Arenas).ToArray();

                    //Add 5 just as a failsafe
                    //pathCount += 5;

                    //Something to do with path count maybe???
                    newAlnk[40] = pathCount;

                    file.WriteLine("Chosen Indexes: " + string.Join(", ", chosenIndexes));
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

        private static byte[] AddIdentifier(AlnkPaths chosenAlnk, byte[] copyBuffer)
        {
            byte[] identifier = new byte[8];
            //Copy the map number
            Array.Copy(copyBuffer, identifier, 4);
            //Set the new path count
            identifier[4] = chosenAlnk.PathCount;
            //Insert the identifier bytes to the start
            copyBuffer = identifier.Concat(copyBuffer).ToArray();
            return copyBuffer;
        }
    }
}
