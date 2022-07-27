namespace i_App.View.Model
{
    public class ViewAccount
    {
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string PositionTitle { get; set; } = String.Empty;
        public string Email { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int DepId { get; set; }
        public int EmployeeNum { get; set; }
        public string Extension { get; set; } = string.Empty;

        public UserRoles Roles { get; set; }

        public DateTime Expire { get; set; }


        private string ToUpper(string Reponse) {

            string Result = Reponse;

            if (Result.Length == 0)
                Result = "";
            else if (Result.Length == 1)
                Result =  char.ToUpper(Result[0]).ToString();
            else
                Result = char.ToUpper(Result[0]) + Result.Substring(1).ToString();

            return Result;
        }


        public string FullName
        {
            get
            {
                var split = UserName.Split(".");
                return $"{ToUpper(split[0])} {ToUpper(split[1])}";

            }
        }


        public string ROLE
        {
            get{
                var response = "";
                if ((int)RolesStats.Admin == Roles.RoleId)
                {
                    response = RolesStats.Admin.ToString();
                }else if((int) RolesStats.Purchasing == Roles.RoleId)
                {
                    response = RolesStats.Purchasing.ToString();
                }
                else if ((int)RolesStats.Executive == Roles.RoleId)
                {
                    response = RolesStats.Executive.ToString();
                }
                else if ((int)RolesStats.Application == Roles.RoleId)
                {
                    response = RolesStats.Application.ToString();
                }
                return response;
            }
           
        }

        public bool IsAdmin()
        {
            return Roles.RoleId == (int) RolesStats.Admin || Roles.RoleId == (int) RolesStats.Executive;
        }

        public bool Disabled()
        {
            return (this.IsAdmin()) ? false : true;
        }

        public string IsAdminClass()
        {
            return (this.IsAdmin()) ? "" : "disabled";
        }
    }
}
