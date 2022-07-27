using Microsoft.AspNetCore.Components;


namespace i_App.Pages.VehiclePages.Forms

{
    public partial class InsuranceTranTableView: ComponentBase
    {
        [Parameter]
        public List<AssetInsurances> Transactions { get; set; } = new List<AssetInsurances>();
    }
}
