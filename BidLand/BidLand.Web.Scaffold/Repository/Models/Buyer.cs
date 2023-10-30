using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BidLand.Web.Scaffold.Repository.Models;

[Table("Buyer")]
public partial class Buyer
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? FullName { get; set; }

    public int? Credit { get; set; }

    public int? TotalPurchaseAmount { get; set; }

    public bool? IsBan { get; set; }

    public DateTime? InsertionDate { get; set; }

    [InverseProperty("Buyer")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [InverseProperty("Buyer")]
    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    [InverseProperty("Buyer")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [InverseProperty("Buyer")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
