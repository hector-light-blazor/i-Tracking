﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace i_App.Data.models
{
    public partial class AssetTypes
    {
        public AssetTypes()
        {
            Vehicles = new HashSet<Vehicles>();
        }

        public int AssetTypeId { get; set; }
        public int? AssetId { get; set; }
        public string AssetTypeName { get; set; }

        public virtual Assets Asset { get; set; }
        public virtual ICollection<Vehicles> Vehicles { get; set; }
    }
}