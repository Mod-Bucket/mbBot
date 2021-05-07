using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot_Bucket {
    public class BotConfig {
        [JsonProperty("DiscordToken")]
        public string Token { get; private set; }
    }
}
