using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._01_Purchause.Entities;
using System.ComponentModel.Design;

namespace App.Domin.Core._02_Users.Entities;

public partial class Seller
{
    public int Id { get; set; }

    public int UserId{ get; set; }
    //public string? FullName { get; set; }

    public bool IsActive { get; set; }

    public bool IsBan { get; set; }

    public bool IsDelete { get; set; }

    public double? CommissionPercentage { get; set; }

    public int? CommissionsAmount { get; set; }

    public int? SalesAmount { get; set; }

    public DateTime? InsertionDate { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual Booth? Booth { get; set; }

    public virtual ICollection<Medal> Medals { get; set; } = new List<Medal>();
    public virtual User User{ get; set; }
}
