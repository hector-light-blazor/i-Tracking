namespace i_App.Data.State
{

    public enum Asset { 
        Vehicles = 1,
        HeavyEquipment=2,
        Properties=3
    }

    public enum InsuranceType
    {
        LiabilityOnly = 1,
        PhysicalDamage = 2,
        LiabilityOnlyPhysicalDamage = 3
    }

    public enum AssetAction
    {
        AddAsset =1,
        TransferAsset = 2,
        DeleteAsset = 3,
    }

    public enum InsuranceAction
    {
        AddInsurance = 1,
        ChangeInsurance = 2,
        DeleteInsurance = 3,
    }


    public enum AssStatus
    {
        Active = 1,
        Pending = 2,
        Deleted = 3,
    }

    public enum RolesStats { 
        Admin = 1,
        Executive = 5,
        Purchasing = 6,
        Application = 7
    }

    public class MessageResponse
    {
        public bool Response { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }

    public class AppState
    {

        private List<ViewVehicleMakeModel> _tableView { get; set; } = new List<ViewVehicleMakeModel>();

        private ViewAccount _viewAccount { get; set; } = new ViewAccount();
        private List<Roles> _roles { get; set; } = new List<Roles> ();

        private List<InsuranceTypes> _InsTypes { get; set; } = new List<InsuranceTypes>();


        public event Action? OnInsTypesChange;
        public event Action? OnRolesChange;
        public event Action? OnViewAccountChange;
        public event Action? OnTableChange;

        public ViewAccount ViewAccount {
            get { return _viewAccount; }
            set { _viewAccount = value; OnViewAccountChange?.Invoke(); }
        }

        public List<Roles> Roles {
            get { return _roles; }
            set { _roles = value; OnRolesChange?.Invoke(); }
        }

        public List<InsuranceTypes> InsTypes
        {
            get { return _InsTypes; }
            set {
                _InsTypes = value;
                OnInsTypesChange?.Invoke();
            }
        }

        public List<ViewVehicleMakeModel> MasterTableView {
            get { return _tableView; }
            set { _tableView = value;
                OnTableChange?.Invoke();
            } 
        } 

        public int DEFAULT_ASS_TYPE_ID { get; set; } = 30;
        public int DEFAULT_VEHICLE { get; set; } = 1;
        public int DEFAULT_MAKE { get; set; } = 314;
        public int DEFAULT_MODEL { get; set; } = 441;
        public int STATUS_PENDING { get; set; } = 2;
        public int DEFAULT_ACQID { get; set; } = 9;
        public int RESET_ZERO { get; set; } = 0;

    }
}
