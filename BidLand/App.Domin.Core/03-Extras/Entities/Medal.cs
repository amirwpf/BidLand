using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._01_Purchause.Entities;


namespace App.Domin.Core._03_Extras.Entities;

public partial class Medal
{
    public int Id { get; set; }

    public int? LevelType { get; set; }

    public int? SellerId { get; set; }

    public DateTime? InsertionDate { get; set; }
    public virtual Seller? Seller { get; set; }
}
