using Microsoft.EntityFrameworkCore;
using i_App.Data.models;
using System.Collections;
using System.Linq;


namespace i_App.Services.Department
{
    public class DepartmentRepository
    {

        private readonly IDbContextFactory<InsuranceContext> _factory;
        public DepartmentRepository(IDbContextFactory<InsuranceContext> factory)
        {
            _factory = factory;
        }


        public async Task<List<Departments>> GetDepartAndLocations()
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(
                    ctx.Departments.Include(c => c.DepLocations).Include(c => c.Users).OrderBy(c => c.DepName).ToList());
            }
        }
    }
}
