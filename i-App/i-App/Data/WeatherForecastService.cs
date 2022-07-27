namespace i_App.Data
{
   


    public class WeatherForecastService
    {
        private readonly InsuranceContext _context;

        public WeatherForecastService(InsuranceContext context)
        {
            _context = context;
        }

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        public Task<List<Items>> GetForecastAsync(DateTime startDate)
        {
            return Task.FromResult(_context.Items.ToList());
        }

        public Task<List<Vehicles>> GetAllVechicles(string statusID)
        {
            return Task.FromResult(_context.Vehicles.Where(c => c.StatusId.Equals(statusID)).ToList());
        }
    }
}