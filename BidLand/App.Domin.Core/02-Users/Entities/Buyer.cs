using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._01_Purchause.Entities;


namespace App.Domin.Core._02_Users.Entities;


public partial class Buyer
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public int? Credit { get; set; }

    public int? TotalPurchaseAmount { get; set; }

    public bool? IsBan { get; set; }

    public DateTime? InsertionDate { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
