namespace Recepati.Code.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class SecToken
    {
        [JsonProperty("exp")]
        public long Exp { get; set; }

        [JsonProperty("idstring")]
        public string Idstring { get; set; }

        [JsonProperty("usermail")]
        public string Usermail { get; set; }
    }
}
