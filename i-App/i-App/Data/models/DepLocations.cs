// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace i_App.Data.models
{
    public partial class DepLocations
    {
        public DepLocations()
        {
            Properties = new HashSet<Properties>();
            Vehicles = new HashSet<Vehicles>();
        }

        public int LocationId { get; set; }
        public int? DepId { get; set; }
        public string LocationNum { get; set; }
        public string LocationName { get; set; }

        public virtual Departments Dep { get; set; }
        public virtual ICollection<Properties> Properties { get; set; }
        public virtual ICollection<Vehicles> Vehicles { get; set; }
    }
}