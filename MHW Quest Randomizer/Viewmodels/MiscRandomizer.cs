using System.Collections.Generic;
using System.IO;
using Troschuetz.Random.Generators;

namespace MHW_Randomizer
{
    public static class MiscRandomizer
    {
        /// <summary>
        /// First number is the colour index, second is the red, green, and blue values
        /// </summary>
        private static List<byte[]> Colours;

        public static void Randomize()
        {
            if (IoC.Settings.RandomScoutflyColour)
            {
                Colours = new List<byte[]> {
                                                new byte[] { 147, 71, 213 }, //Purple
                                                new byte[] { 180, 39, 125 }, //Pinky Purple
                                                new byte[] { 227, 38, 190 }, //Pink
                                                new byte[] { 38, 87, 227 }, //Blue
                                                new byte[] { 34, 55, 128 }, //Dark Blue
                                                new byte[] { 38, 206, 227 }, //Light Blue
                                                new byte[] { 182, 255, 248 }, //Pastel Light Blue
                                                new byte[] { 33, 148, 0 }, //Dark Green
                                                new byte[] { 129, 238, 53 }, //Green
                                                new byte[] { 70, 214, 124 }, //Cyan Green
                                                new byte[] { 215, 241, 112 }, //Light Green
                                                new byte[] { 255, 190, 123 }, //DarkTan
                                                new byte[] { 227, 160, 38 }, //Orange
                                                new byte[] { 218, 127, 18 }, //Darker Orange
                                                new byte[] { 227, 38, 21 }, //Red
                                                new byte[] { 169, 0, 33 }, //Dark Red
                                                new byte[] { 254, 231, 21 }, //Yellow
                                            };
                byte[] scoutflyBytes = ChunkOTF.files["gi_param.gip"].ChunkState.ExtractItem(ChunkOTF.files["gi_param.gip"]);

                NR3Generator r = new NR3Generator(IoC.Randomizer.Seed);
                //Scoutfly Colour
                //Normal Monster/Tracking Anything
                scoutflyBytes = scoutflyBytes.RandomColourPicker(220, r, 240);
                //Elder Monster
                scoutflyBytes = scoutflyBytes.RandomColourPicker(232, r, 244);
                //Tempered Monster
                scoutflyBytes = scoutflyBytes.RandomColourPicker(236, r, 248);

                if (IoC.Settings.DifferentTrackScoutflyColour)
                {
                    //Trace Colour
                    //Normal Monster Trace
                    scoutflyBytes = scoutflyBytes.RandomColourPicker(240, r);
                    //Elder Monster Trace
                    scoutflyBytes = scoutflyBytes.RandomColourPicker(244, r);
                    //Tempered Monster Trace
                    scoutflyBytes = scoutflyBytes.RandomColourPicker(248, r);
                }

                Directory.CreateDirectory(IoC.Settings.ChunkFolderPath + @"\randomized\common\guide_insect\");
                File.WriteAllBytes(IoC.Settings.ChunkFolderPath + @"\randomized\common\guide_insect\gi_param.gip", scoutflyBytes);

            }
        }

        private static byte[] RandomColourPicker(this byte[] fileBytes, uint offset, NR3Generator r, uint traceOffset = 0)
        {
            int index = r.Next(Colours.Count);
            for (int i = 0; i < 3; i++)
            {
                fileBytes[offset + i] = IoC.Settings.CompletelyRandomScoutflyColour ? (byte)r.Next(256) : Colours[index][i];
                if (traceOffset != 0)
                    fileBytes[traceOffset + i] = fileBytes[offset + i];
                
            }
            Colours.RemoveAt(index);
            return fileBytes;
        }
    }
}
