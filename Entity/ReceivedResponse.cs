using System;
using System.Collections.Generic;

namespace Entity
{
    public class ReceivedResponse
    {
        public class Status
        {
            public int code { get; set; }
            public string message { get; set; }
        }

        public class Datum
        {
            public string access_key { get; set; }
            public string xml { get; set; }
        }

        public class Page
        {
            public string next { get; set; }
            public string previous { get; set; }
        }

        public Status status { get; set; }
        public List<Datum> data { get; set; }
        public Page page { get; set; }
        public int count { get; set; }
        public string signature { get; set; }
    }
}
