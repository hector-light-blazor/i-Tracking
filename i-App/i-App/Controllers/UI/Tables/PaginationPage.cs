namespace i_App.Controllers.UI.Tables
{
    public class PaginationPage
    {

        public int Number { get; set; }
        public string Active { get; set; } = "";

        public PaginationPage(int number, string active)
        {
            Number = number;
            Active = active;
        }

        public PaginationPage()
        {

        }
    }
}
