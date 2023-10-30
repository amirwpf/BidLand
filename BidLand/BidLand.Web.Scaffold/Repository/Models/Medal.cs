using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BidLand.Web.Scaffold.Repository.Models;

[Index("SellerId", Name = "IX_Medals_SellerId")]
public partial class Medal
{
    [Key]
    public int Id { get; set; }

    public int? LevelType { get; set; }

    public int? SellerId { get; set; }

    public DateTime? InsertionDate { get; set; }

    [ForeignKey("SellerId")]
    [InverseProperty("Medals")]
    public virtual Seller? Seller { get; set; }
}
