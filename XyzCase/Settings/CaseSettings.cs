using System.Collections.Generic;

namespace XyzCase.Settings
{
    public class CaseSettings
    {
        public IEnumerable<string> Symbols { get; set; }
        public string Interval { get; set; }
        public int WorkingPeriod { get; set; }
    }
}
