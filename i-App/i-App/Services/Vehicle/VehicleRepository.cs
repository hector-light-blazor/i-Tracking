using Microsoft.EntityFrameworkCore;
using i_App.Data.models;
using System.Collections;
using System.Linq;
using EntityFramework.Exceptions.Common;

namespace i_App.Services.Vehicle
{
    public class VehicleRepository
    {

        private readonly IDbContextFactory<InsuranceContext> _factory;
        public VehicleRepository(IDbContextFactory<InsuranceContext> factory)
        {
            _factory = factory;
        }


        public async Task<int> GetTotalVehicles()
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(
                        ctx.Vehicles.Count());
            }
        }


        public async Task<int> GetTotalVehicles(int ByDept)
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(
                        ctx.Vehicles.Where(c => c.DepId.Equals(ByDept)).Count());
            }
        }

        public async Task<int> GetTotalReqVehicles() {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(
                        ctx.Vehicles.Where(c => c.ReqNum != null && c.PoNumber.Equals(null) && c.RcvdDate == null).Count()
                        );
            }
        }

        public async Task<int> GetTotalActiveVehicles() {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(
                        ctx.Vehicles.Where(c => c.StatusId.Equals((int) AssStatus.Active)).Count()
                        );
            }
        }

        public async Task<List<ViewVehicleMakeModel>> GetVehiclesStepOne()
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.Vehicles.Join(ctx.VehicleMakes,
                         vehicle => vehicle.MakeId,
                         makes => makes.MakeId,
                         (vehicle, makes) => new
                         {
                             vehicle,
                             makes
                         }).Join(ctx.VehicleModels,
                           vmakes => vmakes.vehicle.ModelId,
                           model => model.ModelId,
                           (vmakes, model) => new
                           {
                               vmakes,
                               model
                           }
                         )
                         .Join(ctx.AssetStatus,
                           vmm => vmm.vmakes.vehicle.StatusId,
                           astatus => astatus.StatusId,
                           (vmm, astatus) => new
                           {
                               vmm,
                               astatus
                           })
                         .Where(c => c.vmm.vmakes.vehicle.ReqNum != null
                         && c.vmm.vmakes.vehicle.PoNumber.Equals(null)
                         && c.vmm.vmakes.vehicle.RcvdDate == null)
                         .Select(result => new ViewVehicleMakeModel
                         {
                             VehicleId = result.vmm.vmakes.vehicle.VehicleId,
                             BookValue = result.vmm.vmakes.vehicle.BookValue ?? 0,
                             MakeId = result.vmm.vmakes.makes.MakeId,
                             ReqNum = result.vmm.vmakes.vehicle.ReqNum ?? 0,
                             MakeName = result.vmm.vmakes.makes.MakeName,
                             ModelId = result.vmm.model.ModelId,
                             ModelName = result.vmm.model.ModelName,
                             status_name = result.astatus.StatusName,
                             PoNumber = result.vmm.vmakes.vehicle.PoNumber
                         }
                         ).AsNoTracking().ToList()); ;

            }
        }

        public async Task<List<ViewVehicleMakeModel>> GetVehiclesStepTwo()
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.Vehicles.Join(ctx.VehicleMakes,
                         vehicle => vehicle.MakeId,
                         makes => makes.MakeId,
                         (vehicle, makes) => new
                         {
                             vehicle,
                             makes
                         }).Join(ctx.VehicleModels,
                           vmakes => vmakes.vehicle.ModelId,
                           model => model.ModelId,
                           (vmakes, model) => new
                           {
                               vmakes,
                               model
                           }
                         )
                         .Join(ctx.AssetStatus,
                           vmm => vmm.vmakes.vehicle.StatusId,
                           astatus => astatus.StatusId,
                           (vmm, astatus) => new
                           {
                               vmm,
                               astatus
                           })
                         .Where(c => c.vmm.vmakes.vehicle.ReqNum != null
                         && c.vmm.vmakes.vehicle.PoNumber != null
                         && c.vmm.vmakes.vehicle.RcvdDate == null)
                         .Select(result => new ViewVehicleMakeModel
                         {
                             VehicleId = result.vmm.vmakes.vehicle.VehicleId,
                             BookValue = result.vmm.vmakes.vehicle.BookValue ?? 0,
                             MakeId = result.vmm.vmakes.makes.MakeId,
                             ReqNum = result.vmm.vmakes.vehicle.ReqNum ?? 0,
                             MakeName = result.vmm.vmakes.makes.MakeName,
                             ModelId = result.vmm.model.ModelId,
                             ModelName = result.vmm.model.ModelName,
                             status_name = result.astatus.StatusName,
                             PoNumber = result.vmm.vmakes.vehicle.PoNumber
                         }
                         ).AsNoTracking().ToList());

            }
        }

        public async Task<List<ViewVehicleMakeModel>> GetVehiclesStepThree()
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.Vehicles.Join(ctx.VehicleMakes,
                         vehicle => vehicle.MakeId,
                         makes => makes.MakeId,
                         (vehicle, makes) => new
                         {
                             vehicle,
                             makes
                         }).Join(ctx.VehicleModels,
                           vmakes => vmakes.vehicle.ModelId,
                           model => model.ModelId,
                           (vmakes, model) => new
                           {
                               vmakes,
                               model
                           }
                         )
                         .Join(ctx.AssetStatus,
                           vmm => vmm.vmakes.vehicle.StatusId,
                           astatus => astatus.StatusId,
                           (vmm, astatus) => new
                           {
                               vmm,
                               astatus
                           })
                         .Where(c => c.vmm.vmakes.vehicle.ReqNum != null
                         && c.vmm.vmakes.vehicle.PoNumber != null
                         && c.vmm.vmakes.vehicle.RcvdDate != null 
                         && c.vmm.vmakes.vehicle.StatusId.Equals((int) AssStatus.Pending))
                         .Select(result => new ViewVehicleMakeModel
                         {
                             VehicleId = result.vmm.vmakes.vehicle.VehicleId,
                             BookValue = result.vmm.vmakes.vehicle.BookValue ?? 0,
                             MakeId = result.vmm.vmakes.makes.MakeId,
                             ReqNum = result.vmm.vmakes.vehicle.ReqNum ?? 0,
                             MakeName = result.vmm.vmakes.makes.MakeName,
                             ModelId = result.vmm.model.ModelId,
                             ModelName = result.vmm.model.ModelName,
                             status_name = result.astatus.StatusName,
                             PoNumber = result.vmm.vmakes.vehicle.PoNumber
                         }
                         ).AsNoTracking().ToList());

            }
        }

        public async Task<List<ViewVehicleMakeModel>> GetVehiclesStepFourth()
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.Vehicles.Join(ctx.VehicleMakes,
                         vehicle => vehicle.MakeId,
                         makes => makes.MakeId,
                         (vehicle, makes) => new
                         {
                             vehicle,
                             makes
                         }).Join(ctx.VehicleModels,
                           vmakes => vmakes.vehicle.ModelId,
                           model => model.ModelId,
                           (vmakes, model) => new
                           {
                               vmakes,
                               model
                           }
                         )
                         .Join(ctx.AssetStatus,
                           vmm => vmm.vmakes.vehicle.StatusId,
                           astatus => astatus.StatusId,
                           (vmm, astatus) => new
                           {
                               vmm,
                               astatus
                           })
                         .Where(c => c.vmm.vmakes.vehicle.ReqNum != null
                         && c.vmm.vmakes.vehicle.PoNumber != null
                         && c.vmm.vmakes.vehicle.RcvdDate != null
                         && c.vmm.vmakes.vehicle.StatusId.Equals((int)AssStatus.Pending))
                         .Select(result => new ViewVehicleMakeModel
                         {
                             VehicleId = result.vmm.vmakes.vehicle.VehicleId,
                             BookValue = result.vmm.vmakes.vehicle.BookValue ?? 0,
                             MakeId = result.vmm.vmakes.makes.MakeId,
                             ReqNum = result.vmm.vmakes.vehicle.ReqNum ?? 0,
                             MakeName = result.vmm.vmakes.makes.MakeName,
                             ModelId = result.vmm.model.ModelId,
                             ModelName = result.vmm.model.ModelName,
                             status_name = result.astatus.StatusName,
                             PoNumber = result.vmm.vmakes.vehicle.PoNumber
                         }
                         ).AsNoTracking().ToList());

            }
        }



        public async Task<List<ViewVehicleMakeModel>> GetPendingWithoutPOVehicles()
        {
            using (var ctx = _factory.CreateDbContext())
            {

                return await Task.FromResult(ctx.Vehicles.Join(ctx.VehicleMakes,
                         vehicle => vehicle.MakeId,
                         makes => makes.MakeId,
                         (vehicle, makes) => new
                         {
                             vehicle,
                             makes
                         }).Join(ctx.VehicleModels,
                           vmakes => vmakes.vehicle.ModelId,
                           model => model.ModelId,
                           (vmakes, model) => new
                           {
                               vmakes,
                               model
                           }
                         )
                         .Join(ctx.AssetStatus,
                           vmm => vmm.vmakes.vehicle.StatusId,
                           astatus => astatus.StatusId,
                           (vmm, astatus) => new
                           {
                               vmm,
                               astatus
                           })
                         .Where(c => c.vmm.vmakes
                                .vehicle
                                .StatusId.Equals((int)AssStatus.Pending)
                                &&
                             c.vmm.vmakes.vehicle.PoNumber.Equals(null)
                                )
                         .Select(result => new ViewVehicleMakeModel
                         {
                             VehicleId = result.vmm.vmakes.vehicle.VehicleId,
                             BookValue = result.vmm.vmakes.vehicle.BookValue ?? 0,
                             MakeId = result.vmm.vmakes.makes.MakeId,
                             ReqNum = result.vmm.vmakes.vehicle.ReqNum ?? 0,
                             MakeName = result.vmm.vmakes.makes.MakeName,
                             ModelId = result.vmm.model.ModelId,
                             ModelName = result.vmm.model.ModelName,
                             status_name = result.astatus.StatusName,
                             PoNumber = result.vmm.vmakes.vehicle.PoNumber
                         }
                         ).AsNoTracking().ToList());
            }
        }

        public async Task<int> GetTotalPoVehicles()
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(
                        ctx.Vehicles.Where(c => c.ReqNum != null && c.PoNumber != null).Count());
            }
        }


        public async Task<int> GetTotalArrivedVehicles()
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(
                        ctx.Vehicles.Where(c => c.ReqNum != null
                        && c.PoNumber != null
                        && c.RcvdDate != null
                        && c.StatusId.Equals((int) AssStatus.Pending)).Count());
            }
        }

        public async Task<int> GetTotalPendingVehicles()
        {
            using (var ctx = _factory.CreateDbContext())
            {


                return await Task.FromResult(
                        ctx.Vehicles.Where(c => c.StatusId.Equals((int)AssStatus.Pending)).Count());
            }
        }

        public async Task<int> GetTotalPendingWithoutPOVehicles()
        {
            using (var ctx = _factory.CreateDbContext())
            {


                return await Task.FromResult(
                        ctx.Vehicles.Where(c =>
                        c.StatusId.Equals((int)AssStatus.Pending) &&
                        c.PoNumber.Equals(null)
                        ).Count());
            }
        }




        public async Task<int> GetTotalPendingVehiclesByDept(int DeptByID)
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(
                        ctx.Vehicles.Where(c => c.StatusId.Equals((int)AssStatus.Pending) && c.DepId.Equals(DeptByID)).Count());
            }
        }



        public async Task<List<VehicleItems>> GetVehicleItems(int vehicleID) {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(
                     ctx.VehicleItems.Where(c => c.VehicleId.Equals(vehicleID)).AsNoTracking().ToList()
                    );
            }

        }
        public async Task<bool> AddListVehicleItems(List<VehicleItems> vItems) {

            using (var ctx = _factory.CreateDbContext())
            {
                ctx.VehicleItems.AddRange(vItems);
                ctx.SaveChanges();
                return await Task.FromResult(true);
            }
        }

        public async Task<MessageResponse> UpdatePoNUmber(int VehicleID, string PONumber)
        {
            var vehicle = new Vehicles() { VehicleId = VehicleID, PoNumber = PONumber };
            using (var ctx = _factory.CreateDbContext())
            {
                try
                {
                    ctx.Vehicles.Attach(vehicle);
                    ctx.Entry(vehicle).Property(x => x.PoNumber).IsModified = true;
                    ctx.SaveChanges();
                }
                catch (UniqueConstraintException ex) {
                    return await Task.FromResult(new MessageResponse { Response = false, Message=ex.Message });
                }
               
            }

            return await Task.FromResult(new MessageResponse { Response = true, Message = "Update Successful" });

        }
        public async Task<List<Items>> GetVehicleItems()
        {
            using(var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.Items.OrderBy(c => c.ItemName).AsNoTracking().ToList());
            }
        }



        public async  Task<Vehicles> AddVehicle(Vehicles add)
        {
            using(var ctx = _factory.CreateDbContext())
            {
                ctx.Vehicles.Add(add);
                ctx.SaveChanges();
                return await Task.FromResult(add);
            }
        }

        public async Task<Vehicles> UpdateVehicle(Vehicles update)
        {
            using(var ctx = _factory.CreateDbContext())
            {
                ctx.Vehicles.Update(update);
                ctx.SaveChanges();
                return await Task.FromResult(update);
            }
        }

        public async Task<MessageResponse> UpdateVehicleStatus(Vehicles update)
        {
            try
            {
                using (var ctx = _factory.CreateDbContext())
                {
                    ctx.Vehicles.Update(update);
                    ctx.SaveChanges();
                }

            }
            catch (UniqueConstraintException ex)
            {
                return await Task.FromResult(new MessageResponse { Response = false, Message = ex.Message });
            }
            
            return await Task.FromResult(new MessageResponse { Response = true, Message = "Vehicle Was Updated Successfully" });
        }

        public async Task<List<VehicleMakes>> GetAllMakesModels()
        {
            using(var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.VehicleMakes.Include(t => t.VehicleModels).AsNoTracking().ToList());
            }
        }

        public async Task<Vehicles> GetVehicle(int VehicleID)
        {
            using(var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.Vehicles.Where(c => c.VehicleId.Equals(VehicleID)).AsNoTracking().First());
            }
        }

            public async Task<List<ViewVehicleMakeModel>> GetAllVehicles()
        {
            using(var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.Vehicles.Join(ctx.VehicleMakes,
                         vehicle => vehicle.MakeId,
                         makes => makes.MakeId,
                         (vehicle, makes) => new
                         {
                             vehicle,
                             makes
                         }).Join(ctx.VehicleModels,
                           vmakes => vmakes.vehicle.ModelId,
                           model => model.ModelId,
                           (vmakes, model) => new
                           {
                               vmakes,
                               model
                           }
                         )
                         .Join(ctx.AssetStatus,
                           vmm => vmm.vmakes.vehicle.StatusId,
                           astatus => astatus.StatusId,
                           (vmm, astatus) => new
                           {
                               vmm, astatus
                           })
                         .Select(result => new ViewVehicleMakeModel
                         {
                             VehicleId = result.vmm.vmakes.vehicle.VehicleId,
                             BookValue = result.vmm.vmakes.vehicle.BookValue ?? 0,
                             MakeId = result.vmm.vmakes.makes.MakeId,
                             MakeName = result.vmm.vmakes.makes.MakeName,
                             ModelId = result.vmm.model.ModelId,
                             ModelName = result.vmm.model.ModelName,
                             ReqNum = result.vmm.vmakes.vehicle.ReqNum ?? 0,
                             status_name = result.astatus.StatusName
                         }
                         ).AsNoTracking().ToList());

            }
        }


        public async Task<List<ViewVehicleMakeModel>> GetAllVehiclesByUser(int repID)
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.Vehicles.Join(ctx.VehicleMakes,
                         vehicle => vehicle.MakeId,
                         makes => makes.MakeId,
                         (vehicle, makes) => new
                         {
                             vehicle,
                             makes
                         }).Join(ctx.VehicleModels,
                           vmakes => vmakes.vehicle.ModelId,
                           model => model.ModelId,
                           (vmakes, model) => new
                           {
                               vmakes,
                               model
                           }
                         )
                         .Join(ctx.AssetStatus,
                           vmm => vmm.vmakes.vehicle.StatusId,
                           astatus => astatus.StatusId,
                           (vmm, astatus) => new
                           {
                               vmm,
                               astatus
                           })
                         .Where(c => c.vmm.vmakes.vehicle.UserId.Equals(repID))
                         .Select(result => new ViewVehicleMakeModel
                         {
                             VehicleId = result.vmm.vmakes.vehicle.VehicleId,
                             BookValue = result.vmm.vmakes.vehicle.BookValue ?? 0,
                             MakeId = result.vmm.vmakes.makes.MakeId,
                             ReqNum = result.vmm.vmakes.vehicle.ReqNum ?? 0,
                             MakeName = result.vmm.vmakes.makes.MakeName,
                             ModelId = result.vmm.model.ModelId,
                             ModelName = result.vmm.model.ModelName,
                             status_name = result.astatus.StatusName
                         }
                         ).AsNoTracking().ToList());

            }
        }


        public async Task<List<ViewVehicleMakeModel>> GetAllVehiclesByDept(int DeptID)
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.Vehicles.Join(ctx.VehicleMakes,
                         vehicle => vehicle.MakeId,
                         makes => makes.MakeId,
                         (vehicle, makes) => new
                         {
                             vehicle,
                             makes
                         }).Join(ctx.VehicleModels,
                           vmakes => vmakes.vehicle.ModelId,
                           model => model.ModelId,
                           (vmakes, model) => new
                           {
                               vmakes,
                               model
                           }
                         )
                         .Join(ctx.AssetStatus,
                           vmm => vmm.vmakes.vehicle.StatusId,
                           astatus => astatus.StatusId,
                           (vmm, astatus) => new
                           {
                               vmm,
                               astatus
                           })
                         .Where(c => c.vmm.vmakes.vehicle.DepId.Equals(DeptID))
                         .Select(result => new ViewVehicleMakeModel
                         {
                             VehicleId = result.vmm.vmakes.vehicle.VehicleId,
                             BookValue = result.vmm.vmakes.vehicle.BookValue ?? 0,
                             MakeId = result.vmm.vmakes.makes.MakeId,
                             ReqNum = result.vmm.vmakes.vehicle.ReqNum ?? 0,
                             MakeName = result.vmm.vmakes.makes.MakeName,
                             ModelId = result.vmm.model.ModelId,
                             ModelName = result.vmm.model.ModelName,
                             status_name = result.astatus.StatusName
                         }
                         ).AsNoTracking().ToList());

            }
        }


        public async Task<List<ViewVehicleMakeModel>> GetAllPendingVehicles()
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.Vehicles.Join(ctx.VehicleMakes,
                         vehicle => vehicle.MakeId,
                         makes => makes.MakeId,
                         (vehicle, makes) => new
                         {
                             vehicle,
                             makes
                         }).Join(ctx.VehicleModels,
                           vmakes => vmakes.vehicle.ModelId,
                           model => model.ModelId,
                           (vmakes, model) => new
                           {
                               vmakes,
                               model
                           }
                         )
                         .Join(ctx.AssetStatus,
                           vmm => vmm.vmakes.vehicle.StatusId,
                           astatus => astatus.StatusId,
                           (vmm, astatus) => new
                           {
                               vmm,
                               astatus
                           })
                         .Where(c => c.astatus.StatusId.Equals(2))
                         .Select(result => new ViewVehicleMakeModel
                         {

                             VehicleId = result.vmm.vmakes.vehicle.VehicleId,
                             BookValue = result.vmm.vmakes.vehicle.BookValue ?? 0,
                             MakeId = result.vmm.vmakes.makes.MakeId,
                             MakeName = result.vmm.vmakes.makes.MakeName,
                             ModelId = result.vmm.model.ModelId,
                             ModelName = result.vmm.model.ModelName,
                             status_name = result.astatus.StatusName
                         }
                         ).AsNoTracking().ToList());

            }
        }


        public async Task<List<ViewVehicleMakeModel>> GetAllPendingVehiclesByDept(int ByDeptID)
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.Vehicles.Join(ctx.VehicleMakes,
                         vehicle => vehicle.MakeId,
                         makes => makes.MakeId,
                         (vehicle, makes) => new
                         {
                             vehicle,
                             makes
                         }).Join(ctx.VehicleModels,
                           vmakes => vmakes.vehicle.ModelId,
                           model => model.ModelId,
                           (vmakes, model) => new
                           {
                               vmakes,
                               model
                           }
                         )
                         .Join(ctx.AssetStatus,
                           vmm => vmm.vmakes.vehicle.StatusId,
                           astatus => astatus.StatusId,
                           (vmm, astatus) => new
                           {
                               vmm,
                               astatus
                           })
                         .Where(c => c.astatus.StatusId.Equals(2) && c.vmm.vmakes.vehicle.DepId.Equals(ByDeptID))
                         .Select(result => new ViewVehicleMakeModel
                         {

                             VehicleId = result.vmm.vmakes.vehicle.VehicleId,
                             BookValue = result.vmm.vmakes.vehicle.BookValue ?? 0,
                             MakeId = result.vmm.vmakes.makes.MakeId,
                             MakeName = result.vmm.vmakes.makes.MakeName,
                             ModelId = result.vmm.model.ModelId,
                             ModelName = result.vmm.model.ModelName,
                             status_name = result.astatus.StatusName
                         }
                         ).AsNoTracking().ToList());

            }
        }


    }
}
