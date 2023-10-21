﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AcquireXModel
{
    public class AuthToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public int ExpireIn { get; set; }
        [JsonProperty("refresh_expires_in")]
        public int RefreshExpireIn { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("not-before-policy")]

        public int NotBeforePolicy { get; set; }
        [JsonProperty("scope")]

        public string Scope { get; set; }
    }
}
