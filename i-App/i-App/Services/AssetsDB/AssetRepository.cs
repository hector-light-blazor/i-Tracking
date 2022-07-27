using Microsoft.EntityFrameworkCore;

namespace i_App.Services.AssetsDB
{
    public class AssetRepository
    {
        private readonly IDbContextFactory<InsuranceContext> _factory;

        public AssetRepository(IDbContextFactory<InsuranceContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<AssetTransactions>> GetListAsseTransaction(int vehPropID, int assID)
        {
            using (var ctx = _factory.CreateDbContext())
            {
                var response = ctx
                        .AssetTransactions
                        .Where(c => c.AssetId.Equals(assID) && c.VehPropId.Equals(vehPropID)).AsNoTracking().ToList();

                return await Task.FromResult(response ?? new List<AssetTransactions>());
            }
        }

        public async  Task<AssetTransactions> GetAsseTransactions(int vehPropID, int assID)
        {
            using (var ctx = _factory.CreateDbContext())
            {
                var response =  ctx
                        .AssetTransactions
                        .Where(c => c.AssetId.Equals(assID) && c.VehPropId.Equals(vehPropID)).AsNoTracking().FirstOrDefault();

                return await Task.FromResult(response ?? new AssetTransactions());
            }
        }

        public async Task<AssetTransactions> AddAssetTransaction(AssetTransactions add)
        {
            using(var ctx = _factory.CreateDbContext())
            {
               
                ctx.AssetTransactions.Add(add);
                ctx.SaveChanges();

                return await Task.FromResult(add);
            }
        }

        public async Task<AssetTransactions> UpdateAssetTransaction(AssetTransactions update)
        {
            using (var ctx = _factory.CreateDbContext())
            {
                ctx.AssetTransactions.Update(update);
                ctx.SaveChanges();

                return await Task.FromResult(update);
            }
        }

        public async Task<List<AssetYears>> GetAssetsYears()
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.AssetYears.OrderBy(c => c.YearNum).ToList());
            }
        }

        public async Task<List<AssetAcquisition>> GetAssetAcqu()
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.AssetAcquisition.OrderBy(c => c.AcqId).ToList());
            }
        }

        public async Task<List<Assets>> GetAssets()
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.Assets.OrderBy(c => c.AssetId).ToList());
            }
        }

        public async Task<List<AssetTypes>> GetAssetTypesByID(int AssID)
        {
            using (var ctx = _factory.CreateDbContext())
            {
                return await Task.FromResult(ctx.AssetTypes.Where(c => c.AssetId.Equals(AssID)).OrderBy(c => c.AssetTypeName).ToList());
            }
        }
    }
}
