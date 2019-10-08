using System;

namespace CopyRightDetector.Core.Models
{
    public class FileStore
    {
        public int DocumentId { get; set; }

        public Guid FileStoreId { get; set; }

        public string HashsSum { get; set; }

        public byte[] FileData { get; set; }

        public string FileName { get; set; }

        public string MimeType { get; set; }

        public string FileType { get; set; }

        public long? FileSize { get; set; }

        public DateTime CreationTime { get; set; }

    }
}
