using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BidLand.Web.Scaffold.Repository.Models;

[Index("BuyerId", Name = "IX_Carts_CustomerId")]
public partial class Cart
{
    [Key]
    public int Id { get; set; }

    public int? Value { get; set; }

    public int? BuyerId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public bool PurchaseCompeleted { get; set; }

    public DateTime? InsertionDate { get; set; }

    [ForeignKey("BuyerId")]
    [InverseProperty("Carts")]
    public virtual Buyer? Buyer { get; set; }

    [InverseProperty("Cart")]
    public virtual ICollection<StocksCart> StocksCarts { get; set; } = new List<StocksCart>();
}
