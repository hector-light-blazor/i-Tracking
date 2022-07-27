namespace i_App.View.Model
{


    public class ViewVehicleMakeModel
    {
        public int VehicleId { get; set; } = 0;


        public decimal BookValue { get; set; } = 0;

        public int MakeId { get; set; } = 0;
        public string MakeName { get; set; } = "";

        public int ModelId { get; set; } = 0;

        public int ReqNum { get; set; } = 0;

        public string ModelName { get; set; } = "";
        public string PoNumber { get; set; } = "";

        public string status_name { get; set; } = "";

        public string toggleOptions { get; set; } = "";

        public bool VehicleSelected { get; set; } = false;

        public bool InsuranceSelected { get; set; } = false;



        public string IsActive
        {
            get { return (VehicleSelected || InsuranceSelected) ? "active" : "";  }
        }

        public string StatusClass()
        {
            string results = "";

            switch (status_name)
            {
                case "Active":
                    results = " border-success text-success";
                    break;
                case "Pending":
                    results = " border-warning text-warning";
                    break;
                case "Deleted":
                    results = "border-danger text-danger";
                    break;
            }

            return results;
        }

        public string FormattedAmount
        {
            get
            {
                return (BookValue == 0) ? "0" : string.Format("{0:C}", BookValue);
            }
        }

        public void OnToggle()
        {
            toggleOptions = (!string.IsNullOrEmpty(toggleOptions)) ? "" : "show";
        }

        public bool IsActionable()
        {
            return (status_name.Equals(AssStatus.Active.ToString())) ? true : false;
        }
    }
}
