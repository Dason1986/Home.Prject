using System;

namespace Library.Storage
{
    public class FileIndexWriter : IIndexWriter
    {
        private System.IO.BinaryWriter writer;

        public FileIndexWriter(System.IO.Stream stream)
        {
            if (!stream.CanWrite) throw new System.Exception();
            if (!stream.CanSeek) throw new System.Exception();
            writer = new System.IO.BinaryWriter(stream);
        }

        public void Writer(StorageInfo index)
        {
            if (index == null) throw new Exception();
            if (string.IsNullOrEmpty(index.Name)) throw new Exception();
            writer.Write(index.ID.ToByteArray());
            writer.Write(index.Created.ToOADate());
            var name = index.Name ?? string.Empty;
            var namebytes = System.Text.Encoding.UTF8.GetBytes(name);
            var span = StorageInfo.NameSizeBuffer - namebytes.Length;

            if (span > 0)
            {
                writer.Write(namebytes.Length);
                writer.Write(namebytes);
            }
            else
            {
                writer.Write(StorageInfo.NameSizeBuffer);
                writer.Write(namebytes, 0, StorageInfo.NameSizeBuffer);
            }
        }
    }
}