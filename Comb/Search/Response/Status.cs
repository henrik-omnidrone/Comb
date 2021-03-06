﻿using Newtonsoft.Json;

namespace Comb
{
    public class Status
    {
        /// <summary>
        /// The encrypted Resource ID provided by CloudSearch.
        /// </summary>
        [JsonProperty("rid")]
        public string ResourceId { get; set; }

        /// <summary>
        /// How long it took to process the search request in milliseconds, as reported by
        /// CloudSearch.
        /// </summary>
        [JsonProperty("time-ms")]
        public int TimeMs { get; set; }
    }
}
