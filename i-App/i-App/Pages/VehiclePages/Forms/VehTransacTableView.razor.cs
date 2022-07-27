using Microsoft.AspNetCore.Components;


namespace i_App.Pages.VehiclePages.Forms
{
    public partial class VehTransacTableView: ComponentBase
    {

        [Parameter]
        public List<AssetTransactions> Transaction { get; set; }
            = new List<AssetTransactions>();

    }
}
