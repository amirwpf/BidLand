using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BidLand.Web.Scaffold.Repository.Models;

[Table("Product")]
public partial class Product
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    public int? BasePrice { get; set; }

    public bool? IsConfirm { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int? BidId { get; set; }

    public DateTime? InsertionDate { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    [InverseProperty("Product")]
    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    [ForeignKey("ProductId")]
    [InverseProperty("Products")]
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
