using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace i_App.Pages.VehiclePages
{
    public partial class AddVehiclePage : ComponentBase
    {

        [Inject]
        public AppState AppState { get; set; }

        [Inject]
        public AssetService AssetService { get; set; }

        [Inject]
        public VehicleService VehicleService { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Inject]
        NavigationManager NavManager { get; set; }

        public bool ShowModal { get; set; } = false;

        public List<Items> Items { get; set; } = new List<Items>();

        public Vehicles NewVehicle { get; set; } = new Vehicles { VehicleId= 0};

        public AssetTransactions Transaction { get; set; } = new AssetTransactions();

        public List<VehicleItems> Vitems { get; set; } = new List<VehicleItems>();
        private string message { get; set; } = "";
        private bool errorModal { get; set; } = false;


        //TODO: Wrap everything to add the new vehicle..
        protected override Task OnInitializedAsync()
        {
            NewVehicle.DepId = AppState.ViewAccount.DepId;
            GrabData();
            ProcessVehicleTransaction();
            return base.OnInitializedAsync();
        }

        public async void GrabData() {

            Items = await VehicleService.GetVehicleItems();
        }


        //TODO: ADD THE DATABASE SAVING 
        public async void ProcessVehicleTransaction()
        {
            
            Transaction.VehPropId = NewVehicle.VehicleId;
            Transaction.AssetId = NewVehicle.AssetId;
            Transaction.AsstActionId = (int)AssetAction.AddAsset;
            Transaction.TranDate = DateTime.Now;
            Transaction.UserId = AppState.ViewAccount.UserId;


            //Send to database for saving.
            //Transaction = await AssetService.AddAssetTransaction(Transaction);

        }


        public async void ProcessNewVehicleRecord()
        {


            //Lets send the new Vehicle for the initial to get a return Vehicleid from database...
            NewVehicle.AssetId = AppState.DEFAULT_VEHICLE;
            NewVehicle.UserId = AppState.ViewAccount.UserId;
            NewVehicle.StatusId = AppState.STATUS_PENDING; // Is for Pending.
            NewVehicle.DepId = AppState.ViewAccount.DepId;
            NewVehicle.MakeId = AppState.DEFAULT_MAKE;
            NewVehicle.ModelId = AppState.DEFAULT_MODEL;
            NewVehicle.AcqId = AppState.DEFAULT_ACQID;
            NewVehicle.AcqDate = DateTime.Today;
            NewVehicle.AssetTypeId = AppState.DEFAULT_ASS_TYPE_ID;

           


            //Reset Values to Zero
            NewVehicle.YearId = NewVehicle.LocationId = NewVehicle.AssetTypeId = NewVehicle.MakeId = NewVehicle.ModelId = NewVehicle.AcqId = AppState.RESET_ZERO;


        }

        //TODO: Change add modal to a JS Enum
        public async void OnShowModal()
        {
            ShowModal = true;
            await JS.InvokeVoidAsync("addModal");
        }

        //TODO: Change remove Modal to a JS Enum
        public async void OnCloseModal() {
            ShowModal = false;
            await JS.InvokeVoidAsync("removeModal");
        }


        //TODO: Change remove Modal to a JS Enum
        async void OnErrorClose()
        {
            errorModal = false;
            await JS.InvokeVoidAsync("removeModal");
        }


        public void OnUpdate(VehicleItems vItems)
        {
            Vitems.Add(vItems);
        }

        public bool checkValidation()
        {
            if (NewVehicle.LocationId == AppState.RESET_ZERO)
            {
                errorModal = true;
                message = "Please enter Location Name?";
                return false;
            }
            else if (NewVehicle.AcqId == AppState.RESET_ZERO)
            {
                errorModal = true;
                message = "Please enter vehicle acquired?";
                return false;
            }
            else if (NewVehicle.AssetTypeId == AppState.RESET_ZERO)
            {
                errorModal = true;
                message = "Please enter asset type?";
                return false;
            }
            else if (NewVehicle.MakeId == AppState.RESET_ZERO)
            {
                errorModal = true;
                message = "Please enter Make?";
                return false;
            }
            else if (NewVehicle.ModelId == AppState.RESET_ZERO)
            {
                errorModal = true;
                message = "Please enter Model?";
                return false;
            }
            else if (NewVehicle.YearId == AppState.RESET_ZERO)
            {
                errorModal = true;
                message = "Please enter Year?";
                return false;
            }
            else if (NewVehicle.ReqNum == null)
            {
                errorModal = true;
                message = "Please enter Requisition Number?";
                return false;
            }
            else if (NewVehicle.EstimateDate == null)
            {
                errorModal = true;
                message = "Please enter Estiamted Arrival?";
                return false;
            }
            else if (string.IsNullOrEmpty(NewVehicle.TitleName))
            {
                errorModal = true;
                message = "Please enter Title Name or Lease?";
                return false;
            }

            return true;

        }

        public async void OnSubmit()
        {
            if (!checkValidation())
            {
                //await JS.InvokeVoidAsync("addModal");
                await JS.InvokeVoidAsync("ToastShow", "bg-default", "Saved Record", "Added New Record to Vehicles Database.");

                return;
            }

            //Here we going to add the new vehicle to database..
            NewVehicle = await VehicleService.AddVehicle(NewVehicle);

            //Change the id of the transaction since we have it from database.
            Transaction.VehPropId = NewVehicle.VehicleId;

            //Save it to the DB server..
            Transaction = await AssetService.AddAssetTransaction(Transaction);

            //First make sure there are items to send and loop

            if (Vitems.Count > 0)
            {
                //Going to loop to add the Vehicle Id in Vehicle Items List..
                foreach (var item in Vitems)
                {
                    item.VehicleId = NewVehicle.VehicleId;
                }

                //save to db the list of items..
                await VehicleService.AddListVehicleItems(Vitems);
            }
            NavManager.NavigateTo("/Dashboard");

        }


    }
}
