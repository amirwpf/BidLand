using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

[Index("BuyerId", Name = "IX_Comments_CustomerId")]
[Index("StockId", Name = "IX_Comments_ProductId")]
public partial class Comment
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Title { get; set; }

    public bool? IsPositive { get; set; }

    [StringLength(100)]
    public string? Advantages { get; set; }

    [StringLength(100)]
    public string? DisAdvantages { get; set; }

    public bool IsConfirm { get; set; }

    public string? Description { get; set; }

    public DateTime? ConfirmDate { get; set; }

    public int? StockId { get; set; }

    public int? BuyerId { get; set; }

    [ForeignKey("BuyerId")]
    [InverseProperty("Comments")]
    public virtual Buyer? Buyer { get; set; }

    [ForeignKey("StockId")]
    [InverseProperty("Comments")]
    public virtual Stock? Stock { get; set; }
}
