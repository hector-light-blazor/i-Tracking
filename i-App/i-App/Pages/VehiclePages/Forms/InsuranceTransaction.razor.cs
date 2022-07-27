using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace i_App.Pages.VehiclePages.Forms
{
    public partial class InsuranceTransaction: ComponentBase
    {
        [Inject]
        AppState AppState { get; set; }


        [Inject]
        InsuranceService InsuranceService { get; set; }

        [Inject]
        VehicleService VehicleService { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }


        [Parameter]
        public AssetInsurances Transaction { get; set;  } = new AssetInsurances();


        [Parameter]
        public Vehicles Vehicle { get; set; } = new Vehicles();


        public async Task OnSubmit()
        {
            Transaction.UserId = AppState.ViewAccount.UserId;
            Transaction.AssetId = AppState.DEFAULT_VEHICLE;
            Vehicle.StatusId = (int)AssStatus.Active;

            MessageResponse response = await InsuranceService.AddTransactionInsurance(Transaction);
            MessageResponse VehicleRes = await VehicleService.UpdateVehicleStatus(Vehicle);
            if (response.Response) { 
                await JS.InvokeVoidAsync("ShowToast", "bg-success", "Add Successful", $"Insurance Added for Vehicle ID {Transaction.VehPropId} ");

            }
            else
            {
                await JS.InvokeVoidAsync("ShowToast", "bg-danger", "Failed TO Add Successful", $"Insurance Add Failed for Vehicle ID {Transaction.VehPropId} ");
            }


            if (VehicleRes.Response) { 
                await JS.InvokeVoidAsync("ShowToast", "bg-success", "Update Vehicle Successful", $"Vehicle Status change to Active for Vehicle ID {Transaction.VehPropId} ");
            }

        }

    }
}
