namespace i_App.Controllers.UI.TableToggle
{

    public enum ToggleOperator
    {
        ALL, PENDING, ME
    }
    public class Toggle
    {
        public string AllTag { get; set; } = "";
        public string PendingTag { get; set; } = "";

        public string MeTag { get; set; } = "";


        public void SetActive(ToggleOperator op)
        {
            AllTag = PendingTag = MeTag = "";
            switch (op)
            {

                case ToggleOperator.ALL:
                    AllTag = string.IsNullOrEmpty(AllTag) ? "active" : "";
                    break;
                case ToggleOperator.PENDING:
                    PendingTag = string.IsNullOrEmpty(PendingTag) ? "active" : "";
                    break;
                case ToggleOperator.ME:
                    MeTag = string.IsNullOrEmpty(MeTag) ? "active" : "";
                    break;
            }
        }
    }
}
