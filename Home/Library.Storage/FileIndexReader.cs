using System;
using System.Collections.Generic;

namespace Library.Storage
{

    public class FileIndexReader : IIndexReader
    {
        private System.IO.BinaryReader reader;

        public FileIndexReader(System.IO.Stream stream)
        {
            if (!stream.CanRead) throw new System.Exception();
            if (!stream.CanSeek) throw new System.Exception();
            if (stream.Position != 0) stream.Seek(0, System.IO.SeekOrigin.Begin);
            reader = new System.IO.BinaryReader(stream);
        }

        public bool IsEnd
        {
            get { return reader.BaseStream.Position == reader.BaseStream.Length; }
        }

        public StorageInfo Read()
        {
            if (IsEnd) return null;
            var guidbytes = reader.ReadBytes(16);
            if (guidbytes.Length == 0) return null;
            var oaDate = reader.ReadDouble();
            var length = reader.ReadInt32();
            var namebytes = reader.ReadBytes(length);

            return new FileStorageInfo() { ID = new Guid(guidbytes), Created = DateTime.FromOADate(oaDate), Name = System.Text.Encoding.UTF8.GetString(namebytes).Replace("\0", string.Empty).Trim() };
        }

        public IEnumerable<Guid> ReadAllID()
        {
            while (!IsEnd)
            {
                var guidbytes = reader.ReadBytes(16);
                if (guidbytes.Length == 0) break;

                var id = new Guid(guidbytes);
                //  reader.ReadDouble();
                reader.BaseStream.Seek(8, System.IO.SeekOrigin.Current);
                var length = reader.ReadInt32();
                reader.BaseStream.Seek(length, System.IO.SeekOrigin.Current);
                yield return id;
            }
        }

        public IEnumerable<StorageInfo> ReadToEnd()
        {
            StorageInfo index = Read();

            while (index != null)
            {
                yield return index;

                index = Read();
            }
        }
    }
}