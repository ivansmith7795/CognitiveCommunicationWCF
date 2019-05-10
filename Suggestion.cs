using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CognitiveCommunicationWCF
{
    public class Suggestion
    {
        public string changeNumber { get; set; }
        public string changeGroup { get; set; }
        public string application { get; set; }
        public DateTime release { get; set; }
        public DateTime archive { get; set; }
        public string title { get; set; }
        public string summaryText { get; set; }
        public string summaryHTML { get; set; }
        public string bodyHTML { get; set; }
    }
}