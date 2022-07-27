using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace i_App.Services.InsuranceDB
{
    public class InsuranceRepository
    {
        private readonly IDbContextFactory<InsuranceContext> _factory;
        public InsuranceRepository(IDbContextFactory<InsuranceContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<InsuranceTypes>> GetInsuranceTypes() {

            using (var ctx = _factory.CreateDbContext()) {

                return await Task.FromResult(
                        ctx.InsuranceTypes.ToList());
            }
        }


        public async Task<List<AssetInsurances>> GetInsurance(int VhPropId, int option)
        {
            using (var ctx = _factory.CreateDbContext())
            {

                return await Task.FromResult(
                        ctx.AssetInsurances.Where(c => c.VehPropId.Equals(VhPropId) && c.AssetId.Equals(option)).ToList());
            }
        }


        public async Task<MessageResponse> AddTransactionInsurance(AssetInsurances Transactions)
        {
            try {
                using (var ctx = _factory.CreateDbContext())
                {
                    ctx.AssetInsurances.Add(Transactions);
                    ctx.SaveChanges();

                }
            }
            catch (UniqueConstraintException ex)
            {
                return await Task.FromResult(new MessageResponse { Response = false, Message = ex.Message });
            }

            return await Task.FromResult(new MessageResponse { Response = true, Message = "Added Vehicle Insurance" });
        }
    }
}
