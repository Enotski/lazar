﻿using lazarData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazarData.ResponseModels.Dx.Base
{
    public class DataGridSort
    {
        public string ColumnName { get; set; }
        public DataGridSortType Type { get; set; }
    }
}