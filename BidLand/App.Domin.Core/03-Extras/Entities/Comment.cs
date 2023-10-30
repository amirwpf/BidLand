using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._01_Purchause.Entities;


namespace App.Domin.Core._03_Extras.Entities;

public partial class Comment
{

    public int Id { get; set; }

    public string? Title { get; set; }

    public bool? IsPositive { get; set; }

    public string? Advantages { get; set; }

    public string? DisAdvantages { get; set; }

    public bool IsConfirm { get; set; }

    public string? Description { get; set; }

    public DateTime? ConfirmDate { get; set; }

    public int? StockId { get; set; }

    public int? BuyerId { get; set; }

    public virtual Buyer? Buyer { get; set; }

    public virtual Stock? Stock { get; set; }
}
