﻿using Otium.Domain.Models;

namespace Otium.Models;

public class IndexViewModel
{
    public int CategoriesCount { get; set; }
    public List<Categories>? Categories { get; set; }
    public int RowsCount { get; set; }
    public int RemainColumnsCount { get; set; }
}