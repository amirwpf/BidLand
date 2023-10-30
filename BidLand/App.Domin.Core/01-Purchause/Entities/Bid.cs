using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._01_Purchause.Entities;


namespace App.Domin.Core._01_Purchause.Entities;

public partial class Bid
{

    public int Id { get; set; }

    public int? Price { get; set; }

    public DateTime? BidDate { get; set; }

    public bool? HasWon { get; set; }

    public int? AuctionId { get; set; }

    public int? BuyerId { get; set; }

    public virtual Auction? Auction { get; set; }

    public virtual Buyer? Buyer { get; set; }
}
