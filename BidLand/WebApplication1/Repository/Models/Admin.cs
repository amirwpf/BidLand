using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repository.Models;

public partial class Admin
{
    [Key]
    public int Id { get; set; }

    public int? SiteCommissionIncome { get; set; }

    public DateTime? InsertionDate { get; set; }
}
