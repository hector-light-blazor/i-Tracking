using Microsoft.AspNetCore.Components;

namespace i_App.Pages.VehiclePages.Forms
{
    public partial class VehicleInfo
    {
        [Inject]
        AssetService AssetService { get; set; }

        [Inject]
        VehicleService VehicleService { get; set; }

        [Inject]
        AppState AppState { get; set; }

        [Parameter]
        public Vehicles Vehicle { get; set; } = new Vehicles();

        [Parameter]
        public bool BtnHide { get; set; } = true;


        //TODO: We need to understand if this variable is needed.
        [Parameter]
        public List<Items> Items { get; set; } = new List<Items>();


        [Parameter]
        public List<VehicleItems> Vitems { get; set; } = new List<VehicleItems>();

        [Parameter]
        public List<AssetTypes> ListAssetTypes { get; set; } = new List<AssetTypes>();

        [Parameter]
        public EventCallback<bool> ShowModal { get; set; }
      

     
        public List<AssetAcquisition> ListAcqus = new List<AssetAcquisition>();
        public List<Assets> ListAssets = new List<Assets>();
        public List<AssetYears> ListAssetYears = new List<AssetYears>();
        public List<VehicleMakes> ListMakes = new List<VehicleMakes>();

        protected override async Task OnInitializedAsync()
        {
            //Grab initial data as needed...
            await Task.Delay(500);
            GrabData();
        }


        public async void GrabData()
        {
           
            ListAcqus = await AssetService.GetAssetAcqu();
            ListAssets = await AssetService.GetAssets();
            ListMakes = await VehicleService.GetAllMakesModels();
            ListAssetYears = await AssetService.GetAssetsYears();
            
        }


        //Get Item Name from Vehicle Items..
        string GetItemName(int? id)
        {
            return Items.Where(c => c.ItemId.Equals(id)).Select(c => c.ItemName).FirstOrDefault() ?? String.Empty;
        }


        public List<VehicleModels> GetModels()
        {
            if (ListMakes.Count > 0)
            {
                return (Vehicle.MakeId != null && Vehicle.MakeId > 0) ?
                ListMakes.Find(c => c.MakeId.Equals(Vehicle.MakeId)).VehicleModels.OrderBy(c => c.ModelName).ToList() : new List<VehicleModels>();
            }
            return new List<VehicleModels>();
        }

        public async void OnShowModal()
        {
            await ShowModal.InvokeAsync(true);
        }


        public void RemoveItem(VehicleItems item) { 
            Vitems.Remove(item);
        }
        

        public int MakeId
        {
            get { return Vehicle.MakeId ?? 0; }
            set
            {
                Vehicle.MakeId = value;
                Vehicle.ModelId = 0;
            }
        }

      

    }
}
