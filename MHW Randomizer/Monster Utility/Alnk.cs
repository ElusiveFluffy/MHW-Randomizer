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
            XorShift128Generator r = new XorShift128Generator(ViewModels.Randomizer.Seed);
            string[] alnkFiles = GameFiles.GetAllFilesOfType("dtt_alnk");
            alnkFiles = alnkFiles.OrderBy(x => x).ToArray();

            File.Create(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\Alnk Log.txt").Dispose();
            using (StreamWriter file = File.AppendText(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"\Alnk Log.txt"))
            {
                for (int a = 0; a < alnkFiles.Length; a++)
                {
                    //(Remove the root folder in the path)
                    string alnk = alnkFiles[a].Replace(GameFiles.GameFilesPath, "");
                    //Don't change small monsters, safi, xeno, fatalis, or zorah
                    if (alnk.Contains("ems") || alnk.Contains("em104") || alnk.Contains("em105") || alnk.Contains("em013") || alnk.Contains("em106"))
                        continue;

                    string[] folderNames = alnk.Split('\\');
                    file.WriteLine(QuestData.MonsterNames[Array.IndexOf(QuestData.MonsterEmNumber, folderNames[2] + "_" + folderNames[3]) + 1] + ": " + folderNames[2] + "_" + folderNames[3]);
                    file.WriteLine(Alnks.IsGroundMonster[a]);
                    file.WriteLine("Chosen Paths:");


                    byte[] alnkBytes = GameFiles.GetFile(alnk);
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
                            byte[] chosenAlnkData = GameFiles.GetFile(chosenAlnk.TargetAlnk!);
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
                            byte[] chosenAlnkData = GameFiles.GetFile(chosenAlnk.TargetAlnk!);
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

                    Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + Path.GetDirectoryName(alnk));
                    GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + alnk, newAlnk);
                    //Add alnks for variant monsters
                    if (alnk.Contains("em018"))
                    {
                        //Edit the monster ID
                        newAlnk[8] = 99;
                        Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"/em/em018/05/data");
                        GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"/em/em018/05/data/em018.dtt_alnk", newAlnk);
                    }

                    if (alnk.Contains("em023"))
                    {
                        //Edit the monster ID
                        newAlnk[8] = 92;
                        Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"/em/em023/05/data");
                        GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"/em/em023/05/data/em023.dtt_alnk", newAlnk);
                    }
                }
            }
            //Add custom think table for Shara to transform
            if (ViewModels.Settings.IncludeShara || ViewModels.Settings.ExpeditionIncludeShara)
            {
                //Add custom think file
                Directory.CreateDirectory(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"/em/em126/00/data");
                GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"/em/em126/00/data/em126_00.thk", Properties.Resources.em126_00thk);
            }
            //Write custom think file for Raging Brachydios otherwise they will get stuck at 30% HP
            //Credit to Fandirus for finding a way to make Raging Brachydios work
            GameFiles.WriteAndLogFile(ViewModels.Settings.SaveFolderPath + ViewModels.Randomizer.RandomizeRootFolder + @"/em/em063/05/data/em063_55.thk", Properties.Resources.em063_55thk);
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
