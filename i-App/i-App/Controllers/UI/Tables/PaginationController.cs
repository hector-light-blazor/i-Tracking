namespace i_App.Controllers.UI.Tables
{
    public class PaginationController
    {
        public List<PaginationPage> Pages { get; set; } = new List<PaginationPage>();
        
        public int PAGINATION_ROWS { get; set; } = 6;
        public int COUNTER { get; set; } = 0;
        public int ROWS_DISPLAY { get; set; } = 10;
        public int TOTAL_BUTTONS { get; set; } = 0;
        public double TABLE_SIZE { get; set; } = 0;

        public string FROM { get; set; } = "1";
        public string END { get; set; } = "10";


        // Build the pagination buttons for the table..
        public void Build()
        {
            // Clear In case we need to call it more than once...
            Pages.Clear();
            END = "10";
            FROM = "1";
            double size = TABLE_SIZE;

            // Diveide by the total records by the row display..
            TOTAL_BUTTONS = (int)Math.Ceiling((size / ROWS_DISPLAY));
            
            for (int i = 0; i < TOTAL_BUTTONS; i++)
            {
                PaginationPage page;

                if (i == 0)
                    page = new PaginationPage((i + 1), "active");
                else
                    page = new PaginationPage((i + 1), "");

                Pages.Add(page);
            }

            if((int) TABLE_SIZE < 10 && TOTAL_BUTTONS == 1)
            {
                END = TABLE_SIZE.ToString();
            }

        }

        public List<PaginationPage> GET_PAGES()
        {
            return Pages.Skip(COUNTER).Take(PAGINATION_ROWS).ToList();
        }

        public string IsPreviousDisabled()
        {
            return (COUNTER >= 6) ? "" : "disabled";
        }

        public string IsNextDisabled()
        {
            return ((COUNTER + 6) < TOTAL_BUTTONS) ? "" : "disabled";
        }

        public void INCREASE_COUNT()
        {
            COUNTER += 6;
        }
        public void DECREASE_COUNT()
        {
            COUNTER -= 6;
        }

        public void DeActive()
        {
            Pages.ForEach(page =>
            {
                if (!string.IsNullOrEmpty(page.Active))
                {
                    page.Active = "";
                    return;
                }
            });
        }

        public void Next()
        {
            if((COUNTER + 6) < TOTAL_BUTTONS)
            {
                INCREASE_COUNT();
            }

        }

        public void Previous()
        {
            DECREASE_COUNT();
        }

        public void UPDATE_LABELS(int skip)
        {
            if (skip == ROWS_DISPLAY)
            {
                FROM = "1";
                END = ROWS_DISPLAY.ToString();
            }
            else
            {
                FROM = (skip - (ROWS_DISPLAY - 1)).ToString();
                END  = (skip > TABLE_SIZE) ? TABLE_SIZE.ToString() : skip.ToString();
            }
        }

    }

}
