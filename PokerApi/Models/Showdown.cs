﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokerApi.Models
{
    public class Showdown
    {
        public int Id { get; set; }
        public string Hand1 { get; set; }
        public string Hand2 { get; set; }
        public string Hand1Type { get; set; }
        public string Hand2Type { get; set; }
        public string Result { get; set; }
        public string Deck { get; set; }
    }
}
