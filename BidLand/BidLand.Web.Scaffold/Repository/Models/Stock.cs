using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BidLand.Web.Scaffold.Repository.Models;

public partial class Stock
{
    [Key]
    public int Id { get; set; }

    [Column("ProductID")]
    public int? ProductId { get; set; }

    public int? BoothId { get; set; }

    public int Price { get; set; }

    public string? AdditionalDescription { get; set; }

    public int? AvailableNumber { get; set; }

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public bool IsAuction { get; set; }

    public int? AuctionId { get; set; }

    public DateTime? InsertionDate { get; set; }

    public virtual Auction? Auction { get; set; }

    [ForeignKey("BoothId")]
    [InverseProperty("Stocks")]
    public virtual Booth? Booth { get; set; }

    [InverseProperty("Stock")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [ForeignKey("ProductId")]
    [InverseProperty("Stocks")]
    public virtual Product? Product { get; set; }

    [InverseProperty("Stock")]
    public virtual ICollection<StocksCart> StocksCarts { get; set; } = new List<StocksCart>();
}
