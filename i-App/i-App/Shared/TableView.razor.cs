using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace i_App.Shared
{
    public partial class TableView: ComponentBase
    {
        [Inject]
        IJSRuntime JS { get; set; }

        [Inject]
        AppState AppState { get; set; }

        [Inject]
        VehicleService VehicleService { get; set; }

        [Parameter]
        public List<ViewVehicleMakeModel> ViewRows { get; set; } = new List<ViewVehicleMakeModel>();

        [Parameter]
        public int Count { get; set; } = 0;


        [Parameter]
        public EventCallback<EventCallBackArgs> OnVehicleSelect { get; set; }


        [Parameter]
        public EventCallback<EventCallBackArgs> OnInsurance { get; set; }


        PaginationDisplay? PagiPage;

        public int PaginationSize = 0;
        public int TOTAL_ROWS = 10;
        public int Skip = 0;
        private int counter = 0;

        private string Search { get; set; } = string.Empty;

        public async void OnRouteChange()
        {

            await JS.InvokeVoidAsync("removeModal");
        }

        public void ChangeTable(PaginationPage page)
        {
            // Skip and Take only the rows desire..
            Skip = (page.Number - 1) * TOTAL_ROWS;

            ViewRows = AppState.MasterTableView.Skip(Skip).Take(TOTAL_ROWS).ToList();
        }

        public async Task OnReady()
        {
            await Task.Delay(200);
            PagiPage.Build();
        }

        private async void KeyBoardSave(KeyboardEventArgs e, ViewVehicleMakeModel vehicle) {

            if (e.Key.Equals("Enter") && !string.IsNullOrEmpty(vehicle.PoNumber)) {
                //Update the record database..
                MessageResponse response = await VehicleService.UpdatePoNUmber(vehicle.VehicleId, vehicle.PoNumber);
                System.Diagnostics.Debug.WriteLine($"STATUS: {response.Response}");
                if (response.Response)
                {
                    await JS.InvokeVoidAsync("ShowToast", "bg-success", "PO Updated Successful", $"Vehicle ID {vehicle.VehicleId} : PO Number: {vehicle.PoNumber}");
                }
            }
        }

        public async void Test()
        {
            await JS.InvokeVoidAsync("ShowToast", "bg-success", "PO Updated Successful", $"Counter : {counter}");
            counter++;

        }

       

        private void KeyboardEventHandler(KeyboardEventArgs e)
        {
            int Number = 0;
           
            if (e.Key == "Enter")
            {

                System.Diagnostics.Debug.WriteLine($"VALUE {Search}");
                if (!string.IsNullOrEmpty(Search) && Int32.TryParse(Search, out Number))
                {
                    ViewRows = AppState.MasterTableView.Where(c => c.VehicleId.Equals(Number) || c.ReqNum.Equals(Number)).ToList();
                    PagiPage.Build(ViewRows.Count);

                }
                else if (!string.IsNullOrEmpty(Search))
                {
                    var result = Search.ToLower();
                    ViewRows = AppState.MasterTableView.Where(c => c.MakeName.ToLower().Contains(result) || c.ModelName.ToLower().Contains(result) 
                    || c.PoNumber.ToLower().Contains(Search)).ToList();
                    PagiPage.Build(ViewRows.Count);
                }
                else if (string.IsNullOrEmpty(Search))
                {
                    ViewRows = AppState.MasterTableView.Take(TOTAL_ROWS).ToList();
                    PagiPage.Build(AppState.MasterTableView.Count);
                }

                Search = "";


            }
        }

        public void ResetAllSelected(ViewVehicleMakeModel vehicle) {
            
            foreach (var Svehicle in ViewRows) {
                if ((Svehicle.VehicleId != vehicle.VehicleId && Svehicle.VehicleSelected)) {
                    Svehicle.VehicleSelected = false;
                    Svehicle.VehicleSelected = false;

                }
            }
        }


        //TODO: Change Option to Enumeration is easier to handle the configuration

        public void ResetSelected(int vehicle, int Option)
        {
            foreach (var veh in ViewRows) {
                if (veh.VehicleId == vehicle && Option == 1)
                {
                    veh.InsuranceSelected = false;
                }
                else if (veh.VehicleId == vehicle && Option == 2) {
                    veh.VehicleSelected = false;
                }
            }
            
        }




        public void OnVehicleView(ViewVehicleMakeModel vehicle)
        {

                    var InsuranceResponse = ViewRows.Where(c => c.InsuranceSelected).ToList();

                    var VehicleResponse = ViewRows.Where(c => c.VehicleSelected).ToList();


                    if (VehicleResponse.Count == 1 && VehicleResponse.Where(C => C.VehicleId.Equals(vehicle.VehicleId)).FirstOrDefault() != null)
                    {

                        vehicle.VehicleSelected = false;
                        OnVehicleSelect.InvokeAsync(new EventCallBackArgs { VehicleId = 0, Confirm = false });
                        return;
                    }
                    else if (VehicleResponse.Count == 1 && VehicleResponse.Where(t => t.VehicleId.Equals(vehicle.VehicleId)).FirstOrDefault() == null)
                    {
                        ResetSelected(VehicleResponse.FirstOrDefault().VehicleId, 2);

                    }



                    vehicle.VehicleSelected = true;
                    OnVehicleSelect.InvokeAsync(new EventCallBackArgs { VehicleId = vehicle.VehicleId, Confirm = true });

                    if (InsuranceResponse.Count == 1 && InsuranceResponse.Where(t => t.VehicleId.Equals(vehicle.VehicleId)).FirstOrDefault() == null)
                    {
                        ResetSelected(InsuranceResponse.FirstOrDefault().VehicleId, 1);
                        vehicle.InsuranceSelected = true;
                        OnInsurance.InvokeAsync(new EventCallBackArgs { VehicleId = vehicle.VehicleId, Confirm = true });
                    }
            
        }


        public void OnInsuranceView(ViewVehicleMakeModel vehicle)
        {
            var InsuranceResponse = ViewRows.Where(c => c.InsuranceSelected).ToList();

            var VehicleResponse = ViewRows.Where(c => c.VehicleSelected).ToList();

            if (InsuranceResponse.Count == 1 && InsuranceResponse.Where(C => C.VehicleId.Equals(vehicle.VehicleId)).FirstOrDefault() != null)
            {

                vehicle.InsuranceSelected = false;
                OnInsurance.InvokeAsync(new EventCallBackArgs { VehicleId =0, Confirm = false });
                return;
            }
            else if (InsuranceResponse.Count == 1 && InsuranceResponse.Where(t => t.VehicleId.Equals(vehicle.VehicleId)).FirstOrDefault() == null) {
               ResetSelected(InsuranceResponse.FirstOrDefault().VehicleId, 1);

            }

            vehicle.InsuranceSelected = true;
            OnInsurance.InvokeAsync(new EventCallBackArgs { VehicleId = vehicle.VehicleId, Confirm = true });

            if (VehicleResponse.Count == 1 && VehicleResponse.Where(t => t.VehicleId.Equals(vehicle.VehicleId)).FirstOrDefault() == null) {
                ResetSelected(VehicleResponse.FirstOrDefault().VehicleId, 2);
                vehicle.VehicleSelected = true;
                OnVehicleSelect.InvokeAsync(new EventCallBackArgs { VehicleId = vehicle.VehicleId, Confirm = true });
            }
           
        }


    }
}
