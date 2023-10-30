using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Index("ProductId", Name = "IX_Images_ProductId")]
public partial class Image
{
    [Key]
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public string? Url { get; set; }

    public DateTime? InsertionDate { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Images")]
    public virtual Product? Product { get; set; }
}
