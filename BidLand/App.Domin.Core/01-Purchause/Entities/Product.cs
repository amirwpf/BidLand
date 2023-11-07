using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domin.Core._03_Extras.Entities;
using App.Domin.Core._02_Users.Entities;
using App.Domin.Core._01_Purchause.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Domin.Core._01_Purchause.Entities;


public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? BasePrice { get; set; }

    public bool? IsConfirm { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public DateTime? InsertionDate { get; set; }

    public int? CategoryId { get; set; }

    public virtual ICollection<Image> Images { get; set; } 

    public virtual ICollection<Stock> Stocks { get; set; }

    public virtual Category Category { get; set; } 


}
