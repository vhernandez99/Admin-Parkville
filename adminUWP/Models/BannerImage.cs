﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adminUWP.Models
{
    public class BannerImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string FullImageUrl => AppSettings.ApiUrl + ImageUrl;
        public byte[] ImageArray { get; set; }
    }
}
