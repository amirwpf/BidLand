using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._01_Purchause.Entities;

namespace App.Domin.Core._03_Extras.Entities;


public partial class Address
{
    public int Id { get; set; }

    public string? Province { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public int? No { get; set; }

    public int? Phone { get; set; }

    public int? PostalCode { get; set; }

    public int? BuyerId { get; set; }

    public int? SellerId { get; set; }

    public virtual Buyer? Buyer { get; set; }

    public virtual Seller? Seller { get; set; }
}
