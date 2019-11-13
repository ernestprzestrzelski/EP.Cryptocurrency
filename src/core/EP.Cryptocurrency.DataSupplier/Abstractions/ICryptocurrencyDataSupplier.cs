﻿using EP.Cryptocurrency.DataSupplier.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EP.Cryptocurrency.DataSupplier.Abstractions
{
    public interface ICryptocurrencyDataSupplier
    {
        Task Supply();
    }
}