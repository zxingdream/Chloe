﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Chloe.Sharding.Models
{
    internal class AggregateModel
    {
        public decimal? Sum { get; set; }
        public long Count { get; set; }
    }
}
