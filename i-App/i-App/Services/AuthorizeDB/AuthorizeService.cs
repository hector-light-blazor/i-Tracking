namespace i_App.Services.AuthorizeDB
{

    public class AuthorizeService
    {
        private readonly AuthorizeRepository _repo;
        public AuthorizeService(AuthorizeRepository repo)

        {
            _repo = repo;
        }

        public async Task<List<Roles>> GetRoles()
        {
            return await _repo.GetRoles();
        }


        public async Task<MessageResponse> UpdateUserRole(UserRoles Role) {
            return await _repo.UpdateUserRole(Role);
        }

        public async Task<bool> GetUser(string username)
        {
            return await _repo.GetUser(username);
        }


        public async Task<ViewAccount> GetAccount(string username)
        {
            return await _repo.GetAccount(username);
        }

        public async Task<ViewAccount> GetAccount(int repID)
        {
            return await _repo.GetAccount(repID);
        }
    }
}

