﻿using System;
using System.Collections.Generic;
using TeduNetcore.Data.Enums;
using TeduNetcore.Data.Interfaces;
using TeduNetcore.Infrastructure.SharedKernel;

namespace TeduNetcore.Data.Entities
{
    public class ProductCategory : DomainEntity<int>,
        IHasSeoMetaData, ISwitchAble, ISortAble, IDateTracking
    {
        public ProductCategory()
        {
            Products = new List<Product>();
        }
        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public int? HomeOrder { get; set; }

        public string Image { get; set; }

        public bool? HomeFlag { get; set; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }
        public string SeoPageTitle { set; get; }
        public string SeoAlias { set; get; }
        public string SeoKeywords { set; get; }
        public string SeoDescription { set; get; }

        public virtual ICollection<Product> Products { set; get; }
    }
}