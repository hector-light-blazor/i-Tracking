using Microsoft.AspNetCore.Components;

namespace i_App.Pages.VehiclePages.Forms
{
    public partial class VehicleTransaction : ComponentBase
    {

        [Parameter]
        public Vehicles Vehicle { get; set; } = new Vehicles();

        [Parameter]
        public AssetTransactions Transaction { get; set; } = new AssetTransactions();
    }
}
