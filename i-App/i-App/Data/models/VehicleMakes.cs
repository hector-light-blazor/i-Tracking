// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace i_App.Data.models
{
    public partial class VehicleMakes
    {
        public VehicleMakes()
        {
            VehicleModels = new HashSet<VehicleModels>();
            Vehicles = new HashSet<Vehicles>();
        }

        public int MakeId { get; set; }
        public string MakeName { get; set; }

        public virtual ICollection<VehicleModels> VehicleModels { get; set; }
        public virtual ICollection<Vehicles> Vehicles { get; set; }
    }
}