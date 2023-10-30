using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._01_Purchause.Entities;


namespace App.Domin.Core._01_Purchause.Entities;


public partial class StocksCart
{
    public int CartId { get; set; }

    public int StockId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? InsertionDate { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Stock Stock { get; set; } = null!;
}
