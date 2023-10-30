using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BidLand.Web.Scaffold.Repository.Models;

[Index("SellerId", Name = "IX_Files_SellerId")]
public partial class File
{
    [Key]
    public int Id { get; set; }

    public string? Url { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    public int? SellerId { get; set; }

    public DateTime? InsertTime { get; set; }

    public DateTime? RemoveTime { get; set; }

    public DateTime? UpdateTime { get; set; }

    [ForeignKey("SellerId")]
    [InverseProperty("Files")]
    public virtual Seller? Seller { get; set; }
}
