namespace CopyRightDetector.Core.Models
{
    public class MatcheDocument
    {
        /// <summary>
        /// شناسه سند
        /// </summary>
        public int DocumentId { get; set; }

        /// <summary>
        /// اندیس پاراگراف در سند موجود
        /// </summary>
        public int DestinationIndex { get; set; }

        /// <summary>
        /// اندیس پاراگراف در سند اصلی
        /// </summary>
        public int SourceIndex { get; set; }
    }
}
