using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._01_Purchause.Entities;

namespace App.Domin.Core._03_Extras.Entities;

public partial class Image
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public string? Url { get; set; }

    public DateTime? InsertionDate { get; set; }
    public virtual Product? Product { get; set; }
}
