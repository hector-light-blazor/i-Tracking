
using System.Collections;

namespace i_App.Services.Department
{
    public class DepartmentService
    {
        private readonly DepartmentRepository _repo;
        public DepartmentService(DepartmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Departments>> GetDepartAndLocations()
        {
            return await _repo.GetDepartAndLocations();
        }
    }
}
