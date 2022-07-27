using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace i_App.Pages.VehiclePages
{
    public partial class ViewVehiclePage : ComponentBase
    {
        [Parameter]
        public int VehicleId { get; set; } = 0;

        [Inject]
        public AppState AppState { get; set; }

        [Inject]
        public AssetService AssetService { get; set; }

        [Inject]
        public VehicleService VehicleService { get; set; }


        [Inject]
        public AuthorizeService AuthorizeService { get; set; }


        [Inject]
        public InsuranceService InsuranceService { get; set; }


        [Inject]
        IJSRuntime JS { get; set; }

        [Inject]
        NavigationManager NavManager { get; set; }

        public bool ShowModal { get; set; } = false;

        public List<Items> Items { get; set; } = new List<Items>();

        public Vehicles NewVehicle { get; set; } = new Vehicles { VehicleId = 0 };

        public List<AssetInsurances> TransacInsurance { get; set; } = new List<AssetInsurances>();

        public List<AssetTransactions> Transaction { get; set; } = new List<AssetTransactions>();

        public List<VehicleItems> Vitems { get; set; } = new List<VehicleItems>();
        private string message { get; set; } = "";
        private bool errorModal { get; set; } = false;

        protected override Task OnInitializedAsync()
        {

            GrabData();
            return base.OnInitializedAsync();
        }

        public async void GrabData()
        {
            //Vehicle exists lets search for that vehicle from database...
            NewVehicle = await VehicleService.GetVehicle(VehicleId);

            TransacInsurance = await InsuranceService.GetInsurance(NewVehicle.VehicleId, (int)Asset.Vehicles);

            Transaction = await AssetService.GetListAsseTransaction(NewVehicle.VehicleId, NewVehicle.AssetId ?? 0);
            Vitems = await VehicleService.GetVehicleItems(NewVehicle.VehicleId);

        }



            //TODO: Change add modal to a JS Enum
            public async void OnShowModal()
        {
            ShowModal = true;
            await JS.InvokeVoidAsync("addModal");
        }

        //TODO: Change remove Modal to a JS Enum
        public async void OnCloseModal()
        {
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

    }
}
