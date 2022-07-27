using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using System.Text.Json;

namespace i_App.Pages
{
    public partial class Settings : ComponentBase
    {
        [Inject]
        AuthorizeService AuthorizeService { get; set; }


        [Inject]
        AppState AppState { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Inject]
        ProtectedLocalStorage BrowserStorage { get; set; }


        public async void OnUpdateRole() {

            MessageResponse response = await AuthorizeService.UpdateUserRole(AppState.ViewAccount.Roles);

            if (response.Response) {

                int res = DateTime.Compare(AppState.ViewAccount.Expire, DateTime.Now);

                //Only if expire is greater than today date we will save to local storage..
                if (res > 0)
                {
                    await BrowserStorage.SetAsync("account", JsonSerializer.Serialize<ViewAccount>(AppState.ViewAccount));
                }

                await JS.InvokeVoidAsync("ShowToast", "bg-success", "Update Successfull", response.Message);
            }
        }
       
    }
}
