using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MHW_Randomizer
{
    // Largely based on https://github.com/zhangtaoxinzi/MHWNoChunk/
    public class ChunkOTF
    {
        Dictionary<long, long> MetaChunk;
        Dictionary<int, long> ChunkOffsetDict;
        BinaryReader Reader;
        byte[] ChunkDecompressed, NextChunkDecompressed;
        int cur_pointer = 0;
        int cur_index = 0;
        int DictCount = 0;
        string ChunkFile;
        Dictionary<int, byte[]> ChunkCache;
        public static Dictionary<string, Files> files = new Dictionary<string, Files>();

        public void AnalyzeChunks(String fileInput)
        {
            //string[] chunkFiles = Directory.GetFiles(folderInput, "chunk*.bin", SearchOption.TopDirectoryOnly).CustomSort().ToArray();
            List<FileNode> filelist = new List<FileNode>();

            //foreach (string FileInput in chunkFiles)
            //{
            ChunkFile = fileInput;
            FileInfo fileinputInfo = new FileInfo(ChunkFile);
            //Console.WriteLine($"Now analyzing {fileinputInfo.Name}");
            ChunkCache = new Dictionary<int, byte[]>();

            MetaChunk = new Dictionary<long, long>();
            ChunkOffsetDict = new Dictionary<int, long>();
            string NamePKG = $"{Environment.CurrentDirectory}\\{Path.GetFileNameWithoutExtension(fileInput)}.pkg";
            Reader = new BinaryReader(File.Open(fileInput, FileMode.Open));

            // Read header
            Reader.BaseStream.Seek(4, SeekOrigin.Begin);
            int ChunkCount = Reader.ReadInt32(); int ChunkPadding = ChunkCount.ToString().Length;

            // Read file list
            DictCount = 0;
            long totalChunkSize = 0;
            for (int i = 0; i < ChunkCount; i++)
            {
                // Process file size
                byte[] ArrayTmp1 = new byte[8];
                byte[] ArrayChunkSize = Reader.ReadBytes(3);
                int High = ArrayChunkSize[0] >> 4;
                ArrayChunkSize[0] = BitConverter.GetBytes(High)[0];
                Array.Copy(ArrayChunkSize, ArrayTmp1, ArrayChunkSize.Length);
                long ChunkSize = BitConverter.ToInt64(ArrayTmp1, 0);
                ChunkSize = (ChunkSize >> 4) + (ChunkSize & 0xF);
                totalChunkSize += ChunkSize;

                // Process offset
                byte[] ArrayTmp2 = new byte[8];
                byte[] ArrayChunkOffset = Reader.ReadBytes(5);
                Array.Copy(ArrayChunkOffset, ArrayTmp2, ArrayChunkOffset.Length);
                long ChunkOffset = BitConverter.ToInt64(ArrayTmp2, 0);

                MetaChunk.Add(ChunkOffset, ChunkSize);
                ChunkOffsetDict.Add(i, ChunkOffset);
                DictCount = i + 1;
            }

            cur_index = 0;
            long cur_offset = ChunkOffsetDict[cur_index];
            long cur_size = MetaChunk[cur_offset];

            ChunkDecompressed = getDecompressedChunk(cur_offset, cur_size, Reader, false, cur_index);
            if (cur_index + 1 < DictCount)
            {
                NextChunkDecompressed = getDecompressedChunk(ChunkOffsetDict[cur_index + 1], MetaChunk[ChunkOffsetDict[cur_index + 1]], Reader, false, cur_index + 1);
            }
            else
            {
                NextChunkDecompressed = new byte[0];
            }
            cur_pointer = 0x0C;
            int TotalParentCount = BitConverter.ToInt32(ChunkDecompressed, cur_pointer);
            cur_pointer += 4;
            int TotalChildrenCount = BitConverter.ToInt32(ChunkDecompressed, cur_pointer);
            cur_pointer = 0x100;
            FileNode root_node = null;
            for (int i = 0; i < TotalParentCount; i++)
            {
                string StringNameParent = getName(0x3C, false);
                long FileSize = getInt64(false);
                long FileOffset = getInt64(false);
                int EntryType = getInt32(false);
                int CountChildren = getInt32(false);

                if (filelist.Count == 0)
                {
                    root_node = new FileNode(StringNameParent, false, fileInput);
                    root_node.EntireName = root_node.Name;
                    filelist.Add(root_node);
                }
                else
                {
                    root_node = filelist[0];
                    root_node.FromChunk = ChunkFile;
                    root_node.FromChunkName = $"({Path.GetFileNameWithoutExtension(ChunkFile)})";
                }
                for (int j = 0; j < CountChildren; j++)
                {
                    int origin_pointer = cur_pointer;
                    int origin_loc = cur_index;
                    if (!ChunkCache.ContainsKey(cur_index)) ChunkCache.Add(cur_index, ChunkDecompressed);
                    if (!ChunkCache.ContainsKey(cur_index + 1)) ChunkCache.Add(cur_index + 1, NextChunkDecompressed);

                    string StringNameChild = getName(0xA0, false);
                    FileSize = getInt64(false);
                    FileOffset = getInt64(false);
                    EntryType = getInt32(false);
                    int Unknown = getInt32(false);

                    bool isFile = false;
                    if (EntryType == 0x02 || EntryType == 0x00) isFile = true;
                    else continue;


                    if (EntryType == 0x02)
                    {
                        cur_pointer = origin_pointer;
                        if (cur_index != origin_loc)
                        {
                            cur_index = origin_loc;
                            ChunkDecompressed = ChunkCache[cur_index];
                            NextChunkDecompressed = ChunkCache[cur_index + 1];
                            ChunkCache.Remove(cur_index);
                            ChunkCache.Remove(cur_index + 1);
                        }
                        StringNameChild = getName(0x50, false);
                        getOnLength(0x68, new byte[0x68], 0, false);
                    }
                    string[] fathernodes = StringNameChild.Split('\\');
                    FileNode child_node = new FileNode(fathernodes[fathernodes.Length - 1], isFile, fileInput);
                    if (fathernodes[fathernodes.Length - 2] == "trace" || (fathernodes[fathernodes.Length - 2] == "text" && fathernodes[fathernodes.Length - 1] == "q51613_eng.gmd"))
                        continue;

                    if (fathernodes[fathernodes.Length - 1].Contains(".fsm") || fathernodes[1] == "em")
                    {
                        files[StringNameChild] = new Files
                        {
                            Size = FileSize,
                            Offset = FileOffset,
                            ChunkIndex = (int)(FileOffset / 0x40000),
                            ChunkPointer = (int)(FileOffset % 0x40000),
                            EntireName = StringNameChild,
                            Name = fathernodes[fathernodes.Length - 1],
                            FromChunk = child_node.FromChunk,
                            FromChunkName = child_node.FromChunkName,
                        };
                    files[StringNameChild].ChunkState = this;
                    }
                    else
                    {
                        files[fathernodes[fathernodes.Length - 1]] = new Files
                        {
                            Size = FileSize,
                            Offset = FileOffset,
                            ChunkIndex = (int)(FileOffset / 0x40000),
                            ChunkPointer = (int)(FileOffset % 0x40000),
                            EntireName = StringNameChild,
                            Name = fathernodes[fathernodes.Length - 1],
                            FromChunk = child_node.FromChunk,
                            FromChunkName = child_node.FromChunkName,
                        };
                    files[fathernodes[fathernodes.Length - 1]].ChunkState = this;
                    }

                }
            }
            ChunkCache.Clear();
            Reader.Dispose();
            //}
        }

        public byte[] ExtractItem(Files file)
        {
            int failed = 0;
            try
            {
                Reader = new BinaryReader(File.Open(ChunkFile, FileMode.Open));

                // Read header
                Reader.BaseStream.Seek(4, SeekOrigin.Begin);
                ChunkOTF CurNodeChunk = this;
                CurNodeChunk.cur_index = file.ChunkIndex;
                CurNodeChunk.cur_pointer = file.ChunkPointer;
                long size = file.Size;
                if (CurNodeChunk.ChunkCache.ContainsKey(CurNodeChunk.cur_index))
                {
                    CurNodeChunk.ChunkDecompressed = CurNodeChunk.ChunkCache[CurNodeChunk.cur_index];
                }
                else
                {
                    if (CurNodeChunk.ChunkCache.Count > 20) CurNodeChunk.ChunkCache.Clear();
                    CurNodeChunk.ChunkDecompressed = CurNodeChunk.getDecompressedChunk(CurNodeChunk.ChunkOffsetDict[CurNodeChunk.cur_index], CurNodeChunk.MetaChunk[CurNodeChunk.ChunkOffsetDict[CurNodeChunk.cur_index]], CurNodeChunk.Reader, false, CurNodeChunk.cur_index);
                    CurNodeChunk.ChunkCache.Add(CurNodeChunk.cur_index, CurNodeChunk.ChunkDecompressed);
                }
                if (CurNodeChunk.ChunkCache.ContainsKey(CurNodeChunk.cur_index + 1))
                {
                    CurNodeChunk.NextChunkDecompressed = CurNodeChunk.ChunkCache[CurNodeChunk.cur_index + 1];
                }
                else
                {
                    if (CurNodeChunk.ChunkCache.Count > 20) CurNodeChunk.ChunkCache.Clear();
                    if (CurNodeChunk.cur_index + 1 < CurNodeChunk.DictCount) { CurNodeChunk.NextChunkDecompressed = CurNodeChunk.getDecompressedChunk(CurNodeChunk.ChunkOffsetDict[CurNodeChunk.cur_index + 1], CurNodeChunk.MetaChunk[CurNodeChunk.ChunkOffsetDict[CurNodeChunk.cur_index + 1]], CurNodeChunk.Reader, false, CurNodeChunk.cur_index + 1); }
                    else { CurNodeChunk.NextChunkDecompressed = new byte[0]; }
                    CurNodeChunk.ChunkCache.Add(CurNodeChunk.cur_index + 1, CurNodeChunk.NextChunkDecompressed);
                }
                //Console.Write($"Extracting {node.EntireName} ...                          \r");
                byte[] fileBites = CurNodeChunk.getOnLength(size, new byte[size], 0, false);
                Reader.Dispose();
                return fileBites;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while extracting {file.EntireName}{file.FromChunkName}, skipped. Please try again later.");
                Console.WriteLine(ex.StackTrace);
                failed += 1;
            }
            return null;
        }

        //To get decompressed chunk
        private byte[] getDecompressedChunk(long offset, long size, BinaryReader reader, bool Flag, int chunkNum)
        {
            if (size != 0)
            {
                reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                byte[] ChunkCompressed = reader.ReadBytes((int)size); // Unsafe cast
                byte[] ChunkDecompressed = Utils.Decompress(ChunkCompressed, ChunkCompressed.Length, 0x40000);
                if (!Flag) { Utils.DecryptChunk(ChunkDecompressed, Utils.GetChunkKey(chunkNum)); }
                return ChunkDecompressed;
            }
            else
            {
                reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                byte[] ChunkDecompressed = Reader.ReadBytes(0x40000);
                if (!false) { Utils.DecryptChunk(ChunkDecompressed, Utils.GetChunkKey(chunkNum)); }
                return ChunkDecompressed;
            }
        }

        //To read an ASCII string from chunk bytes
        private string getName(int targetlength, bool Flag)
        {
            return Convert.ToString(System.Text.Encoding.ASCII.GetString(getOnLength(targetlength, new byte[targetlength], 0, false).Where(b => b != 0x00).ToArray()));
        }

        //To read int64 from chunk bytes
        private long getInt64(bool Flag)
        {
            return BitConverter.ToInt64(getOnLength(8, new byte[8], 0, false), 0);
        }

        //To read int64 from chunk bytes
        private int getInt32(bool Flag)
        {
            return BitConverter.ToInt32(getOnLength(4, new byte[4], 0, false), 0);
        }

        //To read a byte array at length of targetlength
        private byte[] getOnLength(long targetlength, byte[] tmp, long startAddr, bool Flag)
        {
            if (cur_pointer + targetlength < 0x40000)
            {
                Array.Copy(ChunkDecompressed, cur_pointer, tmp, startAddr, targetlength);
                cur_pointer += (int)targetlength;
            }
            else
            {
                int tmp_can_read_length = 0x40000 - cur_pointer;
                long tmp_remain_length = targetlength - tmp_can_read_length;
                Array.Copy(ChunkDecompressed, cur_pointer, tmp, startAddr, tmp_can_read_length);
                cur_pointer = 0;
                ChunkDecompressed = NextChunkDecompressed;
                cur_index += 1;
                if (cur_index + 1 < DictCount) { NextChunkDecompressed = getDecompressedChunk(ChunkOffsetDict[cur_index + 1], MetaChunk[ChunkOffsetDict[cur_index + 1]], Reader, false, cur_index + 1); }
                else
                {
                    NextChunkDecompressed = new byte[0];
                }
                getOnLength(tmp_remain_length, tmp, startAddr + tmp_can_read_length, false);
            }
            return tmp;
        }
    }

    public class Files
    {
        public string Name { get; set; }
        public string EntireName { get; set; }
        public long Offset { get; set; }
        public long Size { get; set; }
        public int ChunkIndex { get; set; }
        public int ChunkPointer { get; set; }
        public string NameWithSize { get; set; }
        public ChunkOTF ChunkState { get; set; }
        public string FromChunk { get; set; }
        public string FromChunkName { get; set; }

        public byte[] Extract()
        {
            return ChunkState.ExtractItem(this);
        }
    }

    public class FileNode : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public List<FileNode> Childern { get; set; }
        public string Icon { get; set; }
        public string EntireName { get; set; }
        public long Offset { get; set; }
        public long Size { get; set; }
        public int ChunkIndex { get; set; }
        public bool IsFile { get; set; }
        public int ChunkPointer { get; set; }
        public string NameWithSize { get; set; }
        public string FromChunk { get; set; }
        public string FromChunkName { get; set; }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                setChilrenSelected(value);
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }

        public int getSelectedCount()
        {
            int count = 0;
            foreach (FileNode node in Childern)
            {
                count += node.getSelectedCount();
            }
            if (IsFile && IsSelected)
            {
                count++;
            }
            return count;
        }

        public void setChilrenSelected(bool selected)
        {
            foreach (FileNode child in Childern)
            {
                child.IsSelected = selected;
            }
        }

        public long getSize()
        {
            if (IsFile) { setNameWithSize(Name, Size); return Size; }
            else
            {
                long _size = 0;
                foreach (FileNode child in Childern)
                {
                    _size += child.getSize();
                }
                Size = _size;
                setNameWithSize(Name, Size);
                return _size;
            }
        }

        private void setNameWithSize(string name, long _size)
        {
            NameWithSize = $"{Name} ({getSizeStr(_size)})";
        }

        public string getSizeStr(long _size)
        {
            string sizestr = "";
            if (_size < 1024)
            {
                sizestr = $"{_size} B";
            }
            else if (_size >= 1024 && _size < 1048576)
            {
                sizestr = $"{_size / 1024f:F2} KB";
            }
            else if (_size < 1073741824 && _size >= 1048576)
            {
                sizestr = $"{(_size >> 10) / 1024f:F2} MB";
            }
            else
            {
                sizestr = $"{(_size >> 20) / 1024f:F2} GB";
            }
            return sizestr;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public FileNode(string name, bool isFile, string fromChunk)
        {
            Name = name;
            NameWithSize = "";
            IsFile = isFile;
            if (isFile) Icon = AppDomain.CurrentDomain.BaseDirectory + "\\file.png";
            else Icon = AppDomain.CurrentDomain.BaseDirectory + "\\dir.png";
            Childern = new List<FileNode>();
            IsSelected = true;
            FromChunk = fromChunk;
            FromChunkName = $"({System.IO.Path.GetFileNameWithoutExtension(fromChunk)})";
        }
    }
}
