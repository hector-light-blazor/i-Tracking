
using System.Collections;

namespace i_App.Services.Vehicle
{
    public class VehicleService
    {
        private readonly VehicleRepository _repo;
        public VehicleService(VehicleRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> GetTotalVehicles()
        {
            return await _repo.GetTotalVehicles();
        }

        public async Task<int> GetTotalVehicles(int ByDept)
        {
            return await _repo.GetTotalVehicles(ByDept);
        }

        public async Task<int> GetTotalReqVehicles()
        {
            return await _repo.GetTotalReqVehicles();
        }

        public async Task<int> GetTotalActiveVehicles() { 
            return await _repo.GetTotalActiveVehicles();
        }

        public async Task<List<ViewVehicleMakeModel>> GetVehiclesStepOne()
        {
            return await _repo.GetVehiclesStepOne();
        }

        public async Task<List<ViewVehicleMakeModel>> GetVehiclesStepTwo()
        {
            return await _repo.GetVehiclesStepTwo();
        }


        public async Task<List<ViewVehicleMakeModel>> GetVehiclesStepThree()
        {
            return await _repo.GetVehiclesStepThree();
        }

        public async Task<int> GetTotalPoVehicles()
        {
            return await _repo.GetTotalPoVehicles();
        }

        public async Task<int> GetTotalArrivedVehicles() {
            return await _repo.GetTotalArrivedVehicles();
        }

        public async Task<int> GetTotalPendingVehicles()
        {
            return await _repo.GetTotalPendingVehicles();
        }

        public async Task<int> GetTotalPendingVehiclesByDept(int DeptByID)
        {
            return await _repo.GetTotalPendingVehiclesByDept(DeptByID);
        }

        public async Task<List<VehicleItems>> GetVehicleItems(int vehicleID) {

            return await _repo.GetVehicleItems(vehicleID);
        }
        public async Task<bool> AddListVehicleItems(List<VehicleItems> vItems) {
            return await _repo.AddListVehicleItems(vItems);
        }

        public async Task<MessageResponse> UpdatePoNUmber(int VehicleID, string PONumber)
        {
            return await _repo.UpdatePoNUmber(VehicleID, PONumber);
        }

        public async Task<MessageResponse> UpdateVehicleStatus(Vehicles update)
        {
            return await _repo.UpdateVehicleStatus(update);
        }

        public async Task<List<Items>> GetVehicleItems()
        {
            return await _repo.GetVehicleItems();
        }
        public async Task<List<VehicleMakes>> GetAllMakesModels()
        {
            return await _repo.GetAllMakesModels();
        }

        public async Task<List<ViewVehicleMakeModel>> GetAllVehicles()
        {
            return await _repo.GetAllVehicles();
        }

        public async Task<List<ViewVehicleMakeModel>> GetAllVehiclesByDept(int DeptID)
        {
            return await _repo.GetAllVehiclesByDept(DeptID);
        }

        public async Task<List<ViewVehicleMakeModel>> GetAllPendingVehicles()
        {
            return await _repo.GetAllPendingVehicles();
        }
        public async Task<int> GetTotalPendingWithoutPOVehicles()
        {
            return await _repo.GetTotalPendingWithoutPOVehicles();
        }

        public async Task<List<ViewVehicleMakeModel>> GetAllPendingVehiclesByDept(int ByDeptID){

            return await _repo.GetAllPendingVehiclesByDept(ByDeptID);
        }

        public async Task<List<ViewVehicleMakeModel>> GetPendingWithoutPOVehicles()
        {
            return await _repo.GetPendingWithoutPOVehicles();
        }

            public async Task<Vehicles> AddVehicle(Vehicles add)
        {
            return await _repo.AddVehicle(add);

        }

        public async Task<Vehicles> UpdateVehicle(Vehicles update)
        {
            return await _repo.UpdateVehicle(update);
        }
        public async Task<Vehicles> GetVehicle(int VehicleID)
        {
            return await _repo.GetVehicle(VehicleID);
        }

        public async Task<List<ViewVehicleMakeModel>> GetAllVehiclesByUser(int repID)
        {
            return await _repo.GetAllVehiclesByUser(repID);
        }
    }
}
