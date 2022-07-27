namespace i_App.View.Model
{
    public class ViewDeparmentLocationsModel
    {
        public int DepId { get; set; }
        public string DepName { get; set; } = "";

        public List<ViewLocationModel> LocModel { get; set; } = new List<ViewLocationModel>();

    }
}
