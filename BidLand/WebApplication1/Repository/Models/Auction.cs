using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Index("StockId", Name = "IX_Auctions_ProductId", IsUnique = true)]
public partial class Auction
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDelete { get; set; }

    public int? CurrentHighestPrice { get; set; }

    public int MinimumFinalPrice { get; set; }

    public int StockId { get; set; }

    public DateTime? InsertionDate { get; set; }

    [InverseProperty("Auction")]
    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
