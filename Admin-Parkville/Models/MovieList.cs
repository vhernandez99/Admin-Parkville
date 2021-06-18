﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Admin_Parkville.Models
{
    public class MovieList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Language { get; set; }
        public double Rating { get; set; }
        public string Genre { get; set; }
        public string ImageUrl { get; set; }
        public string FullImageUrl => AppSettings.ApiUrl + ImageUrl;
        public int Status { get; set; }
    }
}
