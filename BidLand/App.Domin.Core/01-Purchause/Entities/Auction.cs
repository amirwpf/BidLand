using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._01_Purchause.Entities;

namespace App.Domin.Core._01_Purchause.Entities;

public partial class Auction
{

    public int Id { get; set; }


    public DateTime? StartDate { get; set; }


    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDelete { get; set; }

    public int? CurrentHighestPrice { get; set; }

    public int MinimumFinalPrice { get; set; }

    public int StockId { get; set; }

    public DateTime? InsertionDate { get; set; }

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    //public virtual Stock Stock { get; set; } 
}
