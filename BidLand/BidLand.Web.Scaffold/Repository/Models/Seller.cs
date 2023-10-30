using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BidLand.Web.Scaffold.Repository.Models;

public partial class Seller
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? FullName { get; set; }

    public bool IsActive { get; set; }

    public bool IsBan { get; set; }

    public bool IsDelete { get; set; }

    public double? CommissionPercentage { get; set; }

    public int? CommissionsAmount { get; set; }

    public int? SalesAmount { get; set; }

    public DateTime? InsertionDate { get; set; }

    [InverseProperty("Seller")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [InverseProperty("Seller")]
    public virtual Booth? Booth { get; set; }

    [InverseProperty("Seller")]
    public virtual ICollection<File> Files { get; set; } = new List<File>();

    [InverseProperty("Seller")]
    public virtual Image? Image { get; set; }

    [InverseProperty("Seller")]
    public virtual ICollection<Medal> Medals { get; set; } = new List<Medal>();
}
