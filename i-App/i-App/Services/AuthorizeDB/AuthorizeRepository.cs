using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace i_App.Services.AuthorizeDB
{
    public class AuthorizeRepository
    {
        private readonly IDbContextFactory<InsuranceContext> _factory;
        public AuthorizeRepository(IDbContextFactory<InsuranceContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Roles>> GetRoles()
        {
            using (var ctx = _factory.CreateDbContext()) {
                return await Task.FromResult(ctx.Roles.ToList());
            }    
        }

        public async Task<MessageResponse> UpdateUserRole(UserRoles Role) {

            
           
                try
                {
                    using (var ctx = _factory.CreateDbContext())
                    {
                        Role.CreateDate = DateTime.Now;
                        ctx.Entry(Role).State = EntityState.Modified; 
                        ctx.SaveChanges();
                    }
                }
                catch (UniqueConstraintException ex)
                {
                    return await Task.FromResult(new MessageResponse { Response = false, Message = ex.Message });
                }

            return await Task.FromResult(new MessageResponse { Response = true, Message = "Role Updated Successfully" });
        }

        public async Task<bool> GetUser(string username)
        {
            using(var ctx = _factory.CreateDbContext())
            {
                var found = await Task.FromResult(ctx.Users.Where(c => c.UserName.Contains(username) && (c.Active ?? false))
                    .AsNoTracking().FirstOrDefault());
                return found != null;
            }
        }

        //TODO: Need to add active
        //TODO: Fix when user roles don't exist on the table as relationship.. handle the nullable.
        public async Task<ViewAccount> GetAccount(int repID)
        {
            using (var ctx = _factory.CreateDbContext())
            {
                var user = ctx.Users.Include(i => i.UserRoles).Where(c => c.UserId.Equals(repID) && (c.Active ?? false))
                    .Select(result => new ViewAccount
                    {
                        UserName = result.UserName,
                        Email = result.Email ?? "",
                        UserId = result.UserId,
                        DepId = result.DepId ?? 0,
                        Extension = result.Extension ?? "",
                        Roles = result.UserRoles.FirstOrDefault()
                    }).AsNoTracking().FirstOrDefault();

                    return await Task.FromResult(user ?? new ViewAccount());
            }
        }

        //TODO: Need to add active
        public async Task<ViewAccount> GetAccount(string username)
        {
            using (var ctx = _factory.CreateDbContext())
            {
               var response = await Task.FromResult(ctx.Users.Include(c => c.UserRoles)
                   .Where(c => c.UserName.Contains(username))
                   .Select(result => new ViewAccount
                    {
                        UserName = result.UserName,
                        Email = result.Email,
                        UserId = result.UserId,
                        DepId = result.DepId ?? 0,
                        Roles = result.UserRoles.FirstOrDefault(),
                        Extension= result.Extension
                    }).AsNoTracking().FirstOrDefault());

                // IF response is null send empty object if not send the information to the user...
                return (response == null) ? new ViewAccount() : response;
                
            }
        }
    }
}
