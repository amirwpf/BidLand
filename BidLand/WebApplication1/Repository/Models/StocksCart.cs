using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[PrimaryKey("CartId", "StockId")]
public partial class StocksCart
{
    [Key]
    public int CartId { get; set; }

    [Key]
    public int StockId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? InsertionDate { get; set; }

    [ForeignKey("CartId")]
    [InverseProperty("StocksCarts")]
    public virtual Cart Cart { get; set; } = null!;

    [ForeignKey("StockId")]
    [InverseProperty("StocksCarts")]
    public virtual Stock Stock { get; set; } = null!;
}
