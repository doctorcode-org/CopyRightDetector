using System;

namespace CopyRightDetector.Core
{
    public class MatchDetectedEventArgs : EventArgs
    {
        public int TotalDocuments { get; set; }
        public int Progress { get; set; }
    }
}
