namespace i_App.Services.InsuranceDB
{
    public class InsuranceService
    {
        private readonly InsuranceRepository _repo;
        public InsuranceService(InsuranceRepository repo)
        {
            _repo = repo;
        }


        public async Task<List<InsuranceTypes>> GetInsuranceTypes()
        {
            return await _repo.GetInsuranceTypes();
        }

        public async Task<List<AssetInsurances>> GetInsurance(int VhPropId, int option)
        { 
            return await _repo.GetInsurance(VhPropId, option);
        }
        public async Task<MessageResponse> AddTransactionInsurance(AssetInsurances Transactions)
        { 
            return await _repo.AddTransactionInsurance(Transactions);
        }
    }
}
