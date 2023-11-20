using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._01_Purchause.Entities;
using App.Domin.Core._03_Extras.Enums;

namespace App.Domin.Core._03_Extras.Entities;

public partial class Medal
{
    public int Id { get; set; }
    public MedalEnum? LevelType { get; set; }
    public int? Percentage { get; set; }
    public DateTime? InsertionDate { get; set; }
    public virtual ICollection<Seller>? Seller { get; set; }
}
