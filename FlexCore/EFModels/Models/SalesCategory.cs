﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFModels.Models
{
    public partial class SalesCategory
    {
        public SalesCategory()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }

        [Key]
        public int SalesCategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string SalesCategoryName { get; set; }

        [InverseProperty("fk_SalesCategory")]
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}