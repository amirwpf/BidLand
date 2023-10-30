using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Index("No", Name = "IX_Addresses_CustomerId")]
public partial class Address
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Province { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    [StringLength(50)]
    public string? Street { get; set; }

    public int? No { get; set; }

    public int? Phone { get; set; }

    public int? PostalCode { get; set; }

    public int? BuyerId { get; set; }

    public int? SellerId { get; set; }

    [ForeignKey("BuyerId")]
    [InverseProperty("Addresses")]
    public virtual Buyer? Buyer { get; set; }

    [ForeignKey("SellerId")]
    [InverseProperty("Addresses")]
    public virtual Seller? Seller { get; set; }
}
