namespace i_App.Services.AssetsDB
{
    public class AssetService
    {

        private readonly AssetRepository _repo;
        public AssetService(AssetRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AssetAcquisition>> GetAssetAcqu()
        {
            return await _repo.GetAssetAcqu();
        }

        public async Task<List<Assets>> GetAssets()
        {
            return await _repo.GetAssets();
        }


        public async Task<List<AssetTypes>> GetAssetTypesByID(int AssID)
        {
            return await _repo.GetAssetTypesByID(AssID);
        }

        public async Task<List<AssetYears>> GetAssetsYears()
        {
            return await _repo.GetAssetsYears();
        }

        public async Task<AssetTransactions> AddAssetTransaction(AssetTransactions add)
        {
            return await _repo.AddAssetTransaction(add);
        }

        public async Task<AssetTransactions> UpdateAssetTransaction(AssetTransactions update)
        {
            return await _repo.UpdateAssetTransaction(update);
        }

        public async Task<AssetTransactions> GetAsseTransactions(int vehPropID, int assID)
        {
            return await _repo.GetAsseTransactions(vehPropID, assID);
        }

        public async Task<List<AssetTransactions>> GetListAsseTransaction(int vehPropID, int assID)
        {
            return await _repo.GetListAsseTransaction(vehPropID, assID);
        }
    }
}
