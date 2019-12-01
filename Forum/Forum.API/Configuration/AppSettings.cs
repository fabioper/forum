﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.API.Configuration
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public int ExpirationHours { get; set; }

        public string Issuer { get; set; }

        public string ValidAt { get; set; }
    }
}
