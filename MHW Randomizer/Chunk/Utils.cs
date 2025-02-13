﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace MHW_Randomizer
{
    class Utils
    {
        // Import Oodle decompression
        [DllImport("oo2core_8_win64.dll")]
        private static extern int OodleLZ_Decompress(byte[] buffer, long bufferSize, byte[] outputBuffer, long outputBufferSize, uint a, uint b, ulong c, uint d, uint e, uint f, uint g, uint h, uint i, uint threadModule);

        // Decompress oodle chunk
        // Part of https://github.com/Crauzer/OodleSharp
        public static byte[] Decompress(byte[] buffer, int size, int uncompressedSize)
        {
            byte[] decompressedBuffer = new byte[uncompressedSize];
            int decompressedCount = OodleLZ_Decompress(buffer, size, decompressedBuffer, uncompressedSize, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3);

            if (decompressedCount == uncompressedSize)
            {
                return decompressedBuffer;
            }
            else if (decompressedCount < uncompressedSize)
            {
                return decompressedBuffer.Take(decompressedCount).ToArray();
            }
            else
            {
                throw new Exception("There was an error while decompressing.");
            }
        }

        // Console printing helper
        public static void Print(string Input, bool Before)
        {
            if (!Before)
            {
                Console.WriteLine(Input);
                Console.WriteLine("==============================");
            }
            else
            {
                Console.WriteLine("\n==============================");
                Console.WriteLine(Input);
            }
        }

        // Get right chunk encryption key for iteration
        public static byte[] GetChunkKey(int i)
        {
            List<byte[]> chunkKeys = new List<byte[]>
            {
                // 0: ac76cb97ec7500133a81038e7a82c80a
                new byte[] { 0xac, 0x76, 0xcb, 0x97, 0xec, 0x75, 0x00, 0x13, 0x3a, 0x81, 0x03, 0x8e, 0x7a, 0x82, 0xc8, 0x0a },
                // 1: 6bb5c956e44d00bc305233cfbfaafa25
                new byte[] { 0x6b, 0xb5, 0xc9, 0x56, 0xe4, 0x4d, 0x00, 0xbc, 0x30, 0x52, 0x33, 0xcf, 0xbf, 0xaa, 0xfa, 0x25 },
                // 2: 8f021dccb0f2787206fbdee2390bbb5c
                new byte[] { 0x8f, 0x02, 0x1d, 0xcc, 0xb0, 0xf2, 0x78, 0x72, 0x06, 0xfb, 0xde, 0xe2, 0x39, 0x0b, 0xbb, 0x5c },
                // 3: da5c1e531d8359157875bd63bfaafa25
                new byte[] { 0xda, 0x5c, 0x1e, 0x53, 0x1d, 0x83, 0x59, 0x15, 0x78, 0x75, 0xbd, 0x63, 0xbf, 0xaa, 0xfa, 0x25 },
                // 4: 1f6d31c883dd716d7e8f598ce23f1929
                new byte[] { 0x1f, 0x6d, 0x31, 0xc8, 0x83, 0xdd, 0x71, 0x6d, 0x7e, 0x8f, 0x59, 0x8c, 0xe2, 0x3f, 0x19, 0x29 },
                // 5: 4bb0de04e4e0856980ccb2942f9ce9f9
                new byte[] { 0x4b, 0xb0, 0xde, 0x04, 0xe4, 0xe0, 0x85, 0x69, 0x80, 0xcc, 0xb2, 0x94, 0x2f, 0x9c, 0xe9, 0xf9 },
                // 6: 8bce54dc4c11139a7875bd63bfaafa25
                new byte[] { 0x8b, 0xce, 0x54, 0xdc, 0x4c, 0x11, 0x13, 0x9a, 0x78, 0x75, 0xbd, 0x63, 0xbf, 0xaa, 0xfa, 0x25 },
                // 7: ec13345966ce7312440089a2ceddcee9
                new byte[] { 0xec, 0x13, 0x34, 0x59, 0x66, 0xce, 0x73, 0x12, 0x44, 0x00, 0x89, 0xa2, 0xce, 0xdd, 0xce, 0xe9 },
                // 8: e4662c709c753a039a2c0f5ae23f1929
                new byte[] { 0xe4, 0x66, 0x2c, 0x70, 0x9c, 0x75, 0x3a, 0x03, 0x9a, 0x2c, 0x0f, 0x5a, 0xe2, 0x3f, 0x19, 0x29 },
                // 9: a492fc9033949c15a033ac223735cca7
                new byte[] { 0xa4, 0x92, 0xfc, 0x90, 0x33, 0x94, 0x9c, 0x15, 0xa0, 0x33, 0xac, 0x22, 0x37, 0x35, 0xcc, 0xa7 },
                // 10: 25099c1f911a26e5ce9172f07a82c80a
                new byte[] { 0x25, 0x09, 0x9c, 0x1f, 0x91, 0x1a, 0x26, 0xe5, 0xce, 0x91, 0x72, 0xf0, 0x7a, 0x82, 0xc8, 0x0a },
                // 11: d1d29d7446d4fdf1a033ac223735cca7
                new byte[] { 0xd1, 0xd2, 0x9d, 0x74, 0x46, 0xd4, 0xfd, 0xf1, 0xa0, 0x33, 0xac, 0x22, 0x37, 0x35, 0xcc, 0xa7 },
                // 12: 7eb268373b5d361ed6d313e2933c4dcb
                new byte[] { 0x7e, 0xb2, 0x68, 0x37, 0x3b, 0x5d, 0x36, 0x1e, 0xd6, 0xd3, 0x13, 0xe2, 0x93, 0x3c, 0x4d, 0xcb },
                // 13: a1c7d2ea661895ac7875bd63bfaafa25
                new byte[] { 0xa1, 0xc7, 0xd2, 0xea, 0x66, 0x18, 0x95, 0xac, 0x78, 0x75, 0xbd, 0x63, 0xbf, 0xaa, 0xfa, 0x25 },
                // 14: 82a43b2108797c6a440089a2ceddcee9
                new byte[] { 0x82, 0xa4, 0x3b, 0x21, 0x08, 0x79, 0x7c, 0x6a, 0x44, 0x00, 0x89, 0xa2, 0xce, 0xdd, 0xce, 0xe9 },
                // 15: 41d055b3dd6015167e8f598ce23f1929
                new byte[] { 0x41, 0xd0, 0x55, 0xb3, 0xdd, 0x60, 0x15, 0x16, 0x7e, 0x8f, 0x59, 0x8c, 0xe2, 0x3f, 0x19, 0x29 }
            };

            byte[] chunkKeyPattern = Properties.Resources.keyseq;
            int keyPos = chunkKeyPattern[i + 8];
            byte[] chunkKey = chunkKeys[keyPos];
            return chunkKey;
        }


    // Decrypt Iceborne PKG chunks
    public static byte[] DecryptChunk(byte[] data, byte[] chunkKey)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(chunkKey[i % chunkKey.Length] ^ data[i]);
            }
            return data;
        }
    }

    public static class MyExtensions
    {
        // Sort chunks >9 in correct order
        // From https://stackoverflow.com/a/11052176
        public static IEnumerable<string> CustomSort(this IEnumerable<string> list)
        {
            int maxLen = list.Select(s => s.Length).Max();

            return list.Select(s => new
            {
                OrgStr = s,
                SortStr = Regex.Replace(s, @"(\d+)|(\D+)", m => m.Value.PadLeft(maxLen, char.IsDigit(m.Value[0]) ? ' ' : '\xffff'))
            })
            .OrderBy(x => x.SortStr)
            .Select(x => x.OrgStr);
        }

    }
}
