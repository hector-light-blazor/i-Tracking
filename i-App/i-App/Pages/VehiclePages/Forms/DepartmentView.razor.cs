using Microsoft.AspNetCore.Components;


namespace i_App.Pages.VehiclePages.Forms
{
    public partial class DepartmentView
    {
        [Inject]
        VehicleService VehicleService { get; set; }


        [Inject]
        DepartmentService DepartmentService { get; set; }

        [Inject]
        AppState AppState { get; set; }

        [Parameter]
        public Vehicles Vehicle { get; set; } = new Vehicles();

        [Parameter]
        public ViewAccount Account { get; set; } = new ViewAccount();


        //public AssetTransactions transaction = new AssetTransactions();

        public List<Departments> Departments = new List<Departments>();


        

        protected override async Task OnInitializedAsync()
        {
            //Grab initial data as needed...
            await Task.Delay(500);
            GrabData();

            //Set the account to itself.

            Account = AppState.ViewAccount;

           var response = GetLocations().Select(c => c.LocationId)?.FirstOrDefault();
            Vehicle.LocationId = response ?? 0;
        }


        public async void GrabData() {

            Departments = await DepartmentService.GetDepartAndLocations();
        }


        public List<DepLocations> GetLocations()
        {
            if (DepId != 0 && Departments.Count > 0) {

               return  Departments.Find(c => c.DepId.Equals(DepId)).DepLocations.OrderBy(c => c.LocationName).ToList();
            }
            
            return new List<DepLocations>();
        }

        public List<Users> GetRepresentatives()
        {
            List<Users> Response = new List<Users>();

            if (DepId != 0 && Departments.Count > 0)
            {
                Response = Departments.Find(c => c.DepId.Equals(DepId)).Users.OrderBy(c => c.UserName).ToList();
            }

            return (Response == null) ? new List<Users>() : Response;
        }

        [Parameter]
        public int DepId
        {
            get { return Vehicle.DepId ?? 0; }
            set
            {
                Vehicle.DepId = value;
            }
        }
    }
}
