using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MHW_Randomizer
{
    public static class StructTools
    {
        /// <summary>
        /// converts byte[] to struct
        /// </summary>
        public static List<T> RawDeserialize<T>(byte[] rawData, int position)
        {
            List<T> retobj = new List<T>();
            int rawsize = Marshal.SizeOf(typeof(T));

            for (position = position; rawsize <= rawData.Length - position; position = position + rawsize)
            {
                if (rawsize > rawData.Length - position)
                    throw new ArgumentException("Not enough data to fill struct. Array length from position: " + (rawData.Length - position) + ", Struct length: " + rawsize);
                IntPtr buffer = Marshal.AllocHGlobal(rawsize);
                Marshal.Copy(rawData, position, buffer, rawsize);
                retobj.Add((T)Marshal.PtrToStructure(buffer, typeof(T)));
                Marshal.FreeHGlobal(buffer);
            }
            return retobj;
        }

        /// <summary>
        /// converts a struct to byte[]
        /// </summary>
        public static byte[] RawSerialize<T>(List<T> anything)
        {
            List<byte> allData = new List<byte>();
            foreach (object structs in anything)
            {
                int rawSize = Marshal.SizeOf(structs);
                IntPtr buffer = Marshal.AllocHGlobal(rawSize);
                Marshal.StructureToPtr(structs, buffer, false);
                byte[] rawDatas = new byte[rawSize];
                Marshal.Copy(buffer, rawDatas, 0, rawSize);
                Marshal.FreeHGlobal(buffer);
                allData.AddRange(rawDatas);
            }
            return allData.ToArray();
        }
    }
}
