using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Index("AuctionId", Name = "IX_Bids_AuctionId")]
[Index("BuyerId", Name = "IX_Bids_CustomerId")]
public partial class Bid
{
    [Key]
    public int Id { get; set; }

    public int? Price { get; set; }

    public DateTime? BidDate { get; set; }

    public bool? HasWon { get; set; }

    public int? AuctionId { get; set; }

    public int? BuyerId { get; set; }

    [ForeignKey("AuctionId")]
    [InverseProperty("Bids")]
    public virtual Auction? Auction { get; set; }

    [ForeignKey("BuyerId")]
    [InverseProperty("Bids")]
    public virtual Buyer? Buyer { get; set; }
}
