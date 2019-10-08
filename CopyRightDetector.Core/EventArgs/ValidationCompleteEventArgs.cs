namespace CopyRightDetector.Core
{
    public class ValidationCompleteEventArgs
    {
        public int MatchedCount { get; set; }

        public int? ExistsDocumentId { get; set; }
    }
}
