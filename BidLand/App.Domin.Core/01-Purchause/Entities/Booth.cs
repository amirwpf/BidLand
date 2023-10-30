using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._01_Purchause.Entities;


namespace App.Domin.Core._01_Purchause.Entities;

public partial class Booth
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool IsDelete { get; set; }

    public int? SellerId { get; set; }

    public DateTime? InsertionDate { get; set; }

    public virtual Seller? Seller { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
