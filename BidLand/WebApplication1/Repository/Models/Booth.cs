using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

public partial class Booth
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool IsDelete { get; set; }

    public int? SellerId { get; set; }

    public DateTime? InsertionDate { get; set; }

    [ForeignKey("SellerId")]
    [InverseProperty("Booth")]
    public virtual Seller? Seller { get; set; }

    [InverseProperty("Booth")]
    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
