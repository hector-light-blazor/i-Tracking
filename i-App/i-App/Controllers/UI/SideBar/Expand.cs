namespace i_App.Controllers.UI.SideBar
{
    //enumerator
    public enum Operator
    {
        VEHICLE, EQUIPMENT, PROPERTIES
    }

    public enum ActiveOperator
    {
        DASH, VEHICLE, EQUIPMENT, PROPERTIES
    }

    public class Expand
    {
       // public string Email { get; set; } = "";
        public string VehiclesM { get; set; } = "";
        public string EquipmentM { get; set; } = "";
        public string PropertiesM{ get; set; } = "";

        //public void expandEmail(Operator op)
        public void ExpandMenu(Operator op)
        {
            
            switch (op)
            {
             // case Operator.EMAIL:
             //     Email = string.IsNullOrEmpty(Email) ? "expand" : "";
             //     break;
                case Operator.VEHICLE:
                    VehiclesM = string.IsNullOrEmpty(VehiclesM) ? "expand" : "";
                    break;
                case Operator.EQUIPMENT:
                    EquipmentM = string.IsNullOrEmpty(EquipmentM) ? "expand" : "";
                    break;
                case Operator.PROPERTIES:
                    PropertiesM = string.IsNullOrEmpty(PropertiesM) ? "expand" : "";
                    break;
            }
        }
    }


    public class Active
    {
        public string DashboardMenu { get; set; } = "";
        public string VehicleMenu { get; set; } = "";
        public string HeaveEquipMenu { get; set; } = "";

        public string PropertyListMenu { get; set; } = "";

        public string active { get; set; } = "active";

        public void SetActive(ActiveOperator op)
        {
            DashboardMenu = VehicleMenu = HeaveEquipMenu = PropertyListMenu = "";
            switch (op)
            {
                case ActiveOperator.DASH:
                    DashboardMenu = string.IsNullOrEmpty(DashboardMenu) ? active : "";
                    break;
                case ActiveOperator.VEHICLE:
                    VehicleMenu = string.IsNullOrEmpty(VehicleMenu) ? active : "";
                    break;
                case ActiveOperator.EQUIPMENT:
                    HeaveEquipMenu = string.IsNullOrEmpty(HeaveEquipMenu) ? active : "";
                    break;
                case ActiveOperator.PROPERTIES:
                    PropertyListMenu = string.IsNullOrEmpty(PropertyListMenu) ? active : "";
                    break;
            }
        }

    }
}
